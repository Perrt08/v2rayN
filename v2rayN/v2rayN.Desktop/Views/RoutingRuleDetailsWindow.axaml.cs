using v2rayN.Desktop.Base;

namespace v2rayN.Desktop.Views;

public partial class RoutingRuleDetailsWindow : WindowBase<RoutingRuleDetailsViewModel>
{
    public RoutingRuleDetailsWindow()
    {
        InitializeComponent();
    }

    public RoutingRuleDetailsWindow(RulesItem rulesItem)
    {
        InitializeComponent();

        this.Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();
        clbProtocol.SelectionChanged += ClbProtocol_SelectionChanged;
        clbInboundTag.SelectionChanged += ClbInboundTag_SelectionChanged;

        ViewModel = new RoutingRuleDetailsViewModel(rulesItem, UpdateViewHandler);

        cmbOutboundTag.ItemsSource = Global.OutboundTags;
        clbProtocol.ItemsSource = Global.RuleProtocols;
        clbInboundTag.ItemsSource = Global.InboundTags;
        cmbNetwork.ItemsSource = Global.RuleNetworks;
        cmbRuleType.ItemsSource = Utils.GetEnumNames<ERuleType>().AppendEmpty();

        if (!rulesItem.Id.IsNullOrEmpty())
        {
            rulesItem.Protocol?.ForEach(it =>
            {
                clbProtocol?.SelectedItems?.Add(it);
            });
            rulesItem.InboundTag?.ForEach(it =>
            {
                clbInboundTag?.SelectedItems?.Add(it);
            });
        }
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.Close(true);
                break;
        }
        return await Task.FromResult(true);
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        txtRemarks.Focus();
    }

    private void ClbProtocol_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.ProtocolItems = clbProtocol.SelectedItems.Cast<string>().ToList();
        }
    }

    private void ClbInboundTag_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.InboundTagItems = clbInboundTag.SelectedItems.Cast<string>().ToList();
        }
    }

    private void linkRuleobjectDoc_Click(object? sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://xtls.github.io/config/routing.html#ruleobject");
    }

    private async void BtnSelectProfile_Click(object? sender, RoutedEventArgs e)
    {
        var selectWindow = new ProfilesSelectWindow();
        selectWindow.SetConfigTypeFilter(new[] { EConfigType.Custom }, exclude: true);
        var result = await selectWindow.ShowDialog<bool?>(this);
        if (result == true)
        {
            var profile = await selectWindow.ProfileItem;
            if (profile != null)
            {
                cmbOutboundTag.Text = profile.Remarks;
            }
        }
    }
}

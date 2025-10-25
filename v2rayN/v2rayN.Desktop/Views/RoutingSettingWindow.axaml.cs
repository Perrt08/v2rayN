using v2rayN.Desktop.Base;
using v2rayN.Desktop.Common;

namespace v2rayN.Desktop.Views;

public partial class RoutingSettingWindow : WindowBase<RoutingSettingViewModel>
{
    private bool _manualClose = false;

    public RoutingSettingWindow()
    {
        InitializeComponent();

        Loaded += Window_Loaded;
        this.Closing += RoutingSettingWindow_Closing;
        btnCancel.Click += (s, e) => this.Close();
        this.KeyDown += RoutingSettingWindow_KeyDown;
        lstRoutings.SelectionChanged += lstRoutings_SelectionChanged;
        lstRoutings.DoubleTapped += LstRoutings_DoubleTapped;
        menuRoutingAdvancedSelectAll.Click += menuRoutingAdvancedSelectAll_Click;

        ViewModel = new RoutingSettingViewModel(UpdateViewHandler);

        cmbdomainStrategy.ItemsSource = Global.DomainStrategies;
        cmbdomainStrategy4Singbox.ItemsSource = Global.DomainStrategies4Singbox;
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.Close(true);
                break;

            case EViewAction.ShowYesNo:
                if (await UI.ShowYesNo(this, ResUI.RemoveRules) != ButtonResult.Yes)
                {
                    return false;
                }
                break;

            case EViewAction.RoutingRuleSettingWindow:
                if (obj is null)
                    return false;
                return await new RoutingRuleSettingWindow((RoutingItem)obj).ShowDialog<bool>(this);
        }
        return await Task.FromResult(true);
    }

    private void RoutingSettingWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyModifiers is KeyModifiers.Control or KeyModifiers.Meta)
        {
            if (e.Key == Key.A)
            {
                lstRoutings.SelectAll();
            }
        }
        else if (e.Key is Key.Enter or Key.Return)
        {
            ViewModel?.RoutingAdvancedSetDefault();
        }
        else if (e.Key == Key.Delete)
        {
            ViewModel?.RoutingAdvancedRemoveAsync();
        }
    }

    private void menuRoutingAdvancedSelectAll_Click(object? sender, RoutedEventArgs e)
    {
        lstRoutings.SelectAll();
    }

    private void lstRoutings_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.SelectedSources = lstRoutings.SelectedItems.Cast<RoutingItemModel>().ToList();
        }
    }

    private void LstRoutings_DoubleTapped(object? sender, TappedEventArgs e)
    {
        ViewModel?.RoutingAdvancedEditAsync(false);
    }

    private void linkdomainStrategy_Click(object? sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://xtls.github.io/config/routing.html");
    }

    private void linkdomainStrategy4Singbox_Click(object? sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://sing-box.sagernet.org/zh/configuration/route/rule_action/#strategy");
    }

    private void btnCancel_Click(object? sender, RoutedEventArgs e)
    {
        _manualClose = true;
        this.Close(ViewModel?.IsModified);
    }

    private void RoutingSettingWindow_Closing(object? sender, WindowClosingEventArgs e)
    {
        if (ViewModel?.IsModified == true)
        {
            if (!_manualClose)
            {
                btnCancel_Click(null, null);
            }
        }
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        btnCancel.Focus();
    }
}

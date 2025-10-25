using v2rayN.Desktop.Base;

namespace v2rayN.Desktop.Views;

public partial class SubEditWindow : WindowBase<SubEditViewModel>
{
    public SubEditWindow()
    {
        InitializeComponent();
    }

    public SubEditWindow(SubItem subItem)
    {
        InitializeComponent();

        Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();

        ViewModel = new SubEditViewModel(subItem, UpdateViewHandler);

        cmbConvertTarget.ItemsSource = Global.SubConvertTargets;
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

    private async void BtnSelectPrevProfile_Click(object? sender, RoutedEventArgs e)
    {
        var selectWindow = new ProfilesSelectWindow();
        selectWindow.SetConfigTypeFilter(new[] { EConfigType.Custom, EConfigType.PolicyGroup, EConfigType.ProxyChain }, exclude: true);
        var result = await selectWindow.ShowDialog<bool?>(this);
        if (result == true)
        {
            var profile = await selectWindow.ProfileItem;
            if (profile != null)
            {
                txtPrevProfile.Text = profile.Remarks;
            }
        }
    }

    private async void BtnSelectNextProfile_Click(object? sender, RoutedEventArgs e)
    {
        var selectWindow = new ProfilesSelectWindow();
        selectWindow.SetConfigTypeFilter(new[] { EConfigType.Custom, EConfigType.PolicyGroup, EConfigType.ProxyChain }, exclude: true);
        var result = await selectWindow.ShowDialog<bool?>(this);
        if (result == true)
        {
            var profile = await selectWindow.ProfileItem;
            if (profile != null)
            {
                txtNextProfile.Text = profile.Remarks;
            }
        }
    }
}

namespace v2rayN.Desktop.Views;

public partial class ClashProxiesView : ReactiveUserControl<ClashProxiesViewModel>
{
    public ClashProxiesView()
    {
        InitializeComponent();
        ViewModel = new ClashProxiesViewModel(UpdateViewHandler);
        lstProxyDetails.DoubleTapped += LstProxyDetails_DoubleTapped;
        this.KeyDown += ClashProxiesView_KeyDown;
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        return await Task.FromResult(true);
    }

    private void ClashProxiesView_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.F5:
                ViewModel?.ProxiesReload();
                break;

            case Key.Enter:
                ViewModel?.SetActiveProxy();
                break;
        }
    }

    private void LstProxyDetails_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        ViewModel?.SetActiveProxy();
    }
}

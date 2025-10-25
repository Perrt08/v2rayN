using DialogHostAvalonia;
using v2rayN.Desktop.Base;
using v2rayN.Desktop.Common;

namespace v2rayN.Desktop.Views;

public partial class SubSettingWindow : WindowBase<SubSettingViewModel>
{
    private bool _manualClose = false;

    public SubSettingWindow()
    {
        InitializeComponent();

        menuClose.Click += menuClose_Click;
        Loaded += Window_Loaded;
        this.Closing += SubSettingWindow_Closing;
        this.KeyDown += SubSettingWindow_KeyDown;
        ViewModel = new SubSettingViewModel(UpdateViewHandler);
        lstSubscription.DoubleTapped += LstSubscription_DoubleTapped;
        lstSubscription.SelectionChanged += LstSubscription_SelectionChanged;
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.Close();
                break;

            case EViewAction.ShowYesNo:
                if (await UI.ShowYesNo(this, ResUI.RemoveServer) != ButtonResult.Yes)
                {
                    return false;
                }
                break;

            case EViewAction.SubEditWindow:
                if (obj is null)
                    return false;
                var window = new SubEditWindow((SubItem)obj);
                await window.ShowDialog(this);
                break;

            case EViewAction.ShareSub:
                if (obj is null)
                    return false;
                await ShareSub((string)obj);
                break;
        }
        return await Task.FromResult(true);
    }

    private async Task ShareSub(string url)
    {
        if (url.IsNullOrEmpty())
        {
            return;
        }
        var dialog = new QrcodeView(url);
        await DialogHost.Show(dialog, "dialogHostSub");
    }

    private void LstSubscription_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        ViewModel?.EditSubAsync(false);
    }

    private void LstSubscription_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.SelectedSources = lstSubscription.SelectedItems.Cast<SubItem>().ToList();
        }
    }

    private void menuClose_Click(object? sender, RoutedEventArgs e)
    {
        _manualClose = true;
        this.Close(ViewModel?.IsModified);
    }

    private void SubSettingWindow_Closing(object? sender, WindowClosingEventArgs e)
    {
        if (ViewModel?.IsModified == true)
        {
            if (!_manualClose)
            {
                menuClose_Click(null, null);
            }
        }
    }

    private void SubSettingWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            menuClose_Click(null, null);
        }
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        lstSubscription.Focus();
    }
}

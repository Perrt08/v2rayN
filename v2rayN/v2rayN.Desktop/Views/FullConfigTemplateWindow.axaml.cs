using v2rayN.Desktop.Base;

namespace v2rayN.Desktop.Views;

public partial class FullConfigTemplateWindow : WindowBase<FullConfigTemplateViewModel>
{
    private static Config _config;

    public FullConfigTemplateWindow()
    {
        InitializeComponent();

        _config = AppManager.Instance.Config;
        Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();
        ViewModel = new FullConfigTemplateViewModel(UpdateViewHandler);
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

    private void linkFullConfigTemplateDoc_Click(object sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://github.com/2dust/v2rayN/wiki/Description-of-some-ui#%E5%AE%8C%E6%95%B4%E9%85%8D%E7%BD%AE%E6%A8%A1%E6%9D%BF%E8%AE%BE%E7%BD%AE");
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        btnCancel.Focus();
    }
}

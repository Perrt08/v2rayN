using v2rayN.Desktop.Base;

namespace v2rayN.Desktop.Views;

public partial class OptionSettingWindow : WindowBase<OptionSettingViewModel>
{
    private static Config _config;

    public OptionSettingWindow()
    {
        InitializeComponent();

        Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();
        _config = AppManager.Instance.Config;

        ViewModel = new OptionSettingViewModel(UpdateViewHandler);

        clbdestOverride.SelectionChanged += ClbdestOverride_SelectionChanged;
        clbdestOverride.ItemsSource = Global.destOverrideProtocols;
        _config.Inbound.First().DestOverride?.ForEach(it =>
        {
            clbdestOverride.SelectedItems.Add(it);
        });

        cmbsystemProxyAdvancedProtocol.ItemsSource = Global.IEProxyProtocols;
        cmbloglevel.ItemsSource = Global.LogLevels;
        cmbdefFingerprint.ItemsSource = Global.Fingerprints;
        cmbdefUserAgent.ItemsSource = Global.UserAgent;
        cmbmux4SboxProtocol.ItemsSource = Global.SingboxMuxs;
        cmbMtu.ItemsSource = Global.TunMtus;
        cmbStack.ItemsSource = Global.TunStacks;

        cmbCoreType1.ItemsSource = Global.CoreTypes;
        cmbCoreType2.ItemsSource = Global.CoreTypes;
        cmbCoreType3.ItemsSource = Global.CoreTypes;
        cmbCoreType4.ItemsSource = Global.CoreTypes;
        cmbCoreType5.ItemsSource = Global.CoreTypes;
        cmbCoreType6.ItemsSource = Global.CoreTypes;
        cmbCoreType9.ItemsSource = Global.CoreTypes;

        cmbMixedConcurrencyCount.ItemsSource = Enumerable.Range(2, 7).ToList();
        cmbSpeedTestTimeout.ItemsSource = Enumerable.Range(2, 5).Select(i => i * 5).ToList();
        cmbSpeedTestUrl.ItemsSource = Global.SpeedTestUrls;
        cmbSpeedPingTestUrl.ItemsSource = Global.SpeedPingTestUrls;
        cmbSubConvertUrl.ItemsSource = Global.SubConvertUrls;
        cmbGetFilesSourceUrl.ItemsSource = Global.GeoFilesSources;
        cmbSrsFilesSourceUrl.ItemsSource = Global.SingboxRulesetSources;
        cmbRoutingRulesSourceUrl.ItemsSource = Global.RoutingRulesSources;
        cmbIPAPIUrl.ItemsSource = Global.IPAPIUrls;

        cmbMainGirdOrientation.ItemsSource = Utils.GetEnumNames<EGirdOrientation>();

        if (Utils.IsWindows())
        {
            txbSettingsExceptionTip2.IsVisible = false;

            labHide2TrayWhenClose.IsVisible = false;
            togHide2TrayWhenClose.IsVisible = false;
            labHide2TrayWhenCloseTip.IsVisible = false;
        }
        else if (Utils.IsLinux())
        {
            txbSettingsExceptionTip.IsVisible = false;
            panSystemProxyAdvanced.IsVisible = false;

            tbAutoRunTip.IsVisible = false;
        }
        else if (Utils.IsOSX())
        {
            txbSettingsExceptionTip.IsVisible = false;
            panSystemProxyAdvanced.IsVisible = false;

            tbAutoRunTip.IsVisible = false;

            labHide2TrayWhenClose.IsVisible = false;
            togHide2TrayWhenClose.IsVisible = false;
            labHide2TrayWhenCloseTip.IsVisible = false;
        }
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.Close(true);
                break;

            case EViewAction.InitSettingFont:
                await InitSettingFont();
                break;
        }
        return await Task.FromResult(true);
    }

    private async Task InitSettingFont()
    {
        var lstFonts = await GetFonts();

        lstFonts.Add(string.Empty);
        cmbcurrentFontFamily.ItemsSource = lstFonts;
    }

    private async Task<List<string>> GetFonts()
    {
        var lstFonts = new List<string>();
        try
        {
            if (Utils.IsWindows())
            {
                return lstFonts;
            }
            else if (Utils.IsNonWindows())
            {
                var result = await Utils.GetLinuxFontFamily("zh");
                if (result.IsNullOrEmpty())
                {
                    return lstFonts;
                }

                var lst = result.Split(Environment.NewLine)
                    .Where(t => t.IsNotEmpty())
                    .ToList()
                    .Select(t => t.Split(",").FirstOrDefault() ?? "")
                    .OrderBy(t => t)
                    .ToList();
                return lst;
            }
        }
        catch (Exception ex)
        {
            Logging.SaveLog("GetFonts", ex);
        }
        return lstFonts;
    }

    private void ClbdestOverride_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.DestOverride = clbdestOverride.SelectedItems.Cast<string>().ToList();
        }
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        btnCancel.Focus();
    }
}

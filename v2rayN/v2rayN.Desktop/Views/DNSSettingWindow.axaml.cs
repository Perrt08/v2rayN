using v2rayN.Desktop.Base;

namespace v2rayN.Desktop.Views;

public partial class DNSSettingWindow : WindowBase<DNSSettingViewModel>
{
    private static Config _config;

    public DNSSettingWindow()
    {
        InitializeComponent();

        _config = AppManager.Instance.Config;
        Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();
        ViewModel = new DNSSettingViewModel(UpdateViewHandler);

        cmbRayFreedomDNSStrategy.ItemsSource = Global.DomainStrategy4Freedoms;
        cmbSBDirectDNSStrategy.ItemsSource = Global.SingboxDomainStrategy4Out;
        cmbSBRemoteDNSStrategy.ItemsSource = Global.SingboxDomainStrategy4Out;
        cmbDirectDNS.ItemsSource = Global.DomainDirectDNSAddress;
        cmbRemoteDNS.ItemsSource = Global.DomainRemoteDNSAddress;
        cmbBootstrapDNS.ItemsSource = Global.DomainPureIPDNSAddress;
        cmbDirectExpectedIPs.ItemsSource = Global.ExpectedIPs;

        cmbdomainStrategy4FreedomCompatible.ItemsSource = Global.DomainStrategy4Freedoms;
        cmbdomainStrategy4OutCompatible.ItemsSource = Global.SingboxDomainStrategy4Out;
        cmbdomainDNSAddressCompatible.ItemsSource = Global.DomainPureIPDNSAddress;
        cmbdomainDNSAddress2Compatible.ItemsSource = Global.DomainPureIPDNSAddress;
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

    private void linkDnsObjectDoc_Click(object? sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://xtls.github.io/config/dns.html#dnsobject");
    }

    private void linkDnsSingboxObjectDoc_Click(object? sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://sing-box.sagernet.org/zh/configuration/dns/");
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        btnCancel.Focus();
    }
}

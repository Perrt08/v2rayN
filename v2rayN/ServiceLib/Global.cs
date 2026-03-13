namespace ServiceLib;

public class Global
{
    #region const

    public const string AppName = "v2rayN";
    public const string GithubUrl = "https://github.com";
    public const string GithubApiUrl = "https://api.github.com/repos";
    public const string GeoUrl = "https://github.com/Loyalsoldier/v2ray-rules-dat/releases/latest/download/{0}.dat";
    public const string SingboxRulesetUrl = @"https://raw.githubusercontent.com/2dust/sing-box-rules/rule-set-{0}/{1}.srs";

    public const string PromotionUrl = @"aHR0cHM6Ly85LjIzNDQ1Ni54eXovYWJjLmh0bWw=";
    public const string ConfigFileName = "guiNConfig.json";
    public const string CoreConfigFileName = "config.json";
    public const string CorePreConfigFileName = "configPre.json";
    public const string CoreSpeedtestConfigFileName = "configTest{0}.json";
    public const string ClashMixinConfigFileName = "Mixin.yaml";

    public const string NamespaceSample = "ServiceLib.Sample.";
    public const string V2raySampleClient = NamespaceSample + "SampleClientConfig";
    public const string SingboxSampleClient = NamespaceSample + "SingboxSampleClientConfig";
    public const string V2raySampleHttpRequestFileName = NamespaceSample + "SampleHttpRequest";
    public const string V2raySampleHttpResponseFileName = NamespaceSample + "SampleHttpResponse";
    public const string V2raySampleInbound = NamespaceSample + "SampleInbound";
    public const string V2raySampleOutbound = NamespaceSample + "SampleOutbound";
    public const string SingboxSampleOutbound = NamespaceSample + "SingboxSampleOutbound";
    public const string CustomRoutingFileName = NamespaceSample + "custom_routing_";
    public const string TunSingboxDNSFileName = NamespaceSample + "tun_singbox_dns";
    public const string TunSingboxInboundFileName = NamespaceSample + "tun_singbox_inbound";
    public const string TunSingboxRulesFileName = NamespaceSample + "tun_singbox_rules";
    public const string DNSV2rayNormalFileName = NamespaceSample + "dns_v2ray_normal";
    public const string DNSSingboxNormalFileName = NamespaceSample + "dns_singbox_normal";
    public const string ClashMixinYaml = NamespaceSample + "clash_mixin_yaml";
    public const string ClashTunYaml = NamespaceSample + "clash_tun_yaml";
    public const string LinuxAutostartConfig = NamespaceSample + "linux_autostart_config";
    public const string PacFileName = NamespaceSample + "pac";
    public const string ProxySetOSXShellFileName = NamespaceSample + "proxy_set_osx_sh";
    public const string ProxySetLinuxShellFileName = NamespaceSample + "proxy_set_linux_sh";
    public const string KillAsSudoOSXShellFileName = NamespaceSample + "kill_as_sudo_osx_sh";
    public const string KillAsSudoLinuxShellFileName = NamespaceSample + "kill_as_sudo_linux_sh";
    public const string SingboxFakeIPFilterFileName = NamespaceSample + "singbox_fakeip_filter";

    public const string DefaultSecurity = "auto";
    public const string DefaultNetwork = "tcp";
    public const string TcpHeaderHttp = "http";
    public const string None = "none";
    public const string ProxyTag = "proxy";
    public const string DirectTag = "direct";
    public const string BlockTag = "block";
    public const string DnsTag = "dns-module";
    public const string DirectDnsTag = "direct-dns";
    public const string BalancerTagSuffix = "-round";
    public const string StreamSecurity = "tls";
    public const string StreamSecurityReality = "reality";
    public const string Loopback = "127.0.0.1";
    public const string InboundAPIProtocol = "dokodemo-door";
    public const string HttpProtocol = "http://";
    public const string HttpsProtocol = "https://";
    public const string SocksProtocol = "socks://";
    public const string Socks5Protocol = "socks5://";
    public const string AsIs = "AsIs";
    public const string IPIfNonMatch = "IPIfNonMatch";
    public const string IPOnDemand = "IPOnDemand";

    public const string UserEMail = "t@t.tt";
    public const string AutoRunRegPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    public const string AutoRunName = "v2rayNAutoRun";
    public const string SystemProxyExceptionsWindows = "localhost;127.*;10.*;172.16.*;172.17.*;172.18.*;172.19.*;172.20.*;172.21.*;172.22.*;172.23.*;172.24.*;172.25.*;172.26.*;172.27.*;172.28.*;172.29.*;172.30.*;172.31.*;192.168.*";
    public const string SystemProxyExceptionsLinux = "localhost,127.0.0.0/8,::1";
    public const string RoutingRuleComma = "<COMMA>";
    public const string GrpcGunMode = "gun";
    public const string GrpcMultiMode = "multi";
    public const int MaxPort = 65536;
    public const int MinFontSize = 8;
    public const int MinFontSizeCount = 13;
    public const string RebootAs = "rebootas";
    public const string AvaAssets = "avares://v2rayN/Assets/";
    public const string LocalAppData = "V2RAYN_LOCAL_APPLICATION_DATA_V2";
    public const string V2RayLocalAsset = "V2RAY_LOCATION_ASSET";
    public const string XrayLocalAsset = "XRAY_LOCATION_ASSET";
    public const string XrayLocalCert = "XRAY_LOCATION_CERT";
    public const int SpeedTestPageSize = 1000;
    public const string LinuxBash = "/bin/bash";

    public const string SingboxDirectDNSTag = "direct_dns";
    public const string SingboxRemoteDNSTag = "remote_dns";
    public const string SingboxLocalDNSTag = "local_local";
    public const string SingboxHostsDNSTag = "hosts_dns";
    public const string SingboxFakeDNSTag = "fake_dns";

    public const int Hysteria2DefaultHopInt = 10;

    public const string PolicyGroupExcludeKeywords = @"剩余|过期|到期|重置|[Rr]emaining|[Ee]xpir|[Rr]eset";

    public const string PolicyGroupDefaultAllFilter = $"^(?!.*(?:{PolicyGroupExcludeKeywords})).*$";

    public static readonly List<string> PolicyGroupDefaultFilterList =
    [
        // All nodes (exclude traffic/expiry info)
        PolicyGroupDefaultAllFilter,
        // Low multiplier nodes, e.g. ×0.1, 0.5x, 0.1倍
        @"^.*(?:[×xX✕*]\s*0\.[0-9]+|0\.[0-9]+\s*[×xX✕*倍]).*$",
        // Dedicated line nodes, e.g. IPLC, IEPL
        $@"^(?!.*(?:{PolicyGroupExcludeKeywords})).*(?:专线|IPLC|IEPL|中转).*$",
        // Japan nodes
        $@"^(?!.*(?:{PolicyGroupExcludeKeywords})).*(?:日本|\\b[Jj][Pp]\\b|🇯🇵|[Jj]apan).*$",
    ];

    public static readonly List<string> IEProxyProtocols =
    [
        "{ip}:{http_port}",
        "socks={ip}:{socks_port}",
        "http={ip}:{http_port};https={ip}:{http_port};ftp={ip}:{http_port};socks={ip}:{socks_port}",
        "http=http://{ip}:{http_port};https=http://{ip}:{http_port}",
        ""
    ];

    public static readonly List<string> SubConvertUrls =
    [
        @"https://sub.xeton.dev/sub?url={0}",
        @"https://api.dler.io/sub?url={0}",
        @"http://127.0.0.1:25500/sub?url={0}",
        ""
    ];

    public static readonly List<string> SubConvertConfig =
    [
        @"https://raw.githubusercontent.com/ACL4SSR/ACL4SSR/master/Clash/config/ACL4SSR_Online.ini"
    ];

    public static readonly List<string> SubConvertTargets =
    [
        "",
        "mixed",
        "v2ray",
        "clash",
        "ss"
    ];

    public static readonly List<string> SpeedTestUrls =
    [
        @"https://raw.githubusercontent.com/Loyalsoldier/geoip/refs/heads/release/geoip.dat",
        @"https://speed.cloudflare.com/__down?bytes=10000000",
        @"https://speed.cloudflare.com/__down?bytes=50000000",
        @"https://speed.cloudflare.com/__down?bytes=100000000",
    ];

    public static readonly List<string> SpeedPingTestUrls =
    [
        @"https://www.google.com/generate_204",
        @"https://www.gstatic.com/generate_204",
        @"https://www.apple.com/library/test/success.html",
        @"http://www.msftconnecttest.com/connecttest.txt"
    ];

    public static readonly List<string> GeoFilesSources =
    [
        "",
        @"https://github.com/runetfreedom/russia-v2ray-rules-dat/releases/latest/download/{0}.dat",
        @"https://github.com/Chocolate4U/Iran-v2ray-rules/releases/latest/download/{0}.dat"
    ];

    public static readonly List<string> SingboxRulesetSources =
    [
        "",
        @"https://raw.githubusercontent.com/runetfreedom/russia-v2ray-rules-dat/release/sing-box/rule-set-{0}/{1}.srs",
        @"https://raw.githubusercontent.com/chocolate4u/Iran-sing-box-rules/rule-set/{1}.srs"
    ];

    public static readonly List<string> RoutingRulesSources =
    [
        "",
        @"https://raw.githubusercontent.com/runetfreedom/russia-v2ray-custom-routing-list/main/v2rayN/template.json",
        @"https://raw.githubusercontent.com/Chocolate4U/Iran-v2ray-rules/main/v2rayN/template.json"
    ];

    public static readonly List<string> DNSTemplateSources =
    [
        "",
        @"https://raw.githubusercontent.com/runetfreedom/russia-v2ray-custom-routing-list/main/v2rayN/",
        @"https://raw.githubusercontent.com/Chocolate4U/Iran-v2ray-rules/main/v2rayN/"
    ];

    public static readonly Dictionary<string, string> UserAgentTexts = new()
    {
        {"chrome","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/145.0.0.0 Safari/537.36" },
        {"firefox","Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_2; rv:139.578.445) Gecko/20100101 Firefox/139.578.445" },
        {"safari","Mozilla/5.0 (Macintosh; Intel Mac OS X 11_0_1) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/19.0 Safari/605.1.15 Gear/6.21.8" },
        {"edge","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.0.0 Safari/537.36 Edg/139.0.0.6" },
        {"opera-linux","Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/139.0.7258.160 Safari/537.36 OPR/120.0.5543.93"},
        {"opera-windows","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36 OPR/120.0.0.0 (Edition Yx GX TR 2)" },
        {"qqbrowser-honor-gt","Mozilla/5.0 (Linux; U; Android 16; zh-cn; AMG-AN00 Build/HONORAMG-AN00) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/121.0.6167.71 MQQBrowser/19.7 Mobile Safari/537.36 COVC/048601"},
        {"huawei","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.84 Safari/537.36 HBPC/12.1.4.300"},
        {"gnome-web","Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/60.5 Safari/605.1.15"},
        {"none",""}
    };

    public const string Hysteria2ProtocolShare = "hy2://";

    public const string NaiveHttpsProtocolShare = "naive+https://";

    public const string NaiveQuicProtocolShare = "naive+quic://";

    public static readonly Dictionary<EConfigType, string> ProtocolShares = new()
    {
        { EConfigType.VMess, "vmess://" },
        { EConfigType.Shadowsocks, "ss://" },
        { EConfigType.SOCKS, "socks://" },
        { EConfigType.VLESS, "vless://" },
        { EConfigType.Trojan, "trojan://" },
        { EConfigType.Hysteria2, "hysteria2://" },
        { EConfigType.TUIC, "tuic://" },
        { EConfigType.WireGuard, "wireguard://" },
        { EConfigType.Anytls, "anytls://" },
        { EConfigType.Naive, "naive://" }
    };

    public static readonly Dictionary<EConfigType, string> ProtocolTypes = new()
    {
        { EConfigType.VMess, "vmess" },
        { EConfigType.Shadowsocks, "shadowsocks" },
        { EConfigType.SOCKS, "socks" },
        { EConfigType.HTTP, "http" },
        { EConfigType.VLESS, "vless" },
        { EConfigType.Trojan, "trojan" },
        { EConfigType.Hysteria2, "hysteria2" },
        { EConfigType.TUIC, "tuic" },
        { EConfigType.WireGuard, "wireguard" },
        { EConfigType.Anytls, "anytls" },
        { EConfigType.Naive, "naive" }
    };

    public static readonly List<string> VmessSecurities =
    [
        "aes-128-gcm",
        "chacha20-poly1305",
        "auto",
        "none",
        "zero"
    ];

    public static readonly List<string> SsSecurities =
    [
        "aes-256-gcm",
        "aes-128-gcm",
        "chacha20-poly1305",
        "chacha20-ietf-poly1305",
        "none",
        "plain"
    ];

    public static readonly List<string> SsSecuritiesInXray =
    [
        "aes-256-gcm",
        "aes-128-gcm",
        "chacha20-poly1305",
        "chacha20-ietf-poly1305",
        "xchacha20-poly1305",
        "xchacha20-ietf-poly1305",
        "none",
        "plain",
        "2022-blake3-aes-128-gcm",
        "2022-blake3-aes-256-gcm",
        "2022-blake3-chacha20-poly1305"
    ];

    public static readonly List<string> SsSecuritiesInSingbox =
    [
        "aes-256-gcm",
        "aes-192-gcm",
        "aes-128-gcm",
        "chacha20-ietf-poly1305",
        "xchacha20-ietf-poly1305",
        "none",
        "2022-blake3-aes-128-gcm",
        "2022-blake3-aes-256-gcm",
        "2022-blake3-chacha20-poly1305",
        "aes-128-ctr",
        "aes-192-ctr",
        "aes-256-ctr",
        "aes-128-cfb",
        "aes-192-cfb",
        "aes-256-cfb",
        "rc4-md5",
        "chacha20-ietf",
        "xchacha20"
    ];

    public static readonly List<string> Flows =
    [
        "",
        "xtls-rprx-vision",
        "xtls-rprx-vision-udp443"
    ];

    public static readonly List<string> Networks =
    [
        "tcp",
        "kcp",
        "ws",
        "httpupgrade",
        "xhttp",
        "h2",
        "quic",
        "grpc"
    ];

    public static readonly List<string> KcpHeaderTypes =
    [
        "srtp",
        "utp",
        "wechat-video",
        "dtls",
        "wireguard",
        "dns"
    ];

    public static readonly Dictionary<string, string> KcpHeaderMaskMap = new()
    {
        { "srtp", "header-srtp" },
        { "utp", "header-utp" },
        { "wechat-video", "header-wechat" },
        { "dtls", "header-dtls" },
        { "wireguard", "header-wireguard" },
        { "dns", "header-dns" }
    };

    public static readonly List<string> CoreTypes =
    [
        "Xray",
        "sing_box"
    ];

    public static readonly HashSet<EConfigType> XraySupportConfigType =
    [
        EConfigType.VMess,
        EConfigType.VLESS,
        EConfigType.Shadowsocks,
        EConfigType.Trojan,
        EConfigType.Hysteria2,
        EConfigType.WireGuard,
        EConfigType.SOCKS,
        EConfigType.HTTP,
    ];

    public static readonly HashSet<EConfigType> SingboxSupportConfigType =
    [
        EConfigType.VMess,
        EConfigType.VLESS,
        EConfigType.Shadowsocks,
        EConfigType.Trojan,
        EConfigType.Hysteria2,
        EConfigType.TUIC,
        EConfigType.Anytls,
        EConfigType.Naive,
        EConfigType.WireGuard,
        EConfigType.SOCKS,
        EConfigType.HTTP,
    ];

    public static readonly HashSet<EConfigType> SingboxOnlyConfigType = SingboxSupportConfigType.Except(XraySupportConfigType).ToHashSet();

    public static readonly List<string> DomainStrategies =
    [
        AsIs,
        IPIfNonMatch,
        IPOnDemand
    ];

    public static readonly List<string> DomainStrategies4Sbox =
    [
        "",
        "prefer_ipv4",
        "prefer_ipv6",
        "ipv4_only",
        "ipv6_only"
    ];

    public static readonly List<string> Fingerprints =
    [
        "chrome",
        "firefox",
        "safari",
        "ios",
        "android",
        "edge",
        "360",
        "qq",
        "random",
        "randomized",
        ""
    ];

    public static readonly List<string> UserAgent =
    [
        "chrome",
        "firefox",
        "safari",
        "edge",
        "opera-linux",
        "opera-windows",
        "qqbrowser-honor-gt",
        "huawei",
        "gnome-web",
        "none"
    ];

    public static readonly List<string> XhttpMode =
    [
        "auto",
        "packet-up",
        "stream-up",
        "stream-one"
    ];

    public static readonly List<string> AllowInsecure =
    [
        "true",
        "false",
        ""
    ];

    public static readonly List<string> DomainStrategy =
    [
        "AsIs",
        "UseIP",
        "UseIPv4v6",
        "UseIPv6v4",
        "UseIPv4",
        "UseIPv6",
        ""
    ];

    public static readonly List<string> DomainDirectDNSAddress =
    [
        "https://doh.360.cn",
        "https://doh.pub/dns-query",
        "180.184.2.2",
        "52.80.52.52",
        "localhost"
    ];

    public static readonly List<string> DomainRemoteDNSAddress =
    [
        "https://dns.google/dns-query",
        "https://freedns.controld.com/p2",
        "https://anycast.dns.nextdns.io",
        "https://dns.adguard-dns.com/dns-query",
        "8.8.8.8",
        "94.140.14.14",
        "95.85.95.85",
        "76.76.2.2"
    ];

    public static readonly List<string> DomainPureIPDNSAddress =
    [
        "52.80.52.52",
        "180.184.2.2",
        "localhost"
    ];

    public static readonly List<string> Languages =
    [
        "zh-Hans",
        "zh-Hant",
        "en",
        "fa-Ir",
        "fr",
        "ru",
        "hu"
    ];

    public static readonly List<string> Alpns =
    [
        "h3",
        "h2",
        "http/1.1",
        "h3,h2",
        "h2,http/1.1",
        "h3,h2,http/1.1",
        ""
    ];

    public static readonly List<string> LogLevels =
    [
        "debug",
        "info",
        "warning",
        "error",
        "none"
    ];

    public static readonly Dictionary<string, string> LogLevelColors = new()
    {
        { "debug",   "#6C757D" },
        { "info",    "#2ECC71" },
        { "warning", "#FFA500" },
        { "error",   "#E74C3C" },
    };

    public static readonly List<string> InboundTags =
    [
        "socks",
        "socks2",
        "socks3"
    ];

    public static readonly List<string> RuleProtocols =
    [
        "http",
        "tls",
        "bittorrent"
    ];

    public static readonly List<string> RuleNetworks =
    [
        "",
        "tcp",
        "udp",
        "tcp,udp"
    ];

    public static readonly List<string> destOverrideProtocols =
    [
        "http",
        "tls",
        "quic",
        "fakedns",
        "fakedns+others"
    ];

    public static readonly List<int> TunMtus =
    [
        1280,
        1408,
        1500,
        4064,
        9000,
        65535
    ];

    public static readonly List<string> TunStacks =
    [
        "gvisor",
        "system",
        "mixed"
    ];

    public static readonly List<string> PresetMsgFilters =
    [
        "proxy",
        "direct",
        "block",
        ""
    ];

    public static readonly List<string> SingboxMuxs =
    [
        "h2mux",
        "smux",
        "yamux",
        ""
    ];

    public static readonly List<string> TuicCongestionControls =
    [
        "cubic",
        "new_reno",
        "bbr"
    ];

    public static readonly List<string> NaiveCongestionControls =
    [
        "bbr",
        "bbr2",
        "cubic",
        "reno"
    ];

    public static readonly List<string> allowSelectType =
    [
        "selector",
        "urltest",
        "loadbalance",
        "fallback"
    ];

    public static readonly List<string> notAllowTestType =
    [
        "selector",
        "urltest",
        "direct",
        "reject",
        "compatible",
        "pass",
        "loadbalance",
        "fallback"
    ];

    public static readonly List<string> proxyVehicleType =
    [
        "file",
        "http"
    ];

    public static readonly Dictionary<ECoreType, string> CoreUrls = new()
    {
        { ECoreType.v2fly, "v2fly/v2ray-core" },
        { ECoreType.v2fly_v5, "v2fly/v2ray-core" },
        { ECoreType.Xray, "XTLS/Xray-core" },
        { ECoreType.sing_box, "SagerNet/sing-box" },
        { ECoreType.mihomo, "MetaCubeX/mihomo" },
        { ECoreType.hysteria, "apernet/hysteria" },
        { ECoreType.hysteria2, "apernet/hysteria" },
        { ECoreType.naiveproxy, "klzgrad/naiveproxy" },
        { ECoreType.tuic, "EAimTY/tuic" },
        { ECoreType.juicity, "juicity/juicity" },
        { ECoreType.brook, "txthinking/brook" },
        { ECoreType.overtls, "ShadowsocksR-Live/overtls" },
        { ECoreType.shadowquic, "spongebob888/shadowquic" },
        { ECoreType.mieru, "enfein/mieru" },
        { ECoreType.v2rayN, "2dust/v2rayN" },
    };

    public static readonly List<string> OtherGeoUrls =
    [
        @"https://raw.githubusercontent.com/Loyalsoldier/geoip/release/geoip-only-cn-private.dat",
        @"https://raw.githubusercontent.com/Loyalsoldier/geoip/release/Country.mmdb",
        @"https://github.com/MetaCubeX/meta-rules-dat/releases/download/latest/geoip.metadb"
    ];

    public static readonly List<string> IPAPIUrls =
    [
        @"https://www.cloudflare.com/cdn-cgi/trace",
        @"https://api.ip.sb/geoip",
        @"https://api-ipv4.ip.sb/geoip",
        @"https://api-ipv6.ip.sb/geoip",
        @"https://api.ipapi.is",
        @"https://www.chatgpt.com/cdn-cgi/trace",
        @"https://www.grok.com/cdn-cgi/trace",
        @""
    ];

    public static readonly List<string> UdpTestTargets =
    [
        "ntp:pool.ntp.org",
        "ntp:time.google.com",
        "dns:1.1.1.1",
        "dns:8.8.8.8",
        "dns:dns.google",
        "stun:stun.voztovoice.org",
        "stun:stun.cloudflare.com",
        "stun:stun.l.google.com:19302",
    ];

    public static readonly List<string> OutboundTags =
    [
        ProxyTag,
        DirectTag,
        BlockTag
    ];

    public static readonly Dictionary<string, List<string>> PredefinedHosts = new()
    {
        { "dns.google", new List<string> { "8.8.8.8", "8.8.4.4", "2001:4860:4860::8888", "2001:4860:4860::8844" } },
        { "doh.360.cn", new List<string> { "101.91.111.153", "106.63.24.74", "36.99.170.86", "112.65.69.15" } },
        { "doh.pub", new List<string> { "1.12.12.12", "120.53.53.53" } },
        { "freedns.controld.com", new List<string> { "76.76.2.11", "2606:1a40::11" } },
        { "anycast.dns.nextdns.io", new List<string> { "45.90.30.0", "45.90.28.0", "2a07:a8c0::", "2a07:a8c1::" } },
        { "dns.adguard-dns.com", new List<string> { "94.140.14.14", "94.140.15.15", "2a10:50c0::ad1:ff", "2a10:50c0::ad2:ff" } },
        { "engage.cloudflareclient.com", new List<string> { "162.159.192.1", "2606:4700:d0::a29f:c001" } }
    };

    public static readonly List<string> ExpectedIPs =
    [
        "geoip:cn",
        "geoip:ir",
        "geoip:ru",
        ""
    ];

    public static readonly List<string> EchForceQuerys =
    [
        "none",
        "half",
        "full",
        ""
    ];

    public static readonly List<string> TunIcmpRoutingPolicies =
    [
        "rule",
        "direct",
        "unreachable",
        "drop",
        "reply",
    ];

    #endregion const
}

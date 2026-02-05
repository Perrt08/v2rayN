using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLib.Handler.Fmt.ClashConvert;

public class BaseClash
{
    public static T? GetOrDefault<T>(Dictionary<string, object> yamlObject, List<string> keys, T? defaultValue)
    {
        foreach (var key in keys)
        {
            if (!yamlObject.TryGetValue(key, out var value))
            {
                continue;
            }

            if (typeof(T) == typeof(Dictionary<string, object>)
                && value is Dictionary<object, object> v)
            {
                return (T)(object)ToDictionary(v);
            }

            if (value is T typedValue)
            {
                return typedValue;
            }

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                // ignore
            }
        }
        return defaultValue;
    }

    public static T? GetOrDefault<T>(Dictionary<string, object> yamlObject, string key, T? defaultValue)
    {
        return GetOrDefault(yamlObject, [key], defaultValue);
    }

    public static Dictionary<string, object> ToDictionary(Dictionary<object, object> source)
    {
        return source.ToDictionary(
            kvp => kvp.Key.ToString() ?? string.Empty,
            kvp => kvp.Value
        );
    }


    public static bool ResolveTls(Dictionary<string, object> yamlObject, ProfileItem item)
    {
        if (!GetOrDefault(yamlObject, "tls", false))
        {
            return false;
        }

        item.StreamSecurity = Global.StreamSecurity;
        item.Sni = GetOrDefault(yamlObject, ["sni", "servername"], string.Empty);
        item.CertSha = GetOrDefault(yamlObject, "fingerprint", string.Empty);
        item.Alpn = string.Join(",", GetOrDefault(yamlObject, "alpn", new List<string>()));
        item.AllowInsecure = GetOrDefault(yamlObject, ["skip-cert-verify", "insecure"], false).ToString();
        item.Fingerprint = GetOrDefault(yamlObject, "client-fingerprint", string.Empty);

        var echOpts = GetOrDefault<Dictionary<string, object>>(yamlObject, "ech-opts", null);
        if (echOpts is not null && GetOrDefault(echOpts, "enable", false))
        {
            item.EchConfigList = GetOrDefault(echOpts, "config", Global.DomainDirectDNSAddress.First());
        }

        var realityOpts = GetOrDefault<Dictionary<string, object>>(yamlObject, "reality-opts", null);
        if (realityOpts is not null)
        {
            item.PublicKey = GetOrDefault(realityOpts, "public-key", string.Empty);
            item.ShortId = GetOrDefault(realityOpts, "short-id", string.Empty);
        }

        return true;
    }

    public static bool ResloveTransport(Dictionary<string, object> yamlObject, ProfileItem item)
    {
        var network = GetOrDefault(yamlObject, "network", string.Empty);
        if (network.IsNullOrEmpty())
        {
            return false;
        }

        var networkOpts = GetOrDefault<Dictionary<string, object>>(yamlObject, $"{network}-opts", null);
        if (networkOpts is null)
        {
            return false;
        }

        switch (network)
        {
            case "http":
                item.Network = Global.DefaultNetwork;
                item.HeaderType = Global.TcpHeaderHttp;
                break;
            case "h2":
                item.Network = network;
                break;
            case "grpc":
                item.Network = network;
                item.RequestHost = GetOrDefault(networkOpts, "grpc-service-name", string.Empty);
                break;
            case "ws":
                item.Network = network;
                item.RequestHost =
                    GetOrDefault(GetOrDefault<Dictionary<string, object>>(networkOpts, "headers", new()), "Host",
                        string.Empty);
                item.Path = GetOrDefault(networkOpts, "path", string.Empty);
                // TODO: Early Data
                if (GetOrDefault(networkOpts, "v2ray-http-upgrade", false))
                {
                    item.Network = "httpupgrade";
                }
                break;
        }

        return true;
    }

    public static bool ResolveProtocol(Dictionary<string, object> yamlObject, ProfileItem item)
    {
        // proxies and proxy-groups
        item.Address = GetOrDefault(yamlObject, "server", string.Empty);
        item.Port = GetOrDefault(yamlObject, "port", 0);
        if (item.Address.IsNullOrEmpty() || item.Port is <= 0 or > 65535)
        {
            return false;
        }

        var protocolType = GetOrDefault(yamlObject, "type", string.Empty).ToLowerInvariant();
        item.Password = GetOrDefault(yamlObject, ["uuid", "password"], string.Empty);
        ResolveTls(yamlObject, item);
        ResloveTransport(yamlObject, item);
    }
}

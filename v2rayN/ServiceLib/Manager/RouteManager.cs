namespace ServiceLib.Manager;

public class RouteManager
{
    public static (string fileName, string arguments) RedirectToTun(string destination, string tunName)
    {
        if (Utils.IsWindows())
        {
            var tunIndex = GetInterfaceIndex(tunName);
            return ("route", $"add {destination} mask 255.255.255.255 0.0.0.0 if {tunIndex}");
        }
        if (Utils.IsLinux())
        {
            return ("ip", $"route add {destination} dev {tunName}");
        }
        if (Utils.IsMacOS())
        {
            return ("route", $"-n add -net {destination}/32 -interface {tunName}");
        }
        throw new NotSupportedException("Unsupported OS");
    }

    public static (string fileName, string arguments) RemoveRouteFromTun(string destination, string tunName)
    {
        if (Utils.IsWindows())
        {
            var tunIndex = GetInterfaceIndex(tunName);
            return ("route", $"delete {destination} if {tunIndex}");
        }
        if (Utils.IsLinux())
        {
            return ("ip", $"route delete {destination} dev {tunName}");
        }
        if (Utils.IsMacOS())
        {
            return ("route", $"-n delete -net {destination}/32 -interface {tunName}");
        }
        throw new NotSupportedException("Unsupported OS");
    }

    private static int GetInterfaceIndex(string name)
    {
        var ni = NetworkInterface.GetAllNetworkInterfaces()
            .FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return ni?.GetIPProperties().GetIPv4Properties().Index ?? -1;
    }

    public static string? GetDefaultLocalIp()
    {
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
        socket.Connect("8.8.8.8", 65530);
        var endPoint = socket.LocalEndPoint as IPEndPoint;
        return endPoint?.Address.ToString();
    }

    public static string? GetDefaultInterfaceName()
    {
        var allInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        var defaultIp = GetDefaultLocalIp();
        if (defaultIp == null)
        {
            return null;
        }

        return (from ni in allInterfaces
            let ipProps = ni.GetIPProperties()
            from ua in ipProps.UnicastAddresses
            where ua.Address.AddressFamily == AddressFamily.InterNetwork && ua.Address.ToString() == defaultIp
            select ni.Name).FirstOrDefault();
    }
}

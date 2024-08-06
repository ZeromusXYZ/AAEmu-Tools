using System.Runtime.InteropServices.ComTypes;

namespace AAEmu.DBEditor.utils;

public static class IpHelper
{
    public static (string, ushort) SplitAsHostAndPort(string ipAndPort, ushort defaultPort)
    {
        if (ipAndPort.Contains(':'))
        {
            var split = ipAndPort.Split(":");
            if ((split.Length == 2) && (ushort.TryParse(split[1], out var newPort)))
            {
                return (split[0], newPort);
            }
        }
        return (ipAndPort, defaultPort);
    }
}
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using MySqlX.XDevAPI;

namespace AAEmu.DBEditor.utils;

public class HttpHelper
{
    /// <summary>
    /// Does an HTTP Get request and returns the result as a string
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="timeOut"></param>
    /// <returns></returns>
    public static string SimpleGetUriAsString(string uri, uint timeOut = 10000)
    {
        var res = string.Empty;
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/AAEmu.DBEditor")
            );
        client.DefaultRequestHeaders.Add("User-Agent", "AAEmu.DBEditor");
        res = client.GetStringAsync(uri).WaitAsync(TimeSpan.FromMilliseconds(timeOut)).Result;
        return res;
    }
}
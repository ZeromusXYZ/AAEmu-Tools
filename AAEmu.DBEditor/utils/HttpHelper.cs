using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using MySqlX.XDevAPI;
using Newtonsoft.Json;

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

    public static string SimplePostJsonUriAsString(string uri, object bodyForJson, uint timeOut = 10000)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/AAEmu.DBEditor")
        );
        client.DefaultRequestHeaders.Add("User-Agent", "AAEmu.DBEditor");
        var jsonString = JsonConvert.SerializeObject(bodyForJson);
        var content = new ByteArrayContent(Encoding.UTF8.GetBytes(jsonString));
        var res = client.PostAsync(uri, content).WaitAsync(TimeSpan.FromMilliseconds(timeOut)).Result;
        return res.Content.ReadAsStringAsync().Result;
    }

}
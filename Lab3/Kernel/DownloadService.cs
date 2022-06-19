using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Kernel.DownLoader;

namespace Kernel;

public class DownloadService
{
    private readonly List<string> _data;
    private readonly IDownLoader<WebsiteData> _downLoader;

    public DownloadService(IDownLoader<WebsiteData> downLoader)
    {
        _data = new List<string>
        {
            "https://www.yahoo.com",
            "https://www.google.com",
            "https://www.cnn.com",
            "https://www.codeproject.com",
            "https://www.stackoverflow.com",
            "https://www.stackoverflow.com",
            "https://www.youtube.com"
        };
        _downLoader = downLoader;
    }

    public string RunDownloadSync()
    {
        var output = string.Empty;
        foreach (var url in _data)
        {
            var result = _downLoader.Download(url);
            output += PrepData(result);
        }

        return output;
    }

    public async Task<string> RunDownloadAsync()
    {
        var output = string.Empty;
        foreach (var url in _data)
        {
            var result = await _downLoader.DownloadAsync(url);
            output += PrepData(result);
        }

        return output;
    }

    public async Task<string> RunDownloadAsyncParallel()
    {
        var tasks = new List<Task<WebsiteData>>();
        foreach (var url in _data)
        {
            var client = new WebClientWrapper();
            var downLoader = new WebsiteDownLoader(client); // use new instance of downLoader, cause WebClient does not support concurrent I/O
            tasks.Add(downLoader.DownloadAsync(url));
        }

        var results = await Task.WhenAll(tasks);

        return results.Aggregate(string.Empty, (current, res) => current + PrepData(res));
    }


    private string PrepData(WebsiteData websiteData)
    {
        return $"{websiteData.Url} downloaded: {websiteData.Data.Length} characters length.{Environment.NewLine}";
    }
}
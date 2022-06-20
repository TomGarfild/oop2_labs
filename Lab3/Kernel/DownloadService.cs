using System;
using System.Collections.Generic;
using System.Linq;
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

    public string RunDownloadSync(IEnumerable<string>? data = null)
    {
        var output = string.Empty;
        foreach (var url in data ?? _data)
        {
            var result = _downLoader.Download(url);
            output += PrepData(result);
        }

        return output;
    }

    public async Task<string> RunDownloadAsync(IEnumerable<string>? data = null)
    {
        var output = string.Empty;
        foreach (var url in data ?? _data)
        {
            var result = await _downLoader.DownloadAsync(url);
            output += PrepData(result);
        }

        return output;
    }

    public async Task<string> RunDownloadAsyncParallel(IEnumerable<string>? data = null, IDownLoader<WebsiteData>? downLoader = null)
    {
        var tasks = new List<Task<WebsiteData>>();
        foreach (var url in data ?? _data)
        {
            var downLoaderNew = downLoader ?? new WebsiteDownLoader(new WebClientWrapper()); // use new instance of downLoader, cause WebClient does not support concurrent I/O
            tasks.Add(downLoaderNew.DownloadAsync(url));
        }

        var results = await Task.WhenAll(tasks);

        return results.Aggregate(string.Empty, (current, res) => current + PrepData(res));
    }


    private string PrepData(WebsiteData websiteData)
    {
        return $"{websiteData.Url} downloaded: {websiteData.Data.Length} characters length.{Environment.NewLine}";
    }
}
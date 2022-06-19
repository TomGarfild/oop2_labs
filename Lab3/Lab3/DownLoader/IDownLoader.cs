using System.Threading.Tasks;

namespace Lab3.DownLoader;

public interface IDownLoader<TResult>
{
    public TResult Download(string url);
    public Task<TResult> DownloadAsync(string url);
}
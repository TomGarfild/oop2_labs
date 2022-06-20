using System.Threading.Tasks;

namespace Kernel.DownLoader;

public interface IDownLoader<TResult>
{
    public TResult Download(string url);
    public Task<TResult> DownloadAsync(string url);
}
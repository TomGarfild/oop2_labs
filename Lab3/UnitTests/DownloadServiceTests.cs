using System.Diagnostics;
using FluentAssertions;
using Kernel;
using Kernel.DownLoader;
using Moq;

namespace UnitTests;

public class DownloadServiceTests
{
    private Mock<IDownLoader<WebsiteData>> websiteDownLoader;
    private DownloadService downloadService;

    [SetUp]
    public void Setup()
    {
        websiteDownLoader = new Mock<IDownLoader<WebsiteData>>();
        downloadService = new DownloadService(websiteDownLoader.Object);
    }

    [Test]
    public void DownloadSyncTest()
    {
        // Arrange
        var data = Guid.NewGuid().ToString();
        var url = "url";
        websiteDownLoader.Setup(c => c.Download(It.Is<string>(str => string.Equals(str, url)))).Returns(new WebsiteData(url, data));
        // Act
        var result = downloadService.RunDownloadSync(new List<string> { url });

        // Assert
        result.Should().Be($"{url} downloaded: {data.Length} characters length.{Environment.NewLine}");
    }

    [Test]
    public async Task DownloadAsyncTest()
    {
        // Arrange
        var data = Guid.NewGuid().ToString();
        var url = "url";
        websiteDownLoader.Setup(c => c.DownloadAsync(It.Is<string>(str => string.Equals(str, url)))).Returns(Task.FromResult(new WebsiteData(url, data)));
        // Act
        var result = await downloadService.RunDownloadAsync(new List<string> { url });

        // Assert
        result.Should().Be($"{url} downloaded: {data.Length} characters length.{Environment.NewLine}");
    }

    [Test]
    public async Task DownloadAsyncParallelTest()
    {
        // Arrange
        var data = Guid.NewGuid().ToString();
        var url = "url";
        websiteDownLoader.Setup(c => c.DownloadAsync(It.Is<string>(str => string.Equals(str, url)))).Returns(Task.FromResult(new WebsiteData(url, data)));
        // Act
        var result = await downloadService.RunDownloadAsyncParallel(new List<string> { url }, websiteDownLoader.Object);

        // Assert
        result.Should().Be($"{url} downloaded: {data.Length} characters length.{Environment.NewLine}");
    }

    [TestCase(2)]
    [TestCase(10)]
    [TestCase(20)]
    public async Task DownloadAsyncTest_BenchMark(int times)
    {
        // Arrange
        var data = GetData(times);
        foreach (var d in data)
        {
            websiteDownLoader.Setup(c => c.DownloadAsync(It.Is<string>(str => string.Equals(str, d.Key)))).Callback(() => Thread.Sleep(1000))
                .Returns(Task.FromResult(new WebsiteData(d.Key, d.Value)));
        }
        // Act
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await downloadService.RunDownloadAsync(data.Keys);

        // Assert
        var output = data.Select(d => $"{d.Key} downloaded: {d.Value.Length} characters length.{Environment.NewLine}")
            .Aggregate(string.Empty, (current, v) => current + v);
        result.Should().Be(output);
        stopwatch.Elapsed.Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(times + 1));
    }

    [TestCase(2)]
    [TestCase(10)]
    [TestCase(20)]
    public async Task DownloadAsyncParallelTest_BenchMark(int times)
    {
        // Arrange
        var data = GetData(times);
        foreach (var d in data)
        {
            websiteDownLoader.Setup(c => c.DownloadAsync(It.Is<string>(str => string.Equals(str, d.Key)))).Callback(
                async () =>
                {
                    await Task.Delay(1000);
                }).Returns(Task.FromResult(new WebsiteData(d.Key, d.Value)));
        }
        // Act
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await downloadService.RunDownloadAsyncParallel(data.Keys, websiteDownLoader.Object);

        // Assert
        var output = data.Select(d => $"{d.Key} downloaded: {d.Value.Length} characters length.{Environment.NewLine}")
            .Aggregate(string.Empty, (current, v) => current + v);
        result.Should().Be(output);
        stopwatch.Elapsed.Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(Math.Sqrt(times)));
    }

    private static Dictionary<string, string> GetData(int value)
    {
        var data = new Dictionary<string, string>();

        for (int i = 0; i < value; i++)
        {
            data.Add($"url{i}", $"data{i}");
        }

        return data;
    }
}
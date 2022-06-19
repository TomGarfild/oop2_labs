using System.Net;
using FluentAssertions;
using Kernel;
using Kernel.DownLoader;
using Moq;

namespace UnitTests
{
    public class WebsiteDownLoaderTests
    {
        private Mock<WebClientWrapper> client;
        private WebsiteDownLoader websiteDownLoader;

        [SetUp]
        public void Setup()
        {
            client = new Mock<WebClientWrapper>();
            websiteDownLoader = new WebsiteDownLoader(client.Object);
        }

        [Test]
        public void DownloadTest()
        {
            // Arrange
            var data = Guid.NewGuid().ToString();
            var url = "url";
            client.Setup(c => c.DownloadString(It.IsAny<string>())).Returns(data);
            // Act
            var result = websiteDownLoader.Download(url);

            // Assert
            result.Url.Should().Be(url);
            result.Data.Should().Be(data);
        }

        [Test]
        public async Task DownloadAsyncTest()
        {
            // Arrange
            var data = Guid.NewGuid().ToString();
            var url = "url";
            client.Setup(c => c.DownloadStringTaskAsync(It.IsAny<string>())).Returns(Task.FromResult(data));
            // Act
            var result = await websiteDownLoader.DownloadAsync(url);

            // Assert
            result.Url.Should().Be(url);
            result.Data.Should().Be(data);
        }
    }
}
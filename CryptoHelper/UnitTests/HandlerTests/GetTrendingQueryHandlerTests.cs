using FluentAssertions;
using Kernel.Client.Contracts;
using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Moq;

namespace UnitTests.HandlerTests;

public class GetTrendingQueryHandlerTests : UnitTestsBase
{
    [Test]
    public async Task GetsTrendingCryptocurrencies()
    {
        // Arrange
        var cryptocurrencies = new List<Cryptocurrency>
        {
            new() { Id = Guid.NewGuid().ToString(), Name = "crypto1", Slug = "crypto1_slug", Symbol = "CR1"},
            new() { Id = Guid.NewGuid().ToString(), Name = "crypto2", Slug = "crypto2_slug", Symbol = "CR2"},
            new() { Id = Guid.NewGuid().ToString(), Name = "crypto3", Slug = "crypto3_slug", Symbol = "CR3"}
        };
        Client.Setup(x => x.GetTrending(CancellationToken.None)).ReturnsAsync(cryptocurrencies);


        // Act
        var result = (await SendCommand(new GetTrendingQuery())).ToList();

        // Assert
        var resultCryptocurrencies =
            cryptocurrencies.Select(x => new InternalCryptocurrency(x.Id, x.Name, x.Symbol, x.Slug));
        result.Should().BeEquivalentTo(resultCryptocurrencies);

    }
}
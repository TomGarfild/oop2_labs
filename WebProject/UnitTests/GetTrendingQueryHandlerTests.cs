using Kernel.Requests.Queries;
using Mediator.Mediator;

namespace UnitTests;

public class GetTrendingQueryHandlerTests
{
    private readonly IMediator _mediator;

    public GetTrendingQueryHandlerTests(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var result = await _mediator.Send(new GetTrendingQuery());
    }
}
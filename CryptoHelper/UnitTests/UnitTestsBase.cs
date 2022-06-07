using Kernel;
using Kernel.Client.Clients;
using Mediator;
using Mediator.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace UnitTests;

public class UnitTestsBase
{
    protected IServiceCollection ServiceCollection { get; set; }
    protected IServiceProvider ServiceProvider { get; set; }
    protected IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();
    protected Mock<BaseClient> Client = new();

    [SetUp]
    public virtual void Setup()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureTestServices(serviceCollection);
        ServiceCollection = serviceCollection;
        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }
    
    protected async Task<TResponse> SendCommand<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default) => await Mediator.Send(request, cancellationToken);


    private void ConfigureTestServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediator(ServiceLifetime.Singleton, typeof(AppExtensions.AssemblyClass).Assembly);
        serviceCollection.AddSingleton(Client.Object);
    }
}
using System.Data.Entity.Infrastructure;
using Kernel.Client.Clients;
using Kernel.Common.ActionTypes;
using Kernel.Data;
using Kernel.Data.Entities;
using Kernel.Data.Managers;
using Kernel.Services.Db;
using Mediator;
using Mediator.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnitTests.TestAsync;
using AppExtensions = Kernel.AppExtensions;

namespace UnitTests;

public class UnitTestsBase
{
    protected IServiceCollection ServiceCollection { get; set; }
    protected IServiceProvider ServiceProvider { get; set; }
    protected IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();
    protected Mock<BaseClient> Client = new();

    // Data
    protected Mock<DataDbContext> MockContext = new();
    protected List<UserData> UserSet = new ();
    protected Mock<DbSet<UserData>> MockUserSet;
    protected List<AlertData> AlertSet = new();
    protected Mock<DbSet<AlertData>> MockAlertSet;

    protected IManager<AlertData, AlertActionType> AlertsManager => ServiceProvider.GetRequiredService<IManager<AlertData, AlertActionType>>();
    protected IManager<UserData, UserActionType> UsersManager => ServiceProvider.GetRequiredService<IManager<UserData, UserActionType>>();

    protected UsersService UsersService => ServiceProvider.GetRequiredService<UsersService>();
    protected AlertsService AlertsService => ServiceProvider.GetRequiredService<AlertsService>();


    protected void SetUpAlertDb()
    {
        MockAlertSet.Setup(m => m.AddAsync(It.IsAny<AlertData>(), CancellationToken.None))
            .Callback<AlertData, CancellationToken>((u, _) => { AlertSet.Add(u); });
        MockContext.Setup(x => x.Alerts).Returns(MockAlertSet.Object);
    }

    protected void SetUpUserDb()
    {
        MockUserSet.Setup(m => m.AddAsync(It.IsAny<UserData>(), CancellationToken.None))
            .Callback<UserData, CancellationToken>((u, _) => { UserSet.Add(u); });
        MockContext.Setup(x => x.Users).Returns(MockUserSet.Object);
    }

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
        ConfigureDbServices(serviceCollection);
    }

    private void ConfigureDbServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(MockContext.Object);
        MockUserSet = CreateDbSetMock(UserSet.AsQueryable());
        serviceCollection.AddSingleton<IManager<UserData, UserActionType>, UsersManager>();
        serviceCollection.AddSingleton<UsersService>();
        MockAlertSet = CreateDbSetMock(AlertSet.AsQueryable());
        serviceCollection.AddSingleton<IManager<AlertData, AlertActionType>, AlertsManager>();
        serviceCollection.AddSingleton<AlertsService>();
    }

    private static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> items) where T : class
    {
        var dbSetMock = new Mock<DbSet<T>>();

        dbSetMock.As<IAsyncEnumerable<T>>()
            .Setup(x => x.GetAsyncEnumerator(default))
            .Returns(new TestAsyncEnumerator<T>(items.GetEnumerator()));
        dbSetMock.As<IQueryable<T>>()
            .Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<T>(items.Provider));
        dbSetMock.As<IQueryable<T>>()
            .Setup(m => m.Expression).Returns(items.Expression);
        dbSetMock.As<IQueryable<T>>()
            .Setup(m => m.ElementType).Returns(items.ElementType);
        dbSetMock.As<IQueryable<T>>()
            .Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

        return dbSetMock;
    }
}
using System.Collections.Concurrent;

namespace Mediator.Mediator;

public class MediatorImpl : IMediator
{
    private readonly Func<Type, object> _serviceResolver;
    private readonly ConcurrentDictionary<Type, Type> _handlerResolvers;

    public MediatorImpl(Func<Type, object> serviceResolver, IDictionary<Type, Type> handlerResolvers)
    {
        _serviceResolver = serviceResolver;
        _handlerResolvers = new ConcurrentDictionary<Type, Type>(handlerResolvers);
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var requestType = request.GetType();
        if (!_handlerResolvers.ContainsKey(requestType))
        {
            throw new ArgumentException($"No handler to handle request of type: {requestType.Name}");
        }

        var requestHandlerType = _handlerResolvers[requestType];
        var handler = _serviceResolver(requestHandlerType);

        return await ((Task<TResponse>)handler.GetType().GetMethod("Handle")!.Invoke(handler, new object[]{ request, cancellationToken }))!;
    }
}
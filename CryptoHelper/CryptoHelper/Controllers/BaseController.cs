using Mediator;
using Mediator.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHelper.Controllers
{
    public class BaseController : Controller
    {
        protected IMediator Mediator { get; set; }

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}

using Kernel.Services;
using Telegram.Bot.Types;

namespace Kernel.States;

public abstract class UpdateServiceState
{
    protected HandleUpdateService _service;

    public void SetContext(HandleUpdateService service)
    {
        _service = service;
    }

    public abstract Task Handle(Update update);
}
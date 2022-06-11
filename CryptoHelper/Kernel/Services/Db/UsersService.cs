using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Kernel.Data.Managers;
using Kernel.Domain.Entities;

namespace Kernel.Services.Db;

public class UsersService
{
    private readonly IManager<UserData, UserActionType> _manager;

    public UsersService(IManager<UserData, UserActionType> manager)
    {
        _manager = manager;
    }

    public UserData GetByChatId(long chatId)
    {
        var users = _manager.GetAll();
        return users.FirstOrDefault(a => a.ChatId == chatId);
    }

    public async Task AddAsync(InternalUser internalUser)
    {
        var oldUser = GetByChatId(internalUser.ChatId);
        if (oldUser != null) throw new ArgumentException($"User with {internalUser.ChatId} already exists");
        var user = new UserData(internalUser.ChatId, internalUser.Username, internalUser.FirstName, internalUser.LastName);
        await _manager.UpdateAsync(user, UserActionType.Created);
    }
}
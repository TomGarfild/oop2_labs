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

    public async Task<UserData> GetByChatId(long chatId)
    {
        var users = await _manager.GetAllAsync();
        return users.ToList().FirstOrDefault(a => a.ChatId == chatId);
    }

    public async Task AddAsync(InternalUser internalUser)
    {
        var oldUser = await GetByChatId(internalUser.ChatId);
        if (oldUser != null) throw new ArgumentException($"User with {internalUser.ChatId} chat id already exists");
        var user = new UserData(internalUser.ChatId, internalUser.Username, internalUser.FirstName, internalUser.LastName);
        await _manager.UpdateAsync(user, UserActionType.Created);
    }
}
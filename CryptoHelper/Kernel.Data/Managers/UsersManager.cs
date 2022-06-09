using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class UsersManager : IManager<UserData, UserActionType>
{
    private readonly DataDbContext _dbContext;

    public UsersManager(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(UserData newUser)
    {
        var user = await GetAsync(newUser.ChatId);
        if (user == null)
        {
            await _dbContext.Users.AddAsync(newUser);
        }
        else
        {
            _dbContext.Users.Update(newUser);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserData?> GetAsync(long chatId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);
    }

    public Task UpdateAsync(UserData entity, UserActionType actionType)
    {
        throw new NotImplementedException();
    }

    public Task<UserData> GetAsync(string key)
    {
        throw new NotImplementedException();
    }
}
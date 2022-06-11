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

    public async Task UpdateAsync(UserData entity, UserActionType actionType)
    {
        var user = await GetAsync(entity.Id);
        if (user == null)
        {
            await _dbContext.Users.AddAsync(entity);
        }
        else
        {
            _dbContext.Users.Update(entity);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserData> GetAsync(string key)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == key);
    }

    public async Task DeleteAsync(string key)
    {
        var user = await GetAsync(key);

        if (user == null)
        {
            throw new ArgumentNullException($"User with id {key} does not exist");
        }

        user = user with { IsActive = false };

        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
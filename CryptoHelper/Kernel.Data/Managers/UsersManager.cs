using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class UsersManager : Manager<UserData, UserActionType>
{
    public UsersManager(DataDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task UpdateAsync(UserData entity, UserActionType actionType)
    {
        var user = await GetAsync(entity.Id);
        if (user == null)
        {
            await DbContext.Users.AddAsync(entity);
        }
        else
        {
            DbContext.Users.Update(entity);
        }
        await DbContext.SaveChangesAsync();
    }

    public override async Task<UserData> GetAsync(string key)
    {
        return await DbContext.Users.FirstOrDefaultAsync(u => u.Id == key);
    }

    public override async Task DeleteAsync(string key)
    {
        var user = await GetAsync(key);

        if (user == null)
        {
            throw new ArgumentNullException($"User with id {key} does not exist");
        }

        user = user with { IsActive = false };

        DbContext.Update(user);
        await DbContext.SaveChangesAsync();
    }
}
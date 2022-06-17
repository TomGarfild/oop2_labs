using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class UsersManager : Manager<UserData, UserActionType>
{
    public UsersManager(DataDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IList<UserData>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Users.Where(u => u.IsActive).ToListAsync(cancellationToken);
    }

    public override async Task UpdateAsync(UserData entity, UserActionType actionType, CancellationToken cancellationToken = default)
    {
        var user = await GetAsync(entity.Id, cancellationToken);
        if (user == null)
        {
            await DbContext.Users.AddAsync(entity, cancellationToken);
        }
        else
        {
            DbContext.Users.Update(entity);
        }
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public override async Task<UserData> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users.FirstOrDefaultAsync(u => u.Id == key && u.IsActive, cancellationToken);
    }

    public override async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        var user = await GetAsync(key, cancellationToken);

        if (user == null)
        {
            throw new ArgumentNullException($"User with id {key} does not exist");
        }

        user = user with { IsActive = false };

        DbContext.Users.Update(user);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
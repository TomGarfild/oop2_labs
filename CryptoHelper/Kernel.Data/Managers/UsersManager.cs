using Kernel.Data.Entities;

namespace Kernel.Data.Managers;

public class UsersManager
{
    private readonly DataDbContext _dbContext;

    public UsersManager(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserData> Create()
    {
        return null;
    }
}
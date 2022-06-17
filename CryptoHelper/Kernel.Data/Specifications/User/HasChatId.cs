using Kernel.Data.Entities;

namespace Kernel.Data.Specifications.User;

public record HasChatId(long ChatId) : ISpecification<UserData>
{
    public async Task<IEnumerable<UserData>> Get()
    {
        throw new NotImplementedException();
    }

}
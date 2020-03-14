using OppJar.Domain;
using OppJar.Dto;

namespace OppJar.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User AddUser(string role, RegisterDto dto);
        User AddChild(ChildInfoDto dto);
    }
}

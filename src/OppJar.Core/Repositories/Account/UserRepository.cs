using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OppJar.AutoMapper;
using OppJar.Common.Factories;
using OppJar.Domain;
using OppJar.Dto;
using System.Linq;
using OppJar.Common.Enum;

namespace OppJar.Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private const string DefaultPassword = "Oppj@r123";

        private readonly IPasswordHasher<User> _passwordHasher;

        private DbSet<UserRole> UserRoles { get; set; }
        private DbSet<Role> Roles { get; set; }

        public UserRepository(OppJarContext context) : base(context)
        {
            UserRoles = context.UserRoles;

            Roles = context.Roles;

            _passwordHasher = ResolverFactory.GetService<IPasswordHasher<User>>();
        }

        public User AddUser(string role, RegisterDto dto)
        {
            var user = dto.ToEntity();

            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            Add(user);

            var userRole = new UserRole
            {
                RoleId = Roles.SingleOrDefault(x => x.Name.Equals(role)).Id,
                UserId = user.Id
            };

            UserRoles.Add(userRole);

            return user;
        }

        public User AddChild(ChildInfoDto dto)
        {
            var user = dto.ToEntity();

            user.Status = UserStatus.Activate;

            user.Password = _passwordHasher.HashPassword(user, DefaultPassword);

            Add(user);

            var userRole = new UserRole
            {
                RoleId = Roles.SingleOrDefault(x => x.Name.Equals(Settings.UserRoles.Child)).Id,
                UserId = user.Id
            };

            UserRoles.Add(userRole);

            return user;
        }
    }
}

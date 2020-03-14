using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OppJar.Common.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OppJar.Domain
{
    public class DbSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly OppJarContext _context;
        private string ApiUrl = string.Empty;

        public DbSeed(IPasswordHasher<User> passwordHasher, OppJarContext context, IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _context = context;
            ApiUrl = configuration.GetSection("JWTSettings").GetValue<string>("Issuer");
        }

        public async Task SeedRole(IEnumerable<string> roles)
        {
            if (!_context.Roles.Any())
            {
                foreach (var role in roles)
                {
                    _context.Roles.Add(new Role
                    {
                        Name = role,
                        Description = role,
                        NormalizedName = role.ToUpper()
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedAdmin()
        {
            if (!_context.Users.Any(x => x.UserName.Equals("admin@oppjar.com")))
            {
                var adminRole = _context.Roles.SingleOrDefault(x => x.Name.Equals("SuperAdministrator"));

                var adminUser = new User
                {
                    UserName = "admin@oppjar.com",
                    NormalizedUserName = "admin@oppjar.com".ToUpper(),
                    Email = "admin@oppjar.com",
                    Status = UserStatus.Activate
                };

                adminUser.Password = _passwordHasher.HashPassword(adminUser, "OppJ@r!2#");

                _context.Users.Add(adminUser);

                var adminDetail = new UserDetail
                {
                    UserId = adminUser.Id,
                    FirstName = "Admin",
                };

                adminDetail.Avatar = $"{ApiUrl}{adminDetail.Avatar}";

                _context.UserDetails.Add(adminDetail);

                _context.UserRoles.Add(new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}

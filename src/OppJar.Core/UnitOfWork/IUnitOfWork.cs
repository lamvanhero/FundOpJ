using Microsoft.AspNetCore.Http;
using OppJar.Core.Repositories;
using OppJar.Domain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OppJar.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region REPOSITORIES
        IUserRepository UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IRepository<UserDetail> UserDetailRepository { get; }
        IRepository<Address> AddressRepository { get; }
        IRepository<UserRelationship> UserRelationshipRepository { get; }
        IRepository<MediaFile> MediaFileRepository { get; }
        IRepository<Feed> FeedRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Event> EventRepository { get; }
        #endregion
        HttpContext HttpContext { get; }
        ClaimsPrincipal User { get; }
        string UserId { get; }
        string UserName { get; }
        string DisplayName { get; }
        void Commit();
        Task CommitAsync();
        Task CommitAsync(Func<Task> action);
    }
}

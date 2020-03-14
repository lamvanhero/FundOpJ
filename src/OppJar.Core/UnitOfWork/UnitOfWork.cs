using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OppJar.Common.Helpers;
using OppJar.Core.Repositories;
using OppJar.Domain;

namespace OppJar.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDictionary<Type, object> _repositories;

        private OppJarContext _context;

        public HttpContext HttpContext { get; private set; }

        public ClaimsPrincipal User
        {
            get
            {
                if (HttpContext == null) return null;

                else return HttpContext.User;
            }
        }

        public string UserId => User.GetValue(ClaimKeyHelper.USER_ID);
        public string UserName => User.GetValue(ClaimKeyHelper.EMAIL);
        public string DisplayName => User.GetValue(ClaimKeyHelper.NAME);

        #region REPOSITORIES
        public IUserRepository UserRepository => GetRepository<UserRepository>();
        public IRepository<Role> RoleRepository => GetGenericRepository<Role>();
        public IRepository<UserRole> UserRoleRepository => GetGenericRepository<UserRole>();
        public IRepository<UserDetail> UserDetailRepository => GetGenericRepository<UserDetail>();
        public IRepository<Address> AddressRepository => GetGenericRepository<Address>();
        public IRepository<UserRelationship> UserRelationshipRepository => GetGenericRepository<UserRelationship>();
        public IRepository<MediaFile> MediaFileRepository => GetGenericRepository<MediaFile>();
        public IRepository<Feed> FeedRepository => GetGenericRepository<Feed>();
        public IRepository<Comment> CommentRepository => GetGenericRepository<Comment>();
        public IRepository<Event> EventRepository => GetGenericRepository<Event>();
        #endregion

        public UnitOfWork(OppJarContext context, IHttpContextAccessor httpContextAccessor) : this(context)
        {
            HttpContext = httpContextAccessor.HttpContext;

            _repositories = new Dictionary<Type, object>();
        }

        public UnitOfWork(OppJarContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            BeforeCommit();

            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            BeforeCommit();

            await _context.SaveChangesAsync();
        }

        public async Task CommitAsync(Func<Task> action)
        {
            BeforeCommit();

            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = _context.Database.BeginTransaction();

                await _context.SaveChangesAsync();

                await action();

                transaction.Commit();
            });
        }

        protected void BeforeCommit()
        {
            var entriesAdded = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity);

            var entriesModified = _context.ChangeTracker.Entries()
                  .Where(e => e.State == EntityState.Modified).Select(e => e.Entity as IAudit);

            if (entriesAdded.Count() > 0) ProcessAudit(entriesAdded, EntityState.Added);

            if (entriesModified.Count() > 0) ProcessAudit(entriesModified, EntityState.Modified);
        }

        private void ProcessAudit(IEnumerable<object> entries, EntityState state)
        {
            foreach (var e in entries.Select(e => e as IAudit))
            {
                if (e != null)
                {
                    if (state == EntityState.Added)
                    {
                        e.CreatedBy = UserId;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        e.UpdatedBy = UserId;
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }
        }

        private T GetRepository<T>()
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (T)_repositories[typeof(T)];
            }
            else
            {
                var repository = (T)Activator.CreateInstance(typeof(T), _context);

                _repositories.Add(typeof(T), repository);

                return repository;
            }

        }

        private GenericRepository<T> GetGenericRepository<T>() where T : class, IBaseEntity
        {
            if (_repositories.ContainsKey(typeof(GenericRepository<T>)))
            {
                return (GenericRepository<T>)_repositories[typeof(GenericRepository<T>)];
            }
            else
            {
                var repository = new GenericRepository<T>(_context);

                _repositories.Add(typeof(GenericRepository<T>), repository);

                return repository;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

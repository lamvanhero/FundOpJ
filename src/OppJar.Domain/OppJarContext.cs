using Microsoft.EntityFrameworkCore;
using OppJar.Domain.Configurations;
using System.Threading;
using System.Threading.Tasks;

namespace OppJar.Domain
{
    public class OppJarContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventEmail> EventEmails { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletDetail> WalletDetails { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public OppJarContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ConfigureBase();
            modelBuilder.Entity<Role>().ConfigureBase();
            modelBuilder.Entity<UserRole>().ConfigureBase();
            modelBuilder.Entity<Address>().ConfigureBase();
            modelBuilder.Entity<Event>().ConfigureBase();
            modelBuilder.Entity<EventEmail>().ConfigureBase();
            modelBuilder.Entity<Group>().ConfigureBase();
            modelBuilder.Entity<GroupMember>().ConfigureBase();
            modelBuilder.Entity<Transaction>().ConfigureBase();
            modelBuilder.Entity<UserDetail>().ConfigureBase();
            modelBuilder.Entity<UserRelationship>().ConfigureBase();
            modelBuilder.Entity<Wallet>().ConfigureBase();
            modelBuilder.Entity<WalletDetail>().ConfigureBase();
            modelBuilder.Entity<MediaFile>().ConfigureBase();
            modelBuilder.Entity<Feed>().ConfigureBase();
            modelBuilder.Entity<Comment>().ConfigureBase();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OppJarContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

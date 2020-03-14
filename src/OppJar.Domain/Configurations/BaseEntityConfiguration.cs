using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OppJar.Domain.Configurations
{
    public static class EntityBaseConfiguration
    {
        public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IBaseEntity
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}

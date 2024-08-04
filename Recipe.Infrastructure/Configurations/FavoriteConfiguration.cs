using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Configurations
{
    internal class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);

            builder.HasOne(x => x.Recipe).WithMany(u => u.Favorites).HasForeignKey(x => x.RecipeId);
            builder.HasOne(x => x.User).WithMany(u => u.Favorites).HasForeignKey(x => x.UserId);

            builder.ToTable("Favorites");
        }
    }
}
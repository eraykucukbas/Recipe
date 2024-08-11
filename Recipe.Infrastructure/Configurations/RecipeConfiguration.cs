using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.Infrastructure.Configurations
{
    internal class RecipeConfiguration : IEntityTypeConfiguration<RecipeEntity>
    {
        public void Configure(EntityTypeBuilder<RecipeEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);

            builder.HasOne(x => x.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Recipes");
        }
    }
}
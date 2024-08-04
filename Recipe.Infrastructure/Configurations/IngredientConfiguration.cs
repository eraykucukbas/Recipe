using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Configurations
{
    internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(450);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);

            builder.HasOne(x => x.Recipe).WithMany(r => r.Ingredients).HasForeignKey(x => x.RecipeId);
            builder.HasOne(x => x.Unit).WithMany(u => u.Ingredients).HasForeignKey(x => x.UnitId);

            builder.ToTable("Ingredients");
        }
    }
}
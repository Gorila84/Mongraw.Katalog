using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Infrastructure.DbConfiguration
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.HasKey(sc => sc.Id);
            builder.HasIndex(builder => builder.Name).IsUnique();
            builder.Property(sc => sc.Name).IsRequired().HasMaxLength(100);
           
            builder.HasMany(sc => sc.Items)
                   .WithOne(i => i.Subcategory)
                   .HasForeignKey(i => i.SubcategoryId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

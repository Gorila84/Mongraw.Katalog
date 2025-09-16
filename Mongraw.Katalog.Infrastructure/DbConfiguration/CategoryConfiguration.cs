using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Infrastructure.DbConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(builder => builder.Name).IsUnique();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(c => c.Subcategories)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Items)
                     .WithOne(i => i.Category)
                     .HasForeignKey(i => i.CategoryId)
                     .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

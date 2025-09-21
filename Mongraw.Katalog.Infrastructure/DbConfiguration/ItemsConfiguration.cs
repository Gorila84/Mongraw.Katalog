using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Mongraw.Katalog.Domain.Models.ItemsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Infrastructure.DbConfiguration
{
    internal class ItemsConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(builder => builder.Name).IsUnique();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(1000);
            builder.Property(e => e.Price)
                   .HasColumnType("decimal(18,2)");
            builder.Property(e => e.WholesalerName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Quantity);
            builder.HasOne(c => c.Category);
            builder.HasOne(c => c.Subcategory);
            builder.HasMany(c => c.Images);
                    
        }
    }
}

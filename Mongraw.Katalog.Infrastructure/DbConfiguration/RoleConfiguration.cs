using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Mongraw.Katalog.Domain.Models.UserManagement;

namespace Mongraw.Katalog.Infrastructure.DbConfiguration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id);
            
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Data.CommonConfiguration
{
    public class PermissionConfiguration: EntityConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.SystemName).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Name).HasMaxLength(256);

            builder.HasIndex(p => p.SystemName).IsUnique();
        }
    }
}

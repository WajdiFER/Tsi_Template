using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Data.CommonConfiguration
{
    public class PermissionUserRoleMappingConfiguration : EntityConfiguration<PermissionUserRoleMapping>
    {
        public override void Configure(EntityTypeBuilder<PermissionUserRoleMapping> builder)
        {
            base.Configure(builder);

            builder.HasOne<Permission>()
               .WithMany()
               .HasForeignKey(p => p.PermissionId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<UserRole>()
              .WithMany()
              .HasForeignKey(p => p.UserRoleId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Data.CommonConfiguration
{
    public class UserRoleMappingConfiguration : EntityConfiguration<UserRoleMapping>
    {
        public override void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            base.Configure(builder);

            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<UserRole>()
              .WithMany()
              .HasForeignKey(p => p.UserRoleId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

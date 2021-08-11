using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Data.CommonConfiguration
{
    public class LogConfiguration : EntityConfiguration<Log>
    {
        public override void Configure(EntityTypeBuilder<Log> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.ShortMessage).HasMaxLength(256).IsRequired();
            builder.Property(p => p.FullMessage).IsRequired();
            builder.Property(p => p.IpAddress).HasMaxLength(15);
            builder.Property(p => p.PageUrl).HasMaxLength(256);
            builder.Property(p => p.ReferrerUrl).HasMaxLength(256);
        }
    }
}

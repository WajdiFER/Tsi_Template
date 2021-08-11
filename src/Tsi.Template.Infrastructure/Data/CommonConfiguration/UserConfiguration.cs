using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Data.CommonConfiguration
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Username).HasMaxLength(256);
            builder.Property(p => p.NormalizedUsername).HasMaxLength(256);
            builder.Property(p => p.Email).HasMaxLength(256);
            builder.Property(p => p.NormalizedEmail).HasMaxLength(256);
            builder.Property(p => p.PhoneNumber).HasMaxLength(15);
            builder.Property(p => p.Salt).HasMaxLength(256);
            builder.Property(p => p.Password).HasMaxLength(512);

            builder.HasIndex(p => p.NormalizedUsername).IsUnique().HasFilter($" {nameof(User.NormalizedUsername)} IS NULL ");
            builder.HasIndex(p => p.Username).IsUnique().HasFilter($" {nameof(User.Username)} IS NULL ");
            builder.HasIndex(p => p.Email).IsUnique().HasFilter($" {nameof(User.Email)} IS NULL ");
            builder.HasIndex(p => p.NormalizedEmail).IsUnique().HasFilter($" {nameof(User.NormalizedEmail)} IS NULL ");
            builder.HasIndex(p => p.PhoneNumber).IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Configuration
{
    public class ProductConfiguration: EntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(50);
            builder.HasIndex(e => e.Code).IsUnique();

            builder.Property(e => e.Libelle).IsRequired().HasMaxLength(250);

            builder.Property(e => e.Price).IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Configuration
{
    public class CategoryConfiguration: EntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Code) 
                .IsUnique(); 
        }
    }
}

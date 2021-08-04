using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Helpers;

namespace Tsi.Template.Infrastructure.Abstraction
{
    public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable(TablesNamingHelper.NameOf<T>());
        }
    }
}

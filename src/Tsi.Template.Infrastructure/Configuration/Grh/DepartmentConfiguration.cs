using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Domain.Grh;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Configuration.Grh
{
    public class DepartmentConfiguration : EntityConfiguration<Departement>
    {
        public override void Configure(EntityTypeBuilder<Departement> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Libelle)
                .IsRequired();
        }


     }
}

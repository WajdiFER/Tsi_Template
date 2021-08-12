using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tsi.Template.Domain.Grh;
using Tsi.Template.Infrastructure.Abstraction;

namespace Tsi.Template.Infrastructure.Configuration.Grh
{
    class EmployeeConfiguration : EntityConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.LastName)
                .IsRequired();

            builder.Property(e => e.FirstName)
                .IsRequired();

            builder.HasIndex(e => e.Cin)
                .IsUnique();

            builder.Property(e => e.Cin)
                .HasMaxLength(8);

            builder.HasOne(s => s.Departement)
                .WithMany(g => g.Employees)
                .HasForeignKey(s => s.DepartementId);
        }
    }
}

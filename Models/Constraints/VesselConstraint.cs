using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Constraints
{
    public class VesselConstraint
    {
        public VesselConstraint(EntityTypeBuilder<Vessel> builder)
        {
            builder.Property(t => t.CompanyId).IsRequired();
            builder.Property(t => t.Name).IsRequired();
            builder.HasOne(t => t.Company).WithMany(c => c.Vessels);

            builder.HasOne(v => v.Company).WithMany(c => c.Vessels).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

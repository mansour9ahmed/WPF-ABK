using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Constraints
{
    public class SoaElementConstraint
    {
        public SoaElementConstraint(EntityTypeBuilder<SoaElement> builder)
        {
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.IsActive).HasDefaultValue(true);
            builder.HasOne(t => t.Company).WithMany(c => c.Elements).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

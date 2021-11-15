using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Constraints
{
    public class SoaConstraint
    {
        public SoaConstraint(EntityTypeBuilder<Soa> builder)
        {
            builder.Property(t => t.SoaNo).IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Constraints
{
    public class InvoiceSoaElementConstraint
    {
        public InvoiceSoaElementConstraint(EntityTypeBuilder<InvoiceSoaElement> builder)
        {
            builder.HasOne(c => c.Invoice).WithMany(i => i.SoaElements);
            builder.HasOne(c => c.SoaElement).WithMany(i => i.Invoices);
        }
    }
}

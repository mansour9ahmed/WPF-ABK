using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Constraints
{
    public class InvoiceConstraint
    {
        public InvoiceConstraint(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(t => t.InvoiceNo).IsRequired();
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.IntegraPrice).HasDefaultValue(0.0);
            builder.Property(t => t.IsPaid).HasDefaultValue(false);
            builder.HasOne(t => t.Vessel).WithMany(v => v.Invoices);

            builder.HasOne(t => t.Vessel).WithMany(v => v.Invoices).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Bank).WithMany(b => b.Invoices).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

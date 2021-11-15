
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Constraints
{
    public class CompanyConstraint
    {
        public CompanyConstraint(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(t => t.Name).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();

        }
    }
}

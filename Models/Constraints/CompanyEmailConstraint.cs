using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Constraints
{
    public class CompanyEmailConstraint
    {
        public CompanyEmailConstraint(EntityTypeBuilder<CompanyEmail> builder)
        {
            builder.HasOne(t => t.Company).WithMany(c => c.Emails);
        }
    }
}

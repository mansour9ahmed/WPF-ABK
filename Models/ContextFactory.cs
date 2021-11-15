using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class  ContextFactory : IDesignTimeDbContextFactory<BurakDbContext>
    {
        
        public BurakDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BurakDbContext>();
            optionsBuilder.UseSqlServer(args[0]);
            var context = new BurakDbContext(optionsBuilder.Options);

            return context;
        }
    }
}

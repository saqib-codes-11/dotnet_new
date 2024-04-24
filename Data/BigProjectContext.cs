using BigProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigProject.Data.Entities
{
    public class BigProjectContext: DbContext
    {
        public BigProjectContext(DbContextOptions<BigProjectContext> options): base(options) {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

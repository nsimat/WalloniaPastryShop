using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalloniaPastryShop.Models
{
    public class WalloniaPastryShopDbContext: DbContext
    {
        public WalloniaPastryShopDbContext(DbContextOptions<WalloniaPastryShopDbContext> options): base(options)
        {

        }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

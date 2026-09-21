using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using SalesCet107.Web.Data.Entities;

namespace SalesCet107.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}

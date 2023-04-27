using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using site.Models.Domain;
namespace site.Data
{
    public class MVCMagazinDbContext : DbContext
    {
        public MVCMagazinDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Tovar> Tovar { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Sklad> Sklad { get; set; }
        public DbSet<Zakaz> Zakaz { get; set; }

    }
}

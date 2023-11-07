using backendTask.DataBase.Models;
using backendTask.DataBase;
using Microsoft.EntityFrameworkCore;
using backendTask.DBContext.Models;

namespace backendTask.DBContext
{
    public class AddressDBContext : DbContext
    {
        public AddressDBContext(DbContextOptions<AddressDBContext> options) : base(options) { }

        public DbSet<as_addr_obj> as_addr_obj { get; set; }
        public DbSet<as_adm_hierachy> as_adm_hierachy { get; set; }
        public DbSet<as_houses> as_houses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

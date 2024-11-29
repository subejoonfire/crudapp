using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crudapp.Models;

namespace crudapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Penumpang> Penumpangs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Bisman.Models
{
    public class BismanContext : DbContext
    {
        public BismanContext (DbContextOptions<BismanContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Servico> Servico { get; set; }
    }
}

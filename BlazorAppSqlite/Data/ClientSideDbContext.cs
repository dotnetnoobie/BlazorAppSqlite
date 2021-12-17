using Microsoft.EntityFrameworkCore;

namespace BlazorAppSqlite.Data
{
    internal class ClientSideDbContext : DbContext
    {
        public DbSet<Wave> Waves { get; set; } = default!;

        public ClientSideDbContext(DbContextOptions<ClientSideDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

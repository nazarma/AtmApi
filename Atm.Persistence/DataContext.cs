namespace Atm.Persistence
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<LegalTender> LegalTender { get; set; }
    }
}

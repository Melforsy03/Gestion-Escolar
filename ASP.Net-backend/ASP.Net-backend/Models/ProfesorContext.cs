using Microsoft.EntityFrameworkCore;

namespace ASP.Net_backend.Models
{
    public class ProfesorContext: DbContext
    {
         public ProfesorContext(DbContextOptions options) : base(options)
         {
         }
        public DbSet<Profesor> Profesor { get; set; }
    }
}

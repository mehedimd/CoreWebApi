using Microsoft.EntityFrameworkCore;

namespace CoreWebApi.Models
{
    public class PharmacyModel: DbContext
    {
        public PharmacyModel(DbContextOptions<PharmacyModel> options):base(options) { }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Unit> Unit { get; set; }   
    }
}

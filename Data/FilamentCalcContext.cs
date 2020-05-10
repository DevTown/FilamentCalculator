using FilamentCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Data
{
    public class FilamentCalcContext : DbContext
    {
        private string my_host { get; set; }
        private string my_db { get; set; }
        private string my_user { get; set; }
        private string my_pw { get; set; }

        public DbSet<Filament> Filaments { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host={my_host};Database={my_db};Username={my_user};Password={my_pw}");
    }
}
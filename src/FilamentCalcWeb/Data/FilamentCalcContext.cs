using System;
using FilamentCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Data
{
    public class FilamentCalcContext : DbContext
    {
        public FilamentCalcContext(DbContextOptions<FilamentCalcContext> options) : base(options)
        {
        }

        public FilamentCalcContext():base(new DbContextOptions<FilamentCalcContext>())
        {
            
        }

        private static string MyHost => Environment.GetEnvironmentVariable("PGHostname");

        private static string MyDb => Environment.GetEnvironmentVariable("PGDB");

        private static string MyUser => Environment.GetEnvironmentVariable("PGUsername");

        private static string MyPw => Environment.GetEnvironmentVariable("PGPassword");


        public DbSet<Filament> Filaments { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<FilamentType> FilamentTypes { get; set; }
        
        public DbSet<Settings> Settingses { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            =>
                //optionsBuilder.UseNpgsql($"Host=localhost;Database=filamentcalc;Username=postgres;Password=example"); 
        optionsBuilder.UseNpgsql($"Host={MyHost};Database={MyDb};Username={MyUser};Password={MyPw}");
    }
}
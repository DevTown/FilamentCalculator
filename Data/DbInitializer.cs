using System;
using System.Linq;
using FilamentCalculator.Models;

namespace FilamentCalculator.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FilamentCalcContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.FilamentTypes.Any())
            {
                return; // DB has been seeded
            }

            var FilamentTypes = new FilamentType[]
            {
                new FilamentType{Name = "PLA"},
                new FilamentType{Name = "PETG"},
                new FilamentType{Name = "ASA"},
            };
            foreach (var filamentType in FilamentTypes)
            {
                context.FilamentTypes.Add(filamentType);
            }

            context.SaveChanges();

        }
    }
}
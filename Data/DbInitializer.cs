using System;
using System.Linq;

namespace FilamentCalculator.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FilamentCalcContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Filaments.Any())
            {
                return; // DB has been seeded
            }
            
        }
    }
}
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
            {

                // Look for any students.
                if (context.FilamentTypes.Any())
                {
                    return; // DB has been seeded
                }

                var FilamentTypes = new FilamentType[]
                {
                    new FilamentType{Name = "PLA", WeightPerMM = 0.002f},
                    new FilamentType{Name = "PETG", WeightPerMM = 0.002f},
                    new FilamentType{Name = "ASA", WeightPerMM = 0.002f},
                    new FilamentType{Name = "ABS", WeightPerMM = 0.003f},
                };
                foreach (var filamentType in FilamentTypes)
                {
                    context.FilamentTypes.Add(filamentType);
                }

                context.SaveChanges();
            }
            {
                if (context.Manufacturers.Any())
                {
                    return; // DB has been seeded
                }

                var ManufactorList = new Manufacturer[]
                {
                    new Manufacturer {Name = "Material4Print", Url = "https://material4print.de"},
                    new Manufacturer {Name = "DasFilament"},
                    new Manufacturer {Name = "RedLine"},
                    new Manufacturer {Name = "Extrudr"},
                    new Manufacturer {Name = "Bavaria Filament"},
                };

                foreach (var item in ManufactorList)
                {
                    context.Manufacturers.Add(item);
                }

                context.SaveChanges();
            }

            {
                if (context.Settingses.Any())
                {
                    return; // DB has been seeded
                }
                
                var setting = new Settings
                {
                    Energiekosts = (decimal) 0.24,
                    MissprintChance = 10,
                    PrinterEnergyUsageW = 300,
                    PrinterDepricationKostsPerHour = 5
                    
                };

                context.Settingses.Add(setting);
                context.SaveChanges();
            }
            
            if(Environment.GetEnvironmentVariable("SeedDemoData")?.ToUpper() == "J")
            {
                var FilamentList = new Filament[]
                {
                    new Filament{Diameter = 1.75f, Color = "Black", ManufacturerId = 2, Price = 17.0f, FilamentTypeId = 1, SpoolWeight = 800f, PrintTempBed = "0 - 60 °C", PrintTempNozzle = "205 - 230 °C"}, 
                };

                foreach (var item in FilamentList)
                {
                    context.Filaments.Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}
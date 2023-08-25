using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FilamentCalculator.Data;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Models
{
    public class SettingsViewModel
    {
        public decimal Energiekosts { get; set; }
        
        [Display( Name="MissprintChance in %")]
        public int MissprintChance { get; set; }
        
        [Display( Name="Printerdepricationkosts per Hour")]
        public decimal PrinterDepricationKostsPerHour { get; set; }
        
        [Display( Name="Energyusage in W")]
        public decimal PrinterEnergyUsageW { get; set; }
        
        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());

        public SettingsViewModel(int id = 1)
        {
            var item = context.Settings.Any() 
                ? context.Settings.First(c=>c.SettingsId == id) : new Settings();

            if (item != null)
            {
                this.Energiekosts = item.Energiekosts;
                this.MissprintChance = item.MissprintChance;
                this.PrinterDepricationKostsPerHour = item.PrinterDepricationKostsPerHour;
            }
        }
        
        public SettingsViewModel()
        {
            var item = context.Settings.Any() 
                ? context.Settings.First() : new Settings();

            if (item != null)
            {
                this.Energiekosts = item.Energiekosts;
                this.MissprintChance = item.MissprintChance;
                this.PrinterDepricationKostsPerHour = item.PrinterDepricationKostsPerHour;
            }
        }
    }
}
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
        public int PrinterEnergyUsageW { get; set; }
        
        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());

        public SettingsViewModel(int id = 1)
        {
            var item = context.Settingses.Any() 
                ? context.Settingses.First(c=>c.SettingsId == id) : new Settings();

            if (item != null)
            {
                this.Energiekosts = item.Energiekosts;
                this.MissprintChance = item.MissprintChance;
                this.PrinterDepricationKostsPerHour = item.PrinterDepricationKostsPerHour;
                this.PrinterEnergyUsageW = item.PrinterEnergyUsageW;
            }
        }
        
        public SettingsViewModel()
        {
            var item = context.Settingses.Any() 
                ? context.Settingses.First() : new Settings();

            if (item != null)
            {
                this.Energiekosts = item.Energiekosts;
                this.MissprintChance = item.MissprintChance;
                this.PrinterDepricationKostsPerHour = item.PrinterDepricationKostsPerHour;
                this.PrinterEnergyUsageW = item.PrinterEnergyUsageW;
            }
        }
    }
}
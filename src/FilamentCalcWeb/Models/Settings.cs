using System.ComponentModel;
using System.ComponentModel.DataAnnotations;   

namespace FilamentCalculator.Models
{
    public class Settings
    {
        public int SettingsId { get; set; }
        
        //€ / kWh
        [DefaultValue(0.24)]
        public decimal Energiekosts { get; set; }
        
        [Range(1, 100)]
        [DefaultValue(10)]
        public int MissprintChance { get; set; }
        
        [DefaultValue(0.10)]
        public decimal PrinterDepricationKostsPerHour { get; set; }
        
        public decimal PrinterEnergyUsageW { get; set; }
        
        
    }
}
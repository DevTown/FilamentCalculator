using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace FilamentCalculator.Models
{
    public class Filament
    {
        public string pricePerG => (Price / SpoolWeight).ToString("0.00");

        public string Displayname => this.Manufacturer.Name +" - " + this.FilamentType.Name + " - " + this.Color + " - " + this.pricePerG + " EUR/G";

        public int FilamentId { get; set; }
        public FilamentType FilamentType { get; set; }
        public int FilamentTypeId { get; set; }
        
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public string Color { get; set; }
        public float Diameter { get; set; }

        [Display(Name="Price in EUR")]
        public float Price { get; set; }
        
        [Display( Name="Spool weight in g")]
        public float SpoolWeight { get; set; }

        [Display( Name="Nozzle printingtemp")]
        public string PrintTempNozzle { get; set; }
        
        [Display( Name="Heatbed printingtemp")]
        [DefaultValue("")]
        public string PrintTempBed { get; set; }
    }
}

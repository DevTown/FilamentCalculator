using System.Drawing;

namespace FilamentCalculator.Models
{
    public class Filament
    {
        public int FilamentId { get; set; }
        public FilamentType FilamentType { get; set; }
        
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public string Color { get; set; }
        public string PrintTempNozzle { get; set; }
        public string PrintTempBed { get; set; }
    }
}

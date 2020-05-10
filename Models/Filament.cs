using System.Drawing;

namespace FilamentCalculator.Models
{
    public class Filament
    {
        public int FilamentId { get; set; }
        public Type Type { get; set; }
        
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public Color Color { get; set; }
        public string PrintTempNozzle { get; set; }
        public string PrintTempBed { get; set; }
    }
}

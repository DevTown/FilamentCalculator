using System.Drawing;

namespace FilamentCalculator.Models
{
    public class Filament
    {
        public int FilamentID { get; set; }
        public Type Type { get; set; }
        
        public Color Color { get; set; }
        public string PrintTempNozzle { get; set; }
        public string PrintTempBed { get; set; }
    }
}

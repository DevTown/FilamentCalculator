using System.Collections.Generic;

namespace FilamentCalculator.Models
{
    public class CalculatorViewModel
    {
        public List<FilamentType> Filamenttypes { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
        public List<Filament> Filaments { get; set; }
    }
}
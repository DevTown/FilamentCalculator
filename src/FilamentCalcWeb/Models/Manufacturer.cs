using System.Collections.Generic;

namespace FilamentCalculator.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        
        public List<Filament> Filaments { get; set; }

        public string UrlClick
        {
            get
            {
                return "";
            }
        }
    }
}
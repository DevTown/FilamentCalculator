namespace FilamentCalculator.Models
{
    public class Filament
    {
        public string Displayname
        {
            get
            {
                return this.Manufacturer.Name + " - " + this.Color + " - ";
            }
        }

        public int FilamentId { get; set; }
        public FilamentType FilamentType { get; set; }
        public int FilamentTypeId { get; set; }
        
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public string Color { get; set; }
        public float Diameter { get; set; }

        public float Price { get; set; }
        public float SpoolWeight { get; set; }

        public string PrintTempNozzle { get; set; }
        public string PrintTempBed { get; set; }
    }
}

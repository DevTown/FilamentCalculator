using System.Linq;
using FilamentCalculator.Data;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Models
{
    public class ManufacturerViewModel
    {
        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());
        
        public Manufacturer Manufacturer { get; set; }

        public ManufacturerViewModel(int id)
        {
            Manufacturer = context.Manufacturers.FirstOrDefault(x => x.ManufacturerId == id);
        }

        public ManufacturerViewModel()
        {
            Manufacturer = new Manufacturer();
        }
    }
}
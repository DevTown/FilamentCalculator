using System.Collections.Generic;
using System.Linq;
using FilamentCalculator.Data;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Models
{
    public class CalculatorViewModel
    {
        public List<FilamentType> Filamenttypes { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
        public List<Filament> Filaments { get; set; }

        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());
        
        public CalculatorViewModel()
        {
            this.Filaments =  context.Filaments.ToList();
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Manufacturers = context.Manufacturers.ToList();
        }
    }
}
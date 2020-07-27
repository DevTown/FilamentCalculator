using System.Collections.Generic;
using System.Linq;
using FilamentCalculator.Data;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Models
{
    public class FilamentViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public Filament Filament { get; set; }

        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());
        
        public FilamentViewModel(int id)
        {
            this.Filament =  context.Filaments.FirstOrDefault(c => c.FilamentId == id);
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Manufacturers = context.Manufacturers.ToList();
        }

        public FilamentViewModel()
        {
            
        }
    }
}
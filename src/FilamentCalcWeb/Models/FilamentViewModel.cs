using System.Collections.Generic;
using System.Linq;
using FilamentCalculator.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Models
{
    public class FilamentViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public Filament Filament { get; set; }

        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());

        public List<SelectListItem> DiameterList = new List<SelectListItem>
        {
            new SelectListItem{Value = "1,75", Text= "1.75"},
            new SelectListItem{Value = "2,85", Text= "2.85"},
        };
        
        public FilamentViewModel(int? id)
        {
            this.Filament =  context.Filaments.FirstOrDefault(c => c.FilamentId == id);
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Manufacturers = context.Manufacturers.ToList();
        }

        public FilamentViewModel()
        {
            this.Filament = new Filament();
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Manufacturers = context.Manufacturers.ToList();
        }
    }
}
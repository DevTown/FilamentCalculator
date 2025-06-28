using System.Collections.Generic;
using System.Linq;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilamentCalculator.ViewModels
{
    public class FilamentViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public Filament Filament { get; set; }

        private FilamentCalcContext _context ;

        public List<SelectListItem> DiameterList = new()
        {
            new SelectListItem{Value = "1,75", Text= "1.75"},
            new SelectListItem{Value = "2,85", Text= "2.85"},
        };
        
        public FilamentViewModel(int? id, FilamentCalcContext context)
        {
            this._context = context;
            this.Filament =  context.Filaments.FirstOrDefault(c => c.FilamentId == id);
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Manufacturers = context.Manufacturers.ToList();
        }

        public FilamentViewModel(FilamentCalcContext context)
        {
            this._context = context;
            this.Filament = new Filament();
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Manufacturers = context.Manufacturers.ToList();
        }
        
        public FilamentViewModel()
        {
            //this._context = context;
            this.Filament = new Filament();
           // this.Filamenttypes = context.FilamentTypes.ToList();
            //this.Manufacturers = context.Manufacturers.ToList();
        }
    }
}
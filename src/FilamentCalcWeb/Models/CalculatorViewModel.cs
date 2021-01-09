using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using FilamentCalculator.Data;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Models
{
    public class CalculatorViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        //public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Filament> Filaments { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
        
        public Settings Settings { get; set; }
        
        [Display( Name="weight of printing object in g")]
        public decimal weight { get; set; }
        
        [Display( Name="lengthmm of used filament for printing object in mm")]
        public decimal lengthmm { get; set; }
        
        [Display( Name="printtime of printing object in min")]
        public decimal printtimeh { get; set; }
        
        
        
        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());
        
        public CalculatorViewModel()
        {
            this.Filaments =  context.Filaments.Include(nameof(Manufacturer)).ToList();
            this.Filamenttypes = context.FilamentTypes.ToList();
            //this.Manufacturers = context.Manufacturers.ToList();
            this.Settings = context.Settingses.FirstOrDefault();
        }
    }
}
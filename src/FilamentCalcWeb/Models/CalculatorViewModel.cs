using System.Collections.Generic;
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
        //public IEnumerable<Filament> Filaments { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
        
        public Settings Settings { get; set; }
        
        public decimal weight { get; set; }
        public decimal lengthmm { get; set; }
        public decimal printtimeh { get; set; }
        
        // Move to Filaments
        public decimal kostperroll { get; set; }
        public decimal rollweight { get; set; }
        
        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());
        
        public CalculatorViewModel()
        {
            //this.Filaments =  context.Filaments.ToList();
            this.Filamenttypes = context.FilamentTypes.ToList();
            //this.Manufacturers = context.Manufacturers.ToList();
            this.Settings = context.Settingses.FirstOrDefault();
        }
    }
}
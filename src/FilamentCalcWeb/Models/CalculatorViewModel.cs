using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;
using FilamentCalculator.Data;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Globalization;

namespace FilamentCalculator.Models
{
    public class CalculatorViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        public IEnumerable<Filament> Filaments { get; set; }
        
        [Required(ErrorMessage = "You have to select a Filament from the list.")]
        public int? SelectedFilament { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public Settings Settings { get; set; }
        
        [Display( Name="weight of printing object in g")]
        public decimal weight { get; set; }
        
        [Display( Name="lengthmm of used filament for printing object in mm")]
        public decimal lengthmm { get; set; }
        
        [Display( Name="printtime of printing object in min")]
        public decimal printtimeh { get; set; }
        
        [Display( Name="printing costs")]
        public decimal costs { get; private set; }

        private FilamentCalcContext context = new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>());
        public decimal energyCosts { get; private set; }
        public decimal filamentCosts { get; private set; }

        public CalculatorViewModel()
        {
            this.Filaments =  context.Filaments.Include(nameof(Manufacturer)).ToList();
            this.Filamenttypes = context.FilamentTypes.ToList();
            this.Settings = context.Settingses.FirstOrDefault();
        }

        public void Calculate()
        {
            var filcost = (this.weight  * 
                              (decimal) (this.Filaments.First(c=>c.FilamentId == this.SelectedFilament).Price 
                                         / this.Filaments.First(c=>c.FilamentId == this.SelectedFilament).SpoolWeight)
                );
            
            var energycosts = (printtimeh / 60) * 
                              this.Settings.PrinterEnergyUsageW * this.Settings.Energiekosts;

            this.filamentCosts = filcost;
            this.energyCosts = energycosts;
            
            this.costs = filcost + energycosts;
            
            var CalcErgText = $"FILAMENTKOSTEN: {filcost.ToString(CultureInfo.GetCultureInfo("DE"))}" +
                           $"ENERGIEKOSTEN: {energycosts.ToString()}";
        }
    }
}
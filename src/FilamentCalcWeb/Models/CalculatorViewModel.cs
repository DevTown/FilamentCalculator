using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using FilamentCalculator.Data;
using Microsoft.EntityFrameworkCore;
using System;


namespace FilamentCalculator.Models
{
    public class CalculatorViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        public IEnumerable<Filament> Filaments { get; set; }
        
        [Required(ErrorMessage = "You have to select a Filament from the list.")]
        public int? SelectedFilament { get; set; }
        public int? SelectedPrinter { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public IEnumerable<Printer> Printers { get; set; }
        
        public Settings Settings { get; set; }
        
        [Display( Name="weight of printing object in g")]
        public decimal weight { get; set; }
        
        [Display( Name="length of used filament for printing object in mm")]
        public decimal lengthmm { get; set; }
        
        [Display( Name="printtime of printing object in min")]
        public decimal printtimeh { get; set; }
        
        [Display( Name="printing costs")]
        public decimal costs { get; private set; }

        private FilamentCalcContext _context;
        public decimal energyCosts { get; private set; }
        public decimal filamentCosts { get; private set; }
        

        public CalculatorViewModel()
        {
        }

        public CalculatorViewModel(FilamentCalcContext context)
        {
            this._context = context;
            this.Filaments =  _context.Filaments.Include(nameof(Manufacturer)).ToList();
            this.Printers = _context.Printers.ToList();
            this.Filamenttypes = _context.FilamentTypes.ToList();
            this.Settings = _context.Settings.FirstOrDefault();
        }

        public void Calculate()
        {
            if (Filaments is null || Filaments.Any() == false)
            {
                this.Filaments =  _context.Filaments.Include(nameof(Manufacturer)).ToList();
                this.Filamenttypes = _context.FilamentTypes.ToList();
                this.Filamenttypes = _context.FilamentTypes.ToList();
                this.Settings = _context.Settings.FirstOrDefault();
            }
            
            var filcost = (this.weight  * 
                              (decimal) (this.Filaments.First(c=>c.FilamentId == this.SelectedFilament).Price 
                                         / this.Filaments.First(c=>c.FilamentId == this.SelectedFilament).SpoolWeight)
                );
            
            var energycosts = (((printtimeh / 60) * 
                              this.Printers.First(p=>p.PrinterId == this.SelectedPrinter).EnergyConsumptionW )/1000)
                              * this.Settings.Energiekosts;

            this.filamentCosts = filcost;
            this.energyCosts = energycosts;
            
            this.costs = filcost + energycosts;
            
            var CalcErgText = $"FILAMENTKOSTEN: {filcost.ToString(CultureInfo.GetCultureInfo("DE"))}" +
                           $"ENERGIEKOSTEN: {energycosts.ToString()}";
        }

        public void GetWeight()
        {
            if (this.weight == 0 && this.lengthmm > 0)
            {
                var filamentdiameter = this.Filaments.First(c => c.FilamentId == this.SelectedFilament).Diameter / 10;
                var i = ( filamentdiameter / 2 ) * ( filamentdiameter / 2 )  * 3.14 * (double) (this.lengthmm / 10) * 1.25;
                this.weight = Decimal.Round((decimal)i,3);
            }
        }
        /*
            d = 15 cm
            l = 425 cm
           Standard PLA* = 1,25 g/cm³

            G = ( d / 2 )2 * π * l * σ
            G = ( 15 cm / 2 )2 * 3.14 * 425 cm * 7.85 g / cm3
            G = 589564 g
            G = 589,564 kg 
            
            
    Standard PLA* = 1,25 g/cm³
    Metall PLA* = 2-4 g/cm³
    Holz PLA* = 1,15-1,25 g/cm³
    Kohlenstoff PLA* = 1,3 g/cm³
    Elektrisch leitfähiges PLA* = 1,15 bis 1,25 g/cm³
    Fluoreszierendes PLA* = 1,21 bis 1,43 g/cm³
    
    ABS -> Gering ( ~ 1,04 g/cm³)
    PETG -> Dichte (g/cm³) 	1,27
        */
    }
}
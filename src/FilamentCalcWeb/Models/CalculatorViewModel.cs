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

        [Display(Name = "Object-weight in g")]
        public decimal weight { get; set; }

        [Display(Name = "Length of used filament for printing object in mm")]
        public decimal lengthmm { get; set; }

        [Display(Name = "Print-time")]
        public decimal printtimemin { get; set; }

        [Display(Name = "in Minutes")]
        public bool isMinuit { get; set; } 
        
        [Display(Name = "Time for Manufactoringwork")]
        public decimal manufacurworktime { get; set; }
        
        
        private FilamentCalcContext _context;

        [Display(Name = "Printing-costs")] 
        public decimal costs
        {
            get
            {
                return this.energyCosts + this.filamentCosts + this.manufacturingCosts;
            }
        }

        [Display(Name = "Energy-costs min 0,5 EUR")]
        public decimal energyCosts { get; private set; }
        
        [Display(Name = "Filament-costs")]
        public decimal filamentCosts { get; private set; }
        
        [Display(Name = "Manufacturing-costs")]
        public decimal manufacturingCosts { get; private set; }


        public CalculatorViewModel()
        {
        }

        public CalculatorViewModel(FilamentCalcContext context)
        {
            this._context = context;
            this.Filaments = _context.Filaments.Include(nameof(Manufacturer)).ToList();
            this.Printers = _context.Printers.ToList();
            this.Filamenttypes = _context.FilamentTypes.ToList();
            this.Settings = _context.Settings.FirstOrDefault();
        }

        public void Calculate()
        {
            if (Filaments is null || Filaments.Any() == false)
            {
                this.Filaments = _context.Filaments.Include(nameof(Manufacturer)).ToList();
                this.Filamenttypes = _context.FilamentTypes.ToList();
                this.Filamenttypes = _context.FilamentTypes.ToList();
                this.Settings = _context.Settings.FirstOrDefault();
            }

            decimal missprintfactor = (((decimal)this.Settings.MissprintChance / 100) + 1);
            var filcost = (this.weight *
                           (decimal)(this.Filaments.First(c => c.FilamentId == this.SelectedFilament).Price
                                     / this.Filaments.First(c => c.FilamentId == this.SelectedFilament).SpoolWeight)
                ) * missprintfactor;
            
            var printtime = isMinuit?  printtimemin / 60 : printtimemin;
            
            var energycosts = ((printtime *
                                this.Printers.First(p => p.PrinterId == this.SelectedPrinter).EnergyConsumptionW) /
                               1000)
                              * this.Settings.Energiekosts;

            this.filamentCosts = filcost;
            
            // Energiekosts min value 0.5 Eur 
            this.energyCosts = (energycosts < (decimal) 0.5) ? (decimal) 0.5 : energycosts;

            this.manufacturingCosts = this.Settings.Hourlywage * (decimal)this.manufacurworktime;

        }
 


public void GetWeight()
        {
            if (this.weight != 0 || this.lengthmm <= 0) return;
            var filamentdiameter = this.Filaments.First(c => c.FilamentId == this.SelectedFilament).Diameter / 10;
            var i = ( filamentdiameter / 2 ) * ( filamentdiameter / 2 )  * 3.14 * (double) (this.lengthmm / 10) * 1.25;
            this.weight = Decimal.Round((decimal)i,3);
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.ViewModels
{
    public class CalculatorViewModel
    {
        public IEnumerable<FilamentType> Filamenttypes { get; set; }
        
        public IEnumerable<Shipment> Shipments { get; set; }
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
        
        [Display(Name = "Extended Materialcosts")]
        public decimal extendedmaterialcosts { get; set; }
        
        
        private FilamentCalcContext _context;

        [Display(Name = "Printing-costs")] 
        public decimal costs => this.energyCosts + this.filamentCosts + this.manufacturingCosts + this.printerCosts+ this.extendedmaterialcosts;
        
        [Display(Name = "Total costs ")]
        public decimal totalCosts => this.costs + this.revenu;
        
        [Display(Name = "Revenu ")]
        public decimal revenu { get; set; }

        [Display(Name = "Energy-costs min 0,5 EUR")]
        public decimal energyCosts { get; set; }
        
        [Display(Name = "Printer costs per h")]
        public decimal printerCosts { get; set; }
        
        [Display(Name = "Filament-costs")]
        public decimal filamentCosts { get; set; }
        
        [Display(Name = "Manufacturing-costs")]
        public decimal manufacturingCosts { get; set; }
        
        [Display(Name = "Manufacturing Title")]
        public string manufacturingTitle { get; set; }


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
            this.Shipments = _context.Shipments.ToList();
        }

        public void Calculate()
        {
            if (Filaments is null || Filaments.Any() == false)
            {
                this.Filaments = _context.Filaments.Include(nameof(Manufacturer)).ToList();
                this.Filamenttypes = _context.FilamentTypes.ToList();
                this.Settings = _context.Settings.FirstOrDefault();
                this.Shipments = _context.Shipments.ToList();
            }

            decimal missprintfactor = (((decimal)this.Settings.MissprintChance / 100) + 1);
            
            this.filamentCosts  = (this.weight *
                                    (decimal)(this.Filaments.First(c => c.FilamentId == this.SelectedFilament).Price
                                              / this.Filaments.First(c => c.FilamentId == this.SelectedFilament).SpoolWeight)
                ) * missprintfactor;
            
            var printtime = isMinuit?  printtimemin / 60 : printtimemin;
            
            var energycosts = ((printtime *
                                this.Printers.First(p => p.PrinterId == this.SelectedPrinter).EnergyConsumptionW) /
                               1000)
                              * this.Settings.Energiekosts;
            
            this.printerCosts =  this.Printers.First(p => p.PrinterId == this.SelectedPrinter).AmotationCostPerHour * printtime;
            
            // Energiekosts min value 0.5 Eur 
            this.energyCosts = (energycosts < (decimal) 0.5) ? (decimal) 0.5 : energycosts;

            this.manufacturingCosts = this.Settings.Hourlywage * this.manufacurworktime;
            
            this.revenu = this.costs * Settings.Revenuepercentage;

        }
 


public void GetWeight()
        {
            if (this.weight != 0 || this.lengthmm <= 0) return;
            var filamentdiameter = this.Filaments.First(c => c.FilamentId == this.SelectedFilament).Diameter / 10;
            var i = ( filamentdiameter / 2 ) * ( filamentdiameter / 2 )  * 3.14 * (double) (this.lengthmm / 10) * 1.25;
            this.weight = Decimal.Round((decimal)i,3);
        }
    }
}
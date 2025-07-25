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
        
        public int? SelectedShipment { get; set; }

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
        public decimal printtime { get; set; }

        [Display(Name = "in Minutes")]
        public bool isMinuit { get; set; } 
        
        [Display(Name = "Time for Manufactoringwork (in min)")]
        public decimal manufacurworktime { get; set; }
        
        [Display(Name = "Extended Materialcosts")]
        public decimal extendedmaterialcosts { get; set; }
        
        
        private FilamentCalcContext _context;
        
        [Display(Name = "Shipping-costs")] 
        public decimal shippingcosts { get; private set; }

        [Display(Name = "Printing-costs")] 
        public decimal costs => this.energyCosts + this.filamentCosts + this.manufacturingCosts + this.printerCosts+ this.extendedmaterialcosts;
        
        [Display(Name = "Total costs ")]
        public decimal totalCosts => this.costs + this.revenu + this.shippingcosts;
        
        [Display(Name = "Revenu ")]
        public decimal revenu { get; set; }

        [Display(Name = "Energy-costs")]
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
            
            var printtime = isMinuit?  this.printtime / 60 : this.printtime;
            
            var energycosts = ((printtime *
                                this.Printers.First(p => p.PrinterId == this.SelectedPrinter).EnergyConsumptionW) /
                               1000)
                              * this.Settings.Energiekosts;
            
            this.printerCosts =  this.Printers.First(p => p.PrinterId == this.SelectedPrinter).AmotationCostPerHour * printtime;
            
            this.energyCosts =  energycosts;

            this.manufacturingCosts = decimal.Round((this.Settings.Hourlywage / 60) * this.manufacurworktime, 2);
            
            this.revenu = this.costs * Settings.Revenuepercentage;
            
            if (SelectedShipment > 0)
            {
                var shipment = _context.Shipments.Find(SelectedShipment);
                if (shipment != null)
                {
                    shippingcosts = shipment.TotalPrice;
                }
                else
                {
                    shippingcosts = 0;
                }
            }


        }
    }
    
}
        
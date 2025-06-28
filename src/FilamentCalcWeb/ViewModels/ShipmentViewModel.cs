using System.Collections.Generic;
using System.Linq;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilamentCalculator.ViewModels;

public class ShipmentViewModel
{
    
    public Shipment Shipment { get; set; }
    private FilamentCalcContext _context ;
    
   

    public ShipmentViewModel()
    {
        this.Shipment = new Shipment();
    }

    public ShipmentViewModel(FilamentCalcContext context)
    {
        this._context = context;
        this.Shipment = new Shipment();
    }

    public ShipmentViewModel(int? id, FilamentCalcContext context)
    {
        this._context = context;
        this.Shipment = this._context.Shipments.FirstOrDefault(p => p.ShipmentID == id);
    }
}
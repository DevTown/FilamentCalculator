using System.Collections.Generic;
using System.Linq;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilamentCalculator.ViewModels;

public class PrinterViewModel
{
    public Printer Printer { get; set; }
    private FilamentCalcContext _context ;
    
    public List<SelectListItem> DiameterList = new()
    {
        new SelectListItem{Value = "1,75", Text= "1.75"},
        new SelectListItem{Value = "2,85", Text= "2.85"},
    };

    public PrinterViewModel()
    {
        this.Printer = new Printer();
    }

    public PrinterViewModel(FilamentCalcContext context)
    {
        this._context = context;
        this.Printer = new Printer();
    }

    public PrinterViewModel(int? id, FilamentCalcContext context)
    {
        this._context = context;
        this.Printer = this._context.Printers.FirstOrDefault(p => p.PrinterId == id);
    }
    
}
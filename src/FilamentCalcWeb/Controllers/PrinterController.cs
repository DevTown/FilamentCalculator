using System.Linq;
using System.Threading.Tasks;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Controllers;

public class PrinterController: Controller
{
    
    private readonly FilamentCalcContext _db = new FilamentCalcContext();
    
    // GET
    public IActionResult Index()
    {
        return View(_db.Printers.ToList());
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
            
        var printer =  await _db.Printers
            .FirstOrDefaultAsync(m => m.PrinterId == id);
        if (printer == null)
        {
            return NotFound();
        }
        
        _db.Printers.Remove(printer);
            
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Edit(int? id)
    {
        PrinterViewModel item;
        item = id == null ? new PrinterViewModel(_db) : new PrinterViewModel(id.Value, _db);
            
        return View(item);
    }
    
    [HttpPost]
    public IActionResult Edit(PrinterViewModel model)
    {
        if (model.Printer.PrinterId == 0)
        {
            _db.Printers.Add(model.Printer);
        }
        else
        {
            _db.Printers.Update(model.Printer);    
        }
            
        _db.SaveChanges();
            
        return RedirectToAction(nameof(Index));
    }
}
using System.Linq;
using System.Threading.Tasks;
using FilamentCalculator.Data;
using FilamentCalculator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Controllers;

public class ShipmentController:Controller
{
    private readonly FilamentCalcContext _db = new();
        
    public IActionResult Index()
    {
        return View(_db.Shipments.ToList());
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
            
        var shipment =  await _db.Shipments
            .FirstOrDefaultAsync(m => m.ShipmentID == id);
        if (shipment == null)
        {
            return NotFound();
        }
        
        _db.Shipments.Remove(shipment);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Edit(int? id)
    {
        ShipmentViewModel item;
        item = id == null ? new ShipmentViewModel(_db) : new ShipmentViewModel(id.Value, _db);
            
        return View(item);
    }
    
    [HttpPost]
    public IActionResult Edit(ShipmentViewModel model)
    {
        if (model.Shipment.ShipmentID == 0)
        {
            _db.Shipments.Add(model.Shipment);
        }
        else
        {
            _db.Shipments.Update(model.Shipment);    
        }
            
        _db.SaveChanges();
            
        return RedirectToAction(nameof(Index));
    }
}
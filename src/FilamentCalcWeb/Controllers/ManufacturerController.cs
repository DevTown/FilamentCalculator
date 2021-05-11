using System.Linq;
using System.Threading.Tasks;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Controllers
{
    public class ManufacturerController :Controller
    {
        private readonly FilamentCalcContext _db = new FilamentCalcContext();
        
        public IActionResult Index()
        {
            return View(_db.Manufacturers.ToList());
        }
        
        public IActionResult Edit(int? id)
        {
            
           var item = id == null ? new ManufacturerViewModel() : new ManufacturerViewModel(id.Value);
            
            return View(item);
        }

        //Check if manufacturer is in use.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var manufacturer =  await _db.Manufacturers
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            _db.Manufacturers.Remove(manufacturer);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
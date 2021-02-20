using System;
using System.Linq;
using System.Threading.Tasks;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilamentCalculator.Controllers
{
    public class FilamentController : Controller
    {
        private readonly FilamentCalcContext _db = new FilamentCalcContext();
        
        // GET
        public IActionResult Index()
        {
            return View(_db.Filaments.Include(nameof(Manufacturer)).ToList());
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var filament =  await _db.Filaments
                .FirstOrDefaultAsync(m => m.FilamentId == id);
            if (filament == null)
            {
                return NotFound();
            }

            _db.Filaments.Remove(filament);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            
            var item = new FilamentViewModel(id.Value);
            
            return View(item);
        }
        
        [HttpPost]
        public IActionResult Edit(FilamentViewModel model)
        {
            
            _db.Filaments.Update(model.Filament);
            _db.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            var item = new FilamentViewModel();
            return View(item);
        }
        
        [HttpPost]
        public IActionResult Add(FilamentViewModel model)
        {
            _db.Filaments.Add(model.Filament);
            _db.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
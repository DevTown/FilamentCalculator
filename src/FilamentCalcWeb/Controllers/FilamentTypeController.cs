using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using FilamentCalculator.ViewModels;
using Microsoft.EntityFrameworkCore;

public class FilamentTypeController : Controller
{
    private readonly FilamentCalcContext _context;

    public FilamentTypeController(FilamentCalcContext context)
    {
        _context = context;
    }

    // GET: FilamentType
    public async Task<IActionResult> Index()
    {
        var types = await _context.FilamentTypes
            .Select(ft => new FilamentTypeViewModel
            {
                FilamentTypeId = ft.FilamentTypeId,
                Name = ft.Name,
                IsInUse = _context.Filaments.Any(f => f.FilamentTypeId == ft.FilamentTypeId)
            })
            .ToListAsync();

        return View(types);
    }

    // GET: FilamentType/Create
    public IActionResult Create()
    {
        return View(new FilamentType());
    }

    // POST: FilamentType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] FilamentType filamentType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(filamentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(filamentType);
    }

    // GET: FilamentType/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var filamentType = await _context.FilamentTypes
            .Select(ft => new FilamentTypeViewModel
            {
                FilamentTypeId = ft.FilamentTypeId,
                Name = ft.Name,
                IsInUse = _context.Filaments.Any(f => f.FilamentTypeId == ft.FilamentTypeId)
            })
            .FirstOrDefaultAsync(ft => ft.FilamentTypeId == id);

        if (filamentType == null)
            return NotFound();

        if (filamentType.IsInUse)
        {
            TempData["ErrorMessage"] = "Dieser Filament-Typ kann nicht gelöscht werden, da er noch von Filamenten verwendet wird.";
            return RedirectToAction(nameof(Index));
        }

        return View(filamentType);
    }

    // POST: FilamentType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var isInUse = await _context.Filaments.AnyAsync(f => f.FilamentTypeId == id);
        if (isInUse)
        {
            TempData["ErrorMessage"] = "Dieser Filament-Typ kann nicht gelöscht werden, da er noch von Filamenten verwendet wird.";
            return RedirectToAction(nameof(Index));
        }

        var filamentType = await _context.FilamentTypes.FindAsync(id);
        if (filamentType != null)
        {
            _context.FilamentTypes.Remove(filamentType);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Andere Action-Methoden bleiben unverändert...
}
using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FilamentCalculator.ViewModels;

namespace FilamentCalculator.Controllers
{
    public class SettingsController: Controller
    {
        private readonly FilamentCalcContext _db = new(new DbContextOptions<FilamentCalcContext>());
        
        public IActionResult Index()
        {
            return View(new SettingsViewModel());
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel viewModel)
        {
            
            if (_db.Settings.Any())
            {
                var settingItem = _db.Settings.FirstOrDefault();
                
                settingItem.Energiekosts = viewModel.Energiekosts;
                settingItem.MissprintChance = viewModel.MissprintChance;
                settingItem.PrinterDepricationKostsPerHour = viewModel.PrinterDepricationKostsPerHour;
                settingItem.Hourlywage = viewModel.Hourlywage;
                settingItem.Revenuepercentage = viewModel.Revenuepercentage;
            }
            else
            {
                var item = new Settings
                {
                    Energiekosts = viewModel.Energiekosts,
                    MissprintChance = viewModel.MissprintChance,
                    PrinterDepricationKostsPerHour = viewModel.PrinterDepricationKostsPerHour,
                    Hourlywage = viewModel.Hourlywage,
                    Revenuepercentage = viewModel.Revenuepercentage
                };
                _db.Settings.Add(item);
            }

            _db.SaveChanges();

            return View(new SettingsViewModel());
        }
    }
}
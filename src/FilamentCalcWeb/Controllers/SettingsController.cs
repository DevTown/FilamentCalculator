using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilamentCalculator.Controllers
{
    public class SettingsController: Controller
    {
        private readonly FilamentCalcContext _db = new();
        
        public IActionResult Index()
        {
            return View(new SettingsViewModel());
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel viewModel)
        {
            var settingItem = _db.Settings.First();
            if (settingItem != null)
            {
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
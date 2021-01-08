using FilamentCalculator.Data;
using FilamentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilamentCalculator.Controllers
{
    public class SettingsController: Controller
    {
        private readonly FilamentCalcContext _db = new FilamentCalcContext();
        
        public IActionResult Index()
        {
            return View(new SettingsViewModel());
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel viewModel)
        {
            var settingItem = _db.Settingses.First();
            if (settingItem != null)
            {
                settingItem.Energiekosts = viewModel.Energiekosts;
                settingItem.MissprintChance = viewModel.MissprintChance;
                settingItem.PrinterEnergyUsageW = viewModel.PrinterEnergyUsageW;
                settingItem.PrinterDepricationKostsPerHour = viewModel.PrinterDepricationKostsPerHour;
            }
            else
            {
                var item = new Settings();
                item.Energiekosts = viewModel.Energiekosts;
                item.MissprintChance = viewModel.MissprintChance;
                item.PrinterEnergyUsageW = viewModel.PrinterEnergyUsageW;
                item.PrinterDepricationKostsPerHour = viewModel.PrinterDepricationKostsPerHour;
                _db.Settingses.Add(item);
            }

            _db.SaveChanges();

            return View(new SettingsViewModel());
        }
    }
}
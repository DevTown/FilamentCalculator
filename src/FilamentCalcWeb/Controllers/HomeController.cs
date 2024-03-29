﻿using System.Diagnostics;
using FilamentCalculator.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilamentCalculator.Models;
using Microsoft.EntityFrameworkCore;


namespace FilamentCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(CalculatorViewModel viewmodel)
        {
            var calculatorViewModel = new CalculatorViewModel(new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>()));

            if (viewmodel.SelectedFilament>0)
            {
                calculatorViewModel.SelectedFilament = viewmodel.SelectedFilament;
                calculatorViewModel.weight = viewmodel.weight;
                calculatorViewModel.lengthmm = viewmodel.lengthmm;
                calculatorViewModel.Calculate();
            }
            
            return View(calculatorViewModel); 
        }

        public IActionResult Index()
        {
            var calculatorViewModel = new CalculatorViewModel(new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>()));
            
            return View(calculatorViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

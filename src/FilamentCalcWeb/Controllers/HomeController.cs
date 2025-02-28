using System;
using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;
using FilamentCalculator.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilamentCalculator.Models;
using FilamentCalculator.ViewModels;
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

            if (viewmodel.SelectedFilament>0 && viewmodel.SelectedPrinter >0)
            {
                calculatorViewModel.SelectedFilament = viewmodel.SelectedFilament;
                calculatorViewModel.SelectedPrinter = viewmodel.SelectedPrinter;
                calculatorViewModel.weight = viewmodel.weight;
                calculatorViewModel.lengthmm = viewmodel.lengthmm;
                calculatorViewModel.printtimemin = viewmodel.printtimemin;
                calculatorViewModel.isMinuit = viewmodel.isMinuit;
                calculatorViewModel.manufacurworktime = viewmodel.manufacurworktime;
                calculatorViewModel.manufacturingTitle = viewmodel.manufacturingTitle;
                calculatorViewModel.extendedmaterialcosts = viewmodel.extendedmaterialcosts;
                calculatorViewModel.Calculate();
            }
           
            
            return View(calculatorViewModel); 
        }

        public IActionResult Index()
        {
            var calculatorViewModel = new CalculatorViewModel(new FilamentCalcContext(new DbContextOptions<FilamentCalcContext>()));
            
            return View(calculatorViewModel);
        }
        
        [HttpPost]
        public IActionResult ExportToExcel(CalculatorViewModel viewmodel)
        {
            var title = viewmodel.manufacturingTitle ?? "";
            var name = title.Trim() == "" ? "3D" : title.Trim(); 
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = name + "-PrintJob.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                        workbook.Worksheets.Add(name+ "-Costs");
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "Item";
                    worksheet.Cell(1, 3).Value = "Price";
                    
                        worksheet.Cell( 2, 1).Value =
                           1;
                        worksheet.Cell(2, 2).Value =
                            "";
                        worksheet.Cell( 2, 3).Value =
                            viewmodel.filamentCosts;
                        //worksheet.Cell(2, 3).Style.Fill.BackgroundColor = XLColor.Blue;
                    
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch(Exception ex)
            {
                return Error();
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

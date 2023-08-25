namespace FilamentCalculator.Models;

public class Printer
{
    public int PrinterId { get; set; }

    public string Name { get; set; }

    public string ManufacturerName { get; set; }
    
    public float Price { get; set; }
    
    public float FilamentDiameter { get; set; }
    public decimal EnergyConsumptionW { get; set; }
    
}
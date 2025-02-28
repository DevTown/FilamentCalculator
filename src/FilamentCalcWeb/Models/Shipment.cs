using System.ComponentModel.DataAnnotations;

namespace FilamentCalculator.Models;

public class Shipment
{
    public int ShipmentID { get; set; }
    
    public string Name { get; set; }
    
    public string ShipmentOrg { get; set; }
    
    public decimal Packagingprice { get; set; }
    
    public decimal FillerPrice { get; set; }
    
    public decimal LablePrice { get; set; }
    
    public decimal AddonItemPrice { get; set; }
    
    public decimal ShippingPrice { get; set; }
    
    [Display(Name = "Total shipment price")]
    public decimal TotalPrice => Packagingprice + FillerPrice + LablePrice + AddonItemPrice + ShippingPrice;
}
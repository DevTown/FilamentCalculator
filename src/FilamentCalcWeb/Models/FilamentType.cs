namespace FilamentCalculator.Models
{
    public class FilamentType
    {
        public int FilamentTypeId { get; set; }
        public string Name { get; set; }
        public float WeightPerMM { get; set; }
        
        public string Usage { get; set; }
    }
}
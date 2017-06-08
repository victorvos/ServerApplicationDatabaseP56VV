namespace ServerApplication.Models
{
    public class ProductDto
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public int AvailableAmount { get; set; }
        public int Price { get; set; }
    }
}
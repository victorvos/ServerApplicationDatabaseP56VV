namespace ServerApplication.Database
{
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
    }
}
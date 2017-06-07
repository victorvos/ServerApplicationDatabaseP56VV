namespace ServerApplication.Database
{
    using System.ComponentModel.DataAnnotations;
    public class Order
    {
        [Key]
        public long Id { get; set; }

        public User User { get; set; }

        public int Amount { get; set; }

        public Product Product { get; set; }

        public int TotalPrice { get; set; }
    }
}
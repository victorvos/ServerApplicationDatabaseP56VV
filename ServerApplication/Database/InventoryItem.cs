using System.ComponentModel.DataAnnotations;

namespace ServerApplication.Database
{
    public class InventoryItem
    {
        [Key]
        public long Id { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }
    }
}
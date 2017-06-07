namespace ServerApplication.Database
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Store
    {
        [Key]
        public long Id {get; set;}

        public string Name { get; set; }

        public virtual List<InventoryItem> InventoryItems { get; set; }
    }
}
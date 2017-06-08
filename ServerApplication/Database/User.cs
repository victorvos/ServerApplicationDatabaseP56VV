namespace ServerApplication.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Key]
        public long Id { get; set; }

        [Index("UserNameIndex", IsUnique = true)]
        [MaxLength(25)]
        public string UserName { get; set; }

        [MaxLength(25)]
        public string Password { get; set; }

        public int Balance { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
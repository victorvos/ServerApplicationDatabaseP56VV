namespace ServerApplication.Migrations
{
    using ServerApplication.Database;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            context
                .Users
                .AddOrUpdate(p => p.UserName,
                new User
                {
                    UserName = "Victor",
                    Password = "Vos"
                });

            context
            .Stores
            .AddOrUpdate(p => p.Name,
            new Store
            {
                Name = "Standard",
                InventoryItems = new System.Collections.Generic.List<InventoryItem>
                {
                    new InventoryItem
                    {
                        Amount = 10,
                        Product = new Product
                        {
                            Name = "Apple",
                            Price = 100
                        },
                    },
                    new InventoryItem
                    {
                        Amount = 100,
                        Product = new Product
                        {
                            Name = "Pear",
                            Price = 10
                        },
                    }
                }
            });
        }
    }
}

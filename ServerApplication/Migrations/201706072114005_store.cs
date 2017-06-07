namespace ServerApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class store : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Product_Id = c.Long(),
                        Store_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Products", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Amount", c => c.Int(nullable: false));
            DropForeignKey("dbo.InventoryItems", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.InventoryItems", "Product_Id", "dbo.Products");
            DropIndex("dbo.InventoryItems", new[] { "Store_Id" });
            DropIndex("dbo.InventoryItems", new[] { "Product_Id" });
            DropTable("dbo.Stores");
            DropTable("dbo.InventoryItems");
        }
    }
}

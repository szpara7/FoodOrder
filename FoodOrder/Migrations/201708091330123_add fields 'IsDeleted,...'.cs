namespace FoodOrder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldsIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderLine", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Order", "IsCanceled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customer", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Review", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Review", "IsEdited", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employee", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Price", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Price", "IsDeleted");
            DropColumn("dbo.Employee", "IsDeleted");
            DropColumn("dbo.Review", "IsEdited");
            DropColumn("dbo.Review", "IsDeleted");
            DropColumn("dbo.Customer", "IsDeleted");
            DropColumn("dbo.Order", "IsCanceled");
            DropColumn("dbo.OrderLine", "IsDeleted");
            DropColumn("dbo.Product", "IsDeleted");
            DropColumn("dbo.Category", "isDeleted");
        }
    }
}

namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNavPropOrderEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ClientOrder_Id", "dbo.Clients");
            DropForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "ClientOrder_Id" });
            DropIndex("dbo.Orders", new[] { "OrderedBook_BookId" });
            AddColumn("dbo.Orders", "OrderBook_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ClientOrder_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "ClientOrder_Id", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "OrderedBook_BookId", c => c.Int());
            CreateIndex("dbo.Orders", "ClientOrder_Id1");
            CreateIndex("dbo.Orders", "OrderedBook_BookId");
            AddForeignKey("dbo.Orders", "ClientOrder_Id1", "dbo.Clients", "Id");
            AddForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books", "BookId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books");
            DropForeignKey("dbo.Orders", "ClientOrder_Id1", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "OrderedBook_BookId" });
            DropIndex("dbo.Orders", new[] { "ClientOrder_Id1" });
            AlterColumn("dbo.Orders", "OrderedBook_BookId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ClientOrder_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Orders", "ClientOrder_Id1");
            DropColumn("dbo.Orders", "OrderBook_Id");
            CreateIndex("dbo.Orders", "OrderedBook_BookId");
            CreateIndex("dbo.Orders", "ClientOrder_Id");
            AddForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ClientOrder_Id", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}

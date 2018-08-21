namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNavPropOrderEntity2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "OrderedBook_BookId" });
            RenameColumn(table: "dbo.Orders", name: "ClientOrder_Id1", newName: "Client_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_ClientOrder_Id1", newName: "IX_Client_Id");
            DropColumn("dbo.Orders", "OrderedBook_BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderedBook_BookId", c => c.Int());
            RenameIndex(table: "dbo.Orders", name: "IX_Client_Id", newName: "IX_ClientOrder_Id1");
            RenameColumn(table: "dbo.Orders", name: "Client_Id", newName: "ClientOrder_Id1");
            CreateIndex("dbo.Orders", "OrderedBook_BookId");
            AddForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books", "BookId");
        }
    }
}

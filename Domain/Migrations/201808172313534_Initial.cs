namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Year = c.DateTime(nullable: false),
                        Ganer = c.String(),
                        Author = c.String(),
                        Client_ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientID)
                .Index(t => t.Client_ClientID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        IsGiven = c.Boolean(nullable: false),
                        ClientOrder_ClientID = c.Int(),
                        OrderedBook_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Clients", t => t.ClientOrder_ClientID)
                .ForeignKey("dbo.Books", t => t.OrderedBook_BookId)
                .Index(t => t.ClientOrder_ClientID)
                .Index(t => t.OrderedBook_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Client_ClientID", "dbo.Clients");
            DropForeignKey("dbo.Orders", "OrderedBook_BookId", "dbo.Books");
            DropForeignKey("dbo.Orders", "ClientOrder_ClientID", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "OrderedBook_BookId" });
            DropIndex("dbo.Orders", new[] { "ClientOrder_ClientID" });
            DropIndex("dbo.Books", new[] { "Client_ClientID" });
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
            DropTable("dbo.Books");
        }
    }
}

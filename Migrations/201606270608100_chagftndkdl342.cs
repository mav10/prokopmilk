namespace WebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagftndkdl342 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Ca_ID = c.Int(nullable: false, identity: true),
                        Category_Name = c.String(nullable: false),
                        Description = c.String(),
                        Pict = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.Ca_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(maxLength: 100),
                        StartData = c.DateTime(nullable: false),
                        Phone = c.String(),
                        City = c.String(),
                        Adres = c.String(),
                        Photo = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        StatusOrder = c.Boolean(nullable: false),
                        TotalSum = c.Double(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        SolveOrder = c.Boolean(nullable: false),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Tovar_ID = c.Int(nullable: false),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .ForeignKey("dbo.Tovars", t => t.Tovar_ID, cascadeDelete: true)
                .Index(t => t.Tovar_ID)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.EmailSettings",
                c => new
                    {
                        MailFromAddress = c.String(nullable: false, maxLength: 128),
                        MailToAddress = c.String(),
                        UseSsl = c.Boolean(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        ServerName = c.String(),
                        ServerPort = c.Int(nullable: false),
                        WriteAsFile = c.Boolean(nullable: false),
                        FileLocation = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MailFromAddress);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Description = c.String(),
                        Text = c.String(),
                        Picture = c.Binary(storeType: "image"),
                        DataPub = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tovars",
                c => new
                    {
                        Tovar_ID = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Volume = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                        HOT = c.String(),
                        Pict = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.Tovar_ID);
            
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        adres = c.String(),
                        city = c.String(),
                        name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.OrderItems", "Tovar_ID", "dbo.Tovars");
            DropForeignKey("dbo.OrderItems", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderItems", new[] { "Order_OrderId" });
            DropIndex("dbo.OrderItems", new[] { "Tovar_ID" });
            DropIndex("dbo.Orders", new[] { "Customer_CustomerId" });
            DropTable("dbo.Tovars");
            DropTable("dbo.News");
            DropTable("dbo.EmailSettings");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}

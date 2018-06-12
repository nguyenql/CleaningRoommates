namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Controls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sweep = c.Boolean(nullable: false),
                        Wash = c.Boolean(nullable: false),
                        Trash = c.Boolean(nullable: false),
                        When = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                        Checker_Id = c.Int(),
                        Executer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.Checker_Id)
                .ForeignKey("dbo.Users", t => t.Executer_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Checker_Id)
                .Index(t => t.Executer_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Key = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Swaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeadLine = c.Boolean(nullable: false),
                        Sick = c.Boolean(nullable: false),
                        NotInTheTown = c.Boolean(nullable: false),
                        Reason = c.String(),
                        When = c.DateTime(nullable: false),
                        OnWhat = c.DateTime(nullable: false),
                        Agree_Id = c.Int(),
                        From_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Agree_Id)
                .ForeignKey("dbo.Users", t => t.From_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Agree_Id)
                .Index(t => t.From_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Controls", "Executer_Id", "dbo.Users");
            DropForeignKey("dbo.Controls", "Checker_Id", "dbo.Users");
            DropForeignKey("dbo.Swaps", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Swaps", "From_Id", "dbo.Users");
            DropForeignKey("dbo.Swaps", "Agree_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Controls", "User_Id", "dbo.Users");
            DropIndex("dbo.Swaps", new[] { "User_Id" });
            DropIndex("dbo.Swaps", new[] { "From_Id" });
            DropIndex("dbo.Swaps", new[] { "Agree_Id" });
            DropIndex("dbo.Users", new[] { "Room_Id" });
            DropIndex("dbo.Controls", new[] { "Executer_Id" });
            DropIndex("dbo.Controls", new[] { "Checker_Id" });
            DropIndex("dbo.Controls", new[] { "User_Id" });
            DropTable("dbo.Swaps");
            DropTable("dbo.Rooms");
            DropTable("dbo.Users");
            DropTable("dbo.Controls");
        }
    }
}

namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Controls", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Swaps", "User_Id", "dbo.Users");
            DropIndex("dbo.Controls", new[] { "User_Id" });
            DropIndex("dbo.Swaps", new[] { "User_Id" });
            DropColumn("dbo.Controls", "User_Id");
            DropColumn("dbo.Swaps", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Swaps", "User_Id", c => c.Int());
            AddColumn("dbo.Controls", "User_Id", c => c.Int());
            CreateIndex("dbo.Swaps", "User_Id");
            CreateIndex("dbo.Controls", "User_Id");
            AddForeignKey("dbo.Swaps", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Controls", "User_Id", "dbo.Users", "Id");
        }
    }
}

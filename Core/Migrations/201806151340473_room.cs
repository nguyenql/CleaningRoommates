namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class room : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Swaps", "Room_Id", c => c.Int());
            CreateIndex("dbo.Swaps", "Room_Id");
            AddForeignKey("dbo.Swaps", "Room_Id", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Swaps", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Swaps", new[] { "Room_Id" });
            DropColumn("dbo.Swaps", "Room_Id");
        }
    }
}

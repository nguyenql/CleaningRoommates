namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Check : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Controls", "When");
            DropColumn("dbo.Swaps", "When");
            DropColumn("dbo.Swaps", "OnWhat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Swaps", "OnWhat", c => c.DateTime(nullable: false));
            AddColumn("dbo.Swaps", "When", c => c.DateTime(nullable: false));
            AddColumn("dbo.Controls", "When", c => c.DateTime(nullable: false));
        }
    }
}

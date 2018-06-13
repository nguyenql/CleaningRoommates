namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateInInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Controls", "WhenDone", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "WhenChecked", c => c.Int(nullable: false));
            AddColumn("dbo.Swaps", "When", c => c.Int(nullable: false));
            AddColumn("dbo.Swaps", "OnWhat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Swaps", "OnWhat");
            DropColumn("dbo.Swaps", "When");
            DropColumn("dbo.Controls", "WhenChecked");
            DropColumn("dbo.Controls", "WhenDone");
        }
    }
}

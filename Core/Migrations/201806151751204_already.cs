namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class already : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submits", "AlreadyChecked", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submits", "AlreadyChecked");
        }
    }
}

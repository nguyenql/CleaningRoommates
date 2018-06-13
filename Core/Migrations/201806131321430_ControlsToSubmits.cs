namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlsToSubmits : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Controls", newName: "Submits");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Submits", newName: "Controls");
        }
    }
}

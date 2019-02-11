namespace TaskManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Group", "GroupName", c => c.String(nullable: false));
            DropColumn("dbo.Group", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Group", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Group", "GroupName");
        }
    }
}

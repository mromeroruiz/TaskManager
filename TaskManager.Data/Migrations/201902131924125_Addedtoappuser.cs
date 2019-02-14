namespace TaskManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtoappuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "GroupID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "GroupID");
        }
    }
}

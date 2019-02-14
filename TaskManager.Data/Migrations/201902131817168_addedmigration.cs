namespace TaskManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Group", "OwnerID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Group", "OwnerID");
        }
    }
}

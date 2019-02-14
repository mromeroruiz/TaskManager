namespace TaskManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDo", "IsDone", c => c.Boolean(nullable: false));
            DropColumn("dbo.ToDo", "Checkbox");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDo", "Checkbox", c => c.Boolean(nullable: false));
            DropColumn("dbo.ToDo", "IsDone");
        }
    }
}

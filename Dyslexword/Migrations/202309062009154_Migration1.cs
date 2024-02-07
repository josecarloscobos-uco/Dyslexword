namespace Dyslexword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LongReadings", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LongReadings", "Title", c => c.String(nullable: false, maxLength: 60));
        }
    }
}

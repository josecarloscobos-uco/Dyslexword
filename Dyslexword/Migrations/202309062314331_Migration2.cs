namespace Dyslexword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LongReadings", "Text", c => c.String(nullable: false, maxLength: 510));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LongReadings", "Text", c => c.String(nullable: false, maxLength: 255));
        }
    }
}

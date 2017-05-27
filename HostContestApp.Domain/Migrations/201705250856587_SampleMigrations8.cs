namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "ClosingDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Forms", "СlosingDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "СlosingDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Forms", "ClosingDate");
        }
    }
}

namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "Description", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "Description", c => c.String(nullable: false, maxLength: 20));
        }
    }
}

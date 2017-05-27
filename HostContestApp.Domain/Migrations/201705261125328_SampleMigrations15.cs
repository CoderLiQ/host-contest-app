namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "Date", c => c.DateTime());
        }
    }
}
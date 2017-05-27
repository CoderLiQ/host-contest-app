namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "ResponsiblePersonName", c => c.String());
            AlterColumn("dbo.Forms", "Description", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "Description", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Forms", "ResponsiblePersonName", c => c.String(nullable: false));
        }
    }
}

namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "ResponsiblePersonName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "ResponsiblePersonName", c => c.String());
        }
    }
}

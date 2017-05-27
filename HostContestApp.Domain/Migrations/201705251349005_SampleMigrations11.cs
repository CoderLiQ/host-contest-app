namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "AttachmentName", c => c.String());
            DropColumn("dbo.Forms", "AttachmentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "AttachmentType", c => c.String());
            DropColumn("dbo.Forms", "AttachmentName");
        }
    }
}

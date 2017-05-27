namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "Attachment", c => c.Binary());
            AddColumn("dbo.Forms", "AttachmentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "AttachmentType");
            DropColumn("dbo.Forms", "Attachment");
        }
    }
}

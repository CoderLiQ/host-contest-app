namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachements", "Form_FormId", "dbo.Forms");
            DropIndex("dbo.Attachements", new[] { "Form_FormId" });
            AddColumn("dbo.Forms", "ClosingDate1", c => c.DateTime(nullable: false));
            AddColumn("dbo.Forms", "ClosingDate2", c => c.DateTime(nullable: false));
            AddColumn("dbo.Forms", "ClosingDate3", c => c.DateTime(nullable: false));
            AddColumn("dbo.Forms", "DateEditedXTimes", c => c.Int(nullable: false));
            DropColumn("dbo.Attachements", "Form_FormId");
            DropColumn("dbo.Forms", "AttachmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "AttachmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Attachements", "Form_FormId", c => c.Int());
            DropColumn("dbo.Forms", "DateEditedXTimes");
            DropColumn("dbo.Forms", "ClosingDate3");
            DropColumn("dbo.Forms", "ClosingDate2");
            DropColumn("dbo.Forms", "ClosingDate1");
            CreateIndex("dbo.Attachements", "Form_FormId");
            AddForeignKey("dbo.Attachements", "Form_FormId", "dbo.Forms", "FormId");
        }
    }
}

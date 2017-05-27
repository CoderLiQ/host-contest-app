namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachements",
                c => new
                    {
                        AttachementId = c.Int(nullable: false, identity: true),
                        AttachementName = c.String(),
                        AttachementData = c.Binary(),
                        Form_FormId = c.Int(),
                    })
                .PrimaryKey(t => t.AttachementId)
                .ForeignKey("dbo.Forms", t => t.Form_FormId)
                .Index(t => t.Form_FormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachements", "Form_FormId", "dbo.Forms");
            DropIndex("dbo.Attachements", new[] { "Form_FormId" });
            DropTable("dbo.Attachements");
        }
    }
}

namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        FormId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        ResponsiblePersonName = c.String(),
                        Description = c.String(nullable: false, maxLength: 20),
                        TypeId = c.Int(nullable: false),
                        СlosingDate = c.DateTime(nullable: false),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Forms");
        }
    }
}

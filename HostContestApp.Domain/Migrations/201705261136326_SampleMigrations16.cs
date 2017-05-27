namespace HostContestApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations16 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Attachements");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Attachements",
                c => new
                    {
                        AttachementId = c.Int(nullable: false, identity: true),
                        AttachementName = c.String(),
                        AttachementData = c.Binary(),
                    })
                .PrimaryKey(t => t.AttachementId);
            
        }
    }
}

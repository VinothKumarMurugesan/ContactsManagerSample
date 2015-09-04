namespace ContactsManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Cellphone = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Born = c.DateTime(nullable: false),
                        CreationTs = c.DateTime(nullable: false),
                        CreationUserId = c.String(maxLength: 50),
                        LastChangeTs = c.DateTime(),
                        LastChangeUserId = c.String(maxLength: 50),
                        StatusType = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contact");
        }
    }
}

namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Player1Id = c.Int(nullable: false),
                        Player2Id = c.Int(nullable: false),
                        WinnerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player1Id, t.Player2Id });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Matches");
        }
    }
}

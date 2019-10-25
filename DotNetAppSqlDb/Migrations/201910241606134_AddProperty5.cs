namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Matches");
            AddColumn("dbo.Matches", "MatchId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Matches", "MatchId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Matches");
            DropColumn("dbo.Matches", "MatchId");
            AddPrimaryKey("dbo.Matches", new[] { "Player1Id", "Player2Id" });
        }
    }
}

namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Todoes", "Email");
        }
    }
}

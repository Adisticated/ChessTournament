namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "Name", c => c.String());
            DropColumn("dbo.Todoes", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Todoes", "Description", c => c.String());
            DropColumn("dbo.Todoes", "Name");
        }
    }
}

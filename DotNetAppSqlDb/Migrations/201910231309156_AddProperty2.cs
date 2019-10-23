namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Todoes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Todoes", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Todoes", "Password", c => c.String());
            AlterColumn("dbo.Todoes", "Name", c => c.String());
        }
    }
}

namespace SmartStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DBUsers", "UserName", c => c.String());
            AddColumn("dbo.DBUsers", "Password", c => c.String());
            AddColumn("dbo.DBUsers", "LoginIP", c => c.String());
            AddColumn("dbo.DBUsers", "RegisterDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.DBUsers", "name");
            DropColumn("dbo.DBUsers", "pass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DBUsers", "pass", c => c.String());
            AddColumn("dbo.DBUsers", "name", c => c.String());
            DropColumn("dbo.DBUsers", "RegisterDateTime");
            DropColumn("dbo.DBUsers", "LoginIP");
            DropColumn("dbo.DBUsers", "Password");
            DropColumn("dbo.DBUsers", "UserName");
        }
    }
}

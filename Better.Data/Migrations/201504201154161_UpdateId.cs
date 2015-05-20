namespace Better.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RawMatches", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.RawMatches", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RawMatches");
            AlterColumn("dbo.RawMatches", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.RawMatches", "Id");
        }
    }
}

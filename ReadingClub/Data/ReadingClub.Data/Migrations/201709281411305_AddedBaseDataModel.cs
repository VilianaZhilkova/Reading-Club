namespace ReadingClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBaseDataModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Authors", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Authors", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Books", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Books", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Books", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Discussions", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Discussions", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Discussions", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            CreateIndex("dbo.Authors", "IsDeleted");
            CreateIndex("dbo.Books", "IsDeleted");
            CreateIndex("dbo.Discussions", "IsDeleted");
            CreateIndex("dbo.Comments", "IsDeleted");
            CreateIndex("dbo.AspNetUsers", "IsDeleted");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "IsDeleted" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropIndex("dbo.Discussions", new[] { "IsDeleted" });
            DropIndex("dbo.Books", new[] { "IsDeleted" });
            DropIndex("dbo.Authors", new[] { "IsDeleted" });
            DropColumn("dbo.AspNetUsers", "ModifiedOn");
            DropColumn("dbo.AspNetUsers", "CreatedOn");
            DropColumn("dbo.AspNetUsers", "DeletedOn");
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.Comments", "ModifiedOn");
            DropColumn("dbo.Comments", "CreatedOn");
            DropColumn("dbo.Comments", "DeletedOn");
            DropColumn("dbo.Discussions", "ModifiedOn");
            DropColumn("dbo.Discussions", "CreatedOn");
            DropColumn("dbo.Discussions", "DeletedOn");
            DropColumn("dbo.Books", "ModifiedOn");
            DropColumn("dbo.Books", "CreatedOn");
            DropColumn("dbo.Books", "DeletedOn");
            DropColumn("dbo.Authors", "ModifiedOn");
            DropColumn("dbo.Authors", "CreatedOn");
            DropColumn("dbo.Authors", "DeletedOn");
        }
    }
}

namespace ReadingClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaxNumberOfParticipantsInDiscussionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Discussions", "MaximumNumberOfParticipants", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussions", "MaximumNumberOfParticipants");
            DropColumn("dbo.Books", "IsApproved");
        }
    }
}

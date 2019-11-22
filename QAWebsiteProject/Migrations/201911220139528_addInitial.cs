namespace QAWebsiteProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        UserId = c.String(),
                        question_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Questions", t => t.question_QuestionID)
                .Index(t => t.question_QuestionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "question_QuestionID", "dbo.Questions");
            DropIndex("dbo.Votes", new[] { "question_QuestionID" });
            DropTable("dbo.Votes");
        }
    }
}

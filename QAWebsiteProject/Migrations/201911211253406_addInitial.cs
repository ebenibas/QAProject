namespace QAWebsiteProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInitial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            CreateIndex("dbo.Answers", "QuestionID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            CreateIndex("dbo.Answers", "QuestionId");
        }
    }
}

namespace App.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EA_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionModel1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer1 = c.String(),
                        Answer2 = c.String(),
                        Answer3 = c.String(),
                        Answer4 = c.String(),
                        CorrectAnswer = c.String(),
                        TopicId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        SubjectId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Theories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TopicId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.QuestionModel2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        CorrectAnswer = c.String(),
                        TopicId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Email = c.String(),
                        FullName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Hash = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionModel2", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Theories", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.QuestionModel1", "TopicId", "dbo.Topics");
            DropIndex("dbo.TestResults", new[] { "UserId" });
            DropIndex("dbo.QuestionModel2", new[] { "TopicId" });
            DropIndex("dbo.Theories", new[] { "TopicId" });
            DropIndex("dbo.Topics", new[] { "SubjectId" });
            DropIndex("dbo.QuestionModel1", new[] { "TopicId" });
            DropTable("dbo.Users");
            DropTable("dbo.TestResults");
            DropTable("dbo.QuestionModel2");
            DropTable("dbo.Theories");
            DropTable("dbo.Subjects");
            DropTable("dbo.Topics");
            DropTable("dbo.QuestionModel1");
        }
    }
}

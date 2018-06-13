namespace App.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EA_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionModel1", "QuestionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionModel2", "QuestionNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionModel2", "QuestionNumber");
            DropColumn("dbo.QuestionModel1", "QuestionNumber");
        }
    }
}

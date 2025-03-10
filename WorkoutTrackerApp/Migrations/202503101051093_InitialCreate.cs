namespace WorkoutTrackerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(nullable: false),
                        Reps = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        WorkoutID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseID)
                .ForeignKey("dbo.Workouts", t => t.WorkoutID, cascadeDelete: true)
                .Index(t => t.WorkoutID);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        WorkoutID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        TotalDuration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkoutID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "WorkoutID", "dbo.Workouts");
            DropIndex("dbo.Exercises", new[] { "WorkoutID" });
            DropTable("dbo.Workouts");
            DropTable("dbo.Exercises");
        }
    }
}

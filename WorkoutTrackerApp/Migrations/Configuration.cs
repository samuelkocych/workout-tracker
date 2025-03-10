namespace WorkoutTrackerApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkoutTrackerApp.Classes.WorkoutData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WorkoutTrackerApp.Classes.WorkoutData";
        }

        protected override void Seed(WorkoutTrackerApp.Classes.WorkoutData context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

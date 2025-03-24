using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp.Classes
{
    // database context for storing workouts and exercises
    public class WorkoutData : DbContext
    {
        public WorkoutData() :base("MyWorkoutData") { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}

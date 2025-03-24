using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkoutTrackerApp;
using WorkoutTrackerApp.Classes;

namespace DataManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkoutData db = new WorkoutData();

            using (db)
            {
                // create workout sessions
                Workout w1 = new Workout() { WorkoutID = 1, Name = "Upper Body", Date = DateTime.Now, TotalDuration = 60 };
                Workout w2 = new Workout() { WorkoutID = 2, Name = "Lower Body", Date = DateTime.Now, TotalDuration = 65 };

                // create workout sessions
                Exercise e1 = new Exercise() { ExerciseID = 1, Name = "Bench Press", Sets = 4, Reps = 8, Weight = 80.5, WorkoutID = 1, Workout = w1 };
                Exercise e2 = new Exercise() { ExerciseID = 2, Name = "Bench Press", Sets = 5, Reps = 9, Weight = 80, WorkoutID = 1, Workout = w1 };
                Exercise e3 = new Exercise() { ExerciseID = 3, Name = "Bench Press", Sets = 3, Reps = 7, Weight = 85.5, WorkoutID = 2, Workout = w2 };
                Exercise e4 = new Exercise() { ExerciseID = 4, Name = "Bench Press", Sets = 2, Reps = 12, Weight = 70, WorkoutID = 2, Workout = w2 };

                // add workouts to database
                db.Workouts.Add(w1);
                db.Workouts.Add(w2);
                Console.WriteLine("Added workouts to database");

                // add exercises to database
                db.Exercises.Add(e1);
                db.Exercises.Add(e2);
                db.Exercises.Add(e3);
                db.Exercises.Add(e4);
                Console.WriteLine("Added exercises to database");

                // save changes to database
                db.SaveChanges();
                Console.WriteLine("Saved to database");
            }
        }
    }
}

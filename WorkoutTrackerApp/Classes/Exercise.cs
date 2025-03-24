using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp
{
    // represents an exercise in a workout
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }

        public int WorkoutID { get; set; }
        public virtual Workout Workout { get; set; }

        public Exercise() { }
        
        public Exercise(string name, int sets, int reps, double weight)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
            Weight = weight;
        }

        // returns exercise details as a string
        public override string ToString()
        {
            return $"{Name} - {Sets} Sets x {Reps} Reps x {Weight} kg";
        }
    }
}

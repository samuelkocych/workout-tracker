using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }

        public int WorkoutID { get; set; }
        public virtual Workout Workout { get; set; }
    }
}

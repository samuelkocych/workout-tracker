using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp
{
    public class Workout
    {
        public int WorkoutID { get; set; }
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public int TotalDuration { get; set; }

        public virtual List<Exercise> Exercises { get; set; }

        public Workout(string name, DateTime date, int totalDuration)
        {
            Name = name;
            Date = date;
            TotalDuration = totalDuration;
            Exercises = new List<Exercise>();
        }

        public override string ToString()
        {
            return $"{Name} ({Date.ToShortDateString()}) - {TotalDuration} min";
        }
    }
}

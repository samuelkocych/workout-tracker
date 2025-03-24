using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp
{
    // represents an exercise from the external API
    public class ExerciseApi
    {
        public string Name { get; set; }
        public string Instructions { get; set; }
    }
}

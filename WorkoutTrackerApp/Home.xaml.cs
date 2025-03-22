using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkoutTrackerApp.Classes;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.Entity;


namespace WorkoutTrackerApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        WorkoutData db = new WorkoutData();

        public Home()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartData();
            LoadStats();
            NextTraining();
        }
        
        private void LoadChartData()
        {
            DateTime today = DateTime.Today;

            // get days since last sunday
            int daysSinceSunday= (int) today.DayOfWeek;

            DateTime currentSunday = today.AddDays(-daysSinceSunday);

            int[] weeklyWorkouts = new int [4];

            for (int i = 0; i < 4; i++)
            {
                // calculate start and end of each week
                DateTime sunday = currentSunday.AddDays(-((3 - i) * 7));
                DateTime saturday = sunday.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59); // whole saturday

                // get number of workouts in this week
                int workoutCount = db.Workouts
                    .Where(w => w.Date >= sunday && w.Date <= saturday)
                    .Count();

                weeklyWorkouts[i] = workoutCount;
            }

            WorkoutChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Workouts per week",
                    Values = new ChartValues<int>(weeklyWorkouts),
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y}" // lambda function to display Y value as label on chart points  
                }
            };

            WorkoutChart.AxisX.Clear();
            WorkoutChart.AxisX.Add(new Axis
            {
                Title = "Weeks",
                Labels = new List<string> { "3 weeks ago", "2 weeks ago", "Last week", "This week" },
                Separator = new LiveCharts.Wpf.Separator { Step = 1 }
            });
        }
        
        private void LoadStats()
        {
           
            DateTime oneMonthAgo = DateTime.Now.AddDays(-28);

            int totalMinutes = db.Workouts
                .Where(w => w.Date >= oneMonthAgo)
                .Sum(w => w.TotalDuration);

            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;


            tblkTotalDuration.Text = $"{hours} h {minutes} min";

            double totalCalories = totalMinutes * (323.0 / 60);

            tblkCalories.Text = $"{totalCalories:f0} cal";

            double totalWeightLifted = db.Exercises
                .Where(e => e.Workout.Date >= oneMonthAgo)
                .Sum(e => e.Sets * e.Reps * e.Weight);

            if (totalWeightLifted < 1000)
                tblkWeight.Text = $"{totalWeightLifted:f0} kg";
            else
                tblkWeight.Text = $"{totalWeightLifted / 1000:f0} t";
        }

        private void NextTraining()
        {
            DateTime today = DateTime.Today;

            var nextTraining = db.Workouts
                .Where(w => w.Date > today)
                .OrderBy(w => w.Date)
                .FirstOrDefault();

            if (nextTraining != null)
            {
                tblkNextTraining.Text = $"Next training on {nextTraining.Date:dddd, dd MMMM yyyy}";
            }
            else
            {
                tblkNextTraining.Text = "No upcoming training scheduled.";
            }
        }

        private void btnStartNewTraining_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("NewTraining.xaml", UriKind.Relative));
        }
    }
}

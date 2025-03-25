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
    public partial class Home : Page
    {
        WorkoutData db = new WorkoutData(); // database context

        public Home()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartData(); // load chart data
            LoadStats(); // load stats
            NextTraining(); // show next training
        }

        // load workout chart for last 4 weeks
        private void LoadChartData()
        {
            DateTime today = DateTime.Today;
            int daysSinceSunday = (int)today.DayOfWeek;
            DateTime currentSunday = today.AddDays(-daysSinceSunday);

            int[] weeklyWorkouts = new int [4];

            for (int i = 0; i < 4; i++)
            {
                // get week start and end
                DateTime sunday = currentSunday.AddDays(-((3 - i) * 7));
                DateTime saturday = sunday.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59); // whole saturday

                // count workouts in this week
                int workoutCount = db.Workouts
                    .Where(w => w.Date >= sunday && w.Date <= saturday)
                    .Count();

                weeklyWorkouts[i] = workoutCount;
            }

            // update chart
            WorkoutChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Workouts per week",
                    Values = new ChartValues<int>(weeklyWorkouts),
                    DataLabels = true,
                }
            };

            WorkoutChart.AxisX.Clear();
            WorkoutChart.AxisX.Add(new Axis
            {
                Title = "Weeks",
                Labels = new List<string> { "3 weeks ago", "2 weeks ago", "Last week", "This week" },
                Separator = new LiveCharts.Wpf.Separator { Step = 1 }
            });

            WorkoutChart.AxisY.Clear();
            WorkoutChart.AxisY.Add(new Axis { MinValue = 0 }); // ensures no negative values
        }

        // load total stats for last 4 weeks
        private void LoadStats()
        {           
            DateTime oneMonthAgo = DateTime.Now.AddDays(-28);

            // total workout time
            int totalMinutes = db.Workouts
                .Where(w => w.Date >= oneMonthAgo)
                .Select(w => (int?)w.TotalDuration)
                .Sum() ?? 0; // default 0 if no workouts

            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            if (tblkTotalDuration != null)
                tblkTotalDuration.Text = $"{hours} h {minutes} min";

            // total calories burned
            double totalCalories = totalMinutes * (323.0 / 60);
            if (tblkCalories != null)
                tblkCalories.Text = $"{totalCalories:f0} cal";

            // total weight lifted
            double totalWeight = db.Exercises
                .Select(e => (double?)(e.Sets * e.Reps * e.Weight))
                .Sum() ?? 0;

            if (tblkWeight != null)
                tblkWeight.Text = totalWeight < 1000 ? $"{totalWeight:f0} kg" : $"{totalWeight / 1000:f0} t";
        }

        // show next scheduled training
        private void NextTraining()
        {
            DateTime today = DateTime.Today;

            var nextTraining = db.Workouts
                .Where(w => w.Date > today)
                .OrderBy(w => w.Date)
                .FirstOrDefault();

            tblkNextTraining.Text = nextTraining != null
                ? $"Next training on {nextTraining.Date:dddd, dd MMMM yyyy}"
                : "No upcoming training scheduled.";
        }

        // navigate to new training page
        private void btnStartNewTraining_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/NewTraining.xaml", UriKind.Relative)); // '?' means is only called if is not null
        }
    }
}

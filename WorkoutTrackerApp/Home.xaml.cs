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
            if (WorkoutChart == null)
            {
                MessageBox.Show("WorkoutChart is not initialized in XAML.");
                return;
            }

            LoadChartData();
        }

        public List<int> GetWeeklyVisits()
        {
            if (db == null)
            {
                MessageBox.Show("Database context is null!");
                return new List<int> { 0, 0, 0, 0 };
            }

            if (db.Workouts == null || !db.Workouts.Any())
            {
                return new List<int> { 0, 0, 0, 0 };
            }


            DateTime today = DateTime.Today;
            DateTime start = today.AddDays(-28); // last 4 weeks

            var query = db.Workouts
                        .Where(w => w.Date >= start)
                        .GroupBy(w => DbFunctions.DiffDays(start, w.Date) / 7)
                        .OrderBy(g => g.Key)
                        .Select(g => g.Count())
                        .ToList();


            while (query.Count < 4)
            {
                query.Insert(0, 0); // inserts number 0 at the beginning of the workout list
            }

            return query;
        }

        private void LoadChartData()
        {
            if (WorkoutChart == null)
            {
                MessageBox.Show("WorkoutChart is not initialized!");
                return;
            }

            List<int> workoutCounts = GetWeeklyVisits();

            WorkoutChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Workouts",
                    Values = new ChartValues<int>(workoutCounts)
                }
            };

            if (WorkoutChart.AxisX.Count == 0)
            {
                WorkoutChart.AxisX.Add(new Axis());
            }

            WorkoutChart.AxisX[0].Labels = new List<string> { "Week 1", "Week 2", "Week 3", "Week 4" };
        }
    }
}

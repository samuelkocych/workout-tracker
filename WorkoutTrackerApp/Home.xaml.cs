using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Threading;
using WorkoutTrackerApp.Classes;

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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100); // Počkáme na inicializaci XAML

            if (WorkoutChart == null)
            {
                MessageBox.Show("WorkoutChart is NULL!");
                return;
            }

            LoadChartData();
        }

        public List<int> GetWeeklyVisits()
        {
            using (db)
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
                DateTime start = today.AddDays(-28); // poslední 4 týdny

                var query = db.Workouts
                            .Where(w => w.Date >= start)
                            .GroupBy(w => DbFunctions.DiffDays(start, w.Date) / 7)
                            .OrderBy(g => g.Key)
                            .Select(g => g.Count())
                            .ToList();

                while (query.Count < 4)
                {
                    query.Insert(0, 0); // doplnění nul, pokud chybí týdny
                }

                return query;
            }
        }

        private void LoadChartData()
        {
            if (WorkoutChart == null)
            {
                MessageBox.Show("WorkoutChart is NULL!");
                return;
            }

            if (WorkoutChart.Series == null)
            {
                WorkoutChart.Series = new SeriesCollection();
            }

            List<int> workoutCounts = GetWeeklyVisits();

            Dispatcher.Invoke(() =>
            {
                WorkoutChart.Series.Clear();
                WorkoutChart.Series.Add(new ColumnSeries
                {
                    Title = "Workouts",
                    Values = new ChartValues<int>(workoutCounts)
                });

                if (WorkoutChart.AxisX.Count == 0)
                {
                    WorkoutChart.AxisX.Add(new Axis
                    {
                        Title = "Weeks",
                        Labels = new List<string> { "Week 1", "Week 2", "Week 3", "Week 4" }
                    });
                }
            });
        }
    }
}

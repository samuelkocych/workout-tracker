﻿using System;
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

            // get days since monday, if sunday set to 6  
            int daysSinceMonday = today.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)today.DayOfWeek - (int)DayOfWeek.Monday;

            DateTime currentMonday = today.AddDays(-daysSinceMonday);

            var weeklyWorkouts = new int[4];


            var weeklyTraining = new int[4];

            WorkoutChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Workouts:",
                    Values = new ChartValues<int> { 2, 3, 4, 2 },
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y}"
                }
            };

            WorkoutChart.AxisX.Add(new Axis
            {
                Title = "Weeks",
                Labels = new List<string> { "Week 1", "Week 2", "Week 3", "Week 4" },
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

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
        }

        
        private void LoadChartData()
        {
            if (WorkoutChart == null)
            {
                MessageBox.Show("Workout chart is null!");
                return;
            }

            WorkoutChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Workouts",
                    Values = new ChartValues<int> { 2, 3, 4, 5 }
                }
            };

            WorkoutChart.AxisX.Add(new Axis
            {
                Title = "Weeks",
                Labels = new List<string> { "Week 1", "Week 2", "Week 3", "Week 4" }
            });
        }
    }
}

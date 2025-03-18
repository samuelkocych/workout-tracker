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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkoutTrackerApp
{
    /// <summary>
    /// Interaction logic for Exercises.xaml
    /// </summary>
    public partial class Exercises : Page
    {
        public Exercises()
        {
            InitializeComponent();
        }
       
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string muscle = tbxSearch.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(muscle))
            {
                List<ExerciseApi> exercises = await FetchData.GetExercisesAsync(muscle);
                lbxExercises.ItemsSource = exercises;

                if (exercises.Any())
                {
                    lbxExercises.ItemsSource = exercises;
                }
                else
                {
                    MessageBox.Show("No exercise found for this muscle!");
                }
            }
            else
            {
                MessageBox.Show("Enter muscle name you want to train!");
            }

        }
    }
}

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
            LoadExercises();
        }
        private async void LoadExercises()
        {
            List<ExerciseApi> exercises = await FetchData.GetExercisesAsync();
            ExerciseList.ItemsSource = exercises;
        }
    }
}

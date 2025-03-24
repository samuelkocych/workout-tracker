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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using WorkoutTrackerApp.Services;


namespace WorkoutTrackerApp
{
    public partial class Exercises : Page
    {
        public Exercises()
        {
            InitializeComponent();
        }

        // fetches exercises based on the selected muscle group
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // ensure a muscle group is selected
            if (cbxMuscles.SelectedItem is ComboBoxItem selectedItem)
            {
                string muscle = selectedItem.Content.ToString().ToLower();

                try
                {
                    ExerciseApiService exerciseService = new ExerciseApiService();
                    List<ExerciseApi> exercises = await exerciseService.GetExercisesAsync(muscle);
                    lbxExercises.ItemsSource = exercises;
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show($"Error fetching exercises: {ex.Message}");
                }
            }
            else
            {
                CustomMessageBox.Show("Please select a muscle group.");
            }
        }
    }
}

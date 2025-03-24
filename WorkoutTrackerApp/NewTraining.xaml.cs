using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkoutTrackerApp.Classes;

namespace WorkoutTrackerApp
{
    public partial class NewTraining : Page
    {
        WorkoutData db = new WorkoutData(); // database instance

        public NewTraining()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dpDatePicker.SelectedDate = DateTime.Today; // set default date to today
        }

        private void btnAddExercise_Click(object sender, RoutedEventArgs e)
        {
            // validate name input
            string name = tbxName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                CustomMessageBox.Show("Please enter an exercise name.");
                return;
            }

            // validate sets input
            if (!int.TryParse(tbxSets.Text, out int sets) || sets <= 0)
            {
                CustomMessageBox.Show("Please enter a valid number of sets");
                return;
            }

            // validate reps input
            if (!int.TryParse(tbxReps.Text, out int reps) || reps <= 0)
            {
                CustomMessageBox.Show("Please enter a valid number of reps.");
                return;
            }

            // validate weight input
            if (!double.TryParse(tbxWeight.Text, out double weight) || weight < 0)
            {
                CustomMessageBox.Show("Please enter a valid weight.");
                return;
            }

            // check if editing an existing exercise
            Exercise selectedExercise = lbxExercises.SelectedItem as Exercise;
            if (selectedExercise != null)
            {
                selectedExercise.Name = name;
                selectedExercise.Sets = sets;
                selectedExercise.Reps = reps;
                selectedExercise.Weight = weight;
                lbxExercises.Items.Refresh(); // update list
            }
            else
            {
                lbxExercises.Items.Add(new Exercise(name, sets, reps, weight)); // add new exercise
            }

            ResetExerciseInput(); // clear inputs
        }

        private void btnDeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Exercise exerciseToDelete = btn?.Tag as Exercise; // using '?' prevents null reference errors

            if (exerciseToDelete != null)
            {
                lbxExercises.Items.Remove(exerciseToDelete); // remove exercise
            }
        }

        private void btnEditExercise_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Exercise selectedExercise = btn?.Tag as Exercise;


            if (selectedExercise != null)
            {
                // load exercise details into inputs
                tbxName.Text = selectedExercise.Name;
                tbxSets.Text = selectedExercise.Sets.ToString();
                tbxReps.Text = selectedExercise.Reps.ToString();
                tbxWeight.Text = selectedExercise.Weight.ToString();

                lbxExercises.SelectedItem = selectedExercise;
            }
        }

        private void btnSaveTraining_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbxTrainingName.Text))
                    throw new Exception("Enter workout name");

                if (!int.TryParse(tbxTotalDuration.Text, out int duration) || duration <= 0)
                    throw new Exception("Enter valid duration");

                if (!dpDatePicker.SelectedDate.HasValue)
                    throw new Exception("Select a valid date");

                DateTime date = dpDatePicker.SelectedDate.Value;

                // create new workout
                Workout workout = new Workout(tbxTrainingName.Text, date, duration);

                // add exercises to workout
                foreach (Exercise ex in lbxExercises.Items)
                {
                    ex.Workout = workout;
                    workout.Exercises.Add(ex);
                }

                db.Workouts.Add(workout);
                db.SaveChanges(); // save to database

                CustomMessageBox.Show("Workout succesfully saved");
                ResetTrainingForm(); // clear form
            }
            catch (Exception ex) 
            {
                CustomMessageBox.Show(ex.Message); // show error
            }
        }

        private void ResetExerciseInput()
        {
            tbxName.Clear();
            tbxSets.Clear();
            tbxReps.Clear();
            tbxWeight.Clear();
            lbxExercises.SelectedItem = null;
        }

        private void ResetTrainingForm()
        {
            tbxTrainingName.Clear();
            tbxTotalDuration.Clear();
            dpDatePicker.SelectedDate = null;
            lbxExercises.Items.Clear();
        }
    }
}

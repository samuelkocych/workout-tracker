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
    /// <summary>
    /// Interaction logic for NewTraining.xaml
    /// </summary>
    public partial class NewTraining : Page
    {
        WorkoutData db = new WorkoutData();

        public NewTraining()
        {
            InitializeComponent();
        }

        private void btnAddExercise_Click(object sender, RoutedEventArgs e)
        {
            if (cmbExercises.SelectedItem == null)
            {
                MessageBox.Show("Please select an exercise");
                return;
            }

            if (!int.TryParse(tbxSets.Text, out int sets) || sets <= 0)
            {
                MessageBox.Show("Please enter a valid number of sets");
                return;
            }

            if (!int.TryParse(tbxReps.Text, out int reps) || reps <= 0)
            {
                MessageBox.Show("Please enter a valid number of reps.");
                return;
            }

            if (!double.TryParse(tbxWeight.Text, out double weight) || weight < 0)
            {
                MessageBox.Show("Please enter a valid weight.");
                return;
            }



            string name = ((ComboBoxItem)cmbExercises.SelectedItem).Content.ToString();
           
            Exercise selectedExercise = lbxExercises.SelectedItem as Exercise;

            if (selectedExercise != null)
            {
                selectedExercise.Name = name;
                selectedExercise.Sets = sets;
                selectedExercise.Reps = reps;
                selectedExercise.Weight = weight;

                lbxExercises.Items.Refresh();
            }
            else
            {
                Exercise newExercise = new Exercise(name, sets, reps, weight);
                lbxExercises.Items.Add(newExercise);
            }

            // reset all values
            cmbExercises.SelectedIndex = -1;
            tbxSets.Clear();
            tbxReps.Clear();
            tbxWeight.Clear();

            lbxExercises.SelectedItem = null;
        }

        private void btnDeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            if (lbxExercises.SelectedItem != null)
            {
                lbxExercises.Items.Remove(lbxExercises.SelectedItem);
            }
        }

        private void btnEditExercise_Click(object sender, RoutedEventArgs e)
        {
            Exercise selectedExercise = lbxExercises.SelectedItem as Exercise;

            if (selectedExercise != null)
            {
                tbxSets.Text = selectedExercise.Sets.ToString();
                tbxReps.Text = selectedExercise.Reps.ToString();
                tbxWeight.Text = selectedExercise.Weight.ToString();

                //this code finds the combo box item whose content matches the exercise name and sets it as the selected item in the combo box
                cmbExercises.SelectedItem = cmbExercises.Items 
                .Cast<ComboBoxItem>()                          
                .FirstOrDefault(item => item.Content.ToString() == selectedExercise.Name); 
            }
        }

        private void btnSaveTraining_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = tbxTrainingName.Text;
                int duration = int.Parse(tbxTotalDuration.Text);
                DateTime date = dpDatePicker.SelectedDate.GetValueOrDefault();

                Workout workout = new Workout(name, date, duration);

                foreach (Exercise ex in lbxExercises.Items)
                {
                    ex.Workout = workout;
                    workout.Exercises.Add(ex);
                }

                db.Workouts.Add(workout);
                db.SaveChanges();

                MessageBox.Show("Wokrout succesfully saved");

                // reset the form
                tbxTrainingName.Clear();
                tbxTotalDuration.Clear();
                dpDatePicker.SelectedDate = null;
                lbxExercises.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured", ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dpDatePicker.SelectedDate = DateTime.Today;
        }
    }
}

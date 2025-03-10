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
            string name = ((ComboBoxItem)cmbExercises.SelectedItem).Content.ToString();
            int sets = int.Parse(tbxSets.Text);
            int reps = int.Parse(tbxReps.Text);
            double weight = double.Parse(tbxWeight.Text);

            Exercise exercise = new Exercise(name, sets, reps, weight);

            lbxExercises.Items.Add(exercise);

            // reset all values
            cmbExercises.SelectedIndex = -1;
            tbxSets.Clear();
            tbxReps.Clear();
            tbxWeight.Clear();
        }

        private void btnDeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            var selectedExercise = lbxExercises.SelectedItem;

            if (selectedExercise != null)
            {
                lbxExercises.Items.Remove(selectedExercise);
            }
        }

        private void btnEditExercise_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

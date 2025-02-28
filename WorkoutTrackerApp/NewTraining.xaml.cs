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

namespace WorkoutTrackerApp
{
    /// <summary>
    /// Interaction logic for NewTraining.xaml
    /// </summary>
    public partial class NewTraining : Page
    {
        private List<string> exerciseList = new List<string>();

        public NewTraining()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Můžeš zde načíst data, pokud je chceš předvyplnit
        }

        private void btnAddExercise_Click(object sender, RoutedEventArgs e)
        {
            if (cmbExercises.SelectedItem == null || string.IsNullOrWhiteSpace(txtSets.Text) ||
                string.IsNullOrWhiteSpace(txtReps.Text) || string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string exerciseName = ((ComboBoxItem)cmbExercises.SelectedItem).Content.ToString();
            string sets = txtSets.Text;
            string reps = txtReps.Text;
            string weight = txtWeight.Text;

            string exerciseEntry = $"{exerciseName} - {sets}x{reps} {weight}kg";
            exerciseList.Add(exerciseEntry);
            lstExercises.Items.Add(exerciseEntry);

            // Vyčistit pole po přidání
            txtSets.Clear();
            txtReps.Clear();
            txtWeight.Clear();
        }

        private void btnSaveTraining_Click(object sender, RoutedEventArgs e)
        {
            if (exerciseList.Count == 0)
            {
                MessageBox.Show("Please add at least one exercise.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDuration.Text) || string.IsNullOrWhiteSpace(txtCalories.Text))
            {
                MessageBox.Show("Please enter total duration and calories.");
                return;
            }

            string totalDuration = txtDuration.Text;
            string totalCalories = txtCalories.Text;

            // Zde můžeš implementovat logiku pro uložení tréninku do databáze / souboru
            MessageBox.Show($"Training saved!\nDuration: {totalDuration} min\nCalories: {totalCalories}\nExercises:\n{string.Join("\n", exerciseList)}");

            // Po uložení vymazat seznam
            exerciseList.Clear();
            lstExercises.Items.Clear();
            txtDuration.Clear();
            txtCalories.Clear();
        }
    }
}

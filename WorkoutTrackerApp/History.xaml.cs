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
using WorkoutTrackerApp.Classes;

namespace WorkoutTrackerApp
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {
        WorkoutData db = new WorkoutData();

        public History()
        {
            InitializeComponent();
            LoadWorkouts(); //method to load workouts
        }

        public void LoadWorkouts()
        {
            try
            {
                var query = from w in db.Workouts
                            orderby w.Date descending
                            select w;

                var workouts = query.ToList();

                icWorkouts.ItemsSource = workouts; 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

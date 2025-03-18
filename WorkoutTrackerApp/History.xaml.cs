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
            LoadWorkouts(); 
        }

        //method to load workouts
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

        public List<int> GetWeeklyVisits()
        {
            DateTime today = DateTime.Today;
            DateTime start = today.AddDays(-28); // last 4 weeks

            var query = from w in db.Workouts
                        where w.Date >= start
                        group w by (today - w.Date).Days / 7 into g
                        orderby g.Key
                        select g.Count()
                        .ToList();


            while (query > 4)
            {
                query.Insert(0, 0); // inserts number 0 at the beginning of the workout list
            }





        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // check if sender is a button and has integer ID
            if (sender is Button btn && btn.Tag is int workoutId)
            {
                var query = from w in db.Workouts
                            where w.WorkoutID == workoutId
                            select w;

                var workoutToDelete = query.FirstOrDefault();

                // if workout is find, delete it from the database
                if (workoutToDelete != null)
                {
                    db.Workouts.Remove(workoutToDelete);
                    db.SaveChanges();
                    LoadWorkouts();
                }
            }
        }
    }
}

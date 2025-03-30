using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WorkoutTrackerApp;

namespace WorkoutTracker.Tests
{
    [TestClass]
    public class AllTests
    {
        // testing if the exercise object is properly initaliazed
        [TestMethod]
        public void TestExerciseCreatesCorrectly()
        {
            // arrange
            var exercise = new Exercise("Bench Press", 3, 10, 60.0);

            // assert
            Assert.AreEqual("Bench Press", exercise.Name);
            Assert.AreEqual(3, exercise.Sets);
            Assert.AreEqual(10, exercise.Reps);
            Assert.AreEqual(60.0, exercise.Weight);
        }

        // testing if the ToString method returns the correctly formatted exercise details
        [TestMethod]
        public void TestExerciseToString()
        {
            // arrange
            var exercise = new Exercise("Squat", 4, 8, 70.0);

            // act
            var result = exercise.ToString();

            // assert
            Assert.AreEqual("Squat - 4 Sets x 8 Reps x 70 kg", result);
        }

        // testing if the exercise is correctly added to a workout
        [TestMethod]
        public void TestWorkoutAddsExercise()
        {
            // arrange
            var workout = new Workout();
            var exercise = new Exercise("Deadlift", 5, 5, 100.0);

            // act
            workout.Exercises.Add(exercise);

            // assert
            Assert.AreEqual(1, workout.Exercises.Count);
            Assert.AreEqual("Deadlift", workout.Exercises[0].Name);
        }

        // testing the relationships between workout end exercises
        [TestMethod]
        public void TestRelationships()
        {
            // arrange
            var workout = new Workout("Test Workout", DateTime.Now, 30);
            var exercise = new Exercise("Pull-up", 3, 8, 85);

            // act
            exercise.Workout = workout;
            workout.Exercises.Add(exercise);

            // assert
            Assert.AreEqual(workout, exercise.Workout);
            Assert.AreEqual(1, workout.Exercises.Count);
            Assert.AreEqual(exercise, workout.Exercises[0]);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WorkoutTrackerApp;

namespace WorkoutTracker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        // Exercise class tests
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


        // Workout class tests
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
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkoutTrackerApp.Interfaces;

namespace WorkoutTrackerApp.Services
{
    // handles API requests for fetching exercises
    public class ExerciseApiService : IExerciseApiService
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://api.api-ninjas.com/v1/exercises";
        private const string API_KEY = "Xh/3LZxxts/F28xfqFhQzg==b6JewwBFMZ2fAiKG";

        public ExerciseApiService(HttpClient client)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
        }

        public async Task<List<ExerciseApi>> GetExercisesAsync(string muscle)
        {
            string requestUrl = $"{API_URL}?muscle={muscle}";
            HttpResponseMessage response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ExerciseApi>>(json);
            }

            return new List<ExerciseApi>(); // return empty list if API call fails
        }
    }
}

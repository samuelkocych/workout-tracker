﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp
{
    // handles API requests for fetching exercises
    public class FetchData
    {
        private static readonly HttpClient client = new HttpClient();
        private const string API_URL = "https://api.api-ninjas.com/v1/exercises";
        private const string API_KEY = "Xh/3LZxxts/F28xfqFhQzg==b6JewwBFMZ2fAiKG";

        public static async Task<List<ExerciseApi>> GetExercisesAsync(string muscle)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);

            string requestUrl = $"{API_URL}?muscle={muscle}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ExerciseApi>>(json);
            }

            return new List<ExerciseApi>(); // return empty list if API call fails
        }
    }
}

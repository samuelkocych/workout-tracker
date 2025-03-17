using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerApp
{
    public class FetchData
    {
        private static readonly HttpClient client = new HttpClient();
        private const string API_URL = "https://api.api-ninjas.com/v1/exercises";
        private const string API_KEY = "Xh/3LZxxts/F28xfqFhQzg==b6JewwBFMZ2fAiKG";

        public static async Task<List<ExerciseApi>> GetExercisesAsync()
        {
            client.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            HttpResponseMessage response = await client.GetAsync(API_URL);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ExerciseApi>>(json);
            }
            return new List<ExerciseApi>();
        }
    }
}

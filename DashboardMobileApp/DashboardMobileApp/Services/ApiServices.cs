using DashboardMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DashboardMobileApp.Services
{
    public class ApiServices
    {
        public async Task<bool> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.247.45:7001/Token"); //@office
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.97:7001/Token"); //@home

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            //the below 2 lines are for development only, remove them in production 
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            //------------------------- Remove above ------------------------------//

            return response.IsSuccessStatusCode;
        }
        public async Task<Rootobject> GetProjects()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("http://localhost:60931/api/GetLOV/Project");

            Rootobject result = JsonConvert.DeserializeObject<Rootobject>(response);

            return result;

        }
    }
}

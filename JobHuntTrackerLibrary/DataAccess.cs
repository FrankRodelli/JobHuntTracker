using JobHuntTrackerLibrary.Models;
using JobHuntTrackerLibrary.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System;
using System.Data;

namespace JobHuntTrackerLibrary
{
    //made this internal so it doesn't get confused with the data processor class
    internal static class DataAccess
    {
        public static HttpClient Initial()
        {
            var Client = new HttpClient
            {
                BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/")
            };
            return Client;
        }
        public static List<T> GetJobs<T>(string id)
        {
            using(HttpClient client = Initial())
            {
                HttpResponseMessage response = client.GetAsync("api/jobs?id="+id).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    client.Dispose();
                    return JsonConvert.DeserializeObject<List<T>>(result);
                    
                }
                client.Dispose();
                return null;
            }
        }

        public static T GetJobsByID<T>(string Id)
        {
            using (HttpClient client = Initial())
            {
                HttpResponseMessage response = client.GetAsync("api/jobs/" + Id).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    client.Dispose();
                    return JsonConvert.DeserializeObject<T>(result);

                }
                client.Dispose();
                //Idk how this works tbh
                return default;
            }
        }

        public static bool AddJob<T>(T job)
        {

            using (HttpClient client = Initial())
            {
                //POST to API
                HttpResponseMessage response = client.PostAsync("api/jobs", new StringContent(
                    new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                client.Dispose();

                //Returns whether operation was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                } 
            }
        }

        public static bool UpdateJob<T>(T job, string Id)
        {
            using (HttpClient client = Initial())
            {
                HttpResponseMessage response = client.PutAsync("api/jobs/" + Id, new StringContent(
                        new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                client.Dispose();

                //Returns whether operation was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }           
        }

        public static bool DeleteJob<T>(T job, string Id)
        {
            using (HttpClient client = Initial())
            {
                HttpResponseMessage response = client.DeleteAsync("api/jobs/" + Id).GetAwaiter().GetResult();

                client.Dispose();

                //Returns whether operation was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                } 
            }
        }
    }
}

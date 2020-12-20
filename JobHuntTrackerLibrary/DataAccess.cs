 using JobHuntTracker.Models;
using JobHuntTrackerLibrary.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JobHuntTrackerLibrary
{
    public class DataAccess
    {
        JobHuntTrackerAPIHelper _api = new JobHuntTrackerAPIHelper();
        List<Job> jobList;

        public async Task<List<Job>> GetJobs()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/jobs");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                jobList = JsonConvert.DeserializeObject<List<Job>>(result);
            }

            return jobList;
        }

        public async Task<bool> AddJob(Job job)
        {
            //TODO: Implement adding

            using (
                
                
                
                
                
                
                
                HttpClient client = _api.Initial())
            {
                //Serialize job object and remove id to prepare for sending to API
                //JObject jobJson = JObject.Parse(JsonConvert.SerializeObject(job));
                //jobJson.Property("Id").Remove();

                //POST to API
                var response = client.PostAsync("api/jobs", new StringContent(
                    new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json"));
                //Wait for response
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public async Task UpdateJob(Job job)
        {
            //TODO: Implement updating
        }

        public async Task DeleteJob(Job job)
        {
            //TODO: Implement Deleting
        }
    }
}

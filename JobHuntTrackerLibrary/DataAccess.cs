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
        //TODO: Handle exceptions and pass status messages back to caller for detailed reporting/logging in each method

        JobHuntTrackerAPIHelper _api = new JobHuntTrackerAPIHelper();
        List<Job> jobList;

        public async Task<List<Job>> GetJobs()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("api/jobs");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                jobList = JsonConvert.DeserializeObject<List<Job>>(result);
            }

            return jobList;
        }

        public async Task<bool> AddJob(Job job)
        {
            HttpClient client = _api.Initial();

            //POST to API
            HttpResponseMessage response = await client.PostAsync("api/jobs", new StringContent(
                new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json"));

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

        public async Task<bool> UpdateJob(Job job)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.PutAsync("api/jobs/" + job.Id, new StringContent(
                new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json"));

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

        public async Task<bool> DeleteJob(Job job)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.DeleteAsync("api/jobs/" + job.Id);

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

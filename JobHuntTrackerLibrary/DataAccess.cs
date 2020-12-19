 using JobHuntTracker.Models;
using JobHuntTrackerLibrary.Helper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntTrackerLibrary
{
    public class DataAccess
    {
        JobHuntTrackerAPIHelper _api = new JobHuntTrackerAPIHelper();
        List<Job> jobList;

        public async Task<List<Job>> GetJobs()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/jobs/");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                jobList = JsonConvert.DeserializeObject<List<Job>>(result);
            }

            return jobList;
        }

        public async Task AddJob(Job job)
        {
            //Send the job object to the API here

            Console.WriteLine("Not implemented yet");
        }
    }
}

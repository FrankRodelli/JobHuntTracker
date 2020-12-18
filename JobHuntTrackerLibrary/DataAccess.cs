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

        public static void AddJob(List<Job> jobs,
            string compnayName,
            string companyURL,
            string companyDescription,
            string jobTitle,
            string jobDescription,
            string contactEmail,
            string contactPhoneNumber,
            string ContactName,
            string interviewNotes,
            string engagementStage
            )
        {
            Job job = (new Job
            {
                CompanyName = compnayName,
                CompanyURL = companyURL,
                CompanyDescription = companyDescription,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                ContactEmail = contactEmail,
                ContactPhoneNumber = contactPhoneNumber,
                ContactName = ContactName,
                InterviewNotes = interviewNotes,
                EngagementStage = engagementStage
            });

            //We will send this job object to the API to be added to the database here

            Console.WriteLine(job);
        }
    }
}

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobHuntTrackerAPI.Models;

namespace JobHuntTrackerLibraryAPI.Services
{
    public class JobService
    {
        private readonly IMongoCollection<JobModel> _jobs;

        public JobService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("GridironFantasyDB"));
            var database = client.GetDatabase("JobHuntTracker");
            _jobs = database.GetCollection<JobModel>("Jobs");
        }

        public List<JobModel> Get()
        {
            //player was book before see if this is now a problem
            return _jobs.Find(job => true).ToList();
        }

        public JobModel Get(string docId)
        {
            return _jobs.Find<JobModel>(job => job.Id == docId).FirstOrDefault();
        }

        public JobModel Create(JobModel job)
        {
            _jobs.InsertOne(job);
            return job;
        }

        public void Update(string docId, JobModel jobIn)
        {

            _jobs.ReplaceOne(JobModel => JobModel.Id == docId, jobIn);
        }

        public void Remove(JobModel jobIn)
        {
            _jobs.DeleteOne(job => job.Id == jobIn.Id);
        }

        public void Remove(string id)
        {
            _jobs.DeleteOne(job => job.Id == id);
        }
    }
}
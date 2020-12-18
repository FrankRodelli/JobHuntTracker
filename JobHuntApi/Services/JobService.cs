using JobHuntApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobHuntApi.Services
{
    public class JobService
    {
        private readonly IMongoCollection<Job> _jobs;

        public JobService(IJobHuntTrackerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _jobs = database.GetCollection<Job>(settings.JobsCollectionName);
        }

        public List<Job> Get() =>
            _jobs.Find(job => true).ToList();

        public Job Get(string id) =>
            _jobs.Find<Job>(job => job.Id == id).FirstOrDefault();

        public Job Create(Job job)
        {
            _jobs.InsertOne(job);
            return job;
        }

        public void Update(string id, Job jobIn) =>
            _jobs.ReplaceOne(job => job.Id == id, jobIn);

        public void Remove(Job jobIn) =>
            _jobs.DeleteOne(job => job.Id == jobIn.Id);

        public void Remove(string id) =>
            _jobs.DeleteOne(job => job.Id == id);
    }
}

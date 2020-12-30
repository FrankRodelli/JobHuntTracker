using JobHuntTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntTrackerLibrary
{
    public class DataProcessor
    {
        public static List<Job> LoadJobs()
        {
            return DataAccess.GetJobs<Job>();
        }

        public static Job LoadJobs(string Id)
        {
            return DataAccess.GetJobs<Job>(Id);
        }

        public static bool AddJob(Job job)
        {
            return DataAccess.AddJob<Job>(job);
        }

        public static bool UpdateJob<T>(T newJob)
        {
            Job job = (Job)Convert.ChangeType(newJob, typeof(Job));
            string Id = job.Id;
            return DataAccess.UpdateJob<Job>(job, Id);
        }

        public static bool DeleteJob(Job job)
        {
            string Id = job.Id;
            return DataAccess.DeleteJob<Job>(job, Id);
        }
    }
}

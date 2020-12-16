using JobHuntTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntTrackerLibrary
{
    public class DataAccess
    {
        public static List<JobModel> GetJobModels()
        {
            List<JobModel> jobs = new List<JobModel>();
            jobs.Add(new JobModel
            {
                CompanyName = "test",
                CompanyURL = "testurl.com",
                CompanyDescription = "This is the description of the compnay",
                JobDescription = "This is the description of the job",
                ContactEmail = "testemails@mail.com",
                ContactPhoneNumber =  "727-727-7272",
                Applied = false
            });
            jobs.Add(new JobModel
            {
                CompanyName = "newtest",
                CompanyURL = "newtesturl.com",
                CompanyDescription = "This is the new description of the compnay",
                JobDescription = "This is the new description of the job",
                ContactEmail = "newtestemails@mail.com",
                ContactPhoneNumber = "696-969-6969",
                Applied = true
            });

            return jobs;
        }

        public static List<JobModel> AddJob(List<JobModel> jobs,
            string compnayName,
            string companyURL,
            string companyDescription,
            string jobDescription,
            string contactEmail,
            string contactPhoneNumber,
            bool applied)
        {
            jobs.Add(new JobModel
            {
                CompanyName = compnayName,
                CompanyURL = companyURL,
                CompanyDescription = companyDescription,
                JobDescription = jobDescription,
                ContactEmail = contactEmail,
                ContactPhoneNumber = contactPhoneNumber,
                Applied = applied
            });

            return jobs;
        }
    }
}

 using JobHuntTracker.Models;
using MongoDB.Driver;
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
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://fantasygridiron.azurewebsites.net/");
            return Client;
        }
        //mongo pw is PGt30PSGSqJ9
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
            });
            jobs.Add(new JobModel
            {
                CompanyName = "newtest",
                CompanyURL = "newtesturl.com",
                CompanyDescription = "This is the new description of the compnay",
                JobDescription = "This is the new description of the job",
                ContactEmail = "newtestemails@mail.com",
                ContactPhoneNumber = "696-969-6969",
            });

            OpenConnectio();
            return jobs;
        }

        public static List<JobModel> AddJob(List<JobModel> jobs,
            string compnayName,
            string companyURL,
            string companyDescription,
            string jobDescription,
            string contactEmail,
            string contactPhoneNumber)
        {
            jobs.Add(new JobModel
            {
                CompanyName = compnayName,
                CompanyURL = companyURL,
                CompanyDescription = companyDescription,
                JobDescription = jobDescription,
                ContactEmail = contactEmail,
                ContactPhoneNumber = contactPhoneNumber,
            });

            return jobs;
        }

        private static void OpenConnectio()
        {

            var client = new MongoClient("mongodb+srv://admin:<password>@jobhunttrackercluster.3pngf.mongodb.net/<dbname>?retryWrites=true&w=majority");
            var database = client.GetDatabase("JobHuntTracker");

            List<JobModel> jobs = (List<JobModel>)database.GetCollection<JobModel>("Jobs");
            Console.WriteLine(jobs);

        }
    }
}

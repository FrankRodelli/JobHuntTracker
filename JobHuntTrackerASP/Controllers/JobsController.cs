using JobHuntTrackerASP.Models;
using JobHuntTrackerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JobHuntTrackerASP.Controllers
{
    public class JobsController : Controller
    {
        // GET: Jobs
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Jobs";
            return View(GetJobs());
        }

        // GET: Jobs/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Details";
            List<Job> jobs = GetJobs();

            //Returns job if found by id or nothing
            foreach (var testJob in jobs)
            {
                if (testJob.Id == id)
                {
                    return View(testJob);
                }
            }

            return View();
        }

        // GET: Jobs/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Job job)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/");

                HttpResponseMessage response = client.PostAsync("api/jobs/", new StringContent(
                        new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                client.Dispose();

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }
            return RedirectToAction("Index");
        }


        // GET: Jobs/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            var row = DataProcessor.LoadJobs(id);
            Job job = new Job
            {
                Id = row.Id,
                CompanyName = row.CompanyName,
                CompanyURL = row.CompanyURL,
                CompanyDescription = row.CompanyDescription,
                JobTitle = row.JobTitle,
                JobDescription = row.JobDescription,
                ContactEmail = row.ContactEmail,
                ContactPhoneNumber = row.ContactPhoneNumber,
                ContactName = row.ContactName,
                InterviewNotes = row.InterviewNotes,
                EngagementStage = row.EngagementStage
            };
            
            return View(job);
        }

        // POST: Jobs/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Job job)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/");

                HttpResponseMessage response = client.PutAsync("api/jobs/" + job.Id, new StringContent(
                        new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                client.Dispose();

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }
        return RedirectToAction("Index");
        }

        // GET: Jobs/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            var row = DataProcessor.LoadJobs(id);
            Job job = new Job
            {
                Id = row.Id,
                CompanyName = row.CompanyName,
                CompanyURL = row.CompanyURL,
                CompanyDescription = row.CompanyDescription,
                JobTitle = row.JobTitle,
                JobDescription = row.JobDescription,
                ContactEmail = row.ContactEmail,
                ContactPhoneNumber = row.ContactPhoneNumber,
                ContactName = row.ContactName,
                InterviewNotes = row.InterviewNotes,
                EngagementStage = row.EngagementStage
            };

            return View(job);
        }

        // POST: Jobs/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(Job job)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/");

                HttpResponseMessage response = client.DeleteAsync("api/jobs/" + job.Id).GetAwaiter().GetResult();

                client.Dispose();

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }
            return RedirectToAction("Index");
        }

        public List<Job> GetJobs()
        {
            List<Job> jobs = new List<Job>();
            var data = DataProcessor.LoadJobs();

            foreach (var row in data)
            {
                jobs.Add(new Job
                {
                    Id = row.Id,
                    CompanyName = row.CompanyName,
                    CompanyURL = row.CompanyURL,
                    CompanyDescription = row.CompanyDescription,
                    JobTitle = row.JobTitle,
                    JobDescription = row.JobDescription,
                    ContactEmail = row.ContactEmail,
                    ContactPhoneNumber = row.ContactPhoneNumber,
                    ContactName = row.ContactName,
                    InterviewNotes = row.InterviewNotes,
                    EngagementStage = row.EngagementStage
                });
            }

            return jobs;
        }
    }
}

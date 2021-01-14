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
using Microsoft.AspNet.Identity;

namespace JobHuntTrackerASP.Controllers
{
    public class JobsController : Controller
    {

        //TODO: Use User.Identity.GetUserId() to get user ID and get info from database
        //TODO: Either change structure of MONGODB to accomodate user ID's or switch to SQL to get relational styles
        // GET: Jobs

        private List<Job> _jobList = new List<Job>();
        private bool jobsUpdated = true;

        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            if (jobsUpdated){
                _jobList = GetJobs(User.Identity.GetUserId());
        }
            jobsUpdated = false;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "ComapnyName" : "";
            //ViewBag.DateSortParm = sortOrder == "" ? "date_desc" : "Date";
            //ViewBag.CompanyNameSort = 

            //Grab copy of jobs list so we can reuse the original list without an API call
            var sortedJobs = from s in _jobList select s;

            switch (sortOrder)
            {
                case "Company Name":
                    sortedJobs = sortedJobs.OrderByDescending(s => s.CompanyName);
                    break;
                case "Job Title":
                    sortedJobs = sortedJobs.OrderBy(s => s.JobTitle);
                    break;
                case "Engagement Stage":
                    sortedJobs = sortedJobs.OrderByDescending(s => s.EngagementStage);
                    break;
                default:
                    sortedJobs = sortedJobs.OrderBy(s => s.CompanyName);
                    break;
            }
            return View(sortedJobs.ToList());
        }


        // GET: Jobs/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Details";
            var row = DataProcessor.LoadJobsByID(id);

            Job job = new Job
            {
                Id = row.Id,
                UserID = User.Identity.GetUserId(),
                CompanyName = row.CompanyName,
                CompanyURL = row.CompanyURL,
                CompanyDescription = row.CompanyDescription,
                CompanyLocation = row.CompanyLocation,
                JobTitle = row.JobTitle,
                JobDescription = row.JobDescription,
                JobType = row.JobType,
                Pay = row.Pay,
                PayType = row.PayType,
                ContactEmail = row.ContactEmail,
                ContactPhoneNumber = row.ContactPhoneNumber,
                ContactName = row.ContactName,
                InterviewNotes = row.InterviewNotes,
                PersonalNotes = row.PersonalNotes,
                EngagementStage = row.EngagementStage
            };

            jobsUpdated = true;
            return View(job);
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
            job.UserID = User.Identity.GetUserId();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/");

                HttpResponseMessage response = client.PostAsync("api/jobs/", new StringContent(
                        new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                client.Dispose();

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }
            jobsUpdated = true;
            return RedirectToAction("Index");
        }


        // GET: Jobs/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            var row = DataProcessor.LoadJobsByID(id);
            Job job = new Job
            {
                Id = row.Id,
                UserID = User.Identity.GetUserId(),
                CompanyName = row.CompanyName,
                CompanyURL = row.CompanyURL,
                CompanyDescription = row.CompanyDescription,
                CompanyLocation = row.CompanyLocation,
                JobTitle = row.JobTitle,
                JobDescription = row.JobDescription,
                JobType = row.JobType,
                Pay = row.Pay,
                PayType = row.PayType,
                ContactEmail = row.ContactEmail,
                ContactPhoneNumber = row.ContactPhoneNumber,
                ContactName = row.ContactName,
                InterviewNotes = row.InterviewNotes,
                PersonalNotes = row.PersonalNotes,
                EngagementStage = row.EngagementStage
            };
            
            return View(job);
        }

        // POST: Jobs/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Job job)
        {
            job.UserID = User.Identity.GetUserId();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/");

                HttpResponseMessage response = client.PutAsync("api/jobs/" + job.Id, new StringContent(
                        new JavaScriptSerializer().Serialize(job), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                client.Dispose();

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }
            jobsUpdated = true;
            return RedirectToAction("Index");
        }

        // GET: Jobs/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            var row = DataProcessor.LoadJobsByID(id);
            Job job = new Job
            {
                Id = row.Id,
                UserID = User.Identity.GetUserId(),
                CompanyName = row.CompanyName,
                CompanyURL = row.CompanyURL,
                CompanyDescription = row.CompanyDescription,
                CompanyLocation = row.CompanyLocation,
                JobTitle = row.JobTitle,
                JobDescription = row.JobDescription,
                JobType = row.JobType,
                Pay = row.Pay,
                PayType = row.PayType,
                ContactEmail = row.ContactEmail,
                ContactPhoneNumber = row.ContactPhoneNumber,
                ContactName = row.ContactName,
                InterviewNotes = row.InterviewNotes,
                PersonalNotes = row.PersonalNotes,
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
            jobsUpdated = true;
            return RedirectToAction("Index");
        }

        public List<Job> GetJobs(string id)
        {
            List<Job> jobs = new List<Job>();
            var data = DataProcessor.LoadJobs(id);

            foreach (var row in data)
            {
                jobs.Add(new Job
                {
                    Id = row.Id,
                    UserID = User.Identity.GetUserId(),
                    CompanyName = row.CompanyName,
                    CompanyURL = row.CompanyURL,
                    CompanyDescription = row.CompanyDescription,
                    CompanyLocation = row.CompanyLocation,
                    JobTitle = row.JobTitle,
                    JobDescription = row.JobDescription,
                    JobType = row.JobType,
                    Pay = row.Pay,
                    PayType = row.PayType,
                    ContactEmail = row.ContactEmail,
                    ContactPhoneNumber = row.ContactPhoneNumber,
                    ContactName = row.ContactName,
                    InterviewNotes = row.InterviewNotes,
                    PersonalNotes = row.PersonalNotes,
                    EngagementStage = row.EngagementStage
                });
            }

            return jobs;
        }
    }
}

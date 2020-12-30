using JobHuntTrackerASP.Models;
using JobHuntTrackerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHuntTrackerASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ViewJobs()
        {
            ViewBag.Message = "Jobs";

            var data = DataProcessor.LoadJobs();

            List<JobList> jobs = new List<JobList>();

            foreach (var row in data)
            {
                jobs.Add(new JobList
                {
                    CompanyName = row.CompanyName,
                    JobTitle = row.JobTitle,
                    EngagementStage = row.EngagementStage
                });
            }

            return View(jobs);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHuntTrackerASP.Models
{
    public class Job
    {
        public string Id { get; set; }

        public string UserID { get; set; }

        //Company Description Props
        public string CompanyName { get; set; }
        public string CompanyURL { get; set; }
        public string CompanyDescription { get; set; }
        //Job Description Props
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        //Contact Props
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactName { get; set; }
        //Status Props
        public string InterviewNotes { get; set; }
        public string EngagementStage { get; set; }
    }
}
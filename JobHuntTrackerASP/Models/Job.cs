using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobHuntTrackerASP.Models
{
    public class Job
    {
        public string Id { get; set; }

        public string UserID { get; set; }

        //Company Description Props
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company URL")]
        public string CompanyURL { get; set; }

        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; }

        [Display(Name = "Company Location")]
        public string CompanyLocation { get; set; }

        //Job Description Props
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Display(Name = "Pay")]
        [DataType(DataType.Currency)]
        public string Pay { get; set; }

        [Display(Name = "Pay Type")]
        public string PayType { get; set; }

        //Contact Props
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Phone Number")]
        public string ContactPhoneNumber { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        //Status Props
        [Display(Name = "Interview Notes")]
        [DataType(DataType.MultilineText)]
        public string InterviewNotes { get; set; }

        [Display(Name = "Personal Notes")]
        [DataType(DataType.MultilineText)]
        public string PersonalNotes { get; set; }

        [Display(Name = "Engagement Stage")]
        public string EngagementStage { get; set; }

    }
}

public enum PayType
{
    Salary,
    Hourly
}

public enum JobType
{
    Contract,
    PartTime,
    FullTime
}

public enum EngagementStage
{
    Applied,
    Interviewing,
    NotSelected,
    OfferedPosition,
    Hired,
    

}
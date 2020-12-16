using System;
using System.Collections.Generic;
using System.Text;

namespace JobHuntTracker.Models
{
    public class JobModel
    {
        public string CompanyName { get; set; }
        public string CompanyURL { get; set; }
        public string CompanyDescription { get; set; }
        public string JobDescription { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }

        public bool Applied { get; set; }
    }
}

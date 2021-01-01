using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobHuntTrackerLibrary.Models
{
    public class Job 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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

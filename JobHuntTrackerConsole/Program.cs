using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobHuntTracker.Models;
using JobHuntTrackerLibrary;
using MongoDB.Driver;

namespace JobHuntTrackerConsole
{
    class Program
    {
        static List<Job> jobs;
        static DataAccess dataAccess;
        public static async Task Main(string[] args)
        {
            dataAccess = new DataAccess();

            //Populate Job list
            await LoadJobs();

            await DisplayMenu();

            

            Console.ReadLine();

        }

        private static async Task DisplayMenu()
        {
            bool validSelection = false;
            do
            {
                Console.WriteLine("1. Add New Job\n" +
                "2. View Jobs\n\n" +
                "Type Selection: ");

                string menuSelection = Console.ReadLine();

                if (menuSelection == "1" || menuSelection == "2")
                {
                    if (menuSelection == "1")
                    {
                        await AddJob();
                    }
                    else
                    {
                        await DisplayJobs();
                    }
                    validSelection = true;
                }
                else
                {
                    Console.WriteLine("Bad Selection");
                }
            } while (!validSelection);

            await DisplayMenu();
        }

        private static async Task DisplayJobs()
        {
            foreach (Job j in jobs)
            {
                Console.WriteLine($"The Company is {j.CompanyName} and they are " +
                    $"{j.CompanyDescription}. Their URL is {j.CompanyURL}\n" +
                    $"The job title is {j.JobTitle} and the job description is {j.JobDescription}\n\n" +
                    $"Contact Info:\n{j.ContactEmail}\n{j.ContactPhoneNumber}\n{j.ContactName}\n\n" +
                    $"Interview Notes: {j.InterviewNotes}\nEngagement Stage: {j.EngagementStage}\n\n");
            }
            Console.WriteLine();
            await DisplayMenu();
        }

        private static async Task AddJob()
        {
            //Adjust this here to return a job model instead of each variable. It'll clean things up
            Job newJob = new Job();
            string n, u, d, j, e, p;
            Console.WriteLine("Enter Compnay Name: ");
            n = Console.ReadLine();
            Console.WriteLine("Enter Compnay URL: ");
            u = Console.ReadLine();
            Console.WriteLine("Enter Compnay Description: ");
            d = Console.ReadLine();
            Console.WriteLine("Enter Job Description: ");
            j = Console.ReadLine();
            Console.WriteLine("Enter Contact Email: ");
            e = Console.ReadLine();
            Console.WriteLine("Enter Contact Phone Number: ");
            p = Console.ReadLine();


            Console.WriteLine();
            await dataAccess.AddJob(newJob);
        }

        //This could be useful in the future for 
        //verifying selection; may want to simplify it and make it more dry for future use
        private static bool verifySelection()
        {
            bool validSelection = false;
            bool applied = false;
            string a;
            do
            {
                Console.WriteLine("Have You Applied? (Y/N): ");
                a = Console.ReadLine();
                if (a.ToLower() == "y" || a.ToLower() == "n")
                {
                    if (a.ToLower() == "y")
                    {
                        applied = true;
                        validSelection = true;
                    }
                    else
                    {
                        validSelection = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Entry..Try Again\n");
                }
            } while (!validSelection);

            return applied;
        }

        private static async Task LoadJobs()
        {
            jobs = await dataAccess.GetJobs();
        }
    }
}

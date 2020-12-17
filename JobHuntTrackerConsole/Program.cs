using System;
using System.Collections.Generic;
using JobHuntTracker.Models;
using JobHuntTrackerLibrary;
using MongoDB.Driver;

namespace JobHuntTrackerConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Populate Job array
            List<JobModel> jobs = LoadJobs();

            DisplayMenu(jobs);



        }

        private static void DisplayMenu(List<JobModel> jobs)
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
                        jobs = AddJob(jobs);
                    }
                    else
                    {
                        DisplayJobs(jobs);
                    }
                    validSelection = true;
                }
                else
                {
                    Console.WriteLine("Bad Selection");
                }
            } while (!validSelection);

            DisplayMenu(jobs);
        }

        private static void DisplayJobs(List<JobModel> jobs)
        {
            foreach(JobModel job in jobs)
            {
                Console.WriteLine($"{job.CompanyName}");
            }
            Console.WriteLine();
            DisplayMenu(jobs);
        }

        private static List<JobModel> AddJob(List<JobModel> jobs)
        {
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
            return DataAccess.AddJob(jobs,n,u,d,j,e,p);
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

        private static List<JobModel> LoadJobs()
        {
            return DataAccess.GetJobModels();
        }
    }
}

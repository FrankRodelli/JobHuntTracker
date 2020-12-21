using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobHuntTrackerLibrary.Models;
using JobHuntTrackerLibrary;
using WindowsInput;

namespace JobHuntTrackerConsole
{
    class Program
    {
        //TODO: This needs exception handling. I don't plan to release this as it is just for developing the API and class library, but it would be nice
        static List<Job> jobs;
        static DataAccess dataAccess;
        public static async Task Main(string[] args)
        {
            dataAccess = new DataAccess();
            await LoadJobs();
            await DisplayMenu();

        }

        private static async Task DisplayMenu()
        {
            bool validSelection = false;
            do
            {
                Console.WriteLine("1. Add New Job\n" +
                "2. View Jobs\n" +
                "3. Delete Job\n" +
                "4. Edit Job\n\n"+
                "Type Selection: ");

                string menuSelection = Console.ReadLine();

                if (menuSelection == "1" || menuSelection == "2" || menuSelection == "3" || menuSelection == "4")
                {
                    if (menuSelection == "1")
                    {
                        await AddJob();
                    }
                    if (menuSelection == "2")
                    {
                        await DisplayJobs();
                    }
                    if(menuSelection == "3")
                    {
                        await DeleteJob();
                    }
                    if(menuSelection == "4")
                    {
                        await EditJob();
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

        private static async Task EditJob()
        {
            int jobNumber = 1;
            Console.WriteLine();

            foreach (Job j in jobs)
            {

                Console.WriteLine($"{jobNumber}. {j.CompanyName}\n");
                jobNumber += 1;
            }

            Console.WriteLine("\nWhat job number do you want to edit?: ");

            Job jobSelection = jobs[Int32.Parse(Console.ReadLine()) -1];

            PropertyInfo[] props = typeof(Job).GetProperties();

            InputSimulator inputSim = new InputSimulator();

            foreach(PropertyInfo property in props)
            {
                if (property.Name != "Id")
                {
                    Console.WriteLine("Press Enter when finished editing..");
                    inputSim.Keyboard.TextEntry((string)property.GetValue(jobSelection));
                    property.SetValue(jobSelection, Console.ReadLine());
                }
            }

            if (!await dataAccess.UpdateJob(jobSelection))
            {
                Console.WriteLine("Error deleting item from database!");
            }

            await LoadJobs();

        }

        private static async Task DeleteJob()
        {
            int jobNumber = 1;
            foreach(Job j in jobs)
            {

                Console.WriteLine($"{jobNumber}. {j.CompanyName}\n");
                jobNumber += 1;
            }

            Console.WriteLine("\nWhat job number do you want to delete?: ");

            if(!await dataAccess.DeleteJob(jobs[Int32.Parse(Console.ReadLine()) -1 ]))
            {
                Console.WriteLine("Error deleting item from database!");
            }

            await LoadJobs();
        }

        private static async Task DisplayJobs()
        {
            await LoadJobs();

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
            Job newJob = new Job();
            Console.WriteLine("Enter Compnay Name: ");
            newJob.CompanyName = Console.ReadLine();
            Console.WriteLine("Enter Compnay URL: ");
            newJob.CompanyURL = Console.ReadLine();
            Console.WriteLine("Enter Compnay Description: ");
            newJob.CompanyDescription = Console.ReadLine();
            Console.WriteLine("Enter Job Title: ");
            newJob.JobTitle = Console.ReadLine();
            Console.WriteLine("Enter Job Description: ");
            newJob.JobDescription = Console.ReadLine();
            Console.WriteLine("Enter Contact Email: ");
            newJob.ContactEmail = Console.ReadLine();
            Console.WriteLine("Enter Contact Phone Number: ");
            newJob.ContactPhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter Contact Name");
            newJob.ContactName = Console.ReadLine();
            Console.WriteLine("Enter Interview Notes: ");
            newJob.InterviewNotes = Console.ReadLine();
            Console.WriteLine("Enter Engagement Stage: ");
            newJob.EngagementStage = Console.ReadLine();

            Console.WriteLine();

            //TODO: Return the status code for more detailed description of what went wrong
            if(!await dataAccess.AddJob(newJob))
            {
                Console.WriteLine("Error adding item to database!");
            }
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

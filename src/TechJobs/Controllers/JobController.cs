using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 DONE -get the Job with the given ID and pass it into the view
            //Get this screen after adding a job *hint*
            //can add validation to make sure id is valid later just in case the user types something in

            Job singleJob = jobData.Find(id);
            return View(singleJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 DONE - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if(ModelState.IsValid)
            {
                //make job from new job
                Job newJob = new Job {

                    

                    //so fundamentally enums are just ints right? and we have ints so..
                    Name = newJobViewModel.Name,
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),//WINNER!!!!!!!!!!!!!!!!!
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)
                };
                jobData.Jobs.Add(newJob);

                return Redirect($"/job?id={newJob.ID}");
            }

            return View(newJobViewModel);
        }
    }
}

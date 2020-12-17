using JobHuntTrackerAPI.Models;
using JobHuntTrackerLibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobHuntTrackerAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobService _jobService;

        public JobsController(JobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public ActionResult<List<JobModel>> Get()
        {
            return _jobService.Get();
        }

        [HttpGet("{id}", Name = "GetJob")]
        public ActionResult<JobModel> Get(string id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        [HttpPost]
        public ActionResult<JobModel> Create(JobModel job)
        {
            _jobService.Create(job);

            return CreatedAtRoute("GetJob", new { id = job.Id.ToString() }, job);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, JobModel jobIn)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            _jobService.Update(id, jobIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            _jobService.Remove(job.Id);

            return NoContent();
        }
    }
}

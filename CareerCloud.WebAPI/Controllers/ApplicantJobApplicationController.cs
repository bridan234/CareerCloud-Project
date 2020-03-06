using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1/")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationController()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic (repo);
        }

        [HttpGet, Route("jobapplication/{Id}")]
        public ActionResult GetApplicantJobApplication(Guid Id)
        {
            var poco = _logic.Get(Id);
            
            if (poco==null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("jobapplication")]
        public ActionResult GetAllApplicantJobApplication()
        {
            var poco = _logic.GetAll();
            
            if (poco==null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("jobapplication")]
        public ActionResult PostApplicantJobApplication([FromBody]ApplicantJobApplicationPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("jobapplication")]
        public ActionResult PutApplicantJobApplication([FromBody]ApplicantJobApplicationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("jobapplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody]ApplicantJobApplicationPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}
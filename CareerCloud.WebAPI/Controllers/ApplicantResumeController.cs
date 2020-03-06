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
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }

        [HttpGet, Route("resume/{Id}")]
        public ActionResult GetApplicantResume(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("resume")]
        public ActionResult GetAllApplicantResume()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("resume")]
        public ActionResult PostApplicantResume([FromBody]ApplicantResumePoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }
        
        [HttpPut, Route("resume")]
        public ActionResult PutApplicantResume([FromBody]ApplicantResumePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("resume")]
        public ActionResult DeleteApplicantResume([FromBody]ApplicantResumePoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }

    }
}
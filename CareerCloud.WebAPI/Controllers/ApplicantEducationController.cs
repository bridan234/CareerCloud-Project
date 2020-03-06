using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1/")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }

        [HttpGet, Route("education/{Id}")]
        public ActionResult GetApplicantEducation(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("education")]
        public ActionResult GetAllApplicantEducation()
        {
            var AllApplicants = _logic.GetAll();

            if (AllApplicants == null) return NotFound();

            return Ok(AllApplicants);
        }

        [HttpPost, Route("education")]
        public ActionResult PostApplicantEducation([FromBody]ApplicantEducationPoco [] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("education")]
        public ActionResult PutApplicantEducation([FromBody]ApplicantEducationPoco [] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("education")]
        public ActionResult DeleteApplicantEducation([FromBody]ApplicantEducationPoco [] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }

    }
}
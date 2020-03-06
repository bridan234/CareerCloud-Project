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
    [Route("api/careercloud/applicant/vi/")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _Logic;

        public ApplicantProfileController()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>();
            _Logic = new ApplicantProfileLogic(repo);
        }

        [HttpGet, Route("profile/{Id}")]
        public ActionResult GetApplicantProfile(Guid Id)
        {
            var poco = _Logic.Get(Id);

            if (poco == null) return NotFound();
            
            return Ok(poco);
        }

        [HttpGet, Route("profile")]
        public ActionResult GetApplicantProfile()
        {
            var poco = _Logic.GetAll();

            if (poco == null) return NotFound();
            
            return Ok(poco);
        }

        [HttpPost,Route("profile")]
        public ActionResult PostApplicantProfile([FromBody]ApplicantProfilePoco[] poco)
        {
            _Logic.Add(poco);
            return Ok();
        }

        [HttpPut,Route("profile")]
        public ActionResult PutApplicantProfile([FromBody]ApplicantProfilePoco[] poco)
        {
            _Logic.Update(poco);
            return Ok();
        }

        [HttpDelete,Route("profile")]
        public ActionResult DeleteApplicantProfile([FromBody]ApplicantProfilePoco[] poco)
        {
            _Logic.Delete(poco);
            return Ok();
        }
    }
}
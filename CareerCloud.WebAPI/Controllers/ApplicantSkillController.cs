using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1/")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillController()
        {
            var repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }

        [HttpGet, Route("skill/{Id}")]
        public ActionResult GetApplicantSkill(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("skill")]
        public ActionResult GetAllApplicantSkill()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("skill")]
        public ActionResult PostApplicantSkill([FromBody]ApplicantSkillPoco[] poco)
        {
            _logic.Add(poco);

            return Ok();
        }

        [HttpPut, Route("skill")]
        public ActionResult PutApplicantSkill([FromBody]ApplicantSkillPoco[] poco)
        {
            _logic.Update(poco);

            return Ok();
        }

        [HttpDelete, Route("skill")]
        public ActionResult DeleteApplicantSkill([FromBody]ApplicantSkillPoco[] poco)
        {
            _logic.Delete(poco);

            return Ok();
        }
    }
}
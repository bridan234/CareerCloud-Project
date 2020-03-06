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
    [Route("api/careercloud/company/v1/")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillController()
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet, Route("jobskill/{Id}")]
        public ActionResult GetCompanyJobSkill(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("jobskill")]
        public ActionResult GetAllCompanyJobSkill()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("jobskill")]
        public ActionResult PostCompanyJobSkill([FromBody]CompanyJobSkillPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("jobskill")]
        public ActionResult PutCompanyJobSkill([FromBody]CompanyJobSkillPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("jobskill")]
        public ActionResult DeleteCompanyJobSkill([FromBody]CompanyJobSkillPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}
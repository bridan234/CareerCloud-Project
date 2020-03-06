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
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationController()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);
        }

        [HttpGet, Route("jobeducation/{Id}")]
        public ActionResult GetCompanyJobEducation(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco ==null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("jobeducation")]
        public ActionResult GetAllCompanyJobEducation()
        {
            var poco = _logic.GetAll();

            if (poco ==null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("jobeducation")]
        public ActionResult PostCompanyJobEducation([FromBody]CompanyJobEducationPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("jobeducation")]
        public ActionResult PutCompanyJobEducation([FromBody]CompanyJobEducationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("jobeducation")]
        public ActionResult DeleteCompanyJobEducation([FromBody]CompanyJobEducationPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }

    }
}
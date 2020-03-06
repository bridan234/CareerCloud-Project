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
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobsDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }

        [HttpGet, Route("jobdescription/{Id}")]
        public ActionResult GetCompanyJobsDescription(Guid Id)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(Id);

            if(poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("jobdescription")]
        public ActionResult GetAllCompanyJobsDescription()
        {
            List<CompanyJobDescriptionPoco> poco = _logic.GetAll();

            if(poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("jobdescription")]
        public ActionResult PostCompanyJobsDescription([FromBody]CompanyJobDescriptionPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("jobdescription")]
        public ActionResult PutCompanyJobsDescription([FromBody]CompanyJobDescriptionPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("jobdescription")]
        public ActionResult DeleteCompanyJobsDescription([FromBody]CompanyJobDescriptionPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}
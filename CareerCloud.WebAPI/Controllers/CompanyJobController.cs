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
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobController()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }

        [HttpGet, Route("job/{Id}")]
        public ActionResult GetCompanyJob (Guid Id)
        {
            var poco = _logic.Get(Id);

            if(poco == null) return NotFound();
            
            return Ok(poco);
        }

        [HttpGet, Route("job")]
        public ActionResult GetAllCompanyJob ()
        {
            var poco = _logic.GetAll();

            if(poco == null) return NotFound();
            
            return Ok(poco);
        }

        [HttpPost, Route("job")]
        public ActionResult PostCompanyJob ([FromBody]CompanyJobPoco[] poco)
        {
            _logic.Add(poco);
            
            return Ok();
        }

        [HttpPut, Route("job")]
        public ActionResult PutCompanyJob ([FromBody]CompanyJobPoco[] poco)
        {
            _logic.Update(poco);
            
            return Ok();
        }

        [HttpDelete, Route("job")]
        public ActionResult DeleteCompanyJob ([FromBody]CompanyJobPoco[] poco)
        {
            _logic.Delete(poco);
            
            return Ok();
        }
    }
}
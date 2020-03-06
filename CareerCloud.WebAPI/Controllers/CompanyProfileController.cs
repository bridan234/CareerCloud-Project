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
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController()
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        [HttpGet, Route("profile/{Id}")]
        public ActionResult GetCompanyProfile(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("profile")]
        public ActionResult GetAllCompanyProfile()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("profile")]
        public ActionResult PostCompanyProfile([FromBody]CompanyProfilePoco[] poco)
        {
           _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("profile")]
        public ActionResult PutCompanyProfile([FromBody]CompanyProfilePoco[] poco)
        {
           _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("profile")]
        public ActionResult DeleteCompanyProfile([FromBody]CompanyProfilePoco[] poco)
        {
           _logic.Delete(poco);
            return Ok();
        }
    }
}
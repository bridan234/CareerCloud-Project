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
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationController()
        {
            var repo =  new EFGenericRepository<CompanyLocationPoco>();

            _logic = new CompanyLocationLogic(repo);
        }

        [HttpGet, Route("location/{Id}")]
        public ActionResult GetCompanyLocation(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco ==null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("location")]
        public ActionResult GetAllCompanyLocation()
        {
            var poco = _logic.GetAll();

            if (poco ==null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("location")]
        public ActionResult PostCompanyLocation([FromBody]CompanyLocationPoco[] poco)
        {
            _logic.Add(poco);

            return Ok();
        }

        [HttpPut, Route("location")]
        public ActionResult PutCompanyLocation([FromBody]CompanyLocationPoco[] poco)
        {
            _logic.Update(poco);

            return Ok();
        }

        [HttpDelete, Route("location")]
        public ActionResult DeleteCompanyLocation([FromBody]CompanyLocationPoco[] poco)
        {
            _logic.Add(poco);

            return Ok();
        }
    }
}
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
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);
        }

        [HttpGet, Route("description/{Id}")]
        public ActionResult GetCompanyDescription(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("description")]
        public ActionResult GetAllCompanyDescription()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("description")]
        public ActionResult PostCompanyDescription([FromBody]CompanyDescriptionPoco[] poco)
        {
            _logic.Add(poco);

            return Ok();
        }

        [HttpPut, Route("description")]
        public ActionResult PutCompanyDescription([FromBody]CompanyDescriptionPoco[] poco)
        {
            _logic.Update(poco);

            return Ok();
        }

        [HttpDelete, Route("description")]
        public ActionResult DeleteCompanyDescription([FromBody]CompanyDescriptionPoco[] poco)
        {
            _logic.Delete(poco);

            return Ok();
        }
    }
}
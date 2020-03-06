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
    [Route("api/careercloud/system/v1/")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeController()
        {
            var repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet, Route("countrycode/{Code}")]
        public ActionResult GetSystemCountryCode(string Code)
        {
            var poco = _logic.Get(Code);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("countrycode")]
        public ActionResult GetAllSystemCountryCode()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("countrycode")]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco [] poco)
        {
             _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("countrycode")]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco [] poco)
        {
             _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("countrycode")]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco [] poco)
        {
             _logic.Delete(poco);
            return Ok();
        }
    }
}
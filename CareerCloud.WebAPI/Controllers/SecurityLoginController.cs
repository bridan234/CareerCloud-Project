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
    [Route("api/careercloud/security/v1/")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginController()
        {
            var repo =  new EFGenericRepository<SecurityLoginPoco>();

            _logic = new SecurityLoginLogic(repo);
        }

        [HttpGet, Route("login/{Id}")]
        public ActionResult GetSecurityLogin (Guid Id)
        {
            var pocos = _logic.Get(Id);

            if(pocos ==null) return NotFound();

            return Ok(pocos);

        }

        [HttpGet, Route("login")]
        public ActionResult GetAllSecurityLogin ()
        {
            var pocos = _logic.GetAll();

            if(pocos ==null) return NotFound();

            return Ok(pocos);

        }

        [HttpPost, Route("login")]
        public ActionResult PostSecurityLogin ([FromBody]SecurityLoginPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();

        }

        [HttpPut, Route("login")]
        public ActionResult PutSecurityLogin ([FromBody]SecurityLoginPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();

        }

        [HttpDelete, Route("login")]
        public ActionResult DeleteSecurityLogin ([FromBody]SecurityLoginPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();

        }
    }
}
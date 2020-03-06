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
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogController()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();

            _logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet, Route("loginslog/{Id}")]
        public ActionResult GetSecurityLoginLog (Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("loginslog")]
        public ActionResult GetAllSecurityLoginsLog ()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("loginslog")]
        public ActionResult PostSecurityLoginLog ([FromBody]SecurityLoginsLogPoco[] poco)
        {
             _logic.Add(poco);
             return Ok();
        }

        [HttpPut, Route("loginslog")]
        public ActionResult PutSecurityLoginLog ([FromBody]SecurityLoginsLogPoco[] poco)
        {
             _logic.Update(poco);
             return Ok();
        }

        [HttpDelete, Route("loginslog")]
        public ActionResult DeleteSecurityLoginLog ([FromBody]SecurityLoginsLogPoco[] poco)
        {
             _logic.Delete(poco);
             return Ok();
        }
    }
}
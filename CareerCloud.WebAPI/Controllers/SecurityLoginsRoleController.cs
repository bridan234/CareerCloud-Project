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
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleController()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);
        }

        [HttpGet, Route("loginsrole/{Id}")]
        public ActionResult GetSecurityLoginsRole(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco ==null ) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("loginsrole")]
        public ActionResult GetAllSecurityLoginsRole()
        {
            var poco = _logic.GetAll();

            if (poco ==null ) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("loginsrole")]
        public ActionResult PostSecurityLoginRole([FromBody]SecurityLoginsRolePoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("loginsrole")]
        public ActionResult PutSecurityLoginRole([FromBody]SecurityLoginsRolePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("loginsrole")]
        public ActionResult DeleteSecurityLoginRole([FromBody]SecurityLoginsRolePoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}
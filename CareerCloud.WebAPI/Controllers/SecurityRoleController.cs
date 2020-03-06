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
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleController()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }

        [HttpGet, Route("role/{Id}")]
        public ActionResult GetSecurityRole(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco ==null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("role")]
        public ActionResult GetAllSecurityRole()
        {
            var poco = _logic.GetAll();

            if (poco ==null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("role")]
        public ActionResult PostSecurityRole([FromBody]SecurityRolePoco[] poco)
        {
           _logic.Add(poco);
           return Ok();
        }

        [HttpPut, Route("role")]
        public ActionResult PutSecurityRole([FromBody]SecurityRolePoco[] poco)
        {
           _logic.Update(poco);
           return Ok();
        }

        [HttpDelete, Route("role")]
        public ActionResult DeleteSecurityRole([FromBody]SecurityRolePoco[] poco)
        {
           _logic.Delete(poco);
           return Ok();
        }
    }
}
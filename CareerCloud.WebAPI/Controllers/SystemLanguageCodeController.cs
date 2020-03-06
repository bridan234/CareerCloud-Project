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
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeController()
        {   
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }

        [HttpGet, Route("languagecode/{LanguageId}")]
        public ActionResult GetSystemLanguageCode(string LanguageId)
        {
            var poco = _logic.Get(LanguageId);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("languagecode")]
        public ActionResult GetAllSystemLanguageCode()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("languagecode")]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco [] poco)
        {
           _logic.Add(poco);
            return Ok();
        }

        [HttpPut, Route("languagecode")]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco [] poco)
        {
           _logic.Update(poco);
            return Ok();
        }

        [HttpDelete, Route("languagecode")]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco [] poco)
        {
           _logic.Delete(poco);
            return Ok();
        }
    }
}
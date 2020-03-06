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
    [Route("api/careercloud/applicant/v1/")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryController()
        {
            var repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logic = new ApplicantWorkHistoryLogic(repo);
        }

        [HttpGet, Route("workhistory/{Id}")]
        public ActionResult GetApplicantWorkHistory(Guid Id)
        {
            var poco = _logic.Get(Id);

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpGet, Route("workhistory")]
        public ActionResult GetAllApplicantWorkHistory()
        {
            var poco = _logic.GetAll();

            if (poco == null) return NotFound();

            return Ok(poco);
        }

        [HttpPost, Route("workhistory")]
        public ActionResult PostApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] poco)
        {
            _logic.Add(poco);

            return Ok();
        }

        [HttpPut, Route("workhistory")]
        public ActionResult PutApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] poco)
        {
            _logic.Update(poco);

            return Ok();
        }

        [HttpDelete, Route("workhistory")]
        public ActionResult DeleteApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] poco)
        {
            _logic.Delete(poco);

            return Ok();
        }
    }
}
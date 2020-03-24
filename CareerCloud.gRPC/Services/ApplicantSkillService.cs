using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantSkill;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantSkillService : ApplicantSkillBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillService()
        {
            _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());
        }

        public override Task<ApplicantSkillPayload> ReadApplicantSkill(ApplicantSkillIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Applicant Skill Record with this Id Found ");

            return new Task<ApplicantSkillPayload>( () => new ApplicantSkillPayload() 
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.Skill,
                StartMonth = poco.StartMonth,
                EndMonth = poco.EndMonth,
                StartYear = poco.StartYear,
                EndYear = poco.EndYear,
            });
        }

        public override Task<AllApplicantSkillPayload> GetAllApplicantSkill(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Applicant Skill record was found");

            var AllApplicantSkillPayload = new AllApplicantSkillPayload();

            Pocos.ForEach(poco => AllApplicantSkillPayload.ApplicantSkills.Add(new ApplicantSkillPayload
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.Skill,
                StartMonth = poco.StartMonth,
                EndMonth = poco.EndMonth,
                StartYear = poco.StartYear,
                EndYear = poco.EndYear,
            }));

            return new Task<AllApplicantSkillPayload>(() => AllApplicantSkillPayload);
        }

        public override Task<Empty> CreateApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            ApplicantSkillPoco poco = new ApplicantSkillPoco() 
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                StartMonth = (byte)request.StartMonth,
                EndMonth = Convert.ToByte(request.EndMonth),
                StartYear = request.StartYear,
                EndYear = request.EndYear,
            };
            _logic.Add(new ApplicantSkillPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Applicant Skill Record with this Id Found ");

            var poco = new ApplicantSkillPoco() 
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                StartMonth = (byte)request.StartMonth,
                EndMonth = Convert.ToByte(request.EndMonth),
                StartYear = request.StartYear,
                EndYear = request.EndYear
            };
            _logic.Update(new ApplicantSkillPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
                throw new ArgumentNullException("No Applicant Skill Record with this Id Found ");
                _logic.Delete(new ApplicantSkillPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

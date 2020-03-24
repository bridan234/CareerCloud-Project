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
using static CareerCloud.gRPC.Protos.CompanyJobSkills;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillsBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService()
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        public override Task<CompanyJobSkillsPayload> ReadCompanyJobSkill(CompanyJobSkillsIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Company Job Skill Record with this Id Found ");

            return new Task<CompanyJobSkillsPayload>(() => new CompanyJobSkillsPayload()
            {
                Id = poco.Id.ToString(),
                Importance = poco.Importance,
                Job = poco.Job.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel

            });
        }

        public override Task<AllCompanyJobSkillsPayload> GetAllCompanyJobSkill(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Company Job Skill record was found");

            var AllCompanyJobSkillsPayload = new AllCompanyJobSkillsPayload();

            Pocos.ForEach(poco => AllCompanyJobSkillsPayload.CompanyJobSkills.Add(new CompanyJobSkillsPayload
            {
                Id = poco.Id.ToString(),
                Importance = poco.Importance,
                Job = poco.Job.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel
            }));

            return new Task<AllCompanyJobSkillsPayload>(() => AllCompanyJobSkillsPayload);
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillsPayload request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Importance = request.Importance,
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel
            };
            _logic.Add(new CompanyJobSkillPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillsPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Company Job Skill Record with this Id Found");

            var poco = new CompanyJobSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Importance = request.Importance,
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel
            };
            _logic.Update(new CompanyJobSkillPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillsPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Company Job Skill Record with this Id Found ");
               _logic.Delete(new CompanyJobSkillPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

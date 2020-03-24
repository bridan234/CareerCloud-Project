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
using static CareerCloud.gRPC.Protos.CompanyJobDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobDescriptionService : CompanyJobDescriptionBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionService()
        {
            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }

        public override Task<CompanyJobDescriptionPayload> ReadCompanyJobDescription(CompanyJobDescriptionIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Company Job description Record with this Id Found ");

            return new Task<CompanyJobDescriptionPayload>(() => new CompanyJobDescriptionPayload()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                JobName = poco.JobName,
                JobDescription = poco.JobDescriptions
            });
        }

        public override Task<AllCompanyJobDescriptionPayload> GetAllCompanyJobDescription(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Company Job Description record was found");

            var AllCompanyJobDescriptionPayload = new AllCompanyJobDescriptionPayload();

            Pocos.ForEach(poco => AllCompanyJobDescriptionPayload.CompanyJobDescriptions.Add(new CompanyJobDescriptionPayload
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                JobName = poco.JobName,
                JobDescription = poco.JobDescriptions
            }));

            return new Task<AllCompanyJobDescriptionPayload>(() => AllCompanyJobDescriptionPayload);
        }

        public override Task<Empty> CreateCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                JobName = request.JobName,
                JobDescriptions= request.JobDescription,
                
            };
            _logic.Add(new CompanyJobDescriptionPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Company Job Description Record with this Id Found ");

            var poco = new CompanyJobDescriptionPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                JobName = request.JobName,
                JobDescriptions = request.JobDescription,
            };
            _logic.Update(new CompanyJobDescriptionPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Company Job Description Record with this Id Found ") ;
               _logic.Delete(new CompanyJobDescriptionPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

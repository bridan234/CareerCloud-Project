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
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationService()
        {
            _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }

        public override Task<CompanyJobEducationPayload> ReadCompanyJobEducation(CompanyJobEducationIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Company Job Education Record with this Id Found ");

            return new Task<CompanyJobEducationPayload>(() => new CompanyJobEducationPayload()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Importance= poco.Importance,
                Major = poco.Major
            });
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducationPayload request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Importance =  (Int16) request.Importance,
                Major = request.Major

            };
            _logic.Add(new CompanyJobEducationPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Company Job Education Record with this Id Found");

            var poco = new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Importance = (Int16)request.Importance,
                Major = request.Major
            };
            _logic.Update(new CompanyJobEducationPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Company Job Education Record with this Id Found ");
               _logic.Delete(new CompanyJobEducationPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

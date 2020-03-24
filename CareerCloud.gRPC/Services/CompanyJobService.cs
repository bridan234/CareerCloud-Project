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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public override Task<CompanyJobPayload> ReadCompanyJob(CompanyJobIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Company Job Record with this Id Found ");

            return new Task<CompanyJobPayload>(() => new CompanyJobPayload()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                IsCompanyHidden = poco.IsCompanyHidden,
                IsInactive = poco.IsInactive,
                ProfileCreadted = Timestamp.FromDateTime((DateTime)poco.ProfileCreated)
            });
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            CompanyJobPoco poco = new CompanyJobPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                IsCompanyHidden = request.IsCompanyHidden,
                IsInactive = request.IsInactive,
                ProfileCreated = request.ProfileCreadted.ToDateTime()

            };
            _logic.Add(new CompanyJobPoco[] { poco });
            return null;
        }

        public override Task<AllCompanyJobPayload> GetAllCompanyJob(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Company Job record was found");

            var AllCompanyJobPayload = new AllCompanyJobPayload();

            Pocos.ForEach(poco => AllCompanyJobPayload.CompanyJobs.Add(new CompanyJobPayload
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                IsCompanyHidden = poco.IsCompanyHidden,
                IsInactive = poco.IsInactive,
                ProfileCreadted = Timestamp.FromDateTime((DateTime)poco.ProfileCreated)
            }));

            return new Task<AllCompanyJobPayload>(() => AllCompanyJobPayload);
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Company Job Record with this Id Found");

            var poco = new CompanyJobPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                IsCompanyHidden = request.IsCompanyHidden,
                IsInactive = request.IsInactive,
                ProfileCreated = request.ProfileCreadted.ToDateTime()
            };
            _logic.Update(new CompanyJobPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Company Job Record with this Id Found ");
               _logic.Delete(new CompanyJobPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

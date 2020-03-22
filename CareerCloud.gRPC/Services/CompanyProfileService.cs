using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        public override Task<CompanyProfilePayload> ReadCompanyProfile(CompanyProfileIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Company Profile Record with this Id Found ");

            return new Task<CompanyProfilePayload>(() => new CompanyProfilePayload()
            {
                Id = poco.Id.ToString(),
                ContactName = poco.ContactName,
                ContactPhone = poco.ContactPhone,
                CompanyWebsite = poco.CompanyWebsite,
                RegistrationDate = Timestamp.FromDateTime(poco.RegistrationDate),
                CompanyLogo = ByteString.CopyFrom(poco.CompanyLogo)
            });
        }

        public override Task<Empty> CreateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            CompanyProfilePoco poco = new CompanyProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                ContactName = request.ContactName,
                ContactPhone = request.ContactPhone,
                CompanyWebsite = request.CompanyWebsite,
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyLogo = request.CompanyLogo.ToByteArray()

            };
            _logic.Add(new CompanyProfilePoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Company Profile Record with this Id Found");

            var poco = new CompanyProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                ContactName = request.ContactName,
                ContactPhone = request.ContactPhone,
                CompanyWebsite = request.CompanyWebsite,
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyLogo = request.CompanyLogo.ToByteArray()
            };
            _logic.Update(new CompanyProfilePoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Company Profile Record with this Id Found ");
               _logic.Delete(new CompanyProfilePoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

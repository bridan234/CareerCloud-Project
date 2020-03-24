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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionService()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {

            var pocos = new CompanyDescriptionPoco[]
            { 
                new CompanyDescriptionPoco() 
                {
                    Id = Guid.Parse(request.Id),
                    Company = Guid.Parse(request.Company),
                    CompanyDescription = request.CompanyDescription,
                    CompanyName = request.CompanyName,
                    LanguageId = request.LanguageId,
                } 
            };


            _logic.Add(pocos);
            return null;
        }

        public override Task<AllCompanyDescriptionPayload> GetAllCompanyDescription(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Company Description record was found");

            var AllCompanyDescriptionPayload = new AllCompanyDescriptionPayload();

            Pocos.ForEach(poco => AllCompanyDescriptionPayload.CompanyDescriptions.Add(new CompanyDescriptionPayload
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,
                LanguageId = poco.LanguageId
            }));

            return new Task<AllCompanyDescriptionPayload>(() => AllCompanyDescriptionPayload);
        }

        public override Task<CompanyDescriptionPayload> ReadCompanyDescription(CompanyDescriptionIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Company description Record with this Id Found ");

            return new Task<CompanyDescriptionPayload>(() => new CompanyDescriptionPayload()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,
                LanguageId = poco.LanguageId
            });
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Company Description Record with this Id Found ");

            var poco = new CompanyDescriptionPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CompanyDescription = request.CompanyDescription,
                CompanyName = request.CompanyName,
                LanguageId = request.LanguageId,
            };
            _logic.Update(new CompanyDescriptionPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Company Description Record with this Id Found ") ;
               _logic.Delete(new CompanyDescriptionPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

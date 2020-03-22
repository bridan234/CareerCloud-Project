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
using static CareerCloud.gRPC.Protos.SystemCountryCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeService()
        {
            _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
        }

        public override Task<SystemCountryCodePayload> ReadSystemCountryCode(SystemCountryCodeCodeRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(request.Code);
            _ = poco ?? throw new ArgumentException("No System Country Code with this Code Found ");

            return new Task<SystemCountryCodePayload>(() => new SystemCountryCodePayload()
            {
                Code = poco.Code,
                Name = poco.Name
            });
        }

        public override Task<Empty> CreateSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name
            };
            _logic.Add(new SystemCountryCodePoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            _ = _logic.Get(request.Code) ?? throw new ArgumentNullException("No System Country Code with this Code Found");
            SystemCountryCodePoco poco = new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name
            };
            _logic.Update(new SystemCountryCodePoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            _ = _logic.Get(request.Code) ??
              throw new ArgumentNullException("No System Country Code with this Code Found ") ;
              _logic.Delete(new SystemCountryCodePoco[] { _logic.Get(request.Code) });
            return null;
        }
    }
}

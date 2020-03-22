using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<SystemLanguageCodePayload> ReadSystemLanguageCode(SystemLanguageCodeIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(request.LanguageID);
            _ = poco ?? throw new ArgumentException("No System Language Code with this Id Found ");

            return new Task<SystemLanguageCodePayload>(() => new SystemLanguageCodePayload()
            {
                LanguageID = poco.LanguageID,
                Name = poco.Name,
                NativeName = poco.NativeName
            });
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName= request.NativeName
            };
            _logic.Add(new SystemLanguageCodePoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            _ = _logic.Get(request.LanguageID) ?? throw new ArgumentNullException("No System Language Code with this Id Found");
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName = request.NativeName
            };
            _logic.Update(new SystemLanguageCodePoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            _ = _logic.Get(request.LanguageID) ??
              throw new ArgumentNullException("No System Language Code with this Id Found ") ;
              _logic.Delete(new SystemLanguageCodePoco[] { _logic.Get(request.LanguageID) });
            return null;
        }
    }
}

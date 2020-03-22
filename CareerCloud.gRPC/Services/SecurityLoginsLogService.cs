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
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        public override Task<SecurityLoginsLogPayload> ReadSecurityLoginsLog(SecurityLoginsLogIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Security Login Log with this Id Found ");

            return new Task<SecurityLoginsLogPayload>(() => new SecurityLoginsLogPayload()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                IsSuccesful = poco.IsSuccesful,
                SourceIP = poco.SourceIP,
                LogonDate = Timestamp.FromDateTime(poco.LogonDate)

            });
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                IsSuccesful = request.IsSuccesful,
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),

            };
            _logic.Add(new SecurityLoginsLogPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Security Login Log with this Id Found");
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                IsSuccesful = request.IsSuccesful,
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),

            };
            _logic.Update(new SecurityLoginsLogPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Security Login Log with this Id Found ") ;
               _logic.Delete(new SecurityLoginsLogPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }

    }
}

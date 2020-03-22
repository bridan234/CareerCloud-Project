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
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleService()
        {
            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }

        public override Task<SecurityRolePayload> ReadSecurityRole(SecurityRoleIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Security Role Record with this Id Found ");

            return new Task<SecurityRolePayload>(() => new SecurityRolePayload()
            {
                Id = poco.Id.ToString(),
                Role = poco.Role,
                IsInactive = poco.IsInactive,
            });
        }

        public override Task<Empty> CreateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            SecurityRolePoco poco = new SecurityRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Role = request.Role,
                IsInactive= request.IsInactive
            };
            _logic.Add(new SecurityRolePoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Security Role with this Id Found");
            SecurityRolePoco poco = new SecurityRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive
            };
            _logic.Update(new SecurityRolePoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
              throw new ArgumentNullException("No Security Role with this Id Found ") ;
              _logic.Delete(new SecurityRolePoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

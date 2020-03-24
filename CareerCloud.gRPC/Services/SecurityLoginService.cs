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
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }

        public override Task<SecurityLoginPayload> ReadSecurityLogin(SecurityLoginIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Security Login Record with this Id Found ");

            return new Task<SecurityLoginPayload>(() => new SecurityLoginPayload()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login,
                EmailAddress = poco.EmailAddress,
                ForceChangePassword = poco.ForceChangePassword,
                Password = poco.Password,
                FullName = poco.FullName,
                PhoneNumber = poco.PhoneNumber,
                PrefferredLanguage = poco.PrefferredLanguage,
                IsInactive = poco.IsInactive,
                IsLocked = poco.IsLocked,
                AgreementAccepted = poco.AgreementAccepted != null ? Timestamp.FromDateTime((DateTime)poco.AgreementAccepted) : null,
                Created = poco.Created != null ? Timestamp.FromDateTime((DateTime)poco.Created) : null,
                PasswordUpdate = poco.Created != null ? Timestamp.FromDateTime((DateTime)poco.Created) : null,
            });
        }

        public override Task<AllSecurityLoginPayload> GetAllSecurityLogin(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Security Login record was found");

            var AllSecurityLoginPayload = new AllSecurityLoginPayload();

            Pocos.ForEach(poco => AllSecurityLoginPayload.SecurityLogins.Add(new SecurityLoginPayload
            {
                Id = poco.Id.ToString(),
                Login = poco.Login,
                EmailAddress = poco.EmailAddress,
                ForceChangePassword = poco.ForceChangePassword,
                Password = poco.Password,
                FullName = poco.FullName,
                PhoneNumber = poco.PhoneNumber,
                PrefferredLanguage = poco.PrefferredLanguage,
                IsInactive = poco.IsInactive,
                IsLocked = poco.IsLocked,
                AgreementAccepted = poco.AgreementAccepted != null ? Timestamp.FromDateTime((DateTime)poco.AgreementAccepted) : null,
                Created = poco.Created != null ? Timestamp.FromDateTime((DateTime)poco.Created) : null,
                PasswordUpdate = poco.Created != null ? Timestamp.FromDateTime((DateTime)poco.Created) : null,
            }));

            return new Task<AllSecurityLoginPayload>(() => AllSecurityLoginPayload);
        }

        public override Task<Empty> CreateSecurityLogin(SecurityLoginPayload request, ServerCallContext context)
        {
            SecurityLoginPoco poco = new SecurityLoginPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = request.Login,
                EmailAddress = request.EmailAddress,
                ForceChangePassword = request.ForceChangePassword,
                Password = request.Password,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                PrefferredLanguage = request.PrefferredLanguage,
                IsInactive = request.IsInactive,
                IsLocked = request.IsLocked,
                AgreementAccepted = request.AgreementAccepted.ToDateTime(),
                Created = request.Created.ToDateTime(),
                PasswordUpdate = request.Created.ToDateTime(),
                
            };
            _logic.Add(new SecurityLoginPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Security Login Record with this Id Found");

            var poco = new SecurityLoginPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = request.Login,
                EmailAddress = request.EmailAddress,
                ForceChangePassword = request.ForceChangePassword,
                Password = request.Password,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                PrefferredLanguage = request.PrefferredLanguage,
                IsInactive = request.IsInactive,
                IsLocked = request.IsLocked,
                AgreementAccepted = request.AgreementAccepted.ToDateTime(),
                Created = request.Created.ToDateTime(),
                PasswordUpdate = request.Created.ToDateTime(),
            };
            _logic.Update(new SecurityLoginPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
               throw new ArgumentNullException("No Security Login Record with this Id Found ");
               _logic.Delete(new SecurityLoginPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

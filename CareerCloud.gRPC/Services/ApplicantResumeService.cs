using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantResume;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantResumeService : ApplicantResumeBase
    {
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeService()
        {
            _logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }


        public override Task<ApplicantResumePayload> ReadApplicantResume(ApplicantResumeIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentNullException("Applicant resume record with this Id not found");
            return new Task<ApplicantResumePayload>(() => new ApplicantResumePayload()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Resume = poco.Resume,
                LastUpdated = poco.LastUpdated is null ? null : Timestamp.FromDateTime((DateTime)poco.LastUpdated)
            });
        }

        public override Task<Empty> CreateApplicantResume(ApplicantResumePayload request, ServerCallContext context)
        {

            ApplicantResumePoco poco = new ApplicantResumePoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Resume = request.Resume,
                LastUpdated = null
            };
            _logic.Add(new ApplicantResumePoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateApplicantResume(ApplicantResumePayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Resume Record with this Id Found ");

            ApplicantResumePoco poco = new ApplicantResumePoco
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Resume = request.Resume,
                LastUpdated = DateTime.Now
            };

            _logic.Update(new ApplicantResumePoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteApplicantResume(ApplicantResumePayload request, ServerCallContext context)
        {

            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Resume Record with this Id Found ");
            _logic.Delete(new ApplicantResumePoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}

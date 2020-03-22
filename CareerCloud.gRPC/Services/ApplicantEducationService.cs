using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationService()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }
        public override Task<ApplicantEducationPayload> ReadApplicantEducation(ApplicantEducationIdRequest req, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(req.Id));
            _ = poco ?? throw new ArgumentNullException(req.Id, req.Id + " - No Applicant Education record with this Id is found");

            var appEdu = new Task<ApplicantEducationPayload>(
                () => new ApplicantEducationPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Major = poco.Major,
                    CertificateDiploma = poco.CertificateDiploma,
                    StartDate = poco.StartDate is null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate),
                    CompletionDate = poco.CompletionDate is null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                    CompletionPercent = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent
                });

            return appEdu;
        }

        public override Task<Empty> CreateApplicantEducation(ApplicantEducationPayload request, ServerCallContext context)
        {
            var pocos = new ApplicantEducationPoco[]{ new ApplicantEducationPoco() {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)request.CompletionPercent,
                
            } };


            _logic.Add(pocos);
            return null;
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationPayload request, ServerCallContext context)
        {
            var pocos = new ApplicantEducationPoco[]{ new ApplicantEducationPoco() {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)request.CompletionPercent,

            } };

            _logic.Update(pocos);

            return null;
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationPayload request, ServerCallContext context)
        {
            var pocos = new ApplicantEducationPoco[]{ new ApplicantEducationPoco() {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)request.CompletionPercent,

            } };

            _logic.Delete(pocos);
            return null;
        }

    }

}

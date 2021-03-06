﻿using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantWorkHistory;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantWorkHistoryService : ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryService()
        {
            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }

        public override Task<ApplicantWorkHistoryPayload> ReadApplicantWorkHistory(ApplicantWorkHistoryIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            _ = poco ?? throw new ArgumentException("No Applicant Work History Record with this Id Found ");

            return new Task<ApplicantWorkHistoryPayload>(() => new ApplicantWorkHistoryPayload()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CompanyName = poco.CompanyName,
                CountryCode = poco.CountryCode,
                JobDescription = poco.JobDescription,
                JobTitle = poco.JobTitle,
                Location = poco.Location,
                StartMonth = poco.StartMonth,
                EndMonth = poco.EndMonth,
                StartYear = poco.StartYear,
                EndYear = poco.EndYear,
            });
        }

        public override Task<AllApplicantWorkHistoryPayload> GetAllApplicantWorkHistory(Empty request, ServerCallContext context)
        {
            var Pocos = _logic.GetAll();
            _ = Pocos ?? throw new ArgumentNullException("  No Applicant Work History record was found");

            var AllApplicantWorkHistoryPayload = new AllApplicantWorkHistoryPayload();

            Pocos.ForEach(poco => AllApplicantWorkHistoryPayload.ApplicantWorkHistories.Add(new ApplicantWorkHistoryPayload
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CompanyName = poco.CompanyName,
                CountryCode = poco.CountryCode,
                JobDescription = poco.JobDescription,
                JobTitle = poco.JobTitle,
                Location = poco.Location,
                StartMonth = poco.StartMonth,
                EndMonth = poco.EndMonth,
                StartYear = poco.StartYear,
                EndYear = poco.EndYear,
            }));

            return new Task<AllApplicantWorkHistoryPayload>(() => AllApplicantWorkHistoryPayload);
        }
        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                Location = request.Location,
                StartMonth = (byte)request.StartMonth,
                EndMonth = Convert.ToByte(request.EndMonth),
                StartYear = request.StartYear,
                EndYear = request.EndYear,
            };
            _logic.Add(new ApplicantWorkHistoryPoco[] { poco });
            return null;
        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ?? throw new ArgumentNullException("No Applicant Work History Record with this Id Found ");

            var poco = new ApplicantWorkHistoryPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                Location = request.Location,
                StartMonth = (byte)request.StartMonth,
                EndMonth = Convert.ToByte(request.EndMonth),
                StartYear = request.StartYear,
                EndYear = request.EndYear,
            };
            _logic.Update(new ApplicantWorkHistoryPoco[] { poco });
            return null;
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            _ = _logic.Get(Guid.Parse(request.Id)) ??
                throw new ArgumentNullException("No Applicant Work History Record with this Id Found ");
                _logic.Delete(new ApplicantWorkHistoryPoco[] { _logic.Get(Guid.Parse(request.Id)) });
            return null;
        }
    }
}
    
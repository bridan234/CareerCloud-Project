using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite) || !Regex.IsMatch(poco.CompanyWebsite, @"^\w(?i)(\.ca|\.com|\.biz)\b"))
                    exceptions.Add(new ValidationException(600, $"Critical Error!, a Valid Company websites must end with the following extensions – \".ca\", \".com\", \".biz\""));
                if (string.IsNullOrEmpty(poco.ContactPhone) || !Regex.IsMatch(poco.ContactPhone, @"\b(\d{3}-\d{3}-\d{4})\b"))
                    exceptions.Add(new ValidationException(601, $"Critical Error! Contact phone, Must correspond to a valid phonenumber format(xxx-xxx-xxxx)"));
            }

            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

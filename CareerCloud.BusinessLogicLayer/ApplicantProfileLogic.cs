using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List <ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantProfilePoco Poco in pocos)
            {
                if (Poco.CurrentSalary < 0)
                exceptions.Add(new ValidationException(111,"Oops! Current Salary cannot be negative"));
                if (Poco.CurrentRate<0)
                exceptions.Add(new ValidationException(112,"Oops! Current Rate cannot be negative"));
            }
            if (exceptions.Count>0)
            throw new AggregateException(exceptions);
        }

        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

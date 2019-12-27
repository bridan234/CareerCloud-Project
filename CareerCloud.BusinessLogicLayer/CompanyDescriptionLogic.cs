using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyDescription) || !Regex.IsMatch(poco.CompanyDescription, @"/{3,}/"))
                    exceptions.Add(new ValidationException(107, "Sorry! Comppany Description must be greater than 2 characters"));
                 
                if (string.IsNullOrEmpty(poco.CompanyName) || !Regex.IsMatch(poco.CompanyName, @"/{3,}/"))
                    exceptions.Add(new ValidationException(106, "Sorry! Company Name must be greater than 2 Characters"));
                }

            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CountryCode))
                    exceptions.Add(new ValidationException(500, "Critical Error occured! \n *Country Code* field cannot be blank"));
                if (string.IsNullOrEmpty(poco.Province))
                    exceptions.Add(new ValidationException(501, "Critical Error occured! \n *Province* field cannot be blank"));
                if (string.IsNullOrEmpty(poco.Street))
                    exceptions.Add(new ValidationException(502, "Critical Error occured! \n *Street* field cannot be blank"));
                if (string.IsNullOrEmpty(poco.City))
                    exceptions.Add(new ValidationException(503, "Critical Error occured! \n *City* field cannot be blank"));
                if (string.IsNullOrEmpty(poco.PostalCode))
                    exceptions.Add(new ValidationException(504, "Critical Error occured! \n *Postal Code* field cannot be blank"));

            }

            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }
}

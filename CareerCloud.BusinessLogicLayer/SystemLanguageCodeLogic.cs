using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
		protected IDataRepository<SystemLanguageCodePoco> _repository;
		public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
		{
			_repository = repository;

		}

		protected void Verify(SystemLanguageCodePoco[] pocos)
		{
			List<ValidationException> validationExceptions = new List<ValidationException>();
			foreach (SystemLanguageCodePoco poco in pocos)
			{
				if (string.IsNullOrEmpty(poco.LanguageID))
					validationExceptions.Add(new ValidationException(1000, $"Critical Error occured! \n *Language ID* field cannot be blank"));
				if (string.IsNullOrEmpty(poco.Name))
					validationExceptions.Add(new ValidationException(1001, $"Critical Error occured! \n *Country Name* field cannot be blank"));
				if (string.IsNullOrEmpty(poco.NativeName))
					validationExceptions.Add(new ValidationException(1002, $"Critical Error occured! \n *Native Name* field cannot be blank"));
			}

			if (validationExceptions.Count > 0)
				throw new AggregateException(validationExceptions);
		}

		public SystemLanguageCodePoco Get(string languageID)
		{
			return _repository.GetSingle(c => c.LanguageID == languageID);
		}

		public List<SystemLanguageCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

		public void Add(SystemLanguageCodePoco[] pocos)
		{
			/*foreach (SystemCountryCodePoco poco in pocos)
			{
				if (poco.Id == Guid.Empty)
				{
					poco.Id = Guid.NewGuid();
					
				}
			}*/
			Verify(pocos);
			_repository.Add(pocos);
		}

		public void Update(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}

		public void Delete(SystemLanguageCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}

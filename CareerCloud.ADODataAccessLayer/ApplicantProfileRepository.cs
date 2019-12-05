using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private  string _conStr;

        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                   ([Id]
                   ,[Login]
                   ,[Current_Salary]
                   ,[Current_Rate]
                   ,[Currency]
                   ,[Country_Code]
                   ,[State_Province_Code]
                   ,[Street_Address]
                   ,[City_Town]
                   ,[Zip_Postal_Code])
                VALUES
                   (@Id
                   ,@Login
                   ,@Current_Salary
                   ,@Current_Rate
                   ,@Currency
                   ,@Country_Code
                   ,@State_Province_Code
                   ,@Street_Address
                   ,@City_Town
                   ,@Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("@Id",poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    con.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
                
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                      ,[Login]
                      ,[Current_Salary]
                      ,[Current_Rate]
                      ,[Currency]
                      ,[Country_Code]
                      ,[State_Province_Code]
                      ,[Street_Address]
                      ,[City_Town]
                      ,[Zip_Postal_Code]
                      ,[Time_Stamp]
                  FROM [dbo].[Applicant_Profiles]";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[400];
                int index = 0;

                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = (Guid)reader["Login"];
                    poco.CurrentSalary = (decimal)reader["Current_Salary"];
                    poco.CurrentRate = (decimal)reader["Current_Rate"];
                    poco.Currency = (string)reader["Currency"];
                    poco.Country = (string)reader["Country_Code"];
                    poco.Province = (string)reader["State_Province_Code"];
                    poco.Street = (string)reader["Street_Address"];
                    poco.City = (string)reader["City_Town"];
                    poco.PostalCode = (string)reader["Zip_Postal_Code"];
                    poco.TimeStamp = (Byte[])reader["Time_Stamp"];


                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection=con;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Profiles WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                    SET [Id] = @Id
                        ,[Login] = @Login
                        ,[Current_Salary] = @Current_Salary
                        ,[Current_Rate] = @Current_Rate
                        ,[Currency] = @Currency
                        ,[Country_Code] = @Country_Code
                        ,[State_Province_Code] = @State_Province_Code
                        ,[Street_Address] = @Street_Address
                        ,[City_Town] = @City_Town
                        ,[Zip_Postal_Code] = @Zip_Postal_Code
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
   
    public class ApplicantWorkHistoryRepository:IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _conStr;
        public ApplicantWorkHistoryRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                       ([Id]
                       ,[Applicant]
                       ,[Company_Name]
                       ,[Country_Code]
                       ,[Location]
                       ,[Job_Title]
                       ,[Job_Description]
                       ,[Start_Month]
                       ,[Start_Year]
                       ,[End_Month]
                       ,[End_Year])
                    VALUES
                       (@Id
                       ,@Applicant
                       ,@Company_Name
                       ,@Country_Code
                       ,@Location
                       ,@Job_Title
                       ,@Job_Description
                       ,@Start_Month
                       ,@Start_Year
                       ,@End_Month
                       ,@End_Year)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    con.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Company_Name]
                                  ,[Country_Code]
                                  ,[Location]
                                  ,[Job_Title]
                                  ,[Job_Description]
                                  ,[Start_Month]
                                  ,[Start_Year]
                                  ,[End_Month]
                                  ,[End_Year]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Work_History]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[400];
                int index = 0;

                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.CompanyName = (string)reader["Company_Name"];
                    poco.CountryCode = (string)reader["Country_Code"];
                    poco.Location = (string)reader["Location"];
                    poco.JobTitle = (string)reader["Job_Title"];
                    poco.JobDescription = (string)reader["Job_Description"];
                    poco.StartMonth = (short)reader["Start_Month"];
                    poco.StartYear = (int)reader["Start_Year"];
                    poco.EndMonth = (short)reader["End_Month"];
                    poco.EndYear = (int)reader["End_Year"];
                    poco.TimeStamp = (byte[])reader["Time_Stamp"];

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
             using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History] WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
             using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                    SET [Id] = @Id
                        ,[Applicant] = @Applicant
                        ,[Company_Name] =@Company_Name
                        ,[Country_Code] = @Country_Code
                        ,[Location] = @Location
                        ,[Job_Title] = @Job_Title
                        ,[Job_Description] = @Job_Description
                        ,[Start_Month] = @Start_Month
                        ,[Start_Year] = @Start_Year
                        ,[End_Month] = @End_Month
                        ,[End_Year] = @End_Year
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

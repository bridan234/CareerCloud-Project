using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Linq.Expressions;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _conStr;
        public ApplicantJobApplicationRepository()
        {
            //var config = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //config.AddJsonFile(path, false);
            //var root = config.Build();
            //_conStr = root.GetSection("ConnectionString").GetSection("DataConnection").Value;
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                       ([Id]
                       ,[Applicant]
                       ,[Job]
                       ,[Application_Date])
                    VALUES
                       (@Id
                       ,@Applicant
                       ,@Job
                       ,@Application_Date)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
                    
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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {

            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                              ,[Applicant]
                              ,[Job]
                              ,[Application_Date]
                              ,[Time_Stamp]
                          FROM [dbo].[Applicant_Job_Applications]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[400];
                int index = 0;

                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.Job = (Guid)reader["Job"];
                    poco.ApplicationDate = (DateTime)reader["Application_Date"];
                    poco.TimeStamp = (Byte[])reader["Time_Stamp"];
                  

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
            //End
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd  = new SqlCommand();
                cmd.Connection = con;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText=@"DELETE FROM Applicant_Job_Applications WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                    SET [Id] = @Id
                        ,[Applicant] = @Applicant
                        ,[Job] = @Job
                        ,[Application_Date] = @Application_Date 
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("Job", poco.Job);
                    cmd.Parameters.AddWithValue("Application_Date", poco.ApplicationDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

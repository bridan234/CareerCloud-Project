using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string _conStr;

        public ApplicantResumeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                       ([Id]
                       ,[Applicant]
                       ,[Resume]
                       ,[Last_Updated])
                    VALUES
                       (@Id
                       ,@Applicant
                       ,@Resume
                       ,@Last_Updated)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                      ,[Applicant]
                      ,[Resume]
                      ,[Last_Updated]
                  FROM [dbo].[Applicant_Resumes]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                ApplicantResumePoco[] pocos = new ApplicantResumePoco[400];
                int index = 0;

                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.Resume = (string)reader["Resume"];
                    if (reader["Last_Updated"] != DBNull.Value)
                        poco.LastUpdated = (DateTime)reader["Last_Updated"];
                    else poco.LastUpdated = null;

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using(SqlConnection  con = new SqlConnection(_conStr))
            {
                SqlCommand  cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (ApplicantResumePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [Applicant_Resumes] WHERE Id= @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close(); 
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection con =new SqlConnection(_conStr))
            {
                SqlCommand  cmd = new SqlCommand();
                cmd.Connection= con;
                foreach (ApplicantResumePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                    SET [Id] = @Id
                        ,[Applicant] = @Applicant
                        ,[Resume] = @Resume
                        ,[Last_Updated] = @Last_Updated
                    WHERE Id = @Id";
                    
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

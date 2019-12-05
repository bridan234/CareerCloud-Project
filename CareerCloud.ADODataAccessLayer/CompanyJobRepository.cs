using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Linq.Expressions;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
   public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly String _conStr;
        public CompanyJobRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (CompanyJobPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                    ([Id]
                    ,[Company]
                    ,[Profile_Created]
                    ,[Is_Inactive]
                    ,[Is_Company_Hidden])
                 VALUES
                    (@Id
                    ,@Company
                    ,@Profile_Created
                    ,@Is_Inactive
                    ,@Is_Company_Hidden)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);

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

    public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
    {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Company]
                                  ,[Profile_Created]
                                  ,[Is_Inactive]
                                  ,[Is_Company_Hidden]
                                  ,[Time_Stamp]
                              FROM [dbo].[Company_Jobs]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                CompanyJobPoco[] pocos = new CompanyJobPoco[2000];
                int index = 0;

                while (reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = (Guid)reader["Company"];
                    poco.ProfileCreated = (DateTime)reader["Profile_Created"];
                    poco.IsInactive = (bool)reader["Is_Inactive"];
                    poco.IsCompanyHidden = (bool)reader["Is_Company_Hidden"];
                    poco.TimeStamp = (byte[])reader["Time_Stamp"];

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

    public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyJobPoco[] items)
    {
        using (SqlConnection con = new SqlConnection(_conStr))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            foreach (CompanyJobPoco poco in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs] WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", poco.Id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            foreach (CompanyJobPoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs]
                SET [Id] = @Id
                    ,[Company] = @Company
                    ,[Profile_Created] = @Profile_Created
                    ,[Is_Inactive] = @Is_Inactive
                    ,[Is_Company_Hidden] = @Is_Company_Hidden
                WHERE Id = @Id";
                
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Company", poco.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        }
    }
}

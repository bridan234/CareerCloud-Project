using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository: IDataRepository<CompanyLocationPoco>
    {
        private readonly string _conStr;
        public CompanyLocationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyLocationPoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (CompanyLocationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                       ([Id]
                       ,[Company]
                       ,[Country_Code]
                       ,[State_Province_Code]
                       ,[Street_Address]
                       ,[City_Town]
                       ,[Zip_Postal_Code])
                    VALUES
                       (@Id
                       ,@Company
                       ,@Country_Code
                       ,@State_Province_Code
                       ,@Street_Address
                       ,@City_Town
                       ,@Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Company]
                                  ,[Country_Code]
                                  ,[State_Province_Code]
                                  ,[Street_Address]
                                  ,[City_Town]
                                  ,[Zip_Postal_Code]
                                  ,[Time_Stamp]
                              FROM [dbo].[Company_Locations]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                CompanyLocationPoco[] pocos = new CompanyLocationPoco[400];
                int index = 0;

                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = (Guid)reader["Company"];
                    poco.CountryCode = (string)reader["Country_Code"];
                    poco.Province = (string)reader["State_Province_Code"];
                    poco.Street = (string)reader["Street_Address"];
                    poco.City = reader["City_Town"] ==DBNull.Value ? null : (string)reader["City_Town"];
                    if (reader["Zip_Postal_Code"] == DBNull.Value )
                        poco.PostalCode = null;
                    else poco.PostalCode = (string)reader["Zip_Postal_Code"];
                    poco.TimeStamp = (byte[])reader["Time_Stamp"];

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
        IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations] Where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                    SET [Id] = @Id
                        ,[Company] = @Company
                        ,[Country_Code] = @Country_Code
                        ,[State_Province_Code] = @State_Province_Code
                        ,[Street_Address] = @Street_Address
                        ,[City_Town] = @City_Town
                        ,[Zip_Postal_Code] = @Zip_Postal_Code
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
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

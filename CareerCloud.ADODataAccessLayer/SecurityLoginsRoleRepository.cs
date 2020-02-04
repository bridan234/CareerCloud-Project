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
    public class SecurityLoginsRoleRepository: IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string _conStr;
        public SecurityLoginsRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                       ([Id]
                       ,[Login]
                       ,[Role])
                    VALUES
                       (@Id
                       ,@Login
                       ,@Role)";
                    
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Role]
                                  ,[Time_Stamp]
                              FROM [dbo].[Security_Logins_Roles]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[2000];
                int index = 0;

                while (reader.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = (Guid)reader["Login"];
                    poco.Role = (Guid)reader["Role"];
                    poco.TimeStamp = (byte[])reader["Time_Stamp"];

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
             using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles] Where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                    SET [Id] = @Id
                        ,[Login] = @Login
                        ,[Role] = @Role
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

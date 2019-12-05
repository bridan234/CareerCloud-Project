using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using System.Linq.Expressions;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository:IDataRepository<SecurityLoginPoco>
    {
        private readonly string _conStr;
        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                foreach (SecurityLoginPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                       ([Id]
                       ,[Login]
                       ,[Password]
                       ,[Created_Date]
                       ,[Password_Update_Date]
                       ,[Agreement_Accepted_Date]
                       ,[Is_Locked]
                       ,[Is_Inactive]
                       ,[Email_Address]
                       ,[Phone_Number]
                       ,[Full_Name]
                       ,[Force_Change_Password]
                       ,[Prefferred_Language])
                    VALUES
                       (@Id
                       ,@Login
                       ,@Password
                       ,@Created_Date
                       ,@Password_Update_Date
                       ,@Agreement_Accepted_Date
                       ,@Is_Locked
                       ,@Is_Inactive
                       ,@Email_Address
                       ,@Phone_Number
                       ,@Full_Name
                       ,@Force_Change_Password
                       ,@Prefferred_Language)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Password]
                                  ,[Created_Date]
                                  ,[Password_Update_Date]
                                  ,[Agreement_Accepted_Date]
                                  ,[Is_Locked]
                                  ,[Is_Inactive]
                                  ,[Email_Address]
                                  ,[Phone_Number]
                                  ,[Full_Name]
                                  ,[Force_Change_Password]
                                  ,[Prefferred_Language]
                                  ,[Time_Stamp]
                              FROM [dbo].[Security_Logins]";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                SecurityLoginPoco[] pocos = new SecurityLoginPoco[2000];
                int index = 0;

                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = (string)reader["Login"];
                    poco.Password = (string)reader["Password"];
                    poco.Created = (DateTime)reader["Created_Date"];
                    poco.PasswordUpdate = reader["Password_Update_Date"] != DBNull.Value ? 
                            (DateTime)reader["Password_Update_Date"]: poco.PasswordUpdate;
                    if (reader["Agreement_Accepted_Date"] != DBNull.Value)
                        poco.AgreementAccepted = (DateTime)reader["Agreement_Accepted_Date"]; 
                    else poco.AgreementAccepted = null;
                    poco.IsLocked = (bool)reader["Is_Locked"];
                    poco.IsInactive = (bool)reader["Is_Inactive"];
                    poco.EmailAddress = (string)reader["Email_Address"];
                    poco.PhoneNumber = reader["Phone_Number"]!=DBNull.Value ? (string)reader["Phone_Number"]:null;
                    poco.FullName = reader["Full_Name"] != DBNull.Value ? (string)reader["Full_Name"]: null;
                    poco.ForceChangePassword = (bool)reader["Force_Change_password"];
                    poco.PrefferredLanguage = reader["Prefferred_Language"]==DBNull.Value? 
                                null : (string)reader["Prefferred_Language"];
                    poco.TimeStamp = (byte[])reader["Time_Stamp"];

                    pocos[index] = poco;
                    index++;
                }
                con.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
        IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins] Where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
             using(SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                    SET [Id] = @Id
                        ,[Login] = @Login
                        ,[Password] = @Password
                        ,[Created_Date] = @Created_Date
                        ,[Password_Update_Date] = @Password_Update_Date
                        ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                        ,[Is_Locked] = @Is_Locked
                        ,[Is_Inactive] = @Is_Inactive
                        ,[Email_Address] = @Email_Address
                        ,[Phone_Number] = @Phone_Number
                        ,[Full_Name] = @Full_Name
                        ,[Force_Change_Password] = @Force_Change_Password
                        ,[Prefferred_Language] = @Prefferred_Language
                    WHERE Id = @Id";

                     cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace SystemSecurityUtil
{
    public class SystemSecurityDAC
    {
        #region Constants and Constructors
        byte _noHistoryRecodsMaintained;
        public SystemSecurityDAC()
        {
        }
        public SystemSecurityDAC(byte NoOfRecordsToBeStored)
        {
            _noHistoryRecodsMaintained = NoOfRecordsToBeStored;
        }
        #endregion Constants and Constructors
        #region Properties
        public static string SecuredDataBase
        {
            get
            {
                return GetConnection().Database;
            }
        }
        public static string SecuredServer
        {
            get
            {
                return GetConnection().DataSource;
            }
        }
        #endregion Properties
        #region Database Connection
        private static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = null;
            if (connectionString != "")
            {
                connection = new SqlConnection(connectionString);
            }
            else
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["AppConnectionString"];
                if (settings != null)
                {
                    string ConnectionString = settings.ConnectionString;
                    connection = new SqlConnection(ConnectionString);
                }
            }
            return connection;
        }
        private static SqlConnection GetConnection()
        {
            return GetConnection("");
        }
        #endregion Database Connection
        #region Build, Setup and Clear Tables
        public static bool DbHasSecuritySetupTables()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                string sql = "SELECT Count([name]) AS Nos FROM sysobjects WHERE (type = 'U' OR type = 'P') AND [name] in('ENTSecurityParameter','EntPasswordHistory') ";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return (count == 2);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        private static void DropSecuritySetupObjects(SqlConnection connection)
        {
            SqlCommand command;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_EntPasswordHistory_EntUserAccount]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)");
                sb.AppendLine("ALTER TABLE [dbo].[EntPasswordHistory] DROP CONSTRAINT FK_EntPasswordHistory_EntUserAccount");
                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EntPasswordHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                sb.AppendLine("drop table [dbo].[EntPasswordHistory]");
                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ENTSecurityParameter]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                sb.AppendLine("drop table [dbo].[ENTSecurityParameter]");
                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                sb = null;
            }
        }
        private static void CreateSecuritySetupObjects(SqlConnection connection)
        {
            SqlCommand command;
            StringBuilder sb = new StringBuilder();
            try
            {
                #region Create Table
                sb.AppendLine("CREATE TABLE [dbo].[ENTSecurityParameter] (");
                sb.AppendLine("[LoginIdMinLength] [tinyint] NOT NULL,");
                sb.AppendLine("[PasswordMinLength] [tinyint] NOT NULL,");
                sb.AppendLine("[PasswordMustHaveDigit] [bit] NOT NULL,");
                sb.AppendLine("[PasswordMustHaveLowerCase] [bit] NOT NULL,");
                sb.AppendLine("[PasswordMustHaveUpperCase] [bit] NOT NULL,");
                sb.AppendLine("[PasswordMustHaveSpecialChar] [bit] NOT NULL,");
                sb.AppendLine("[AllowedUnsuccessfulAttempts] [tinyint] NOT NULL,");
                sb.AppendLine("[PasswordInterval] [tinyint] NOT NULL,");
                sb.AppendLine("[PasswordHistory] [tinyint] NOT NULL,");
                sb.AppendLine("[WorkingHourFrom] [varchar](11) NOT NULL,");
                sb.AppendLine("[WorkingHourTo] [varchar](11) NOT NULL,");
                sb.AppendLine("[WorkingHourToSat] [varchar](11) NOT NULL,");
                sb.AppendLine("[AllowWorkOnHoliday] [bit] NOT NULL,");
                sb.AppendLine("[AllowWorkOnWeekend] [bit] NOT NULL,");
                sb.AppendLine("[AllowWorkAfterWorkingHours] [bit] NOT NULL,");
                sb.AppendLine("[UpdateUserId] [varchar](10) NOT NULL,");
                sb.AppendLine("[UpdateDate] [datetime] NOT NULL");
                sb.AppendLine(") ON [PRIMARY]");
                sb.AppendLine("CREATE TABLE [dbo].[EntPasswordHistory] (");
                sb.AppendLine("[Id] [bigint] IDENTITY(1,1) NOT NULL,");
                sb.AppendLine("[UserAccountId] [bigint] NOT NULL,");
                sb.AppendLine("[Password] [varchar](50) NOT NULL, ");
                sb.AppendLine("[InsertUserId] [varchar](10) NOT NULL,");
                sb.AppendLine("[EntryDate] [datetime] NOT NULL,");
                sb.AppendLine("[Version] [timestamp] NOT NULL");
                sb.AppendLine(") ON [PRIMARY]");
                sb.AppendLine("ALTER TABLE [dbo].[EntPasswordHistory] WITH NOCHECK ADD CONSTRAINT [PK_PasswordHistory] PRIMARY KEY  CLUSTERED ");
                sb.AppendLine("( [Id] )  ON [PRIMARY] ");
                sb.AppendLine("ALTER TABLE [dbo].[EntPasswordHistory] ADD  CONSTRAINT [DF_EntPasswordHistory_InsertUserId]  DEFAULT ('dbo') FOR [InsertUserId]");
                sb.AppendLine("ALTER TABLE [dbo].[EntPasswordHistory] ADD  CONSTRAINT [DF_PasswordHistory_EntryDate]  DEFAULT (getdate()) FOR [EntryDate]");
                sb.AppendLine("EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Number of unseccessful attempts allowed before the user account is locked. 0=infinite' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ENTSecurityParameter', @level2type=N'COLUMN',@level2name=N'AllowedUnsuccessfulAttempts';");
                sb.AppendLine("EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'No days after password expires; 0 = never' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ENTSecurityParameter', @level2type=N'COLUMN',@level2name=N'PasswordInterval';");
                sb.AppendLine("EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'No of previous passwords the user not allowed to be used as new password.' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ENTSecurityParameter', @level2type=N'COLUMN',@level2name=N'PasswordHistory';");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_LoginIdMinLength]  DEFAULT ((3)) FOR [LoginIdMinLength]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordMinLength]  DEFAULT ((6)) FOR [PasswordMinLength]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordMustHaveDigit]  DEFAULT ((1)) FOR [PasswordMustHaveDigit]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordMustHaveLowerCase]  DEFAULT ((1)) FOR [PasswordMustHaveLowerCase]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordMustHaveUpperCase]  DEFAULT ((1)) FOR [PasswordMustHaveUpperCase]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordMustHaveSpecialChar]  DEFAULT ((1)) FOR [PasswordMustHaveSpecialChar]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_AllowedUnsuccessfulAttempts]  DEFAULT ((0)) FOR [AllowedUnsuccessfulAttempts]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordInterval]  DEFAULT ((0)) FOR [PasswordInterval]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_PasswordHistory]  DEFAULT ((0)) FOR [PasswordHistory]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_UpdateUserId]  DEFAULT ('dbo') FOR [UpdateUserId]");
                sb.AppendLine("ALTER TABLE [dbo].[ENTSecurityParameter] ADD  CONSTRAINT [DF_SystemSecurityParameter_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]");
                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();
                #endregion
            }
            finally
            {
                sb = null;
            }
        }
        public static void SetupSecurityParameter()
        {
            //SqlCommand command;
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();
                DropSecuritySetupObjects(connection);
                CreateSecuritySetupObjects(connection);
                InsertDefaultSecurityParameter();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static void ClearSecurityParameter()
        {
            //SqlCommand command;
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();
                //Drop the security setup tables & procedures
                DropSecuritySetupObjects(connection);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        #endregion Build, Setup and Clear Tables
        #region System Security Parameter
        private static bool InsertDefaultSecurityParameter()
        {
            SqlCommand command;
            SqlTransaction transaction;
            SqlConnection connection = null;
            //int tableId = auditedTable.TableId;
            connection = GetConnection();
            connection.Open();
            transaction = connection.BeginTransaction();
            command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                //if (auditedTable.TableId == 0) //New Entry
                //{
                command.CommandText = "INSERT INTO ENTSecurityParameter (LoginIdMinLength, PasswordMinLength, PasswordMustHaveDigit, " +
                                      "PasswordMustHaveLowerCase, PasswordMustHaveUpperCase, PasswordMustHaveSpecialChar,AllowedUnsuccessfulAttempts," +
                                      "PasswordInterval,PasswordHistory,WorkingHourFrom,WorkingHourTo,WorkingHourToSat,AllowWorkOnHoliday,AllowWorkOnWeekend," +
                                      "AllowWorkAfterWorkingHours,UpdateUserId,UpdateDate) " +
                                     "VALUES(4,6,1,1,1,1,3,0,3,'8:00:00 AM','4:30:00 PM','12:00:00 PM',1,1," +
                                        "1,'admin'," + DateTime.Now.ToShortDateString() + ")";
                command.ExecuteNonQuery();
                //Get the generated table id
                //command.CommandText = "SELECT [TableId] FROM AuditTables WHERE [TableName]='" + securityParameter.TableName + "'";
                //tableId = Convert.ToInt32(command.ExecuteScalar().ToString());
                ////}
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        private static bool InsertSecurityParameter(SecurityParameter securityParameter)
        {
            SqlCommand command;
            SqlTransaction transaction;
            SqlConnection connection = null;
            //int tableId = auditedTable.TableId;
            connection = GetConnection();
            connection.Open();
            transaction = connection.BeginTransaction();
            command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                //if (auditedTable.TableId == 0) //New Entry
                //{
                command.CommandText = "INSERT INTO ENTSecurityParameter (LoginIdMinLength, PasswordMinLength, PasswordMustHaveDigit, " +
                                      "PasswordMustHaveLowerCase, PasswordMustHaveUpperCase, PasswordMustHaveSpecialChar,AllowedUnsuccessfulAttempts," +
                                      "PasswordInterval,PasswordHistory,WorkingHourFrom,WorkingHourTo,WorkingHourToSat,AllowWorkOnHoliday,AllowWorkOnWeekend," +
                                      "AllowWorkAfterWorkingHours,UpdateUserId,UpdateDate) " +
                                      "VALUES(" + securityParameter.LoginIdMinLength + "," + securityParameter.PasswordMinLength + "," + (securityParameter.PasswordMustHaveDigit ? "1" : "0") + "," +
                                        (securityParameter.PasswordMustHaveLowerCase ? "1" : "0") + "," + (securityParameter.PasswordMustHaveUpperCase ? "1" : "0") + "," + (securityParameter.PasswordMustHaveSpecialChar ? "1" : "0") + "," + securityParameter.NoOfUnsuccessfulAttempts + "," +
                                        securityParameter.PasswordInterval + "," + securityParameter.PasswordHistory + ",'" + securityParameter.WorkingHourFrom + "','" + securityParameter.WorkingHourTo + "','" +
                                         securityParameter.WorkingHourToSat + "'," + (securityParameter.AllowWorkOnHoliday ? "1" : "0") + "," + (securityParameter.AllowWorkOnWeekend ? "1" : "0") + "," +
                                        (securityParameter.AllowWorkAfterWorkingHours ? "1" : "0") + ",'" + securityParameter.UpdateUserId + "'," + securityParameter.UpdateDate.ToShortDateString() + ")";
                command.ExecuteNonQuery();
                //Get the generated table id
                //command.CommandText = "SELECT [TableId] FROM AuditTables WHERE [TableName]='" + securityParameter.TableName + "'";
                //tableId = Convert.ToInt32(command.ExecuteScalar().ToString());
                ////}
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static bool UpdateSecurityParameter(SecurityParameter securityParameter)
        {
            SqlCommand command;
            SqlTransaction transaction;
            SqlConnection connection = null;
            string updateUserId = "";
            connection = GetConnection();
            connection.Open();
            transaction = connection.BeginTransaction();
            command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                command.CommandText = "SELECT UpdateUserId FROM ENTSecurityParameter ";
                updateUserId = command.ExecuteScalar().ToString();
                command.CommandText = " UPDATE ENTSecurityParameter " +
" SET [LoginIdMinLength] =" + securityParameter.LoginIdMinLength +
  ",[PasswordMinLength] =" + securityParameter.PasswordMinLength +
  ",[PasswordMustHaveDigit] =" + (securityParameter.PasswordMustHaveDigit ? "1" : "0") +
  ",[PasswordMustHaveLowerCase] =" + (securityParameter.PasswordMustHaveLowerCase ? "1" : "0") +
  ",[PasswordMustHaveUpperCase] =" + (securityParameter.PasswordMustHaveUpperCase ? "1" : "0") +
  ",[PasswordMustHaveSpecialChar] =" + (securityParameter.PasswordMustHaveSpecialChar ? "1" : "0") +
  ",[AllowedUnsuccessfulAttempts] =" + securityParameter.NoOfUnsuccessfulAttempts +
  ",[PasswordInterval] =" + securityParameter.PasswordInterval +
  ",[PasswordHistory] = " + securityParameter.PasswordHistory +
  ",[WorkingHourFrom] ='" + securityParameter.WorkingHourFrom +
  "',[WorkingHourTo] = '" + securityParameter.WorkingHourTo +
  "',[WorkingHourToSat] ='" + securityParameter.WorkingHourToSat +
  "',[AllowWorkOnHoliday] =" + (securityParameter.AllowWorkOnHoliday ? "1" : "0") +
  ",[AllowWorkOnWeekend] =" + (securityParameter.AllowWorkOnWeekend ? "1" : "0") +
  ",[AllowWorkAfterWorkingHours] =" + (securityParameter.AllowWorkAfterWorkingHours ? "1" : "0") +
  ",[UpdateUserId] = '" + securityParameter.UpdateUserId + "'" +
  ",[UpdateDate] = " + securityParameter.UpdateDate.ToShortDateString() +
                                     " WHERE UpdateUserId = '" + updateUserId + "'";
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static SecurityParameter GetSecurityParameterObject()
        {
            return GetSecurityParameterObject("");
        }
        public static SecurityParameter GetEmpitySecurityParameterObject()
        {
            List<SecurityParameter> securityparameters = new List<SecurityParameter>();
            securityparameters.Add(new SecurityParameter
            {
                LoginIdMinLength = 0,
                PasswordMinLength = 0,
                PasswordMustHaveDigit = false,
                PasswordMustHaveLowerCase = false,
                PasswordMustHaveUpperCase = false,
                PasswordMustHaveSpecialChar = false,
                NoOfUnsuccessfulAttempts = 0,
                PasswordInterval = 0,
                PasswordHistory = 0,
                WorkingHourFrom = "",
                WorkingHourTo = "",
                WorkingHourToSat = "",
                AllowWorkOnHoliday = true,
                AllowWorkOnWeekend = true,
                AllowWorkAfterWorkingHours = true,
                //UpdateUserId = reader["UpdateUserId"].ToString(),
                //UpdateDate = Convert.ToDateTime(reader["UpdateDate"])
            });
            return securityparameters.First();
        }
        public static SecurityParameter GetSecurityParameterObject(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM ENTSecurityParameter";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<SecurityParameter> securityparameters = new List<SecurityParameter>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        securityparameters.Add(new SecurityParameter
                        {
                            LoginIdMinLength = Convert.ToByte(reader["LoginIdMinLength"]),
                            PasswordMinLength = Convert.ToByte(reader["PasswordMinLength"]),
                            PasswordMustHaveDigit = Convert.ToBoolean(reader["PasswordMustHaveDigit"]),
                            PasswordMustHaveLowerCase = Convert.ToBoolean(reader["PasswordMustHaveLowerCase"]),
                            PasswordMustHaveUpperCase = Convert.ToBoolean(reader["PasswordMustHaveUpperCase"]),
                            PasswordMustHaveSpecialChar = Convert.ToBoolean(reader["PasswordMustHaveSpecialChar"]),
                            NoOfUnsuccessfulAttempts = Convert.ToByte(reader["AllowedUnsuccessfulAttempts"]),
                            PasswordInterval = Convert.ToByte(reader["PasswordInterval"]),
                            PasswordHistory = Convert.ToByte(reader["PasswordHistory"]),
                            WorkingHourFrom = reader["WorkingHourFrom"].ToString(),
                            WorkingHourTo = reader["WorkingHourTo"].ToString(),
                            WorkingHourToSat = reader["WorkingHourToSat"].ToString(),
                            AllowWorkOnHoliday = Convert.ToBoolean(reader["AllowWorkOnHoliday"]),
                            AllowWorkOnWeekend = Convert.ToBoolean(reader["AllowWorkOnWeekend"]),
                            AllowWorkAfterWorkingHours = Convert.ToBoolean(reader["AllowWorkAfterWorkingHours"]),
                            UpdateUserId = reader["UpdateUserId"].ToString(),
                            UpdateDate = Convert.ToDateTime(reader["UpdateDate"])
                        }
                        );
                    }
                }
                return securityparameters.First();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        #endregion System Security Parameter
        #region Password History
        public static List<PasswordHistory> GetPasswordHistoryObject()
        {
            return GetPasswordHistoryObject("");
        }
        public static List<PasswordHistory> GetPasswordHistoryObject(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM EntPasswordHistory";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<PasswordHistory> passwordhistories = new List<PasswordHistory>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        passwordhistories.Add(new PasswordHistory
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            UserAccountId = Convert.ToInt64(reader["UserAccountId"]),
                            Password = reader["Password"].ToString(),
                            InsertUserId = reader["InsertUserId"].ToString(),
                            EntryDate = Convert.ToDateTime(reader["EntryDate"])
                        }
                        );
                    }
                }
                return passwordhistories;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static PasswordHistory GetPasswordHistoryObject(long userAccountId, string password)
        {
            return GetPasswordHistoryObject("", userAccountId, password);
        }
        public static PasswordHistory GetEmpityPasswordHistoryObject(long userAccountId, string password)
        {
            List<PasswordHistory> passwordhistories = new List<PasswordHistory>();
            passwordhistories.Add(new PasswordHistory
            {
                Id = 0,
                UserAccountId = 0,
                Password = "",
                InsertUserId = "",
                //EntryDate = Convert.ToDateTime(reader["EntryDate"])
            }
                        );
            return passwordhistories != null ? passwordhistories.First() : null;
        }
        public static PasswordHistory GetPasswordHistoryObject(string connectionString, long userAccountId, string password)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM EntPasswordHistory WHERE UserAccountId=" + userAccountId + "  AND  Password='" + password + "'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<PasswordHistory> passwordhistories = new List<PasswordHistory>();
                //passwordhistories=null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        passwordhistories.Add(new PasswordHistory
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            UserAccountId = Convert.ToInt64(reader["UserAccountId"]),
                            Password = reader["Password"].ToString(),
                            InsertUserId = reader["InsertUserId"].ToString(),
                            EntryDate = Convert.ToDateTime(reader["EntryDate"])
                        }
                        );
                    }
                }
                else
                {
                    passwordhistories = null;
                }
                return passwordhistories != null ? passwordhistories.First() : null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public void DeleteInactiveHistories()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection("");
                connection.Open();
                var result = (from ph in GetPasswordHistoryObject()
                              group ph.Id by ph.UserAccountId
                                  into g
                              where g.Count() > _noHistoryRecodsMaintained
                              select new { UserAccountId = g.Key, NoRecords = g.Count() }).ToList();
                if (result.Count() != 0)
                {
                    foreach (var r in result)
                    {
                        var toBeDeleted = (from p in GetPasswordHistoryObject()
                                           where p.UserAccountId == r.UserAccountId
                                           orderby p.Id
                                           select p).Take(r.NoRecords - _noHistoryRecodsMaintained);
                        foreach (var td in toBeDeleted)
                        {
                            string sql = "DELETE EntPasswordHistory WHERE Id=" + td.Id;
                            SqlCommand command = new SqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            //}
        }
        public void DeleteInactiveHistories(long userAccountId)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection("");
                connection.Open();
                var result = (from ph in GetPasswordHistoryObject()
                              where ph.UserAccountId == userAccountId
                              orderby ph.Id
                              select ph).ToList();
                if (result.Count() > _noHistoryRecodsMaintained)
                {
                    int NoDelRec = result.Count() - _noHistoryRecodsMaintained;
                    foreach (var r in result)
                    {
                        if (NoDelRec > 0)
                        {
                            string sql = "DELETE EntPasswordHistory WHERE Id=" + r.Id;
                            SqlCommand command = new SqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                        }
                        NoDelRec--;
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        #region Insert
        public bool InsertPasswordHistory(long userAccountId, string userPassword, string userId)
        {
            SqlCommand command;
            SqlTransaction transaction;
            SqlConnection connection = null;
            //int tableId = auditedTable.TableId;
            connection = GetConnection();
            connection.Open();
            transaction = connection.BeginTransaction();
            command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                //if (auditedTable.TableId == 0) //New Entry
                //{
                command.CommandText = "INSERT INTO EntPasswordHistory (UserAccountId, Password, InsertUserId,EntryDate) " +
                                      "VALUES(" + userAccountId + ",'" + userPassword + "','" + userId + "'," +
                                      DateTime.Now.ToShortDateString() + ")";
                command.ExecuteNonQuery();
                //Get the generated table id
                //command.CommandText = "SELECT [TableId] FROM AuditTables WHERE [TableName]='" + securityParameter.TableName + "'";
                //tableId = Convert.ToInt32(command.ExecuteScalar().ToString());
                ////}
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        #endregion Insert
        #endregion Password History
        #region Holiday
        public static Holiday GetHolidayObject(DateTime date)
        {
            return GetHolidayObject("", date);
        }
        public static Holiday GetHolidayObject(string connectionString, DateTime date)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM Holiday WHERE HolidayDate=CONVERT(DateTime,'" + date.ToShortDateString() + " 00:00:00.000',103)";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Holiday> holidays = new List<Holiday>();
                //passwordhistories=null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        holidays.Add(new Holiday
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            HolidayDate = Convert.ToDateTime(reader["HolidayDate"]),
                            HolidayName = reader["HolidayName"].ToString()
                            //UserAccountId = Convert.ToInt64(reader["UserAccountId"]),
                            //Password = reader["Password"].ToString(),
                            //InsertUserId = reader["InsertUserId"].ToString(),
                            //EntryDate = Convert.ToDateTime(reader["EntryDate"])
                        }
                        );
                    }
                }
                else
                {
                    holidays = null;
                }
                return holidays != null ? holidays.First() : null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        #endregion Holiday
    }
}

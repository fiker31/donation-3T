using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace AuditLogUtil
{
    public class AuditLogDAC
    {

        public static string AuditedDataBase
        {
            get
            {
                return GetConnection().Database;
            }
        }

        public static string AuditedServer
        {
            get
            {
                return GetConnection().DataSource;
            }
        }

        //Create the audit setup tables & procedures requied for auditing
        public static void SetupAuditing()
        {
            //SqlCommand command;
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                DropAuditSetupObjects(connection);
                CreateAuditSetupObjects(connection);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static void ClearAuditing()
        {
            //SqlCommand command;
            SqlConnection connection = null;
            try
            {
                //Drop the audit triggers
                List<AuditTable> tbls = GetAuditedTables();

                connection = GetConnection();
                connection.Open();

                foreach (AuditTable tbl in tbls)
                {
                    DropAuditTriggers(tbl.TableName, connection);
                }

                //Drop the audit setup tables & procedures
                DropAuditSetupObjects(connection);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static void ClearAuditing(string tableName)
        {
            //SqlCommand command;
            SqlConnection connection = null;
            try
            {
                //Drop the audit triggers

                connection = GetConnection();
                connection.Open();

                DropAuditTriggers(tableName, connection);

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        //This procedure dropes existing auditlog triggers if any and
        //create the audit log triggers using the given table settings.
        public static void BuildAuditTriggers(AuditTable auditedTable)
        {
            SqlCommand command;

            SqlConnection connection = null;
            try
            {

                string[] triggers =  {  InsertTriggerName(auditedTable.TableName),
                                        UpdateTriggerName(auditedTable.TableName),
                                        DeleteTriggerName(auditedTable.TableName) };

                string userColumn = auditedTable.UserColumn;

                connection = GetConnection();
                connection.Open();
                StringBuilder sb;//= new StringBuilder();

                #region Drop Existing Triggers

                DropAuditTriggers(auditedTable.TableName, connection);

                #endregion  Drop Existing Triggers

                #region Insert Trigger

                if (auditedTable.AuditInserts)
                {
                    sb = new StringBuilder();

                    sb.AppendLine("CREATE TRIGGER [" + triggers[0] + "] ON [dbo].[" + auditedTable.TableName + "] FOR INSERT AS ");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine();
                    sb.AppendLine("SET NOCOUNT ON");
                    sb.AppendLine();
                    sb.AppendLine("DECLARE @ValidRun INT, @RowsAffected INT, @AuditedUser VARCHAR(10), @AuditID bigint");
                    sb.AppendLine("SELECT @RowsAffected = COUNT(*) FROM INSERTED");
                    sb.AppendLine();
                    sb.AppendLine("IF( @RowsAffected = 1 )");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine("SELECT @AuditedUser = (" + userColumn + ") FROM INSERTED");
                    sb.AppendLine();
                    sb.AppendLine("EXEC @ValidRun = dbo.[PInsert_AuditLog] @AuditID OUTPUT," + auditedTable.TableId.ToString() + ", @RowsAffected, 'I',@AuditedUser");
                    sb.AppendLine("IF( @ValidRun <> 0 )");
                    sb.AppendLine("RETURN");
                    sb.AppendLine();
                    if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
                    {
                        sb.AppendLine("-- Populate audit detail records...");
                        foreach (AuditColumn ac in auditedTable.AuditColumns)
                        {
                            sb.AppendLine();
                            sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  NewValue )");
                            sb.AppendLine("SELECT @AuditID, '" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), [" + auditedTable.PrimaryKeyField + "] ),");
                            sb.AppendLine(ac.ColumnId.ToString() + ", CONVERT( VARCHAR(4000), [" + ac.ColumnName + "] )");
                            sb.AppendLine("FROM  INSERTED");
                        }
                        sb.AppendLine();

                        sb.AppendLine("END  -- IF( @RowsAffected > 0 )");
                        sb.AppendLine("END");

                    }

                    command = new SqlCommand(sb.ToString(), connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    sb = null;
                }


                #endregion Insert Trigger

                #region Update Trigger

                if (auditedTable.AuditUpdates)
                {
                    sb = new StringBuilder();

                    sb.AppendLine("CREATE TRIGGER [" + triggers[1] + "] ON [dbo].[" + auditedTable.TableName + "] FOR UPDATE AS ");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine();
                    sb.AppendLine("SET NOCOUNT ON");
                    sb.AppendLine();
                    sb.AppendLine("DECLARE @ValidRun INT, @RowsAffected INT, @AuditedUser VARCHAR(10), @AuditID bigint, ");
                    sb.AppendLine("@RowKey VARCHAR(512), @NewValue VARCHAR(4000), @OldValue VARCHAR(4000)");
                    sb.AppendLine("SELECT @RowsAffected = COUNT(*) FROM INSERTED");
                    sb.AppendLine();
                    sb.AppendLine("IF( @RowsAffected = 1 )");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine("SELECT @AuditedUser = [" + userColumn + "],@RowKey='" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), [" + auditedTable.PrimaryKeyField + "]) FROM INSERTED");
                    //sb.AppendLine("SELECT @AuditedUser = (" + userColumn + ") FROM INSERTED");
                    sb.AppendLine();
                    sb.AppendLine("EXEC @ValidRun = dbo.[PInsert_AuditLog] @AuditID OUTPUT," + auditedTable.TableId.ToString() + ", @RowsAffected, 'U',@AuditedUser");
                    sb.AppendLine("IF( @ValidRun <> 0 )");
                    sb.AppendLine("RETURN");
                    sb.AppendLine();
                    if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
                    {
                        sb.AppendLine("-- Populate audit detail records...");
                        foreach (AuditColumn ac in auditedTable.AuditColumns)
                        {
                            sb.AppendLine("IF( UPDATE( [" + ac.ColumnName + "] ))");
                            sb.AppendLine("BEGIN");
                            sb.AppendLine();
                            sb.AppendLine("SELECT @NewValue=CONVERT( VARCHAR(4000), [" + ac.ColumnName + "])   FROM INSERTED ");
                            sb.AppendLine("SELECT @OldValue=CONVERT( VARCHAR(4000), [" + ac.ColumnName + "])   FROM DELETED ");
                            sb.AppendLine("IF(ISNULL(@OldValue,'') <> ISNULL(@NewValue,''))");

                            sb.AppendLine("BEGIN");
                            sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  OldValue , NewValue)");
                            sb.AppendLine("VALUES (@AuditID, @RowKey," + ac.ColumnId.ToString() + ",@OldValue,@NewValue)");

                            sb.AppendLine("END");

                            //sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  OldValue , NewValue)");
                            //sb.AppendLine("SELECT @AuditID, '" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), MAX(i.[" + auditedTable.PrimaryKeyField + "])),");
                            //sb.AppendLine(ac.ColumnId.ToString() + ", CONVERT( VARCHAR(4000),MAX(d.[" + ac.ColumnName + "] )),CONVERT( VARCHAR(4000), MAX(i.[" + ac.ColumnName + "] ))");
                            //sb.AppendLine("FROM  INSERTED i ");
                            //sb.AppendLine("FULL OUTER JOIN DELETED d ON ( d.[" + auditedTable.PrimaryKeyField + "] = i.[" + auditedTable.PrimaryKeyField + "])");
                            //sb.AppendLine("WHERE");
                            //sb.AppendLine("( d.[" + ac.ColumnName + "] <> i.[" + ac.ColumnName + "] ) OR");
                            //sb.AppendLine("( d.[" + ac.ColumnName + "] IS NULL AND i.[" + ac.ColumnName + "] IS NOT NULL ) OR");
                            //sb.AppendLine("( i.[" + ac.ColumnName + "] IS NULL AND d.[" + ac.ColumnName + "] IS NOT NULL )");
                            sb.AppendLine("END");
                            sb.AppendLine();
                        }
                        sb.AppendLine();

                        sb.AppendLine("END  -- IF( @RowsAffected > 0 )");
                        sb.AppendLine("END");

                    }

                    command = new SqlCommand(sb.ToString(), connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    sb = null;
                }
                #endregion Update Trigger

                #region Delete Trigger

                if (auditedTable.AuditDeletes)
                {
                    sb = new StringBuilder();

                    sb.AppendLine("CREATE TRIGGER [" + triggers[2] + "] ON [dbo].[" + auditedTable.TableName + "] FOR DELETE AS ");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine();
                    sb.AppendLine("SET NOCOUNT ON");
                    sb.AppendLine();
                    sb.AppendLine("DECLARE @ValidRun INT, @RowsAffected INT, @AuditedUser VARCHAR(10), @AuditID bigint");
                    sb.AppendLine("SELECT @RowsAffected = COUNT(*) FROM DELETED");
                    sb.AppendLine();
                    sb.AppendLine("IF( @RowsAffected = 1 )");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine("SELECT @AuditedUser = (" + userColumn + ") FROM DELETED");
                    sb.AppendLine();
                    sb.AppendLine("EXEC @ValidRun = dbo.[PInsert_AuditLog] @AuditID OUTPUT," + auditedTable.TableId.ToString() + ", @RowsAffected, 'D',@AuditedUser");
                    sb.AppendLine("IF( @ValidRun <> 0 )");
                    sb.AppendLine("RETURN");
                    sb.AppendLine();
                    if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
                    {
                        sb.AppendLine("-- Populate audit detail records...");
                        foreach (AuditColumn ac in auditedTable.AuditColumns)
                        {
                            sb.AppendLine();
                            sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  OldValue )");
                            sb.AppendLine("SELECT @AuditID, '" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), [" + auditedTable.PrimaryKeyField + "] ),");
                            sb.AppendLine(ac.ColumnId.ToString() + ", CONVERT( VARCHAR(4000), [" + ac.ColumnName + "] )");
                            sb.AppendLine("FROM  DELETED");
                        }
                        sb.AppendLine();

                        sb.AppendLine("END  -- IF( @RowsAffected > 0 )");
                        sb.AppendLine("END");

                    }

                    command = new SqlCommand(sb.ToString(), connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    sb = null;
                }
                #endregion Delete Trigger

                //command = new SqlCommand(sb.ToString(), connection);
                //command.ExecuteNonQuery();

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static List<AuditColumn> GetAuditedColumns(int tableId)
        {
            return GetAuditedColumns(tableId, "");
        }

        public static List<AuditColumn> GetAuditedColumns(int tableId, string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM AuditColumns WHERE TableId = " + tableId;
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<AuditColumn> columns = new List<AuditColumn>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        columns.Add(new AuditColumn
                        {
                            TableId = Convert.ToInt32(reader["TableId"]),
                            ColumnName = reader["ColumnName"].ToString(),
                            ColumnId = Convert.ToInt32(reader["ColumnID"])
                        }
                        );
                    }
                }
                return columns;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
       
        public static List<AuditTable> GetAuditedTables()
        {
            return GetAuditedTables("");
        }

        public static List<AuditTable> GetAuditedTables(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM AuditTables";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<AuditTable> tables = new List<AuditTable>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tables.Add(new AuditTable
                        {
                            TableId = Convert.ToInt32(reader["TableId"]),
                            TableName = reader["TableName"].ToString(),
                            PrimaryKeyField = reader["PKeyField"].ToString(),
                            AuditInserts = Convert.ToBoolean(reader["AuditInserts"]),
                            AuditUpdates = Convert.ToBoolean(reader["AuditUpdates"]),
                            AuditDeletes = Convert.ToBoolean(reader["AuditDeletes"]),
                            Description = reader["Description"].ToString(),
                            UserColumn = reader["UserColumn"].ToString()
                        }
                        );
                    }
                }
                return tables;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static AuditTable GetAuditedTable(string tableName)
        {
            return GetAuditedTable(tableName, "");
        }

        public static AuditTable GetAuditedTable(string tableName,string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM AuditTables WHERE [TableName] = '" + tableName + "'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                //AuditTable tbl = new AuditTable();

                if (reader.HasRows)
                {
                    reader.Read();
                    return new AuditTable
                    {
                        TableId = Convert.ToInt32(reader["TableId"]),
                        TableName = reader["TableName"].ToString(),
                        PrimaryKeyField = reader["PKeyField"].ToString(),
                        AuditInserts = Convert.ToBoolean(reader["AuditInserts"]),
                        AuditUpdates = Convert.ToBoolean(reader["AuditUpdates"]),
                        AuditDeletes = Convert.ToBoolean(reader["AuditDeletes"]),
                        UserColumn = reader["UserColumn"].ToString(),
                        Description = reader["Description"].ToString(),
                        AuditColumns = GetAuditedColumns(Convert.ToInt32(reader["TableId"]),connectionString)
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
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

        public static List<string> GetAllDBTables()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                string sql = "SELECT [name] FROM sysobjects WHERE type = 'U' ORDER BY [name]";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> tables = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tables.Add(reader["name"].ToString());
                    }
                }
                return tables;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static List<string> GetColumns(string tableName)
        {

            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                string sql = " SELECT [name] FROM syscolumns WHERE [id] = OBJECT_ID('" + tableName + "') ";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> columns = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        columns.Add(reader["name"].ToString());
                    }
                }
                return columns;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static List<string> GetPrimaryKey(string tableName)
        {
            return GetPrimaryKey(tableName, "");
        }

        public static List<string> GetPrimaryKey(string tableName,string connectionString)
        {

            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = " exec sp_pkeys '" + tableName + "' ";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> columns = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        columns.Add(reader["COLUMN_NAME"].ToString());
                    }
                }
                return columns;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static List<string> GetUnAuditedDBTables()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                string sql = "SELECT [name] FROM sysobjects WHERE type = 'U' and [name] NOT IN (SELECT [TableName] FROM AuditTables) ORDER BY [name]";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> tables = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tables.Add(reader["name"].ToString());
                    }
                }
                return tables;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static bool UpdateAuditTable(AuditTable auditedTable)
        {
            SqlCommand command;
            SqlTransaction transaction;
            SqlConnection connection = null;
            int tableId = auditedTable.TableId;


            connection = GetConnection();
            connection.Open();

            transaction = connection.BeginTransaction();
            command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                if (auditedTable.TableId == 0) //New Entry
                {
                    command.CommandText = "INSERT INTO AuditTables (TableName, Description, PKeyField, AuditInserts, AuditUpdates, AuditDeletes,UserColumn ) " +
                                        "VALUES('" + auditedTable.TableName + "','" + auditedTable.Description + "','" + auditedTable.PrimaryKeyField + "'," +
                                        (auditedTable.AuditInserts ? "1," : "0,") + (auditedTable.AuditUpdates ? "1," : "0,") + (auditedTable.AuditDeletes ? "1,'" : "0,'") + auditedTable.UserColumn + "')";
                    command.ExecuteNonQuery();
                    //Get the generated table id
                    command.CommandText = "SELECT [TableId] FROM AuditTables WHERE [TableName]='" + auditedTable.TableName + "'";
                    tableId = Convert.ToInt32(command.ExecuteScalar().ToString());
                }
                else
                {
                    command.CommandText = "UPDATE  AuditTables SET Description = '" + auditedTable.Description + "', AuditInserts=" + (auditedTable.AuditInserts ? "1" : "0") +
                                         ", AuditUpdates=" + (auditedTable.AuditUpdates ? "1" : "0") + ",AuditDeletes=" + (auditedTable.AuditDeletes ? "1" : "0") + ", UserColumn = '" + auditedTable.UserColumn +
                                         "' WHERE TableId = " + auditedTable.TableId.ToString();
                    command.ExecuteNonQuery();
                    //command.CommandText = "DELETE AuditColumns WHERE TableId = " + auditedTable.TableId.ToString();
                    //command.ExecuteNonQuery();
                }
                if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
                {
                    foreach (AuditColumn ac in auditedTable.AuditColumns)
                    {
                        long columnId = 0;
                        command.CommandText = "SELECT [ColumnId] FROM AuditColumns WHERE [ColumnName]='" + ac.ColumnName + "'";
                        columnId = Convert.ToInt64(command.ExecuteScalar()!=null?command.ExecuteScalar().ToString():"0");

                        if(columnId==0)command.CommandText = "INSERT INTO AuditColumns (TableId, ColumnName) " +
                                              "VALUES(" + tableId.ToString() + ",'" + ac.ColumnName + "')";
                        command.ExecuteNonQuery();
                    }
                }
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

        private static string DeleteTriggerName(string tableName)
        {
            return "trAudit_" + tableName + "_Del";
        }

        private static string InsertTriggerName(string tableName)
        {
            return "trAudit_" + tableName + "_Ins";
        }

        private static string UpdateTriggerName(string tableName)
        {
            return "trAudit_" + tableName + "_Upd";
        }

        private static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = null;
            if (connectionString != "")
            {
                connection = new SqlConnection(connectionString);
            }
            else
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["HIRASDBConnectionString"];
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

        private static void DropAuditSetupObjects(SqlConnection connection)
        {
            SqlCommand command;
            StringBuilder sb = new StringBuilder();

            try
            {

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuditLogDetail_AuditLog]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)");
                sb.AppendLine("ALTER TABLE [dbo].[AuditLogDetail] DROP CONSTRAINT FK_AuditLogDetail_AuditLog");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuditColumns_AuditTables]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)");
                sb.AppendLine("ALTER TABLE [dbo].[AuditColumns] DROP CONSTRAINT FK_AuditColumns_AuditTables");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuditLog_AuditTables]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)");
                sb.AppendLine("ALTER TABLE [dbo].[AuditLog] DROP CONSTRAINT FK_AuditLog_AuditTables");

                //sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuditColumns1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)");
                //sb.AppendLine("ALTER TABLE [dbo].[AuditColumns] DROP CONSTRAINT FK_AuditColumns1");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PInsert_AuditLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                sb.AppendLine("drop procedure [dbo].[PInsert_AuditLog]");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vwAuditLog]') and OBJECTPROPERTY(id, N'IsView') = 1)");
                sb.AppendLine("drop view [dbo].[vwAuditLog]");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuditTables]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                sb.AppendLine("drop table [dbo].[AuditTables]");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuditColumns]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                sb.AppendLine("drop table [dbo].[AuditColumns]");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuditLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                sb.AppendLine("drop table [dbo].[AuditLog]");

                sb.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuditLogDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                sb.AppendLine("drop table [dbo].[AuditLogDetail]");

                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();

            }
            finally
            {
                sb = null;
            }
        }

        private static void CreateAuditSetupObjects(SqlConnection connection)
        {
            SqlCommand command;
            StringBuilder sb = new StringBuilder();

            try
            {
                #region Create Table

                sb.AppendLine("CREATE TABLE [dbo].[AuditTables] (");
                sb.AppendLine("[TableID] [bigint] IDENTITY (1, 1) NOT NULL ,");
                sb.AppendLine("[TableName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,");
                sb.AppendLine("[Description] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,");
                sb.AppendLine("[PKeyField] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,");
                sb.AppendLine("[AuditInserts] [bit] NOT NULL ,");
                sb.AppendLine("[AuditUpdates] [bit] NOT NULL ,");
                sb.AppendLine("[AuditDeletes] [bit] NOT NULL ,");
                sb.AppendLine("[UserColumn] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_AuditTables_UserColumn] DEFAULT ('NA')");
                sb.AppendLine(") ON [PRIMARY]");

                sb.AppendLine("CREATE TABLE [dbo].[AuditColumns] (");
                sb.AppendLine("[ColumnID] [bigint] IDENTITY (1, 1) NOT NULL ,");
                sb.AppendLine("[TableID] [bigint] NOT NULL ,");
                sb.AppendLine("[ColumnName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ");
                sb.AppendLine(") ON [PRIMARY]");

                sb.AppendLine("CREATE TABLE [dbo].[AuditLog] (");
                sb.AppendLine("[AuditLogID] [bigint] IDENTITY (1, 1) NOT NULL ,");
                sb.AppendLine("[TableID] [bigint] NOT NULL ,");
                sb.AppendLine("[RowsAffected] [bit] NOT NULL ,");
                sb.AppendLine("[Event] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,");
                sb.AppendLine("[EventDateTime] [datetime] NOT NULL ,");
                sb.AppendLine("[UserName] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ");
                sb.AppendLine(") ON [PRIMARY]");

                sb.AppendLine("CREATE TABLE [dbo].[AuditLogDetail] (");
                sb.AppendLine("[AuditLogDetailID] [bigint] IDENTITY (1, 1) NOT NULL ,");
                sb.AppendLine("[AuditLogID] [bigint] NOT NULL ,");
                sb.AppendLine("[RowKey] [varchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,");
                sb.AppendLine("[ColumnID] [bigint] NOT NULL ,");
                sb.AppendLine("[OldValue] [varchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,");
                sb.AppendLine("[NewValue] [varchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ");
                sb.AppendLine(") ON [PRIMARY]");

                sb.AppendLine("ALTER TABLE [dbo].[AuditColumns] WITH NOCHECK ADD CONSTRAINT [PK_AuditColumns] PRIMARY KEY  CLUSTERED ");
                sb.AppendLine("( [ColumnID] )  ON [PRIMARY] ");

                sb.AppendLine("ALTER TABLE [dbo].[AuditLog] WITH NOCHECK ADD CONSTRAINT [PK_AuditLog] PRIMARY KEY  CLUSTERED ");
                sb.AppendLine("( [AuditLogID] )  ON [PRIMARY] ");

                sb.AppendLine("ALTER TABLE [dbo].[AuditLogDetail] WITH NOCHECK ADD 	CONSTRAINT [PK_AuditLogDetail] PRIMARY KEY  CLUSTERED ");
                sb.AppendLine("(	[AuditLogDetailID]	)  ON [PRIMARY] ");

                sb.AppendLine("ALTER TABLE [dbo].[AuditTables] WITH NOCHECK ADD CONSTRAINT [PK_AuditTables] PRIMARY KEY  CLUSTERED ");
                sb.AppendLine("(	[TableID]	)  ON [PRIMARY] ");

                //sb.AppendLine("ALTER TABLE [dbo].[AuditTables] ADD 	CONSTRAINT [DF_AuditTables_UserColumn] DEFAULT ('NA') FOR [UserColumn] ");

                sb.AppendLine("ALTER TABLE [dbo].[AuditColumns] ADD CONSTRAINT [FK_AuditColumns_AuditTables] FOREIGN KEY (	[TableID]	) REFERENCES [dbo].[AuditTables] (	[TableID]) ON UPDATE CASCADE ");

                sb.AppendLine("ALTER TABLE [dbo].[AuditLog] ADD CONSTRAINT [FK_AuditLog_AuditTables] FOREIGN KEY (	[TableID] ) REFERENCES [dbo].[AuditTables] ([TableID]	) ON UPDATE CASCADE ");

                sb.AppendLine("ALTER TABLE [dbo].[AuditLogDetail] ADD CONSTRAINT [FK_AuditLogDetail_AuditLog] FOREIGN KEY ");
                sb.AppendLine("( [AuditLogID] ) REFERENCES [dbo].[AuditLog] ([AuditLogID]) ON DELETE CASCADE ON UPDATE CASCADE ");

                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();

                #endregion

                #region Create Procedure

                sb = null;
                sb = new StringBuilder();

                sb.AppendLine("CREATE PROCEDURE [PInsert_AuditLog]");
                sb.AppendLine("(@AuditLogID 	[bigint] OUTPUT,");
                sb.AppendLine("@TableID 	[bigint],");
                sb.AppendLine("@RowsAffected 	[int],");
                sb.AppendLine("@Event 	[char](1),");
                sb.AppendLine("@UserName 	[varchar](10))");
                sb.AppendLine();
                sb.AppendLine("AS INSERT INTO ["+AuditedDataBase+"].[dbo].[AuditLog] ");
                sb.AppendLine(" (  [TableID], [RowsAffected], [Event], [EventDateTime], [UserName]) ");
                sb.AppendLine("VALUES (  @TableID, @RowsAffected, @Event, getdate(), @UserName) ");
                sb.AppendLine("SET @AuditLogID = @@IDENTITY");
                sb.AppendLine("RETURN @@ERROR");

                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();

                #endregion

                #region Create View

                sb = null;
                sb = new StringBuilder();
                sb.AppendLine("CREATE VIEW vwAuditLog AS");
                sb.AppendLine("SELECT L.AuditLogID, T.TableID AS OperationId, T.TableName,T.Description, C.ColumnID, C.ColumnName, D.RowKey, D.OldValue, D.NewValue, L.Event, L.UserName, L.EventDateTime ");
                sb.AppendLine("FROM AuditLogDetail D INNER JOIN AuditLog L ON D.AuditLogID = L.AuditLogID INNER JOIN ");
                sb.AppendLine("AuditTables T ON L.TableID = T.TableID INNER JOIN AuditColumns C ON D.ColumnID = C.ColumnID ");

                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();

                #endregion Create View
            }
            finally
            {
                sb = null;
            }
        }

        private static void DropAuditTriggers(string tableName, SqlConnection connection)
        {
            SqlCommand command;
            StringBuilder sb = new StringBuilder();
            try
            {

                string[] triggers =  {  InsertTriggerName(tableName),
                                        UpdateTriggerName(tableName),
                                        DeleteTriggerName(tableName) };

                foreach (string tr in triggers)
                {
                    sb.AppendLine(" IF EXISTS (SELECT name FROM sysobjects WHERE name = '" + tr + "' AND type = 'TR')");
                    sb.AppendLine(" DROP TRIGGER " + tr + ";");
                }
                command = new SqlCommand(sb.ToString(), connection);
                command.ExecuteNonQuery();

            }
            finally
            {
                sb = null;
            }
        }

        public static bool DbHasAuditSetupTables()
        {

            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                string sql = "SELECT Count([name]) AS Nos FROM sysobjects WHERE (type = 'U' OR type = 'P') AND [name] in('AuditLog','AuditLogDetail','AuditColumns','AuditTables','PInsert_AuditLog') ";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                int count = (int)command.ExecuteScalar();
                return (count == 5);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        #region Audit Log Data Manipulation

        public static AuditDataset.vwAuditLogDataTable GetAuditLogData(AuditCriteria auditCriteria)
        {
            return GetAuditLogData(GetConnection("").ConnectionString , auditCriteria);
        }

        public static AuditDataset.vwAuditLogDataTable GetAuditLogData(string connectionString , AuditCriteria auditCriteria)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                string sql = "SELECT * FROM vwAuditLog ";
                AuditDataset.vwAuditLogDataTable dt = new AuditDataset.vwAuditLogDataTable();
                StringBuilder sbCriteria = new StringBuilder();

                #region Add Criterias

                if (auditCriteria.AuditDelete || auditCriteria.AuditUpdate || auditCriteria.AuditInsert)
                {
                    if (auditCriteria.AuditDelete && auditCriteria.AuditUpdate && auditCriteria.AuditInsert)
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('D','I','U')");
                    }
                    else if (auditCriteria.AuditDelete && auditCriteria.AuditUpdate)
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('D','U')");
                    }
                    else if (auditCriteria.AuditDelete && auditCriteria.AuditInsert)
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('D','I')");
                    }
                    else if (auditCriteria.AuditUpdate && auditCriteria.AuditInsert)
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('I','U')");
                    }
                    else if (auditCriteria.AuditInsert)
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('I')");
                    }
                    else if (auditCriteria.AuditUpdate )
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('U')");
                    }
                    else if (auditCriteria.AuditDelete)
                    {
                        sbCriteria.AppendLine("WHERE [Event] IN ('D')");
                    }
                    else
                    {

                    }
                    //sbCriteria.AppendLine("WHERE [Event] IN (" + (auditCriteria.AuditDelete ? "'D'," : "") + (auditCriteria.AuditInsert ? "'I'," : "") + (auditCriteria.AuditUpdate ? "'U'" : "") + ")");
                }
                else
                {
                    return dt;
                }

                if (auditCriteria.OperationId > 0)
                    sbCriteria.AppendLine(" AND (OperationId = " + auditCriteria.OperationId.ToString() + ") ");
                if (auditCriteria.ColumnId > 0)
                    sbCriteria.AppendLine(" AND (ColumnID   = " + auditCriteria.ColumnId.ToString() + ")");
                if (auditCriteria.UserName != "")
                    sbCriteria.AppendLine(" AND (UserName = '" + auditCriteria.UserName + "')");
                if (auditCriteria.RowKey != "")
                    sbCriteria.AppendLine(" AND (RowKey = '" + auditCriteria.RowKey + "')");
                if (auditCriteria.FromDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    sbCriteria.AppendLine(" AND (EventDateTime  >= " + FormatDate((DateTime)auditCriteria.FromDate) + ")");
                if (auditCriteria.ToDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    sbCriteria.AppendLine(" AND (EventDateTime  <= " + FormatDate((DateTime)auditCriteria.ToDate) + ")");

                #endregion
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql + sbCriteria.ToString(), connection);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
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

        public static int PurgeAuditLogData(string connectionString, DateTime beforeDate)
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(connectionString);
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandText = "DELETE AuditLog WHERE EventDateTime <= " + FormatDate(beforeDate);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
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

        private static string FormatDate(DateTime txtDate)
        {
            return " CONVERT(DATETIME,'" + txtDate.ToString("dd/MM/yyyy") + "',103)";
        }

        #endregion

        //public static bool UpdateAuditTable(AuditTable auditedTable)
        //{
        //    SqlCommand command;
        //    SqlTransaction transaction;
        //    SqlConnection connection = null;
        //    int tableId = auditedTable.TableId;


        //    connection = GetConnection();
        //    connection.Open();

        //    transaction = connection.BeginTransaction();
        //    command = connection.CreateCommand();
        //    command.Transaction = transaction;

        //    try
        //    {
        //        if (auditedTable.TableId == 0) //New Entry
        //        {
        //            command.CommandText = "INSERT INTO AuditTables (TableName, PKeyField, AuditInserts, AuditUpdates, AuditDeletes ) " +
        //                                "VALUES('" + auditedTable.TableName + "','" + auditedTable.PrimaryKeyField + "'," +
        //                                (auditedTable.AuditInserts ? "1," : "0,") + (auditedTable.AuditUpdates ? "1," : "0,") + (auditedTable.AuditDeletes ? "1" : "0");
        //            command.ExecuteNonQuery();
        //            //Get the generated table id
        //            tableId = GetAuditedTable(auditedTable.TableName).TableId;
        //        }
        //        else
        //        {
        //            command.CommandText = "UPDATE  AuditTables SET AuditInserts=0,AuditUpdates=0,AuditDeletes=0 WHERE TableId = " + auditedTable.TableId.ToString();
        //            command.ExecuteNonQuery();
        //            command.CommandText = "DELETE * AuditColumns WHERE TableId = " + auditedTable.TableId.ToString();
        //            command.ExecuteNonQuery();
        //        }
        //        if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
        //        {
        //            foreach (AuditColumn ac in auditedTable.AuditColumns)
        //            {
        //                command.CommandText = "INSERT INTO AuditColumns (TableId, ColumnName) " +
        //                                      "VALUES(" + tableId.ToString() + ",'" + ac.ColumnName + "')";
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        transaction.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //        {
        //            connection.Close();
        //        }
        //    }

        //}

        //This procedure dropes existing auditlog triggers if any and
        //create the audit log triggers using the given table settings.
        //public static void BuildAuditTriggers(AuditTable auditedTable)
        //{
        //    SqlCommand command;

        //    SqlConnection connection = null;
        //    try
        //    {

        //        string[] triggers =  {  InsertTriggerName(auditedTable.TableName),
        //                                UpdateTriggerName(auditedTable.TableName),
        //                                DeleteTriggerName(auditedTable.TableName) };

        //        string userColumn = auditedTable.UserColumn;

        //        connection = GetConnection();
        //        connection.Open();
        //        StringBuilder sb;//= new StringBuilder();

        //        #region Drop Existing Triggers

        //        DropAuditTriggers(auditedTable.TableName, connection);

        //        #endregion  Drop Existing Triggers

        //        #region Insert Trigger

        //        if (auditedTable.AuditInserts)
        //        {
        //            sb = new StringBuilder();

        //            sb.AppendLine("CREATE TRIGGER [" + triggers[0] + "] ON [dbo].[" + auditedTable.TableName + "] FOR INSERT AS ");
        //            sb.AppendLine("BEGIN");
        //            sb.AppendLine();
        //            sb.AppendLine("SET NOCOUNT ON");
        //            sb.AppendLine();
        //            sb.AppendLine("DECLARE @ValidRun INT, @RowsAffected INT, @AuditedUser VARCHAR(10), @AuditID bigint");
        //            sb.AppendLine("SELECT @RowsAffected = COUNT(*) FROM INSERTED");
        //            sb.AppendLine();
        //            sb.AppendLine("IF( @RowsAffected > 0 )");
        //            sb.AppendLine("BEGIN");
        //            sb.AppendLine("SELECT @AuditedUser = (" + userColumn + ") FROM INSERTED");
        //            sb.AppendLine();
        //            sb.AppendLine("EXEC @ValidRun = dbo.[PInsert_AuditLog] @AuditID OUTPUT," + auditedTable.TableId.ToString() + ", @RowsAffected, 'I',@AuditedUser");
        //            sb.AppendLine("IF( @ValidRun <> 0 )");
        //            sb.AppendLine("RETURN");
        //            sb.AppendLine();
        //            if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
        //            {
        //                sb.AppendLine("-- Populate audit detail records...");
        //                foreach (AuditColumn ac in auditedTable.AuditColumns)
        //                {
        //                    sb.AppendLine();
        //                    sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  NewValue )");
        //                    sb.AppendLine("SELECT @AuditID, '" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), [" + auditedTable.PrimaryKeyField + "] ),");
        //                    sb.AppendLine(ac.ColumnId.ToString() + ", CONVERT( VARCHAR(4000), [" + ac.ColumnName + "] )");
        //                    sb.AppendLine("FROM  INSERTED");
        //                }
        //                sb.AppendLine();

        //                sb.AppendLine("END  -- IF( @RowsAffected > 0 )");
        //                sb.AppendLine("END");

        //            }

        //            command = new SqlCommand(sb.ToString(), connection);
        //            command.ExecuteNonQuery();
        //            command.Dispose();
        //            sb = null;
        //        }


        //        #endregion Insert Trigger

        //        #region Update Trigger

        //        if (auditedTable.AuditUpdates)
        //        {
        //            sb = new StringBuilder();

        //            sb.AppendLine("CREATE TRIGGER [" + triggers[1] + "] ON [dbo].[" + auditedTable.TableName + "] FOR UPDATE AS ");
        //            sb.AppendLine("BEGIN");
        //            sb.AppendLine();
        //            sb.AppendLine("SET NOCOUNT ON");
        //            sb.AppendLine();
        //            sb.AppendLine("DECLARE @ValidRun INT, @RowsAffected INT, @AuditedUser VARCHAR(10), @AuditID bigint");
        //            sb.AppendLine("SELECT @RowsAffected = COUNT(*) FROM INSERTED");
        //            sb.AppendLine();
        //            sb.AppendLine("IF( @RowsAffected > 0 )");
        //            sb.AppendLine("BEGIN");
        //            sb.AppendLine("SELECT @AuditedUser = (" + userColumn + ") FROM INSERTED");
        //            sb.AppendLine();
        //            sb.AppendLine("EXEC @ValidRun = dbo.[PInsert_AuditLog] @AuditID OUTPUT," + auditedTable.TableId.ToString() + ", @RowsAffected, 'U',@AuditedUser");
        //            sb.AppendLine("IF( @ValidRun <> 0 )");
        //            sb.AppendLine("RETURN");
        //            sb.AppendLine();
        //            if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
        //            {
        //                sb.AppendLine("-- Populate audit detail records...");
        //                foreach (AuditColumn ac in auditedTable.AuditColumns)
        //                {
        //                    sb.AppendLine("IF( UPDATE( [" + ac.ColumnName + "] ))");
        //                    sb.AppendLine("BEGIN");
        //                    sb.AppendLine();
        //                    sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  OldValue , NewValue)");
        //                    sb.AppendLine("SELECT @AuditID, '" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), ISNULL(i.[" + auditedTable.PrimaryKeyField + "], d.[" + auditedTable.PrimaryKeyField + "])),");
        //                    sb.AppendLine(ac.ColumnId.ToString() + ", CONVERT( VARCHAR(4000), d.[" + ac.ColumnName + "] ),CONVERT( VARCHAR(4000), i.[" + ac.ColumnName + "] )");
        //                    sb.AppendLine("FROM  INSERTED i ");
        //                    sb.AppendLine("FULL OUTER JOIN DELETED d ON ( d.[" + auditedTable.PrimaryKeyField + "] = i.[" + auditedTable.PrimaryKeyField + "])");
        //                    sb.AppendLine("WHERE");
        //                    sb.AppendLine("( d.[" + ac.ColumnName + "] <> i.[" + ac.ColumnName + "] ) OR");
        //                    sb.AppendLine("( d.[" + ac.ColumnName + "] IS NULL AND i.[" + ac.ColumnName + "] IS NOT NULL ) OR");
        //                    sb.AppendLine("( i.[" + ac.ColumnName + "] IS NULL AND d.[" + ac.ColumnName + "] IS NOT NULL )");
        //                    sb.AppendLine("END");
        //                    sb.AppendLine();
        //                }
        //                sb.AppendLine();

        //                sb.AppendLine("END  -- IF( @RowsAffected > 0 )");
        //                sb.AppendLine("END");

        //            }

        //            command = new SqlCommand(sb.ToString(), connection);
        //            command.ExecuteNonQuery();
        //            command.Dispose();
        //            sb = null;
        //        }
        //        #endregion Update Trigger

        //        #region Delete Trigger

        //        if (auditedTable.AuditDeletes)
        //        {
        //            sb = new StringBuilder();

        //            sb.AppendLine("CREATE TRIGGER [" + triggers[2] + "] ON [dbo].[" + auditedTable.TableName + "] FOR DELETE AS ");
        //            sb.AppendLine("BEGIN");
        //            sb.AppendLine();
        //            sb.AppendLine("SET NOCOUNT ON");
        //            sb.AppendLine();
        //            sb.AppendLine("DECLARE @ValidRun INT, @RowsAffected INT, @AuditedUser VARCHAR(10), @AuditID bigint");
        //            sb.AppendLine("SELECT @RowsAffected = COUNT(*) FROM DELETED");
        //            sb.AppendLine();
        //            sb.AppendLine("IF( @RowsAffected > 0 )");
        //            sb.AppendLine("BEGIN");
        //            sb.AppendLine("SELECT @AuditedUser = (" + userColumn + ") FROM DELETED");
        //            sb.AppendLine();
        //            sb.AppendLine("EXEC @ValidRun = dbo.[PInsert_AuditLog] @AuditID OUTPUT," + auditedTable.TableId.ToString() + ", @RowsAffected, 'D',@AuditedUser");
        //            sb.AppendLine("IF( @ValidRun <> 0 )");
        //            sb.AppendLine("RETURN");
        //            sb.AppendLine();
        //            if (auditedTable.AuditColumns != null && auditedTable.AuditColumns.Count != 0)
        //            {
        //                sb.AppendLine("-- Populate audit detail records...");
        //                foreach (AuditColumn ac in auditedTable.AuditColumns)
        //                {
        //                    sb.AppendLine();
        //                    sb.AppendLine("INSERT dbo.AuditLogDetail( AuditLogID, RowKey, ColumnID,  OldValue )");
        //                    sb.AppendLine("SELECT @AuditID, '" + auditedTable.PrimaryKeyField + "=' + CONVERT( VARCHAR(512), [" + auditedTable.PrimaryKeyField + "] ),");
        //                    sb.AppendLine(ac.ColumnId.ToString() + ", CONVERT( VARCHAR(4000), [" + ac.ColumnName + "] )");
        //                    sb.AppendLine("FROM  DELETED");
        //                }
        //                sb.AppendLine();

        //                sb.AppendLine("END  -- IF( @RowsAffected > 0 )");
        //                sb.AppendLine("END");

        //            }

        //            command = new SqlCommand(sb.ToString(), connection);
        //            command.ExecuteNonQuery();
        //            command.Dispose();
        //            sb = null;
        //        }
        //        #endregion Delete Trigger

        //        //command = new SqlCommand(sb.ToString(), connection);
        //        //command.ExecuteNonQuery();

        //    }
        //    finally
        //    {
        //        if (connection != null)
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
    }
}

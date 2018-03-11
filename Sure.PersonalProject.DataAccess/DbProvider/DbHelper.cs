using System;

namespace Sure.PersonalProject.DataAccess.DbProvider
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DbHelper  
    -----------------------------------------------------------------------*/

    using Sure.PersonalProject.DataAccess.Enum;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using Sure.PersonalProject.DataAccess.Factory;
    using Utilities;

    /// <summary>
    /// Ado.net 数据访问 
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// 数据库访问类型
        /// </summary>
        public static Enum.DbType DbType { get; set; }

        /// <summary>
        /// 接收创建对象
        /// </summary>
        public static string DbParmChar { get; set; }

        /// <summary>
        /// 数据库访问格式
        /// </summary>
        /// <param name="value"></param>
        public void DatabaseTypeEnumParse(string value)
        {
            try
            {
                switch (value)
                {
                    case "System.Data.SqlClient":
                        DbHelper.DbType = Enum.DbType.SqlServer;
                        break;
                    case "System.Data.OracleClient":
                        DbHelper.DbType = Enum.DbType.Oracle;
                        break;
                    case "MySql.Data.MySqlClient":
                        DbHelper.DbType = Enum.DbType.MySql;
                        break;
                    case "System.Data.OleDb":
                        DbHelper.DbType = Enum.DbType.Access;
                        break;
                    case "System.Data.SQLite":
                        DbHelper.DbType = Enum.DbType.SQLite;
                        break;
                }
            }
            catch
            {
                throw new Exception("数据库类型\"" + value + "\"错误，请检查！");
            }
        }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="connstring">数据连接字符串</param>
        public DbHelper(string connstring)
        {
            //获取加密秘钥
            string str = ConfigurationManager.AppSettings["ConStringDESEncrypt"];
            //获取数据库链接字符串
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings[connstring].ConnectionString;
            if (str == "true")
                DbHelper.ConnectionString = DESEncryptGeneral.Decrypt(DbHelper.ConnectionString);
            this.DatabaseTypeEnumParse(ConfigurationManager.ConnectionStrings[connstring].ProviderName);
            DbHelper.DbParmChar = DbFactory.CreateDbParmCharacter();
        }

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                using (DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString))
                {
                    DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, parameters);
                    int num = dbCommand.ExecuteNonQuery();
                    dbCommand.Parameters.Clear();
                    return num;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                using (DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString))
                {
                    DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, (DbParameter[])null);
                    int num = dbCommand.ExecuteNonQuery();
                    dbCommand.Parameters.Clear();
                    return num;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, connection, (DbTransaction)null, cmdType, cmdText, parameters);
                int num = dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();
                return num;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, connection, (DbTransaction)null, cmdType, cmdText, (DbParameter[])null);
                int num = dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();
                return num;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int ExecuteNonQuery(DbTransaction isOpenTrans, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                int num = 0;
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                if (isOpenTrans == null || isOpenTrans.Connection == null)
                {
                    using (DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString))
                    {
                        DbHelper.PrepareCommand(dbCommand, dbConnection, isOpenTrans, cmdType, cmdText, parameters);
                        num = dbCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    DbHelper.PrepareCommand(dbCommand, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, parameters);
                    num = dbCommand.ExecuteNonQuery();
                }
                dbCommand.Parameters.Clear();
                return num;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int ExecuteNonQuery(DbTransaction isOpenTrans, CommandType cmdType, string cmdText)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, (DbParameter[])null);
                int num = dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();
                return num;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region ExecuteReader
        public static IDataReader ExecuteReader(DbTransaction isOpenTrans, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DbCommand dbCommand = DbFactory.CreateDbCommand();
            DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString);
            try
            {
                DbHelper.PrepareCommand(dbCommand, dbConnection, isOpenTrans, cmdType, cmdText, parameters);
                IDataReader dataReader = (IDataReader)dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dbCommand.Parameters.Clear();
                return dataReader;
            }
            catch (Exception)
            {
                dbConnection.Close();
                dbCommand.Dispose();

                throw;
            }
        }

        public static IDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DbCommand dbCommand = DbFactory.CreateDbCommand();
            DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString);
            try
            {
                DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, parameters);
                IDataReader dataReader = (IDataReader)dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dbCommand.Parameters.Clear();
                return dataReader;
            }
            catch (Exception)
            {
                dbConnection.Close();
                dbCommand.Dispose();

                throw;
            }
        }

        public static IDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            DbCommand dbCommand = DbFactory.CreateDbCommand();
            DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString);
            try
            {
                DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, (DbParameter[])null);
                IDataReader dataReader = (IDataReader)dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dbCommand.Parameters.Clear();
                return dataReader;
            }
            catch (Exception)
            {
                dbConnection.Close();
                dbCommand.Dispose();

                throw;
            }
        }
        #endregion

        #region GetDataSet
        public static DataSet GetDataSet(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DataSet dataSet = new DataSet();
            DbCommand dbCommand = DbFactory.CreateDbCommand();
            DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString);
            try
            {
                DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, parameters);
                DbFactory.CreateDataAdapter(dbCommand).Fill(dataSet);
                return dataSet;
            }
            catch (Exception)
            {
                dbConnection.Close();
                dbCommand.Dispose();

                throw;
            }
        }

        public static DataSet GetDataSet(CommandType cmdType, string cmdText)
        {
            DataSet dataSet = new DataSet();
            DbCommand dbCommand = DbFactory.CreateDbCommand();
            DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString);
            try
            {
                DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, (DbParameter[])null);
                DbFactory.CreateDataAdapter(dbCommand).Fill(dataSet);
                return dataSet;
            }
            catch (Exception)
            {
                dbConnection.Close();
                dbCommand.Dispose();

                throw;
            }
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                using (DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString))
                {
                    DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, parameters);
                    object obj = dbCommand.ExecuteScalar();
                    dbCommand.Parameters.Clear();
                    return obj;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                using (DbConnection dbConnection = DbFactory.CreateDbConnection(DbHelper.ConnectionString))
                {
                    DbHelper.PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, cmdType, cmdText, (DbParameter[])null);
                    object obj = dbCommand.ExecuteScalar();
                    dbCommand.Parameters.Clear();
                    return obj;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, connection, (DbTransaction)null, cmdType, cmdText, parameters);
                object obj = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, connection, (DbTransaction)null, cmdType, cmdText, (DbParameter[])null);
                object obj = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static object ExecuteScalar(DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, conn, isOpenTrans, cmdType, cmdText, (DbParameter[])null);
                object obj = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static object ExecuteScalar(DbTransaction isOpenTrans, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                DbCommand dbCommand = DbFactory.CreateDbCommand();
                DbHelper.PrepareCommand(dbCommand, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, parameters);
                object obj = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Private 

        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms == null)
                return;
            cmd.Parameters.AddRange((Array)cmdParms);
        }


        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sure.PersonalProject.DataAccess.Factory
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DbFactory  、 定义数据库访问类型
    -----------------------------------------------------------------------*/

    using MySql.Data.MySqlClient;
    using Oracle.DataAccess.Client;
    using Sure.PersonalProject.DataAccess.DbProvider;
    using Sure.PersonalProject.DataAccess.Enum;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Data.SQLite;

    /// <summary>
    /// Ado.net 数据访问工厂（创建数据库访问对象、Connection、Command、DataAdapter、入参对象（Parameter））
    /// </summary>
    public class DbFactory
    {
        public static string CreateDbParmCharacter()
        {
            string str;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    str = ":";
                    break;
                case Enum.DbType.SqlServer:
                    str = "@";
                    break;
                case Enum.DbType.Access:
                    str = "@";
                    break;
                case Enum.DbType.MySql:
                    str = "?";
                    break;
                case Enum.DbType.SQLite:
                    str = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持 ！ ");
            }
            return str;
        }

        /// <summary>
        /// 创建数据库 Connection 对象
        /// </summary>
        /// <param name="connectionString">数据库链接字符串</param>
        /// <returns>DbConnection</returns>
        public static DbConnection CreateDbConnection(string connectionString)
        {
            DbConnection dbConnection;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    dbConnection = (DbConnection)new OracleConnection(connectionString);
                    break;
                case Enum.DbType.SqlServer:
                    dbConnection = (DbConnection)new SqlConnection(connectionString);
                    break;
                case Enum.DbType.Access:
                    dbConnection = (DbConnection)new OleDbConnection(connectionString);
                    break;
                case Enum.DbType.MySql:
                    dbConnection = (DbConnection)new MySqlConnection(connectionString);
                    break;
                case Enum.DbType.SQLite:
                    dbConnection = (DbConnection)new SQLiteConnection(connectionString);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return dbConnection;
        }

        /// <summary>
        /// 创建数据库 Command 对象
        /// </summary>
        /// <returns>DbCommand</returns>
        public static DbCommand CreateDbCommand()
        {
            DbCommand dbCommand;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    dbCommand = (DbCommand)new OracleCommand();
                    break;
                case Enum.DbType.SqlServer:
                    dbCommand = (DbCommand)new SqlCommand();
                    break;
                case Enum.DbType.Access:
                    dbCommand = (DbCommand)new OleDbCommand();
                    break;
                case Enum.DbType.MySql:
                    dbCommand = (DbCommand)new MySqlCommand();
                    break;
                case Enum.DbType.SQLite:
                    dbCommand = (DbCommand)new SQLiteCommand();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return dbCommand;
        }

        /// <summary>
        /// 创建数据库 DataAdapter 对象
        /// </summary>
        /// <returns>IDbDataAdapter</returns>
        public static IDbDataAdapter CreateDataAdapter()
        {
            IDbDataAdapter dbDataAdapter;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    dbDataAdapter = (IDbDataAdapter)new OracleDataAdapter();
                    break;
                case Enum.DbType.SqlServer:
                    dbDataAdapter = (IDbDataAdapter)new SqlDataAdapter();
                    break;
                case Enum.DbType.Access:
                    dbDataAdapter = (IDbDataAdapter)new OleDbDataAdapter();
                    break;
                case Enum.DbType.MySql:
                    dbDataAdapter = (IDbDataAdapter)new MySqlDataAdapter();
                    break;
                case Enum.DbType.SQLite:
                    dbDataAdapter = (IDbDataAdapter)new SQLiteDataAdapter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return dbDataAdapter;
        }

        /// <summary>
        /// 创建数据库 DataAdapter 对象
        /// </summary>
        /// <returns>IDbDataAdapter</returns>
        public static IDbDataAdapter CreateDataAdapter(DbCommand cmd)
        {
            IDbDataAdapter dbDataAdapter;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    dbDataAdapter = (IDbDataAdapter)new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case Enum.DbType.SqlServer:
                    dbDataAdapter = (IDbDataAdapter)new SqlDataAdapter((SqlCommand)cmd);
                    break;
                case Enum.DbType.Access:
                    dbDataAdapter = (IDbDataAdapter)new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
                case Enum.DbType.MySql:
                    dbDataAdapter = (IDbDataAdapter)new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case Enum.DbType.SQLite:
                    dbDataAdapter = (IDbDataAdapter)new SQLiteDataAdapter((SQLiteCommand)cmd);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return dbDataAdapter;
        }

        #region DbParameter 创建 DbParameter 对象,每个数据库的入参对象都不一样

        public static DbParameter CreateDbParameter()
        {
            DbParameter dbParameter;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    dbParameter = (DbParameter)new OracleParameter();
                    break;
                case Enum.DbType.SqlServer:
                    dbParameter = (DbParameter)new SqlParameter();
                    break;
                case Enum.DbType.Access:
                    dbParameter = (DbParameter)new OleDbParameter();
                    break;
                case Enum.DbType.MySql:
                    dbParameter = (DbParameter)new MySqlParameter();
                    break;
                case Enum.DbType.SQLite:
                    dbParameter = (DbParameter)new SQLiteParameter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return dbParameter;
        }

        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter dbParameter = DbFactory.CreateDbParameter();
            dbParameter.ParameterName = paramName;
            dbParameter.Value = value;
            return dbParameter;
        }

        public static DbParameter CreateDbParameter(string paramName, object value, System.Data.DbType dbType)
        {
            DbParameter dbParameter = DbFactory.CreateDbParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = paramName;
            dbParameter.Value = value;
            return dbParameter;
        }

        public static DbParameter CreateDbParameter(string paramName, object value, System.Data.DbType dbType, int size)
        {
            DbParameter dbParameter = DbFactory.CreateDbParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = paramName;
            dbParameter.Value = value;
            dbParameter.Size = size;
            return dbParameter;
        }

        public static DbParameter CreateDbParameter(string paramName, object value, int size)
        {
            DbParameter dbParameter = DbFactory.CreateDbParameter();
            dbParameter.ParameterName = paramName;
            dbParameter.Value = value;
            dbParameter.Size = size;
            return dbParameter;
        }

        public static DbParameter CreateDbOutParameter(string paramName, int size)
        {
            DbParameter dbParameter = DbFactory.CreateDbParameter();
            dbParameter.Direction = ParameterDirection.Output;
            dbParameter.ParameterName = paramName;
            dbParameter.Size = size;
            return dbParameter;
        }

        public static DbParameter[] CreateDbParameters(int size)
        {
            int index = 0;
            DbParameter[] dbParameterArray;
            switch (DbProvider.DbHelper.DbType)
            {
                case Enum.DbType.Oracle:
                    dbParameterArray = (DbParameter[])new OracleParameter[size];
                    for (; index < size; ++index)
                        dbParameterArray[index] = (DbParameter)new OracleParameter();
                    break;
                case Enum.DbType.SqlServer:
                    dbParameterArray = (DbParameter[])new SqlParameter[size];
                    for (; index < size; ++index)
                        dbParameterArray[index] = (DbParameter)new SqlParameter();
                    break;
                case Enum.DbType.Access:
                    dbParameterArray = (DbParameter[])new OleDbParameter[size];
                    for (; index < size; ++index)
                        dbParameterArray[index] = (DbParameter)new OleDbParameter();
                    break;
                case Enum.DbType.MySql:
                    dbParameterArray = (DbParameter[])new MySqlParameter[size];
                    for (; index < size; ++index)
                        dbParameterArray[index] = (DbParameter)new MySqlParameter();
                    break;
                case Enum.DbType.SQLite:
                    dbParameterArray = (DbParameter[])new SQLiteParameter[size];
                    for (; index < size; ++index)
                        dbParameterArray[index] = (DbParameter)new SQLiteParameter();
                    break;
                default:
                    throw new Exception("不支持该数据库");
            }
            return dbParameterArray;
        }

        #endregion

    }
}

using System;

namespace Sure.PersonalProject.Utilities
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DBGeneral
    -----------------------------------------------------------------------*/
    using System.Collections;
    using System.Data.SqlClient;

    /// <summary>
    /// 创建实体公共类
    /// </summary>
    public class DBGeneral
    {
        public ArrayList GetDataByConAndSql(string strCon, string sql, out string msg)
        {
            ArrayList arrayList = new ArrayList();
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = (SqlDataReader)null;
                try
                {
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                        arrayList.Add((object)sqlDataReader[0].ToString());
                    msg = "登录成功！";
                }
                catch (SqlException ex)
                {
                    msg = ex.Message;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    if (sqlDataReader != null)
                        sqlDataReader.Close();
                    connection.Close();
                }
                return arrayList;
            }
        }

        public ArrayList GetFieldByConAndSql(string strCon, string sql)
        {
            ArrayList arrayList = new ArrayList();
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = (SqlDataReader)null;
                try
                {
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        string strCloumn = sqlDataReader[0].ToString().Substring(0, 1).ToLower() + sqlDataReader[0].ToString().Substring(1);
                        string strType = this.DBTpyeConvertCSType(sqlDataReader[1].ToString());
                        string strContent = sqlDataReader[2].ToString();
                        string strIdentity = sqlDataReader[3].ToString();
                        arrayList.Add((object)(strCloumn + "|" + strType + "|" + strContent + "|" + strIdentity));
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (sqlDataReader != null)
                        sqlDataReader.Close();
                    connection.Close();
                }
                return arrayList;
            }
        }

        private string DBTpyeConvertCSType(string dbtype)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add((object)"SQL", (object)"C#");
            hashtable.Add((object)"bigint", (object)"long");
            hashtable.Add((object)"binary", (object)"object");
            hashtable.Add((object)"bit", (object)"bool");
            hashtable.Add((object)"char", (object)"string");
            hashtable.Add((object)"datetime", (object)"DateTime");
            hashtable.Add((object)"decimal", (object)"decimal");
            hashtable.Add((object)"float", (object)"double");
            hashtable.Add((object)"image", (object)"byte[]");
            hashtable.Add((object)"int", (object)"int");
            hashtable.Add((object)"money", (object)"decimal");
            hashtable.Add((object)"nchar", (object)"string");
            hashtable.Add((object)"ntext", (object)"string");
            hashtable.Add((object)"numeric", (object)"decimal");
            hashtable.Add((object)"nvarchar", (object)"string");
            hashtable.Add((object)"real", (object)"float");
            hashtable.Add((object)"smalldatetime", (object)"DateTime");
            hashtable.Add((object)"smallint", (object)"short");
            hashtable.Add((object)"smallmoney", (object)"decimal");
            hashtable.Add((object)"text", (object)"string");
            hashtable.Add((object)"timestamp", (object)"byte[]");
            hashtable.Add((object)"tinyint", (object)"byte");
            hashtable.Add((object)"uniqueidentifier", (object)"Guid");
            hashtable.Add((object)"varbinary", (object)"byte[]");
            hashtable.Add((object)"varchar", (object)"string");
            hashtable.Add((object)"xml", (object)"string");
            hashtable.Add((object)"sql_variant", (object)"object");
            string str = "未知类型";
            if (!string.IsNullOrEmpty(dbtype))
            {
                try
                {
                    str = hashtable[(object)dbtype].ToString();
                    if (string.IsNullOrEmpty(str))
                        str = "未知类型";
                }
                catch
                {
                    str = "未知类型";
                }
            }
            return str;
        }

        public ArrayList GetFieldAndNullByConAndSql(string strCon, string sql)
        {
            ArrayList arrayList = new ArrayList();
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = (SqlDataReader)null;
                try
                {
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        string strOne = sqlDataReader[0].ToString().Substring(0, 1).ToLower() + sqlDataReader[0].ToString().Substring(1);
                        string strTwo = !("NO" == sqlDataReader[1].ToString().ToUpper()) ? "null" : "not null";
                        arrayList.Add((object)new AboutField()
                        {
                            col_name = strOne,
                            col_nullable = strTwo
                        });
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (sqlDataReader != null)
                        sqlDataReader.Close();
                    connection.Close();
                }
                return arrayList;
            }
        }
    }

    /// <summary>
    /// 创建实体对象
    /// </summary>
    public class AboutField
    {
        public string col_name;
        public string col_nullable;
        public AboutField() { }
        public AboutField(string col_name, string col_nullable)
        {
            this.col_name = col_name;
            this.col_nullable = col_nullable;
        }
    }
}

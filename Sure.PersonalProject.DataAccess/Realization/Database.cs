using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sure.PersonalProject.DataAccess.Realization
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: Database 、 接口实现  
    -----------------------------------------------------------------------*/

    using System.Data;
    using System.Data.Common;
    using System.Collections;
    using Sure.PersonalProject.DataAccess.Interface;
    using Sure.PersonalProject.DataAccess.DbProvider;
    using Sure.PersonalProject.DataAccess.Factory;

    /// <summary>
    /// Ado.net 数据访问类(实现接口)
    /// </summary>
    public class Database : IDatabase, IDisposable
    {
        #region 属性

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string connString { get; set; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        private DbConnection dbConnection { get; set; }

        /// <summary>
        /// 事物
        /// </summary>
        private DbTransaction isOpenTrans { get; set; }

        /// <summary>
        /// 定义事物状态
        /// </summary>
        public bool inTransaction { get; set; }

        #endregion

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="connstring">链接字符串</param>
        public Database(string connstring)
        {
            //创建DbHelper对象
            DbProvider.DbHelper dbHelper = new DbProvider.DbHelper(connstring);
        }

        #region DbTransaction 事物

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTrans()
        {
            if (!this.inTransaction)
            {
                this.dbConnection = DbFactory.CreateDbConnection(DbProvider.DbHelper.ConnectionString);
                if (this.dbConnection.State == ConnectionState.Closed)
                    this.dbConnection.Open();
                this.inTransaction = true;
                this.isOpenTrans = this.dbConnection.BeginTransaction();
            }
            return this.isOpenTrans;
        }

        /// <summary>
        /// 事物提交
        /// </summary>
        public void Commit()
        {
            if (!this.inTransaction)
                return;
            this.inTransaction = false;
            this.isOpenTrans.Commit();
        }

        /// <summary>
        /// 事物回滚
        /// </summary>
        public void Rollback()
        {
            if (!this.inTransaction)
                return;
            this.inTransaction = false;
            this.isOpenTrans.Rollback();
        }
        #endregion

        #region ExecuteBySql 执行sql语句

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>是否成功</returns>
        public int ExecuteBySql(StringBuilder strSql)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 执行SQL语句-事物
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>是否成功</returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 执行SQL语句-事物
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion

        #region ExecuteByProc 执行存储

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <returns>是否成功</returns>
        public int ExecuteByProc(string procName)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, procName);
        }

        /// <summary>
        /// 执行存储过程-事物
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int ExecuteByProc(string procName, DbTransaction isOpenTrans)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.StoredProcedure, procName);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="parameters">入参</param>
        /// <returns>是否成功</returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, procName, parameters);
        }

        /// <summary>
        /// 执行存储过程-事物
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="parameters">入参</param>
        /// <returns>是否成功</returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.StoredProcedure, procName, parameters);
        }

        #endregion

        #region Insert 新增

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <returns>是否成功</returns>
        public int Insert<T>(T entity)
        {
            object obj = (object)0;
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, DbHelperSqlGeneration.InsertSql<T>(entity).ToString(), DbHelperSqlGeneration.GetParameter<T>(entity)));
        }

        /// <summary>
        /// Insert-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Insert<T>(T entity, DbTransaction isOpenTrans)
        {
            object obj = (object)0;
            StringBuilder stringBuilder = DbHelperSqlGeneration.InsertSql<T>(entity);
            DbParameter[] parameter = DbHelperSqlGeneration.GetParameter<T>(entity);
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), parameter));
        }

        /// <summary>
        /// Insert-多条插入
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        public int Insert<T>(List<T> entity)
        {
            object obj1 = (object)0;
            DbTransaction isOpenTrans = this.BeginTrans();
            object obj2;
            try
            {
                foreach (T entity1 in entity)
                    this.Insert<T>(entity1, isOpenTrans);
                this.Commit();
                obj2 = (object)1;
            }
            catch (Exception ex)
            {
                this.Rollback();
                this.Close();
                obj1 = (object)-1;
                throw ex;
            }
            return Convert.ToInt32(obj2);
        }

        /// <summary>
        /// Insert-多条插入-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        public int Insert<T>(List<T> entity, DbTransaction isOpenTrans)
        {
            object obj1 = (object)0;
            object obj2;
            try
            {
                foreach (T entity1 in entity)
                    this.Insert<T>(entity1, isOpenTrans);
                obj2 = (object)1;
            }
            catch (Exception ex)
            {
                obj1 = (object)-1;
                throw ex;
            }
            return Convert.ToInt32(obj2);
        }

        /// <summary>
        /// Insert-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <returns>是否成功</returns>
        public int Insert(string tableName, Hashtable ht)
        {
            object obj = (object)0;
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, DbHelperSqlGeneration.InsertSql(tableName, ht).ToString(), DbHelperSqlGeneration.GetParameter(ht)));
        }

        /// <summary>
        /// Insert-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="ht">数据集</param>
        /// <returns>是否成功</returns>
        public int Insert(string tableName, Hashtable ht, DbTransaction isOpenTrans)
        {
            object obj = (object)0;
            StringBuilder stringBuilder = DbHelperSqlGeneration.InsertSql(tableName, ht);
            DbParameter[] parameter = DbHelperSqlGeneration.GetParameter(ht);
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), parameter));
        }

        #endregion

        #region Update 修改

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <returns>是否成功</returns>
        public int Update<T>(T entity)
        {
            object obj = (object)0;
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, DbHelperSqlGeneration.UpdateSql<T>(entity).ToString(), DbHelperSqlGeneration.GetParameter<T>(entity)));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Update<T>(T entity, DbTransaction isOpenTrans)
        {
            object obj = (object)0;
            StringBuilder stringBuilder = DbHelperSqlGeneration.UpdateSql<T>(entity);
            DbParameter[] parameter = DbHelperSqlGeneration.GetParameter<T>(entity);
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), parameter));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns>是否成功</returns>
        public int Update<T>(string propertyName, string propertyValue)
        {
            object obj = (object)0;
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("Update ");
            stringBuilder2.Append(typeof(T).Name);
            stringBuilder2.Append(" Set ");
            stringBuilder2.Append(propertyName);
            stringBuilder2.Append("=");
            stringBuilder2.Append(DbProvider.DbHelper.DbParmChar + propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder1.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// Update-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Update<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            object obj = (object)0;
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("Update ");
            stringBuilder2.Append(typeof(T).Name);
            stringBuilder2.Append(" Set ");
            stringBuilder2.Append(propertyName);
            stringBuilder2.Append("=");
            stringBuilder2.Append(DbProvider.DbHelper.DbParmChar + propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder1.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// Update-多条修改
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        public int Update<T>(List<T> entity)
        {
            object obj1 = (object)0;
            DbTransaction isOpenTrans = this.BeginTrans();
            object obj2;
            try
            {
                foreach (T entity1 in entity)
                    this.Update<T>(entity1, isOpenTrans);
                this.Commit();
                obj2 = (object)1;
            }
            catch (Exception ex)
            {
                this.Rollback();
                this.Close();
                obj1 = (object)-1;
                throw ex;
            }
            return Convert.ToInt32(obj2);
        }

        /// <summary>
        /// Update-事物-多条修改
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Update<T>(List<T> entity, DbTransaction isOpenTrans)
        {
            object obj1 = (object)0;
            object obj2;
            try
            {
                foreach (T entity1 in entity)
                    this.Update<T>(entity1, isOpenTrans);
                obj2 = (object)1;
            }
            catch (Exception ex)
            {
                obj1 = (object)-1;
                throw ex;
            }
            return Convert.ToInt32(obj2);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <param name="propertyName">主键字段</param>
        /// <returns>是否成功</returns>
        public int Update(string tableName, Hashtable ht, string propertyName)
        {
            object obj = (object)0;
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, DbHelperSqlGeneration.UpdateSql(tableName, ht, propertyName).ToString(), DbHelperSqlGeneration.GetParameter(ht)));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Update(string tableName, Hashtable ht, string propertyName, DbTransaction isOpenTrans)
        {
            object obj = (object)0;
            StringBuilder stringBuilder = DbHelperSqlGeneration.UpdateSql(tableName, ht, propertyName);
            DbParameter[] parameter = DbHelperSqlGeneration.GetParameter(ht);
            return Convert.ToInt32((object)DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), parameter));
        }

        #endregion

        #region Delete 删除

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(T entity)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, DbHelperSqlGeneration.DeleteSql<T>(entity).ToString(), DbHelperSqlGeneration.GetParameter<T>(entity));
        }

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(T entity, DbTransaction isOpenTrans)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql<T>(entity);
            DbParameter[] parameter = DbHelperSqlGeneration.GetParameter<T>(entity);
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)parameter));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(object propertyValue)
        {
            string name = typeof(T).Name;
            string pkName = DbHelperSqlGeneration.GetKeyField<T>().ToString();
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(name, pkName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + pkName, propertyValue));
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
        }

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(object propertyValue, DbTransaction isOpenTrans)
        {
            string name = typeof(T).Name;
            string pkName = DbHelperSqlGeneration.GetKeyField<T>().ToString();
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(name, pkName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + pkName, propertyValue));
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(string propertyName, string propertyValue)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(typeof(T).Name, propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
        }

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(typeof(T).Name, propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <returns>是否成功</returns>
        public int Delete(string tableName, string propertyName, string propertyValue)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(tableName, propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
        }

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(tableName, propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
        }

        /// <summary>
        /// Delete-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <returns>是否成功</returns>
        public int Delete(string tableName, Hashtable ht)
        {
            return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, DbHelperSqlGeneration.DeleteSql(tableName, ht).ToString(), DbHelperSqlGeneration.GetParameter(ht));
        }

        /// <summary>
        /// Delete-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.DeleteSql(tableName, ht);
            DbParameter[] parameter = DbHelperSqlGeneration.GetParameter(ht);
            return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), parameter);
        }

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(object[] propertyValue)
        {
            StringBuilder stringBuilder = new StringBuilder("DELETE FROM " + typeof(T).Name + " WHERE " + DbHelperSqlGeneration.GetKeyField<T>().ToString() + " IN (");
            try
            {
                IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
                int index1 = 0;
                string str = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                for (int index2 = 0; index2 < propertyValue.Length - 1; ++index2)
                {
                    object obj = propertyValue[index2];
                    string paramName = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                    stringBuilder.Append(paramName).Append(",");
                    list.Add(DbFactory.CreateDbParameter(paramName, obj));
                    ++index1;
                }
                string paramName1 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                stringBuilder.Append(paramName1);
                list.Add(DbFactory.CreateDbParameter(paramName1, propertyValue[index1]));
                stringBuilder.Append(")");
                return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete-多条数据-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(object[] propertyValue, DbTransaction isOpenTrans)
        {
            StringBuilder stringBuilder = new StringBuilder("DELETE FROM " + typeof(T).Name + " WHERE " + DbProvider.DbHelper.DbParmChar + DbHelperSqlGeneration.GetKeyField<T>().ToString() + " IN (");
            try
            {
                IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
                int index1 = 0;
                string str = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                for (int index2 = 0; index2 < propertyValue.Length - 1; ++index2)
                {
                    object obj = propertyValue[index2];
                    string paramName = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                    stringBuilder.Append(paramName).Append(",");
                    list.Add(DbFactory.CreateDbParameter(paramName, obj));
                    ++index1;
                }
                string paramName1 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                stringBuilder.Append(paramName1);
                list.Add(DbFactory.CreateDbParameter(paramName1, propertyValue[index1]));
                stringBuilder.Append(")");
                return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(string propertyName, object[] propertyValue)
        {
            StringBuilder stringBuilder = new StringBuilder("DELETE FROM " + typeof(T).Name + " WHERE " + DbProvider.DbHelper.DbParmChar + propertyName + " IN (");
            try
            {
                IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
                int index1 = 0;
                string str = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                for (int index2 = 0; index2 < propertyValue.Length - 1; ++index2)
                {
                    object obj = propertyValue[index2];
                    string paramName = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                    stringBuilder.Append(paramName).Append(",");
                    list.Add(DbFactory.CreateDbParameter(paramName, obj));
                    ++index1;
                }
                string paramName1 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                stringBuilder.Append(paramName1);
                list.Add(DbFactory.CreateDbParameter(paramName1, propertyValue[index1]));
                stringBuilder.Append(")");
                return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">字段字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            StringBuilder stringBuilder = new StringBuilder("DELETE FROM " + typeof(T).Name + " WHERE " + DbProvider.DbHelper.DbParmChar + propertyName + " IN (");
            try
            {
                IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
                int index1 = 0;
                string str = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                for (int index2 = 0; index2 < propertyValue.Length - 1; ++index2)
                {
                    object obj = propertyValue[index2];
                    string paramName = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                    stringBuilder.Append(paramName).Append(",");
                    list.Add(DbFactory.CreateDbParameter(paramName, obj));
                    ++index1;
                }
                string paramName1 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                stringBuilder.Append(paramName1);
                list.Add(DbFactory.CreateDbParameter(paramName1, propertyValue[index1]));
                stringBuilder.Append(")");
                return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">字段字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        public int Delete(string tableName, string propertyName, object[] propertyValue)
        {
            string str1 = propertyName;
            StringBuilder stringBuilder = new StringBuilder("DELETE FROM " + tableName + " WHERE " + DbProvider.DbHelper.DbParmChar + str1 + " IN (");
            try
            {
                IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
                int index1 = 0;
                string str2 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                for (int index2 = 0; index2 < propertyValue.Length - 1; ++index2)
                {
                    object obj = propertyValue[index2];
                    string paramName = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                    stringBuilder.Append(paramName).Append(",");
                    list.Add(DbFactory.CreateDbParameter(paramName, obj));
                    ++index1;
                }
                string paramName1 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                stringBuilder.Append(paramName1);
                list.Add(DbFactory.CreateDbParameter(paramName1, propertyValue[index1]));
                stringBuilder.Append(")");
                return DbProvider.DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete-多条数据-事物
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">字段字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        public int Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            string str1 = propertyName;
            StringBuilder stringBuilder = new StringBuilder("DELETE FROM " + tableName + " WHERE " + DbProvider.DbHelper.DbParmChar + str1 + " IN (");
            try
            {
                IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
                int index1 = 0;
                string str2 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                for (int index2 = 0; index2 < propertyValue.Length - 1; ++index2)
                {
                    object obj = propertyValue[index2];
                    string paramName = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                    stringBuilder.Append(paramName).Append(",");
                    list.Add(DbFactory.CreateDbParameter(paramName, obj));
                    ++index1;
                }
                string paramName1 = DbProvider.DbHelper.DbParmChar + (object)"ID" + (string)(object)index1;
                stringBuilder.Append(paramName1);
                list.Add(DbFactory.CreateDbParameter(paramName1, propertyValue[index1]));
                stringBuilder.Append(")");
                return DbProvider.DbHelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FindListTop 查询数据列表-Top 返回List<T>

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListTop<T>(int Top) where T : new()
        {
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, DbHelperSqlGeneration.SelectSql<T>(Top).ToString()));
        }

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">字段名</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListTop<T>(int Top, string propertyName, string propertyValue) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(Top);
            stringBuilder.Append(" AND " + propertyName + " = " + DbProvider.DbHelper.DbParmChar + propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListTop<T>(int Top, string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(Top);
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(Top);
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        #endregion

        #region FindList 查询数据列表 - 返回List<T>

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>List 泛型 </returns>
        public List<T> FindList<T>() where T : new()
        {
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, DbHelperSqlGeneration.SelectSql<T>().ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindList<T>(string propertyName, string propertyValue) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(" AND " + propertyName + " = " + DbProvider.DbHelper.DbParmChar + propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindList<T>(string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindList<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        #endregion

        #region FindListBySql 查询数据列表 - 返回List<T>

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">语句</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListBySql<T>(string strSql)
        {
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListBySql<T>(string strSql, DbParameter[] parameters)
        {
            return DataReaderConversion.ReaderToList<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters));
        }

        #endregion

        #region FindListPage 查询数据列表-分页 返回List<T>

        /// <summary>
        /// 查询数据列表-分页 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示</param>
        /// <param name="recordCount">返回总行数</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListPage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            return SqlServerHelper.GetPageList<T>(DbHelperSqlGeneration.SelectSql<T>().ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 查询数据列表-分页 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">sql 语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示</param>
        /// <param name="recordCount">返回总行数</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListPage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return SqlServerHelper.GetPageList<T>(stringBuilder.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 查询数据列表-分页 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="parameters">入参</param>
        /// <param name="WhereSql">sql 语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示</param>
        /// <param name="recordCount">返回总行数</param>
        /// <returns>List 泛型 </returns>
        public List<T> FindListPage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return SqlServerHelper.GetPageList<T>(stringBuilder.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        #endregion

        #region FindListPageBySql 查询数据列表 - 返回List<T>

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql 语句</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="recordCount">总行数</param>
        /// <returns> List </returns>
        public List<T> FindListPageBySql<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            return SqlServerHelper.GetPageList<T>(strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="parameters">入参</param>
        /// <param name="strSql">sql 语句</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="recordCount">总行数</param>
        /// <returns> List </returns>
        public List<T> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            return SqlServerHelper.GetPageList<T>(strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        #endregion

        #region FindTableTop 查询数据列表 - 返回 DataTable Top 

        /// <summary>
        /// 查询数据列表 - 返回 DataTable Top 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableTop<T>(int Top) where T : new()
        {
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, DbHelperSqlGeneration.SelectSql<T>(Top).ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回 DataTable Top 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableTop<T>(int Top, string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(Top);
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回 DataTable Top 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(Top);
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        #endregion

        #region FindTable 查询数据列表 - 返回DataTable

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>DataTable</returns>
        public DataTable FindTable<T>() where T : new()
        {
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, DbHelperSqlGeneration.SelectSql<T>().ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>DataTable</returns>
        public DataTable FindTable<T>(string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        public DataTable FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        #endregion

        #region FindTableBySql 查询数据列表 - 返回DataTable

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableBySql(string strSql)
        {
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString()));
        }

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableBySql(string strSql, DbParameter[] parameters)
        {
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters));
        }

        #endregion

        #region FindTablePage 查询数据列表 - 返回DataTable

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="recordCount">返回总条数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            return SqlServerHelper.GetPageTable(DbHelperSqlGeneration.SelectSql<T>().ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="recordCount">返回总条数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return SqlServerHelper.GetPageTable(stringBuilder.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="recordCount">返回总条数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>();
            stringBuilder.Append(WhereSql);
            return SqlServerHelper.GetPageTable(stringBuilder.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        #endregion

        #region FindTablePageBySql 查询数据列表 - 返回DataTable sql

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示书</param>
        /// <param name="recordCount">返回总条数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            return SqlServerHelper.GetPageTable(strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="parameters">入参</param>
        /// <param name="strSql">sql 语句</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示书</param>
        /// <param name="recordCount">返回总条数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            return SqlServerHelper.GetPageTable(strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
        }

        #endregion

        #region FindTableByProc 查询数据列表 - 返回 DataTable 存储

        /// <summary>
        /// 查询数据列表 - 返回 DataTable 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableByProc(string procName)
        {
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.StoredProcedure, procName));
        }

        /// <summary>
        /// 查询数据列表 - 返回 DataTable 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        public DataTable FindTableByProc(string procName, DbParameter[] parameters)
        {
            return DataReaderConversion.ReaderToDataTable(DbProvider.DbHelper.ExecuteReader(CommandType.StoredProcedure, procName, parameters));
        }

        #endregion

        #region FindDataSetBySql 查询数据列表 - 返回DataSet

        /// <summary>
        /// 查询数据列表 - 返回DataSet
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>DataSet</returns>
        public DataSet FindDataSetBySql(string strSql)
        {
            return DbProvider.DbHelper.GetDataSet(CommandType.Text, strSql);
        }

        /// <summary>
        /// 查询数据列表 - 返回DataSet
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataSet</returns>
        public DataSet FindDataSetBySql(string strSql, DbParameter[] parameters)
        {
            return DbProvider.DbHelper.GetDataSet(CommandType.Text, strSql, parameters);
        }

        #endregion

        #region FindDataSetByProc 查询数据列表 - 返回DataSet 存储

        /// <summary>
        /// 查询数据列表 - 返回DataSet 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <returns>DataSet</returns>
        public DataSet FindDataSetByProc(string procName)
        {
            return DbProvider.DbHelper.GetDataSet(CommandType.StoredProcedure, procName);
        }

        /// <summary>
        /// 查询数据列表 - 返回DataSet 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataSet</returns>
        public DataSet FindDataSetByProc(string procName, DbParameter[] parameters)
        {
            return DbProvider.DbHelper.GetDataSet(CommandType.StoredProcedure, procName, parameters);
        }

        #endregion

        #region FindEntity 查询对象 - 返回实体

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值</param>
        /// <returns>Model</returns>
        public T FindEntity<T>(object propertyValue) where T : new()
        {
            string str = DbHelperSqlGeneration.GetKeyField<T>().ToString();
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(1);
            stringBuilder.Append(" AND ").Append(str).Append("=").Append(DbProvider.DbHelper.DbParmChar + str);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + str, propertyValue));
            return DataReaderConversion.ReaderToModel<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>Model</returns>
        public T FindEntity<T>(string propertyName, object propertyValue) where T : new()
        {
            string str = propertyName;
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(1);
            stringBuilder.Append(" AND ").Append(str).Append("=").Append(DbProvider.DbHelper.DbParmChar + str);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + str, propertyValue));
            return DataReaderConversion.ReaderToModel<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>Model</returns>
        public T FindEntityByWhere<T>(string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(1);
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToModel<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>Model</returns>
        public T FindEntityByWhere<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql<T>(1);
            stringBuilder.Append(WhereSql);
            return DataReaderConversion.ReaderToModel<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql 语句</param>
        /// <returns>Model</returns>
        public T FindEntityBySql<T>(string strSql)
        {
            return DataReaderConversion.ReaderToModel<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString()));
        }

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns></returns>
        public T FindEntityBySql<T>(string strSql, DbParameter[] parameters)
        {
            return DataReaderConversion.ReaderToModel<T>(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters));
        }

        #endregion

        #region FindHashtable 查询对象 - 返回哈希表

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>Hashtable</returns>
        public Hashtable FindHashtable(string tableName, string propertyName, object propertyValue)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql(tableName, 1);
            stringBuilder.Append(" AND ").Append(propertyName).Append("=").Append(DbProvider.DbHelper.DbParmChar + propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, propertyValue));
            return DataReaderConversion.ReaderToHashtable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>Hashtable</returns>
        public Hashtable FindHashtable(string tableName, StringBuilder WhereSql)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql(tableName, 1);
            stringBuilder.Append((object)WhereSql);
            return DataReaderConversion.ReaderToHashtable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>Hashtable</returns>
        public Hashtable FindHashtable(string tableName, StringBuilder WhereSql, DbParameter[] parameters)
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectSql(tableName, 1);
            stringBuilder.Append((object)WhereSql);
            return DataReaderConversion.ReaderToHashtable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>Hashtable</returns>
        public Hashtable FindHashtableBySql(string strSql)
        {
            return DataReaderConversion.ReaderToHashtable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString()));
        }

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>Hashtable</returns>
        public Hashtable FindHashtableBySql(string strSql, DbParameter[] parameters)
        {
            return DataReaderConversion.ReaderToHashtable(DbProvider.DbHelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters));
        }

        #endregion

        #region FindCount 查询数据 - 返回条数

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>int</returns>
        public int FindCount<T>() where T : new()
        {
            return Convert.ToInt32(DbProvider.DbHelper.ExecuteScalar(CommandType.Text, DbHelperSqlGeneration.SelectCountSql<T>().ToString()));
        }

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>int</returns>
        public int FindCount<T>(string propertyName, string propertyValue) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectCountSql<T>();
            stringBuilder.Append(" AND " + propertyName + " = " + DbProvider.DbHelper.DbParmChar + propertyName);
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            list.Add(DbFactory.CreateDbParameter(DbProvider.DbHelper.DbParmChar + propertyName, (object)propertyValue));
            return Convert.ToInt32(DbProvider.DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list)));
        }

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>int</returns>
        public int FindCount<T>(string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectCountSql<T>();
            stringBuilder.Append(WhereSql);
            return Convert.ToInt32(DbProvider.DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString()));
        }

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>int</returns>
        public int FindCount<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectCountSql<T>();
            stringBuilder.Append(WhereSql);
            return Convert.ToInt32(DbProvider.DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), parameters));
        }

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>int</returns>
        public int FindCountBySql(string strSql)
        {
            return Convert.ToInt32(DbProvider.DbHelper.ExecuteScalar(CommandType.Text, strSql));
        }

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <param name="strSql">sql 语句 </param>
        /// <param name="parameters">入参</param>
        /// <returns>int</returns>
        public int FindCountBySql(string strSql, DbParameter[] parameters)
        {
            return Convert.ToInt32(DbProvider.DbHelper.ExecuteScalar(CommandType.Text, strSql, parameters));
        }

        #endregion

        #region FindMax 查询数据 - 返回最大数

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <typeparam name="T"> 实体 </typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns>object</returns>
        public object FindMax<T>(string propertyName) where T : new()
        {
            return DbProvider.DbHelper.ExecuteScalar(CommandType.Text, DbHelperSqlGeneration.SelectMaxSql<T>(propertyName).ToString());
        }

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>object</returns>
        public object FindMax<T>(string propertyName, string WhereSql) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectMaxSql<T>(propertyName);
            stringBuilder.Append(WhereSql);
            return DbProvider.DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString());
        }

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>object</returns>
        public object FindMax<T>(string propertyName, string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder stringBuilder = DbHelperSqlGeneration.SelectMaxSql<T>(propertyName);
            stringBuilder.Append(WhereSql);
            return DbProvider.DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>object</returns>
        public object FindMaxBySql(string strSql)
        {
            return DbProvider.DbHelper.ExecuteScalar(CommandType.Text, strSql);
        }

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>object</returns>
        public object FindMaxBySql(string strSql, DbParameter[] parameters)
        {
            return DbProvider.DbHelper.ExecuteScalar(CommandType.Text, strSql, parameters);
        }

        #endregion

        #region Dispose 关闭连接
        public void Dispose()
        {
            if (this.dbConnection != null)
                this.dbConnection.Dispose();
            if (this.isOpenTrans == null)
                return;
            this.isOpenTrans.Dispose();
        }
        #endregion

        #region Close 关闭

        public void Close()
        {
            if (this.dbConnection != null)
            {
                this.dbConnection.Close();
                this.dbConnection.Dispose();
            }
            this.dbConnection = (DbConnection)null;
            this.isOpenTrans = (DbTransaction)null;
        }

        #endregion

    }
}

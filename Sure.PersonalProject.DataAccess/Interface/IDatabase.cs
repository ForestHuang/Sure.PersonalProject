using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sure.PersonalProject.DataAccess.Interface
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: IDatabase  、 Ado.net 数据访问接口(创建接口)
    -----------------------------------------------------------------------*/

    using System.Collections;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Ado.net 数据访问接口(创建接口)
    /// </summary>
    public interface IDatabase : IDisposable
    {
        #region Commit 事物提交
       
        /// <summary>
        /// 事物提交
        /// </summary>
        void Commit(); 
        
        #endregion

        #region Rollback 事物回滚 

        /// <summary>
        /// 事物回滚
        /// </summary>
        void Rollback(); 

        #endregion

        #region Close 关闭

        /// <summary>
        /// 关闭
        /// </summary>
        void Close(); 

        #endregion

        #region ExecuteBySql 执行sql语句

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>是否成功</returns>
        int ExecuteBySql(StringBuilder strSql);

        /// <summary>
        /// 执行SQL语句-事物
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>是否成功</returns>
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters);

        /// <summary>
        /// 执行SQL语句-事物
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans);

        #endregion

        #region ExecuteByProc 执行存储

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <returns>是否成功</returns>
        int ExecuteByProc(string procName);

        /// <summary>
        /// 执行存储过程-事物
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int ExecuteByProc(string procName, DbTransaction isOpenTrans);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="parameters">入参</param>
        /// <returns>是否成功</returns>
        int ExecuteByProc(string procName, DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程-事物
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="parameters">入参</param>
        /// <returns>是否成功</returns>
        int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans);

        #endregion

        #region Insert 新增

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <returns>是否成功</returns>
        int Insert<T>(T entity);

        /// <summary>
        /// Insert-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Insert<T>(T entity, DbTransaction isOpenTrans);

        /// <summary>
        /// Insert-多条插入
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        int Insert<T>(List<T> entity);

        /// <summary>
        /// Insert-多条插入-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        int Insert<T>(List<T> entity, DbTransaction isOpenTrans);

        /// <summary>
        /// Insert-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <returns>是否成功</returns>
        int Insert(string tableName, Hashtable ht);
      
        /// <summary>
        /// Insert-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="ht">数据集</param>
        /// <returns>是否成功</returns>
        int Insert(string tableName, Hashtable ht, DbTransaction isOpenTrans);

        #endregion

        #region Update 修改

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <returns>是否成功</returns>
        int Update<T>(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Update<T>(T entity, DbTransaction isOpenTrans);

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns>是否成功</returns>
        int Update<T>(string propertyName, string propertyValue);

        /// <summary>
        /// Update-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Update<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// Update-多条修改
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        int Update<T>(List<T> entity);

        /// <summary>
        /// Update-事物-多条修改
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Update<T>(List<T> entity, DbTransaction isOpenTrans);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <param name="propertyName">主键字段</param>
        /// <returns>是否成功</returns>
        int Update(string tableName, Hashtable ht, string propertyName);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Update(string tableName, Hashtable ht, string propertyName, DbTransaction isOpenTrans);

        #endregion

        #region Delete 删除

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <returns>是否成功</returns>
        int Delete<T>(T entity);

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据集</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete<T>(T entity, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值</param>
        /// <returns>是否成功</returns>
        int Delete<T>(object propertyValue);

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete<T>(object propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <returns>是否成功</returns>
        int Delete<T>(string propertyName, string propertyValue);

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <returns>是否成功</returns>
        int Delete(string tableName, string propertyName, string propertyValue);

        /// <summary>
        /// Delete-事物
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <returns>是否成功</returns>
        int Delete(string tableName, Hashtable ht);

        /// <summary>
        /// Delete-Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">数据集</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        int Delete<T>(object[] propertyValue);

        /// <summary>
        /// Delete-多条数据-事物
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="isOpenTrans">事物</param>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        int Delete<T>(object[] propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        int Delete<T>(string propertyName, object[] propertyValue);

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">字段字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// Delete-多条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">字段字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <returns>是否成功</returns>
        int Delete(string tableName, string propertyName, object[] propertyValue);

        /// <summary>
        /// Delete-多条数据-事物
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">字段字段</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6</param>
        /// <param name="isOpenTrans">事物</param>
        /// <returns>是否成功</returns>
        int Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans);

        #endregion

        #region FindListTop 查询数据列表-Top 返回List<T>

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <returns>List 泛型 </returns>
        List<T> FindListTop<T>(int Top) where T : new();

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">字段名</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns>List 泛型 </returns>
        List<T> FindListTop<T>(int Top, string propertyName, string propertyValue) where T : new();

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>List 泛型 </returns>
        List<T> FindListTop<T>(int Top, string WhereSql) where T : new();

        /// <summary>
        /// 查询数据列表-Top 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>List 泛型 </returns>
        List<T> FindListTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();

        #endregion

        #region FindList 查询数据列表 - 返回List<T>

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>List 泛型 </returns>
        List<T> FindList<T>() where T : new();

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>List 泛型 </returns>
        List<T> FindList<T>(string propertyName, string propertyValue) where T : new();

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>List 泛型 </returns>
        List<T> FindList<T>(string WhereSql) where T : new();

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>List 泛型 </returns>
        List<T> FindList<T>(string WhereSql, DbParameter[] parameters) where T : new();

        #endregion

        #region FindListBySql 查询数据列表 - 返回List<T>

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">语句</param>
        /// <returns>List 泛型 </returns>
        List<T> FindListBySql<T>(string strSql);

        /// <summary>
        /// 查询数据列表 - 返回List<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>List 泛型 </returns>
        List<T> FindListBySql<T>(string strSql, DbParameter[] parameters);

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
        List<T> FindListPage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();

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
        List<T> FindListPage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();

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
        List<T> FindListPage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();

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
        List<T> FindListPageBySql<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);

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
        List<T> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);

        #endregion

        #region FindTableTop 查询数据列表 - 返回 DataTable Top 

        /// <summary>
        /// 查询数据列表 - 返回 DataTable Top 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <returns>DataTable</returns>
        DataTable FindTableTop<T>(int Top) where T : new();

        /// <summary>
        /// 查询数据列表 - 返回 DataTable Top 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>DataTable</returns>
        DataTable FindTableTop<T>(int Top, string WhereSql) where T : new();

        /// <summary>
        /// 查询数据列表 - 返回 DataTable Top 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        DataTable FindTableTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();

        #endregion

        #region FindTable 查询数据列表 - 返回DataTable

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>DataTable</returns>
        DataTable FindTable<T>() where T : new();

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>DataTable</returns>
        DataTable FindTable<T>(string WhereSql) where T : new();

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        DataTable FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new();

        #endregion

        #region FindTableBySql 查询数据列表 - 返回DataTable

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>DataTable</returns>
        DataTable FindTableBySql(string strSql);

        /// <summary>
        /// 查询数据列表 - 返回DataTable
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        DataTable FindTableBySql(string strSql, DbParameter[] parameters);

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
        DataTable FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();

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
        DataTable FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();

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
        DataTable FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();

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
        DataTable FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);

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
        DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);

        #endregion

        #region FindTableByProc 查询数据列表 - 返回 DataTable 存储

        /// <summary>
        /// 查询数据列表 - 返回 DataTable 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <returns>DataTable</returns>
        DataTable FindTableByProc(string procName);

        /// <summary>
        /// 查询数据列表 - 返回 DataTable 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataTable</returns>
        DataTable FindTableByProc(string procName, DbParameter[] parameters);

        #endregion

        #region FindDataSetBySql 查询数据列表 - 返回DataSet

        /// <summary>
        /// 查询数据列表 - 返回DataSet
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>DataSet</returns>
        DataSet FindDataSetBySql(string strSql);

        /// <summary>
        /// 查询数据列表 - 返回DataSet
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataSet</returns>
        DataSet FindDataSetBySql(string strSql, DbParameter[] parameters);

        /// <summary>
        /// 查询数据列表 - 返回DataSet 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <returns>DataSet</returns>
        DataSet FindDataSetByProc(string procName);

        /// <summary>
        /// 查询数据列表 - 返回DataSet 存储
        /// </summary>
        /// <param name="procName">存储名</param>
        /// <param name="parameters">入参</param>
        /// <returns>DataSet</returns>
        DataSet FindDataSetByProc(string procName, DbParameter[] parameters);

        #endregion

        #region FindEntity 查询对象 - 返回实体

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyValue">主键值</param>
        /// <returns>Model</returns>
        T FindEntity<T>(object propertyValue) where T : new();

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>Model</returns>
        T FindEntity<T>(string propertyName, object propertyValue) where T : new();

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>Model</returns>
        T FindEntityByWhere<T>(string WhereSql) where T : new();

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>Model</returns>
        T FindEntityByWhere<T>(string WhereSql, DbParameter[] parameters) where T : new();

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql 语句</param>
        /// <returns>Model</returns>
        T FindEntityBySql<T>(string strSql);

        /// <summary>
        /// 查询对象 - 返回实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns></returns>
        T FindEntityBySql<T>(string strSql, DbParameter[] parameters);

        #endregion

        #region FindHashtable 查询对象 - 返回哈希表

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>Hashtable</returns>
        Hashtable FindHashtable(string tableName, string propertyName, object propertyValue);

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>Hashtable</returns>
        Hashtable FindHashtable(string tableName, StringBuilder WhereSql);

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>Hashtable</returns>
        Hashtable FindHashtable(string tableName, StringBuilder WhereSql, DbParameter[] parameters);

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>Hashtable</returns>
        Hashtable FindHashtableBySql(string strSql);

        /// <summary>
        /// 查询对象 - 返回哈希表
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>Hashtable</returns>
        Hashtable FindHashtableBySql(string strSql, DbParameter[] parameters);

        #endregion

        #region FindCount 查询数据 - 返回条数

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>int</returns>
        int FindCount<T>() where T : new();

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">实体属性值</param>
        /// <returns>int</returns>
        int FindCount<T>(string propertyName, string propertyValue) where T : new();

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <returns>int</returns>
        int FindCount<T>(string WhereSql) where T : new();

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>int</returns>
        int FindCount<T>(string WhereSql, DbParameter[] parameters) where T : new();

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>int</returns>
        int FindCountBySql(string strSql);

        /// <summary>
        /// 查询数据 - 返回条数
        /// </summary>
        /// <param name="strSql">sql 语句 </param>
        /// <param name="parameters">入参</param>
        /// <returns>int</returns>
        int FindCountBySql(string strSql, DbParameter[] parameters);

        #endregion

        #region FindMax 查询数据 - 返回最大数

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <typeparam name="T"> 实体 </typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns>object</returns>
        object FindMax<T>(string propertyName) where T : new();

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <returns>object</returns>
        object FindMax<T>(string propertyName, string WhereSql) where T : new();

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">入参</param>
        /// <returns>object</returns>
        object FindMax<T>(string propertyName, string WhereSql, DbParameter[] parameters) where T : new();

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <returns>object</returns>
        object FindMaxBySql(string strSql);

        /// <summary>
        /// 查询数据 - 返回最大值
        /// </summary>
        /// <param name="strSql">sql 语句</param>
        /// <param name="parameters">入参</param>
        /// <returns>object</returns>
        object FindMaxBySql(string strSql, DbParameter[] parameters);

        #endregion

    }
}

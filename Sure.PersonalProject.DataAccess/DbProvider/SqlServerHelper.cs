using System;
using System.Collections.Generic;
using System.Text;

namespace Sure.PersonalProject.DataAccess.DbProvider
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: SqlServerHelper  、 数据分页
    -----------------------------------------------------------------------*/

    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// SqlServcrHelper 分页
    /// </summary>
    public class SqlServerHelper
    {
        /// <summary>
        /// 查询数据分页 - 返回DataTable 有参
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="param">入参</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">返回总行数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPageTable(string sql, DbParameter[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (pageIndex == 0)
                pageIndex = 1;
            int num1 = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            string str = string.IsNullOrEmpty(orderField) ? "order by (select 0)" : "Order By " + orderField + " " + orderType;
            stringBuilder.Append("Select * From (Select ROW_NUMBER() Over (" + str + ")");
            stringBuilder.Append(" As rowNum, * From (" + (object)sql + ") As T ) As N Where rowNum > " + (string)(object)num1 + " And rowNum <= " + (string)(object)num2);
            count = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "Select Count(1) From (" + sql + ") As t", param));
            return DataReaderConversion.ReaderToDataTable(DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), param));
        }

        /// <summary>
        /// 查询数据分页 - 返回DataTable 无参
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">返回总行数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPageTable(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            return SqlServerHelper.GetPageTable(sql, (DbParameter[])null, orderField, orderType, pageIndex, pageSize, ref count);
        }

        /// <summary>
        /// 查询数据分页 - 返回List<T> 有参
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="param">入参</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">返回总行数</param>
        /// <returns>List</returns>
        public static List<T> GetPageList<T>(string sql, DbParameter[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (pageIndex == 0)
                pageIndex = 1;
            int num1 = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            string str = string.IsNullOrEmpty(orderField) ? "Order By (select 0)" : "Order By " + orderField + " " + orderType;
            stringBuilder.Append("Select * From (Select ROW_NUMBER() Over (" + str + ")");
            stringBuilder.Append(" As rowNum, * From (" + (object)sql + ") As T ) As N Where rowNum > " + (string)(object)num1 + " And rowNum <= " + (string)(object)num2);
            count = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "Select Count(1) From (" + sql + ") As t", param));
            return DataReaderConversion.ReaderToList<T>(DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), param));
        }

        /// <summary>
        /// 查询数据分页 - 返回List<T> 无参
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="orderField">排序</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">返回总行数</param>
        /// <returns>List</returns>
        public static List<T> GetPageList<T>(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            return SqlServerHelper.GetPageList<T>(sql, (DbParameter[])null, orderField, orderType, pageIndex, pageSize, ref count);
        }

    }
}

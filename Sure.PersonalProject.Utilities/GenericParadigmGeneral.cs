using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sure.PersonalProject.Utilities
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: GenericParadigmGeneral 泛型的各类操作
    -----------------------------------------------------------------------*/

    /// <summary>
    /// GenericParadigmGeneral 泛型的各类操作
    /// </summary>
    public class GenericParadigmGeneral
    {
        /// <summary>
        /// 两个对象对比
        /// </summary>
        /// <param name="beforeUpdateData">入参对象</param>
        /// <param name="afterUpdateData">入参对象</param>
        /// <returns>返回不相等的字段以及值</returns>
        public static List<string> Comparison(Object beforeUpdateData, Object afterUpdateData)
        {
            var fieldItems = new List<string>();

            if (null == afterUpdateData || null == beforeUpdateData) { return fieldItems; }

            Type type = afterUpdateData.GetType();

            var fields = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            foreach (var field in fields)
            {
                // 取得字段的值
                object afterUpdateItem = field.GetValue(afterUpdateData);
                var beforeUpdateItem = beforeUpdateData.GetType().GetProperty(field.Name).GetValue(beforeUpdateData, null);
                if (field.PropertyType.IsValueType || field.PropertyType.Name.StartsWith("String"))
                {
                    // 直接比较是否相等，若不相等直接记录
                    if (afterUpdateItem.ToString() != beforeUpdateItem.ToString())
                    {
                        string Item = string.Format(@"{0} , {1} 改 {2} , {1} <> {2}", field.Name, beforeUpdateItem.ToString(), afterUpdateItem.ToString());
                        fieldItems.Add(Item);
                    }
                }
            }
            return fieldItems;
        }


        #region DataTable 、DataRow

        /// <summary>
        /// DataTable 筛选
        /// </summary>
        /// <param name="dataTable">入参数据源</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="sort">排序</param>
        /// <returns>DataTable</returns>
        public static DataTable DataTableSelect(DataTable dataTable, string filter, string sort)
        {
            DataTable new_dataTable = dataTable.Clone();
            foreach (DataRow dataRow in new_dataTable.Select(filter, sort))
            {
                new_dataTable.ImportRow(dataRow);
            }
            return new_dataTable;
        }

        /// <summary>
        /// DataTabelToList<T>
        /// </summary>
        /// <typeparam name="T">任何类型，泛型</typeparam>
        /// <param name="dataTable">数据源</param>
        /// <returns>泛型集合</returns>
        public static List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            var resultList = new List<T>();
            try
            {
                List<string> columnName = new List<string>();
                foreach (DataColumn DataColumn in dataTable.Columns)
                    columnName.Add(DataColumn.ColumnName);
                resultList = dataTable.AsEnumerable().ToList().ConvertAll<T>(row => DataRowToEntity<T>(row, columnName));
                return resultList;
            }
            catch (Exception ex)
            {
                throw new Exception("DataTableToList 转换错误," + ex.Message);
            }
        }

        /// <summary>
        /// ListToDataTable
        /// </summary>
        /// <typeparam name="T">任何类型，泛型</typeparam>
        /// <param name="requstList">数据源</param>
        /// <returns>DataTable</returns>
        public static DataTable ListToDataTable<T>(List<T> requstList)
        {
            try
            {
                DataRow dataRow;
                Type type = requstList[0].GetType();
                DataTable dataTable = new DataTable(type.Name);

                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    Type propertyType = propertyInfo.PropertyType;

                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        propertyType = propertyType.GetGenericArguments()[0];
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyType));
                }

                foreach (T entity in requstList)
                {
                    dataRow = dataTable.NewRow();
                    foreach (PropertyInfo propertyInfo in type.GetProperties())
                    {
                        object value = propertyInfo.GetValue(entity, null);
                        if (null == value)
                            value = System.DBNull.Value;
                        dataRow[propertyInfo.Name] = value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                return dataTable;
            }
            catch (Exception ex)
            {

                throw new Exception("ListToDataTable 错误," + ex.Message);
            }
        }

        /// <summary>
        /// ListToDataTables
        /// </summary>
        /// <typeparam name="T">任何类型，泛型</typeparam>
        /// <param name="requstList">数据源</param>
        /// <returns>DataTable</returns>
        public static DataTable ListToDataTables<T>(List<T> requstList)
        {
            if (requstList == null || requstList.Count < 1)
                return new DataTable();
            Type type = requstList[0].GetType();
            PropertyInfo[] properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            for (int index = 0; index < properties.Length; ++index)
                dataTable.Columns.Add(properties[index].Name);
            foreach (T obj in requstList)
            {
                object newobj = (object)obj;
                if (newobj.GetType() != type)
                    throw new Exception("要转换的集合元素类型不一致");
                object[] objArray = new object[properties.Length];
                for (int index = 0; index < properties.Length; ++index)
                    objArray[index] = properties[index].GetValue(newobj, (object[])null);
                dataTable.Rows.Add(objArray);
            }
            return dataTable;
        }


        /// <summary>
        /// DataTableToXML
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <returns>string XML</returns>
        public static string DataTableToXML(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count <= 0)
                return string.Empty;
            StringWriter stringWriter = new StringWriter();
            dataTable.WriteXml((TextWriter)stringWriter);
            return stringWriter.ToString();
        }


        /// <summary>
        /// EnumerableToDataTable
        /// </summary>
        /// <typeparam name="T">任何类型，泛型或者实体Model</typeparam>
        /// <param name="resqustEnumerable">数据源</param>
        /// <returns>DataTable</returns>
        public static DataTable EnumerableToDataTable<T>(IEnumerable<T> resqustEnumerable)
        {
            try
            {
                Type type = typeof(T);
                DataTable dataTable = new DataTable(type.Name);

                PropertyInfo[] propertyInfo = type.GetProperties();
                foreach (PropertyInfo pItem in propertyInfo)
                {
                    dataTable.Columns.Add(pItem.Name, (pItem.PropertyType.IsGenericType && pItem.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? pItem.PropertyType.GetGenericArguments()[0] : pItem.PropertyType);
                }
                foreach (T item in resqustEnumerable)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow.BeginEdit();
                    foreach (PropertyInfo pItem in propertyInfo)
                    {
                        if (null != pItem.GetValue(item, null))
                        {
                            dataRow[pItem.Name] = pItem.GetValue(item, null);
                        }
                    }
                    dataRow.EndEdit();
                    dataTable.Rows.Add(dataRow);
                }
                return dataTable;
            }
            catch (Exception ex)
            {

                throw new Exception("EnumerableToDataTable 转换错误," + ex.Message);
            }
        }

        /// <summary>
        /// DataRowToEntity
        /// </summary>
        /// <typeparam name="T">任何类型，泛型</typeparam>
        /// <param name="dataRow">数据源</param>
        /// <param name="columnsName">列名集合</param>
        /// <returns>对象Entity</returns>
        public static T DataRowToEntity<T>(DataRow dataRow, List<string> columnsName) where T : new()
        {
            T resultObj = new T();
            try
            {
                string columnName = string.Empty;
                string value = string.Empty;
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnName = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        value = dataRow[columnName].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = dataRow[columnName].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(resultObj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = dataRow[columnName].ToString().Replace("%", "");
                                objProperty.SetValue(resultObj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return resultObj;
            }
            catch (Exception ex)
            {
                throw new Exception("GetEntity 错误," + ex.Message);
            }
        }

        #endregion


        /// <summary>
        /// 验证是否包含该特殊符号
        /// </summary>
        /// <param name="requstStr">入参</param>
        /// <returns>是否包含</returns>
        public static bool CheckIllegalCharacter(string requstStr)
        {
            string[] arryWord = IllegalCharacterArry();
            bool resultBool = false;
            foreach (string str in arryWord)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    if (requstStr.IndexOf(str) > -1)
                    {
                        resultBool = true;
                        return resultBool;
                    }
                }
            }
            return resultBool;
        }

        /// <summary>
        /// 非法字符串集合
        /// </summary>
        /// <returns>Arry 数组</returns>
        private static string[] IllegalCharacterArry()
        {
            string[] arry = new string[20];
            arry[0] = "'";
            arry[1] = @"\";
            arry[2] = ";";
            arry[3] = "--";
            arry[4] = ",";
            arry[5] = "!";
            arry[6] = "~";
            arry[7] = "@";
            arry[8] = "#";
            arry[9] = "$";
            arry[10] = "%";
            arry[11] = "^";
            arry[12] = "&";
            arry[13] = " ";
            arry[14] = "_";
            arry[15] = "'";
            arry[16] = "‘";
            arry[17] = "’";
            return arry;
        }

    }
}

using System;
using System.Collections.Generic;

namespace Sure.PersonalProject.DataAccess.DbProvider
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DataReaderConversion  
    -----------------------------------------------------------------------*/

    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;

    /// <summary>
    /// DataReader 数据转换
    /// </summary>
    public class DataReaderConversion
    {
        /// <summary>
        /// Reader 转 DataTable
        /// </summary>
        /// <param name="reader">数据</param>
        /// <returns>DataTable</returns>
        public static DataTable ReaderToDataTable(IDataReader reader)
        {
            DataTable dataTable = new DataTable("Table");
            int fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; ++i)
                dataTable.Columns.Add(reader.GetName(i).ToLower(), reader.GetFieldType(i));
            dataTable.BeginLoadData();
            object[] values = new object[fieldCount];
            while (reader.Read())
            {
                reader.GetValues(values);
                dataTable.LoadDataRow(values, true);
            }
            reader.Close();
            dataTable.EndLoadData();
            return dataTable;
        }

        /// <summary>
        /// Reader 转 List
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dr">数据</param>
        /// <returns>List 泛型</returns>
        public static List<T> ReaderToList<T>(IDataReader dr)
        {
            using (dr)
            {
                List<string> list1 = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; ++i)
                    list1.Add(dr.GetName(i).ToLower());
                List<T> list2 = new List<T>();
                while (dr.Read())
                {
                    T instance = Activator.CreateInstance<T>();
                    foreach (PropertyInfo propertyInfo in instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
                    {
                        if (list1.Contains(propertyInfo.Name.ToLower()) && !DataReaderConversion.IsNullOrDBNull(dr[propertyInfo.Name]))
                            propertyInfo.SetValue((object)instance, DataReaderConversion.HackType(dr[propertyInfo.Name], propertyInfo.PropertyType), (object[])null);
                    }
                    list2.Add(instance);
                }
                return list2;
            }
        }

        /// <summary>
        /// Reader 转 Model
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dr">数据</param>
        /// <returns>Model</returns>
        public static T ReaderToModel<T>(IDataReader dr)
        {
            T instance = Activator.CreateInstance<T>();
            while (dr.Read())
            {
                foreach (PropertyInfo propertyInfo in instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
                {
                    if (!DataReaderConversion.IsNullOrDBNull(dr[propertyInfo.Name]))
                        propertyInfo.SetValue((object)instance, DataReaderConversion.HackType(dr[propertyInfo.Name], propertyInfo.PropertyType), (object[])null);
                }
            }
            return instance;
        }

        /// <summary>
        /// Reader 转 Hashtable
        /// </summary>
        /// <param name="dr">数据</param>
        /// <returns>Hashtable</returns>
        public static Hashtable ReaderToHashtable(IDataReader dr)
        {
            Hashtable hashtable = new Hashtable();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; ++i)
                {
                    string index = dr.GetName(i).ToLower();
                    hashtable[(object)index] = dr[index];
                }
            }
            return hashtable;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns>bool</returns>
        public static bool IsNullOrDBNull(object obj)
        {
            return obj is DBNull || string.IsNullOrEmpty(obj.ToString());
        }


        #region Private

        private static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return (object)null;
                conversionType = new NullableConverter(conversionType).UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        #endregion

    }
}

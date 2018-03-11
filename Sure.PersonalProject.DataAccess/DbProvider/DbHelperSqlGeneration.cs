using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sure.PersonalProject.DataAccess.DbProvider
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DbHelperSqlGeneration 、 增删改查语句生成  
    -----------------------------------------------------------------------*/

    using Sure.PersonalProject.DataAccess.Attributes;
    using Sure.PersonalProject.DataAccess.Factory;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// DbHelperSqlGeneration 、 增删改查语句生成
    /// </summary>
    public class DbHelperSqlGeneration
    {
        public static DbParameter[] GetParameter<T>(T entity)
        {
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                if (propertyInfo.GetValue((object)entity, (object[])null) != null)
                {
                    DbType dbType;
                    switch (propertyInfo.PropertyType.ToString())
                    {
                        case "System.Nullable`1[System.Int32]":
                            dbType = DbType.Int32;
                            break;
                        case "System.Nullable`1[System.Decimal]":
                            dbType = DbType.Decimal;
                            break;
                        case "System.Nullable`1[System.DateTime]":
                            dbType = DbType.DateTime;
                            break;
                        default:
                            dbType = DbType.String;
                            break;
                    }
                    list.Add(DbFactory.CreateDbParameter(DbHelper.DbParmChar + propertyInfo.Name, propertyInfo.GetValue((object)entity, (object[])null), dbType));
                }
            }
            return Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list);
        }

        public static DbParameter[] GetParameter(Hashtable ht)
        {
            IList<DbParameter> list = (IList<DbParameter>)new List<DbParameter>();
            foreach (string str in (IEnumerable)ht.Keys)
            {
                DbType dbType = !(ht[(object)str] is DateTime) ? DbType.String : DbType.DateTime;
                list.Add(DbFactory.CreateDbParameter(DbHelper.DbParmChar + str, ht[(object)str], dbType));
            }
            return Enumerable.ToArray<DbParameter>((IEnumerable<DbParameter>)list);
        }

        public static object GetKeyField<T>()
        {
            Type type = typeof(T);
            string str = "";
            string name = type.Name;
            foreach (Attribute attribute in type.GetCustomAttributes(true))
            {
                PrimaryKeyAttribute primaryKeyAttribute = attribute as PrimaryKeyAttribute;
                if (primaryKeyAttribute != null)
                    str = primaryKeyAttribute.Name;
            }
            return (object)str;
        }

        public static object GetKeyField<T>(T entity)
        {
            Type type = entity.GetType();
            string str = "";
            IEnumerable<PropertyInfo> query = type.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Any());
            foreach (PropertyInfo p in query)
            {
                str = p.Name;
            }
            return (object)str;
        }

        public static string GetKeyField(string className)
        {
            Type type = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "LeaRun.Entity.dll")).GetType(className);
            string str = "";
            string name = type.Name;
            foreach (Attribute attribute in type.GetCustomAttributes(true))
            {
                PrimaryKeyAttribute primaryKeyAttribute = attribute as PrimaryKeyAttribute;
                if (primaryKeyAttribute != null)
                    str = primaryKeyAttribute.Name;
            }
            return str;
        }

        public static object GetKeyFieldValue<T>(T entity)
        {
            Type type = typeof(T);
            string name1 = "";
            string name2 = type.Name;
            foreach (Attribute attribute in type.GetCustomAttributes(true))
            {
                PrimaryKeyAttribute primaryKeyAttribute = attribute as PrimaryKeyAttribute;
                if (primaryKeyAttribute != null)
                    name1 = primaryKeyAttribute.Name;
            }
            return (object)type.GetProperty(name1).GetValue((object)entity, (object[])null).ToString();
        }

        public static string GetFieldText(PropertyInfo pi)
        {
            object[] customAttributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            return !Enumerable.Any<object>((IEnumerable<object>)customAttributes) ? pi.Name : (customAttributes[0] as DisplayNameAttribute).DisplayName;
        }

        public static string GetClassName<T>()
        {
            Type type = typeof(T);
            IEnumerable<DisplayNameAttribute> source = Enumerable.OfType<DisplayNameAttribute>((IEnumerable)type.GetCustomAttributes(true));
            DisplayNameAttribute[] displayNameAttributeArray = source as DisplayNameAttribute[] ?? Enumerable.ToArray<DisplayNameAttribute>(source);
            return !Enumerable.Any<DisplayNameAttribute>((IEnumerable<DisplayNameAttribute>)displayNameAttributeArray) ? type.Name : Enumerable.ToList<DisplayNameAttribute>((IEnumerable<DisplayNameAttribute>)displayNameAttributeArray)[0].DisplayName;
        }

        /// <summary>
        /// InsertSql , 新增数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">HashTable集合</param>
        /// <returns>Sql</returns>
        public static StringBuilder InsertSql(string tableName, Hashtable ht)
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append(" Insert Into ");
            stringBuilder1.Append(tableName);
            stringBuilder1.Append("(");
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (string str in (IEnumerable)ht.Keys)
            {
                if (ht[(object)str] != null)
                {
                    stringBuilder3.Append("," + str);
                    stringBuilder2.Append("," + DbHelper.DbParmChar + str);
                }
            }
            stringBuilder1.Append(stringBuilder3.ToString().Substring(1, stringBuilder3.ToString().Length - 1) + ") Values (");
            stringBuilder1.Append(stringBuilder2.ToString().Substring(1, stringBuilder2.ToString().Length - 1) + ")");
            return stringBuilder1;
        }

        /// <summary>
        /// InsertSql , 新增数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>Sql</returns>
        public static StringBuilder InsertSql<T>(T entity)
        {
            Type type = entity.GetType();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" Insert Into ");
            stringBuilder.Append(type.Name);
            stringBuilder.Append("(");
            StringBuilder stringBuilder_2 = new StringBuilder();
            StringBuilder stringBuilder_3 = new StringBuilder();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                IEnumerable<PropertyInfo> query = type.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Any());
                foreach (PropertyInfo p in query)
                {
                    if (propertyInfo.GetValue((object)entity, (object[])null) != null && p.Name != propertyInfo.Name)
                    {
                        stringBuilder_3.Append("," + propertyInfo.Name);
                        stringBuilder_2.Append("," + DbHelper.DbParmChar + propertyInfo.Name);
                    }

                }
            }
            stringBuilder.Append(stringBuilder_3.ToString().Substring(1, stringBuilder_3.ToString().Length - 1) + ") Values (");
            stringBuilder.Append(stringBuilder_2.ToString().Substring(1, stringBuilder_2.ToString().Length - 1) + ")");
            return stringBuilder;
        }

        /// <summary>
        /// UpdateSql , 修改数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">HashTable集合</param>
        /// <param name="pkName">条件列名</param>
        /// <returns>Sql</returns>
        public static StringBuilder UpdateSql(string tableName, Hashtable ht, string pkName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" Update ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" Set ");
            bool flag = true;
            foreach (string str in (IEnumerable)ht.Keys)
            {
                if (ht[(object)str] != null && pkName != str)
                {
                    if (flag)
                    {
                        flag = false;
                        stringBuilder.Append(str);
                        stringBuilder.Append("=");
                        stringBuilder.Append(DbHelper.DbParmChar + str);
                    }
                    else
                    {
                        stringBuilder.Append("," + str);
                        stringBuilder.Append("=");
                        stringBuilder.Append(DbHelper.DbParmChar + str);
                    }
                }
            }
            stringBuilder.Append(" Where ").Append(pkName).Append("=").Append(DbHelper.DbParmChar + pkName);
            return stringBuilder;
        }

        /// <summary>
        /// UpdateSql , 修改数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <param name="pkName">条件列名</param>
        /// <returns>Sql</returns>
        public static StringBuilder UpdateSql<T>(T entity, string pkName)
        {
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" Update ");
            stringBuilder.Append(type.Name);
            stringBuilder.Append(" Set ");
            bool flag = true;
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetValue((object)entity, (object[])null) != null && DbHelperSqlGeneration.GetKeyField<T>().ToString() != propertyInfo.Name)
                {
                    if (flag)
                    {
                        flag = false;
                        stringBuilder.Append(propertyInfo.Name);
                        stringBuilder.Append("=");
                        stringBuilder.Append(DbHelper.DbParmChar + propertyInfo.Name);
                    }
                    else
                    {
                        stringBuilder.Append("," + propertyInfo.Name);
                        stringBuilder.Append("=");
                        stringBuilder.Append(DbHelper.DbParmChar + propertyInfo.Name);
                    }
                }
            }
            stringBuilder.Append(" Where ").Append(pkName).Append("=").Append(DbHelper.DbParmChar + pkName);
            return stringBuilder;
        }

        /// <summary>
        /// UpdateSql , 修改
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>Sql</returns>
        public static StringBuilder UpdateSql<T>(T entity)
        {
            string str = DbHelperSqlGeneration.GetKeyField<T>().ToString();
            if (string.IsNullOrEmpty(str))
            {
                str = DbHelperSqlGeneration.GetKeyField<T>(entity).ToString();
            }
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Update ");
            stringBuilder.Append(type.Name);
            stringBuilder.Append(" Set ");
            bool flag = true;
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetValue((object)entity, (object[])null) != null && str != propertyInfo.Name)
                {
                    if (flag)
                    {
                        flag = false;
                        stringBuilder.Append(propertyInfo.Name);
                        stringBuilder.Append("=");
                        stringBuilder.Append(DbHelper.DbParmChar + propertyInfo.Name);
                    }
                    else
                    {
                        stringBuilder.Append("," + propertyInfo.Name);
                        stringBuilder.Append("=");
                        stringBuilder.Append(DbHelper.DbParmChar + propertyInfo.Name);
                    }
                }
            }
            stringBuilder.Append(" Where ").Append(str).Append("=").Append(DbHelper.DbParmChar + str);
            return stringBuilder;
        }

        /// <summary>
        /// DeleteSql , 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">列名</param>
        /// <returns>Sql</returns>
        public static StringBuilder DeleteSql(string tableName, string pkName)
        {
            return new StringBuilder("Delete From " + tableName + " Where " + pkName + " = " + DbHelper.DbParmChar + pkName);
        }

        /// <summary>
        /// DeleteSql  , 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">HashTable集合</param>
        /// <returns>Sql</returns>
        public static StringBuilder DeleteSql(string tableName, Hashtable ht)
        {
            StringBuilder stringBuilder = new StringBuilder("Delete From " + tableName + " Where 1=1");
            foreach (string str in (IEnumerable)ht.Keys)
                stringBuilder.Append(" AND " + str + " = " + DbHelper.DbParmChar + str);
            return stringBuilder;
        }

        /// <summary>
        /// DeleteSql , 删除数据根据实体类的值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>Sql</returns>
        public static StringBuilder DeleteSql<T>(T entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringBuilder = new StringBuilder("Delete From " + type.Name + " Where 1=1");
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetValue((object)entity, (object[])null) != null)
                    stringBuilder.Append(" AND " + propertyInfo.Name + " = " + DbHelper.DbParmChar + propertyInfo.Name);
            }
            return stringBuilder;
        }

        /// <summary>
        /// SelectSql , 查询数据根据实体类
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <returns>Sql</returns>
        public static StringBuilder SelectSql<T>() where T : new()
        {
            string name = typeof(T).Name;
            PropertyInfo[] properties = DbHelperSqlGeneration.GetProperties(new T().GetType());
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PropertyInfo propertyInfo in properties)
            {
                propertyInfo.PropertyType.ToString();
                stringBuilder.Append(propertyInfo.Name + ",");
            }
            if (stringBuilder.Length > 0)
                stringBuilder.Remove(stringBuilder.ToString().Length - 1, 1);
            return new StringBuilder(string.Format("SELECT {0} FROM {1} WHERE 1=1 ", (object)stringBuilder.ToString(), (object)(name + " ")));
        }

        /// <summary>
        /// SelectSql , 查询前多少条数据根据实体类
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="Top">多少条</param>
        /// <returns>Sql</returns>
        public static StringBuilder SelectSql<T>(int Top) where T : new()
        {
            string name = typeof(T).Name;
            PropertyInfo[] properties = DbHelperSqlGeneration.GetProperties(new T().GetType());
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PropertyInfo propertyInfo in properties)
                stringBuilder.Append(propertyInfo.Name + ",");
            if (stringBuilder.Length > 0)
                stringBuilder.Remove(stringBuilder.ToString().Length - 1, 1);
            return new StringBuilder(string.Format("SELECT top {0} {1} FROM {2} WHERE 1=1 ", (object)Top, (object)stringBuilder.ToString(), (object)(name + " ")));
        }

        /// <summary>
        /// SelectSql , 查询数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>Sql</returns>
        public static StringBuilder SelectSql(string tableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM " + tableName + " WHERE 1=1 ");
            return stringBuilder;
        }

        /// <summary>
        /// SelectSql , 查询前多少条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="Top">多少条</param>
        /// <returns>Sql</returns>
        public static StringBuilder SelectSql(string tableName, int Top)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT top " + (object)Top + " * FROM " + tableName + " WHERE 1=1 ");
            return stringBuilder;
        }

        /// <summary>
        /// SelectCountSql , 查询总条数
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <returns>Sql</returns>
        public static StringBuilder SelectCountSql<T>() where T : new()
        {
            return new StringBuilder("SELECT Count(1) FROM " + typeof(T).Name + " WHERE 1=1 ");
        }

        /// <summary>
        /// SelectMaxSql , 查询最大值 MAX
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="propertyName">最大值列名</param>
        /// <returns>Sql</returns>
        public static StringBuilder SelectMaxSql<T>(string propertyName) where T : new()
        {
            string name = typeof(T).Name;
            return new StringBuilder("SELECT MAX(" + propertyName + ") FROM " + name + "  WHERE 1=1 ");
        }

        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

    }
}

namespace Sure.PersonalProject.DataAccess.Factory
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DbHelper  、 访问工厂
    -----------------------------------------------------------------------*/

    using System.Configuration;
    using Sure.PersonalProject.DataAccess.Interface;
    using Sure.PersonalProject.DataAccess.Realization;

    /// <summary>
    /// DbHelper 访问工厂
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 创建接口实现对象 
        /// </summary>
        private static Database db = (Database)null;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string connStringAppkey = ConfigurationManager.AppSettings["SenlinSqlServer"];

        /// <summary>
        /// 创建工厂对象
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns>InterfaceDatabase</returns>
        public static IDatabase Database(string connString)
        {
            if (DbHelper.db == null)
                return (IDatabase)(DbHelper.db = new Database(connString));
            lock (DbHelper.locker)
                return (IDatabase)DbHelper.db;
        }

        /// <summary>
        /// 创建工厂对象
        /// </summary>
        public static IDatabase Database()
        {
            return DbHelper.Database(connStringAppkey);
        }
    }
}

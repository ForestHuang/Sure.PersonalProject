namespace Sure.PersonalProject.DAL
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang   
    [time]:2017-9-21
    [explain]: DEFAULTDAL 首页数据访问
    -----------------------------------------------------------------------*/
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entity;
    using DataAccess.Factory;
    using Utilities;

    /// <summary>
    /// DEFAULT - 主界面所有动作
    /// </summary>
    public class DEFAULTDAL
    {
        /// <summary>
        /// Log4net 配置路径
        /// </summary>
        private static string log4netPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/log4net/log4net.config");

        #region SURE_MENU - 菜单

        /// <summary>
        /// SURE_MENU - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public static int INSERT_SURE_MENU(SURE_MENU model)
        {
            try
            {
                return DbHelper.Database().Insert<SURE_MENU>(model);
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(DEFAULTDAL), log4netPath)
                    .Error("INSERT_SURE_MENU(SURE_MENU model) ----> " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// SURE_MENU - 修改信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public static int UPDATE_SURE_MENU(SURE_MENU model)
        {
            try
            {
                return DbHelper.Database().Update<SURE_MENU>(model);
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(DEFAULTDAL), log4netPath)
                    .Error("UPDATE_SURE_MENU(SURE_MENU model) ----> " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// SURE_MENU - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        public static List<SURE_MENU> GET_SURE_MENU()
        {
            try
            {
                return DbHelper.Database().FindList<SURE_MENU>();
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(DEFAULTDAL), log4netPath)
                    .Error("GET_SURE_MENU() ----> " + ex.Message);
                return new List<SURE_MENU>();
            }
        }

        #endregion
    }
}

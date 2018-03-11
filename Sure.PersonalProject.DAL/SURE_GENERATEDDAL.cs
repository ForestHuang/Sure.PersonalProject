namespace Sure.PersonalProject.DAL
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: SURE_GENERATEDDAL - 生成对应的Model、DAL、BLL
    -----------------------------------------------------------------------*/
    using System;
    using Entity;
    using DataAccess.Factory;
    using Utilities;
    using System.Collections.Generic;

    /// <summary>
    /// SURE_GENERATEDDAL - 生成对应的Model、DAL、BLL
    /// </summary>
    public class SURE_GENERATEDDAL
    {
        /// <summary>
        /// Log4net 配置路径
        /// </summary>
        private static string log4netPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/log4net/log4net.config");

        /// <summary>
        /// INSERT_SURE_GENERATED - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public static int INSERT_SURE_GENERATED(SURE_GENERATED model)
        {
            try
            {
                return DbHelper.Database().Insert<SURE_GENERATED>(model);
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(SURE_GENERATEDDAL), log4netPath)
                    .Error("INSERT_SURE_GENERATED(SURE_GENERATED model) ----> " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// GET_SURE_GENERATED - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        public static List<SURE_GENERATED> GET_SURE_GENERATED()
        {
            try
            {
                return DbHelper.Database().FindList<SURE_GENERATED>();
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(SURE_GENERATEDDAL), log4netPath)
                                   .Error("GET_SURE_GENERATED() ----> " + ex.Message);
                return new List<SURE_GENERATED>();
            }
        }
    }
}

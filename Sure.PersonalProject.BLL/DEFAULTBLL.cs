namespace Sure.PersonalProject.BLL
{
    using Entity;
    using DAL;
    /*---------------------------------------------------------------------
[author]:senlin.huang   
[time]:2017-9-21
[explain]: DEFAULTBLL 首页逻辑
-----------------------------------------------------------------------*/
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// DEFAULTBLL 首页逻辑
    /// </summary>
    public class DEFAULTBLL
    {
        /// <summary>
        /// SURE_MENU - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public static int INSERT_SURE_MENU(SURE_MENU model)
        {
            return DEFAULTDAL.INSERT_SURE_MENU(model);
        }

        /// <summary>
        /// SURE_MENU - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public static int UPDATE_SURE_MENU(SURE_MENU model)
        {
            return DEFAULTDAL.UPDATE_SURE_MENU(model);
        }

        /// <summary>
        /// SURE_MENU - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        public static List<SURE_MENU> GET_SURE_MENU()
        {
            return DEFAULTDAL.GET_SURE_MENU();
        }
    }
}

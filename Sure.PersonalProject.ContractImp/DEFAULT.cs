namespace Sure.PersonalProject.ContractImp
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang   
    [time]:2017-9-21
    [explain]: IDEFAULT 首页接口实现
    -----------------------------------------------------------------------*/
    using Sure.PersonalProject.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entity;
    using BLL;

    /// <summary>
    /// DEFAULT - 主界面接口实现
    /// </summary>
    public class DEFAULT : IDEFAULT
    {
        /// <summary>
        /// SURE_MENU - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public int INSERT_SURE_MENU(SURE_MENU model)
        {
            return DEFAULTBLL.INSERT_SURE_MENU(model);
        }

        /// <summary>
        /// SURE_MENU - 修改信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public int UPDATE_SURE_MENU(SURE_MENU model)
        {
            return DEFAULTBLL.UPDATE_SURE_MENU(model);
        }

        /// <summary>
        /// SURE_MENU - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        public List<SURE_MENU> GET_SURE_MENU()
        {
            return DEFAULTBLL.GET_SURE_MENU();
        }
    }
}

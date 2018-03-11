namespace Sure.PersonalProject.Contract
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang   
    [time]:2017-9-21
    [explain]: IDEFAULT 首页接口
    -----------------------------------------------------------------------*/
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entity;
    using System.ServiceModel;


    /// <summary>
    /// DEFAULT - 主界面接口
    /// </summary>
    [ServiceContract(Name = "DEFAULT - 主页动作")]
    public interface IDEFAULT
    {
        /// <summary>
        /// SURE_MENU - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        [OperationContract(Name = " SURE_MENU - 新增信息")]
        int INSERT_SURE_MENU(SURE_MENU model);

        /// <summary>
        /// SURE_MENU - 修改信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        [OperationContract(Name = " SURE_MENU - 修改信息")]
        int UPDATE_SURE_MENU(SURE_MENU model);

        /// <summary>
        /// SURE_MENU - 获取全部数据信息
        /// </summary>
        /// <returns>数据信息集合</returns>
        [OperationContract(Name = " SURE_MENU - 获取全部信息")]
        List<SURE_MENU> GET_SURE_MENU();

    }

}

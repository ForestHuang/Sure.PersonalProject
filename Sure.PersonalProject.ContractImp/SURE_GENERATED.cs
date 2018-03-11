namespace Sure.PersonalProject.ContractImp
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: ISURE_GENERATED - 生成对应的Model、DAL、BLL接口实现
    -----------------------------------------------------------------------*/
    using Sure.PersonalProject.Entity;
    using Contract;
    using BLL;
    using System.Collections.Generic;

    /// <summary>
    /// ISURE_GENERATED 接口实现
    /// </summary>
    public class SURE_GENERATED : ISURE_GENERATED
    {
        /// <summary>
        /// INSERT_SURE_GENERATED - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        public int INSERT_SURE_GENERATED(Entity.SURE_GENERATED model)
        {
            return SURE_GENERATEDBLL.INSERT_SURE_GENERATED(model);
        }

        /// <summary>
        /// GET_SURE_GENERATED - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        public List<Entity.SURE_GENERATED> GET_SURE_GENERATED()
        {
            return SURE_GENERATEDBLL.GET_SURE_GENERATED();
        }
    }
}

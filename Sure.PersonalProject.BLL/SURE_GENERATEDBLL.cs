namespace Sure.PersonalProject.BLL
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: SURE_GENERATEDBLL - 生成对应的Model、DAL、BLL
    -----------------------------------------------------------------------*/
    using Entity;
    using DAL;
    using System.Collections.Generic;

    /// <summary>
    /// SURE_GENERATEDBLL - 生成对应的Model、DAL、BLL
    /// </summary>
    public class SURE_GENERATEDBLL
    {
        /// <summary>
        /// INSERT_SURE_GENERATED - 新增信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int INSERT_SURE_GENERATED(SURE_GENERATED model)
        {
            return SURE_GENERATEDDAL.INSERT_SURE_GENERATED(model);
        }

        /// <summary>
        /// GET_SURE_GENERATED - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        public static List<SURE_GENERATED> GET_SURE_GENERATED()
        {
            return SURE_GENERATEDDAL.GET_SURE_GENERATED();
        }
    }
}

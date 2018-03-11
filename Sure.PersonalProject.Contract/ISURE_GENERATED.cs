namespace Sure.PersonalProject.Contract
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: ISURE_GENERATED - 生成对应的Model、DAL、BLL接口
    -----------------------------------------------------------------------*/
    using Sure.PersonalProject.Entity;
    using System.Collections.Generic;
    using System.ServiceModel;


    /// <summary>
    /// ISURE_GENERATED 接口
    /// </summary>
    [ServiceContract(Name = "SURE_GENERATED - BLL/DAL/MODEL文件生成")]
    public interface ISURE_GENERATED
    {
        /// <summary>
        /// INSERT_SURE_GENERATED - 新增信息
        /// </summary>
        /// <param name="model">入参</param>
        /// <returns>是否成功</returns>
        [OperationContract(Name = " SURE_GENERATED - 新增信息")]
        int INSERT_SURE_GENERATED(Entity.SURE_GENERATED model);

        /// <summary>
        /// GET_SURE_GENERATED - 获取全部信息
        /// </summary>
        /// <returns>全部信息集合</returns>
        [OperationContract(Name = " SURE_GENERATED - 获取全部信息")]
        List<Entity.SURE_GENERATED> GET_SURE_GENERATED();
    }
}

using System;

namespace Sure.PersonalProject.Utilities
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: Log4net 记录日志
    -----------------------------------------------------------------------*/
    using System.IO;
    using log4net;


    /// <summary>
    /// Log4net , 记录日志
    /// </summary>
    public class Log4net
    {
        public Log4net(string log4netPath)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netPath));
        }
        /// <summary>
        /// 创建Log4net对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回: ILog</returns>
        public ILog log4netCreate(Type type)
        {
            return LogManager.GetLogger(type);
        }

        /// <summary>
        /// 创建Log4net对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="log4netPath">路径</param>
        /// <returns>返回: ILog</returns>
        public static ILog log4netCreate(Type type, string log4netPath)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netPath));
            return LogManager.GetLogger(type);
        }


        /// <summary>
        /// 创建Log4net对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="log4netPath">路径</param>
        /// <returns>返回: ILog</returns>
        public static ILog log4netCreate(string type, string log4netPath)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netPath));
            return LogManager.GetLogger(type);
        }
    }
}

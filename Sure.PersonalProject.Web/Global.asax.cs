using Sure.PersonalProject.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sure.PersonalProject.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /*
         * Application_Error
         * 全局捕获异常，记录到 Txt
         */
        protected void Application_Error()
        {
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                //异常信息
                string strExceptionMessage = string.Empty;
                //异常类
                string type = string.Empty;
                //出错位置
                string position = string.Empty;
                //Log4net 配置文件路径
                string log4netPath = Server.MapPath("~/App_Data/log4net/log4net.config");

                strExceptionMessage = lastError.Message;
                type = lastError.TargetSite.DeclaringType.FullName;
                position = lastError.TargetSite.DeclaringType.FullName + "." + lastError.TargetSite.Name;

                /*
                 * 错误记录到 Txt 文件
                 */
                Log4net.log4netCreate(type, log4netPath).Error("【" + position + "】 全局捕获异常信息 —————————> " + strExceptionMessage);

                //对HTTP 404做额外处理，其他错误全部当成500服务器错误
                HttpException httpError = lastError as HttpException;
                if (httpError != null)
                {
                    //获取错误代码
                    int httpCode = httpError.GetHttpCode();
                    strExceptionMessage = httpError.Message;
                    #region 错误 , 404

                    if (httpCode == 400 || httpCode == 404)
                    {
                        //跳转到指定的静态404信息页面
                        Response.StatusCode = 404;
                        Response.Redirect("/Default/Error_404");
                        Server.ClearError();
                        return;
                    }

                    #endregion
                }

                /*
                 * 跳转到指定的HTTP 500 错误信息页面
                 * 跳转到静态页面一定要用Response.WriteFile方法                 
                 */
                Response.StatusCode = 500;
                //Response.Redirect("/Default/Error_500");

                //一定要调用Server.ClearError() , 否则会触发错误详情页（就是黄页）
                Server.ClearError();
                Response.Redirect("/Default/Error_500");
            }
        }
    }
}

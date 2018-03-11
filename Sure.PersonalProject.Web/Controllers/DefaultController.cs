using Newtonsoft.Json;
using Sure.PersonalProject.Contract;
using Sure.PersonalProject.Entity;
using Sure.PersonalProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Sure.PersonalProject.Web.Controllers
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang   
    [time]:2017-9-21
    [explain]: DefaultController 首页
    -----------------------------------------------------------------------*/
    public class DefaultController : Controller
    {
        #region Private 属性

        /// <summary>
        /// 创建 WCF 服务对象
        /// </summary>
        private static ChannelFactory<IDEFAULT> IDEFAULT_ChannelFactory = new ChannelFactory<IDEFAULT>("BasicHttpBinding_DEFAULT");

        /// <summary>
        ///  WCF 接口对象
        /// </summary>
        private static IDEFAULT proxy = null;

        #endregion

        //主页
        public ActionResult Default() { return View(); }

        //错误 404 ,页面未找到
        public ActionResult Error_404() { return View(); }

        //错误 500 ,内部错误
        public ActionResult Error_500() { return View(); }

        #region Menu 菜单

        //Menu 菜单，编辑（新增、修改、删除）- 图标：http://www.bootcss.com/p/font-awesome/
        public ActionResult Menu()
        {
            if (IDEFAULT_ChannelFactory.State != CommunicationState.Opened)
                IDEFAULT_ChannelFactory.Open();
            proxy = IDEFAULT_ChannelFactory.CreateChannel();
            return View();
        }

        //MenuSave - Menu 菜单新增 
        public ActionResult MenuSave(string requestData)
        {
            SURE_MENU model = JsonConvert.DeserializeObject<SURE_MENU>(requestData);
            model.SURE_MENU_LEVEL = 0;
            model.SURE_CREATE_DATE = DateTime.Now;
            model.SURE_CREATE_EMP = "Admin";
            model.SURE_MODIFY_DATE = DateTime.Now;
            model.SURE_MODIFY_EMP = "Admin";
            if (model.SURE_MENU_ID == null)
            {
                model.SURE_MENU_ID = 0;
            }
            if (proxy.INSERT_SURE_MENU(model) == 1)
                return Json(new { message = "Success", content = "操作成功 ！" });
            else
                return Json(new { message = "Fail", content = "操作错误 ! " });

        }

        //MenuEdit - Menu 菜单修改
        public ActionResult MenuEdit(string requestData)
        {
            SURE_MENU model = JsonConvert.DeserializeObject<SURE_MENU>(requestData);
            model.SURE_MENU_LEVEL = 0;
            model.SURE_CREATE_DATE = DateTime.Now;
            model.SURE_CREATE_EMP = "Admin";
            model.SURE_MODIFY_DATE = DateTime.Now;
            model.SURE_MODIFY_EMP = "Admin";
            if (proxy.UPDATE_SURE_MENU(model) == 1)
                return Json(new { message = "Success", content = "操作成功 ！" });
            else
                return Json(new { message = "Fail", content = "操作错误 ! " });
        }

        // Get_Menu - 获取全部菜单数据
        public string Get_Menu()
        {
            List<SURE_MENU> listData = proxy.GET_SURE_MENU();
            Sure_SURE_MENUResponse model = new Sure_SURE_MENUResponse() { aaData = listData };
            return JsonConvert.SerializeObject(model);
        }

        //Get_MenuByID - 根据ID查询菜单信息
        public string Get_MenuByID(string SURE_MENU_ID)
        {
            SURE_MENU model = proxy.GET_SURE_MENU().Where(x => x.SURE_MENU_ID == Convert.ToInt32(SURE_MENU_ID)).FirstOrDefault();
            return JsonConvert.SerializeObject(model);
        }

        #endregion

        #region API 请求

        //Api 请求
        public ActionResult ApiRequest() { return View(); }

        //发送请求
        public ActionResult ApiMethod(string url, string requestData, string methodType)
        {
            string message = string.Empty;
            try
            {
                if (methodType == "GET")
                {
                    url = url + requestData;
                    return Json(new { messageMata = HttpGeneral.GET(url) });
                }

                if (methodType == "POST")
                {
                    return Json(new { messageMata = HttpGeneral.POST(url, requestData) });
                }
            }
            catch (Exception ex) { message = ex.Message; }
            return Json(new { messageMata = message });
        }

        #endregion

        #region Private 方法



        #endregion

    }

    /// <summary>
    /// 生成类输出 DTO
    /// </summary>
    public class Sure_SURE_MENUResponse
    {
        private List<Entity.SURE_MENU> _aaData;
        public List<Entity.SURE_MENU> aaData
        {
            get { return _aaData; }
            set { _aaData = value; }
        }

    }
}


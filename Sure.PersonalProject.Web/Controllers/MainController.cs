using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sure.PersonalProject.Web.Controllers
{
    public class MainController : Controller
    {
        /*
         * Menu 菜单，新增以及编辑，页面View 
         */
        public ActionResult LeftMenuCompile()
        {
            return View();
        }

        /*
         * 首页，页面View 
         */
        public ActionResult Main() { return View(); }
    }
}
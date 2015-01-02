using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class GoodsManageController : Controller
    {
        public ActionResult GoodsManageList()
        {
            return PartialView();
        }
        public ActionResult CreateGoodsManage()
        {
            return PartialView();
        }
        public ActionResult EditGoodsManage()
        {
            return PartialView();
        }
        public ActionResult DeleteGoodsManage()
        {
            return PartialView();
        }
	}
}
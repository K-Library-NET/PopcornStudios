using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    /// <summary>
    /// 出口商品申报
    /// </summary>
    public class GoodsExitController : Controller
    {
        public ActionResult GoodsExitList()
        {
            return PartialView();
        }
        public ActionResult CreateGoodsExit()
        {
            return PartialView();
        }
        public ActionResult EditGoodsExit()
        {
            return PartialView();
        }
        public ActionResult DeleteGoodsExit()
        {
            return PartialView();
        }
	}
}
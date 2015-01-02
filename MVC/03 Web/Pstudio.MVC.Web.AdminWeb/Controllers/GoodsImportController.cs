using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    /// <summary>
    /// 商品出口申报
    /// </summary>
    public class GoodsImportController : Controller
    {
        public ActionResult GoodsImportList()
        {
            return PartialView();
        }
        public ActionResult CreateGoodsImport()
        {
            return PartialView();
        }
        public ActionResult EditGoodsImport()
        {
            return PartialView();
        }
        public ActionResult DeleteGoodsImport()
        {
            return PartialView();
        }
	}
}
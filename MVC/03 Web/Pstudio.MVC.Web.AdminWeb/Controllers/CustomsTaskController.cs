using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    [AllowAnonymous]
    /// <summary>
    /// 海关作业模块
    /// </summary>
    public class CustomsTaskController : Controller
    {
        //
        // GET: /CustomsTask/
        public ActionResult Index()
        {
            return View();
        }

        #region 单证审核功能
        public ActionResult DocumentAuditingList()
        {
            return PartialView();
        }

        public ActionResult CreateDocumentAuditing()
        {
            return PartialView();
        }

        public ActionResult EditDocumentAuditing()
        {
            return PartialView();
        }

        public ActionResult DeleteDocumentAuditing()
        {
            return PartialView();
        }
        #endregion


        #region 运输工具管理功能
        public ActionResult TransportManageList()
        {
            return PartialView();
        }
        public ActionResult CreateTransportManage()
        {
            return PartialView();
        }
        public ActionResult EditTransportManage()
        {
            return PartialView();
        }
        public ActionResult DeleteTransportManage()
        {
            return PartialView();
        }
        #endregion


        #region 商品管理
        public ActionResult CommodityManageList()
        {
            return PartialView();
        }

        public ActionResult CreateCommodityManage()
        {
            return PartialView();
        }
        public ActionResult EditCommodityManage()
        {
            return PartialView();
        }
        public ActionResult DeleteCommodityManage()
        {
            return PartialView();
        }
        #endregion


        public ActionResult SuperviseGoodsUpDownList()
        {
            return PartialView();
        }
        public ActionResult CreateSuperviseGoodsUpDown()
        {
            return PartialView();
        }
        public ActionResult EditSuperviseGoodsUpDown()
        {
            return PartialView();
        }
        public ActionResult DeleteSuperviseGoodsUpDown()
        {
            return PartialView();
        }
    }
}
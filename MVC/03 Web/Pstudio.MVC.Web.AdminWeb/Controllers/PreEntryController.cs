using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class PreEntryController : Controller
    {
        //
        // GET: /PreEntry/
        public ActionResult Index()
        {
            return View();
        }

        #region 运输工具备案信息

        public ActionResult TransportRecordList()
        {
            return PartialView();
        }

        public ActionResult CreateTransportRecord()
        {
            return PartialView();
        }

        public ActionResult EditTransportRecord()
        {
            return PartialView();
        }

        public ActionResult DeleteTransportRecord()
        {
            return PartialView();
        }

        #endregion


        #region 边民身份本案信息

        public ActionResult IdentityRecordList()
        {
            return View();
        }

        public ActionResult CreateIdentityRecord()
        {
            return PartialView();
        }

        public ActionResult EditIdentityRecord()
        {
            return PartialView();
        }

        public ActionResult DeleteIdentityRecord()
        {
            return PartialView();
        }
        #endregion


        #region 运输工具申报信息
        public ActionResult TransportDeclarationList()
        {
            return PartialView();
        }

        public ActionResult CreateTransportDeclaration()
        {
            return PartialView();
        }
        public ActionResult EditTransportDeclaration()
        {
            return PartialView();
        }
        public ActionResult DeleteTransportDeclaration()
        {
            return PartialView();
        }
        #endregion


        #region 边民进口贸易申报
        public ActionResult CarryRecordList()
        {
            return PartialView();
        }

        public ActionResult CreateCarryRecord()
        {
            return PartialView();
        }
        public ActionResult EditCarryRecord()
        {
            return PartialView();
        }
        public ActionResult DeleteCarryRecord()
        {
            return PartialView();
        }
        #endregion


        #region 边民出口贸易商品申报单
        public ActionResult CarryOutRecortList()
        {
            return PartialView();
        }

        public ActionResult EditCarryOutRecort()
        {
            return PartialView();
        }
        #endregion

        public ActionResult ChuanBoJinJingList()
        {
            return PartialView();
        }

        public ActionResult EditChuanBoJinJing()
        {
            return PartialView();
        }

        public ActionResult ChuanBoChuJingList()
        {
            return PartialView();
        }

        public ActionResult EditChuanBoChuJing()
        {
            return PartialView();
        }
    }
}
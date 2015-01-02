using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class CarLeaveController : Controller
    {
        public ActionResult CarLeaveList()
        {
            return PartialView();
        }
        public ActionResult CreateCarLeave()
        {
            return PartialView();
        }
        public ActionResult EditCarLeave()
        {
            return PartialView();
        }
        public ActionResult DeleteCarLeave()
        {
            return PartialView();
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class BoatLeaveController : Controller
    {
        public ActionResult BoatLeaveList()
        {
            return PartialView();
        }
        public ActionResult CreateBoatLeave()
        {
            return PartialView();
        }
        public ActionResult EditBoatLeave()
        {
            return PartialView();
        }
        public ActionResult DeleteBoatLeave()
        {
            return PartialView();
        }
	}
}
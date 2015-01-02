using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class BoatEnterController : Controller
    {
        public ActionResult BoatEnterList()
        {
            return PartialView();
        }
        public ActionResult CreateBoatEnter()
        {
            return PartialView();
        }
        public ActionResult EditBoatEnter()
        {
            return PartialView();
        }
        public ActionResult DeleteBoatEnter()
        {
            return PartialView();
        }
	}
}
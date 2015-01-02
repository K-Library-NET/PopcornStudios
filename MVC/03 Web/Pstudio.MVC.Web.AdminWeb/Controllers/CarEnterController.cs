using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class CarEnterController : Controller
    {
        public ActionResult CarEnterList()
        {
            return PartialView();
        }
        public ActionResult CreateCarEnter()
        {
            return PartialView();
        }
        public ActionResult EditCarEnter()
        {
            return PartialView();
        }
        public ActionResult DeleteCarEnter()
        {
            return PartialView();
        }
	}
}
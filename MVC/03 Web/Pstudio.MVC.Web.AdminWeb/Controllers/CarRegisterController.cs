using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    /// <summary>
    /// 车辆登记
    /// </summary>
    public class CarRegisterController : Controller
    {
        //
        // GET: /CarRegister/
        public ActionResult CarRegisterList()
        {
            return PartialView();
        }

        public ActionResult CreateCarRegister()
        {
            return PartialView();
        }

        public ActionResult EditCarRegister()
        {
            return PartialView();
        }
        public ActionResult DeleteCarRegister()
        {
            return PartialView();
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class PeopleRegisterController : Controller
    {
        //
        // GET: /PeopleRegister/
        public ActionResult PeopleRegisterList()
        {
            return PartialView();
        }
        public ActionResult CreatePeopleRegister()
        {
            return PartialView();
        }
        public ActionResult EditPeopleRegister()
        {
            return PartialView();
        }
        public ActionResult DeletePeopleRegister()
        {
            return PartialView();
        }
	}
}
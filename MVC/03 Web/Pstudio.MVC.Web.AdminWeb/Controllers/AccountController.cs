using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Pstudio.MVC.Web.AdminWeb.Models;

namespace Pstudio.MVC.Web.AdminWeb.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        
        public ActionResult Login()
        {
            return View();
        }

        //[AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
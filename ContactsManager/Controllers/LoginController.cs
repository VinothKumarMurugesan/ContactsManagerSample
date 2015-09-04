using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ContactsManager.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
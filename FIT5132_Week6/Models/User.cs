using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5132_Week6.Models
{
    public class User : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}
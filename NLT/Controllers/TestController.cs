using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NLT.Models;

namespace NLT.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            CompanyModel objCompanyModel = new CompanyModel();

            return View(objCompanyModel);

        }

        [HttpPost]
        public ActionResult Index(CompanyModel companyModel)
        {

            string Res = "";


            return View();

        }
    }
}
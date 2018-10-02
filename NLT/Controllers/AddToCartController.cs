using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLT.Models;

namespace NLT.Controllers
{
    public class AddToCartController : Controller
    {
        // GET: AddToCart
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Add(TestsList mo)
        {

            if (Session["cart"] == null)
            {
                List<TestsList> li = new List<TestsList>();

                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();

                Session["count"] = 1;

            }
            else
            {
                List<TestsList> li = (List<TestsList>)Session["cart"];
                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }

            LocationTestModel locationTestModel = new LocationTestModel();

            LocationTestModel lTM = new LocationTestModel();
            lTM.LocationId = locationTestModel.LocationId;
            lTM.TestId = locationTestModel.TestId;
            lTM.Type = "H";

            return RedirectToAction("Index", "Order", lTM);


          //  return RedirectToAction("Index", "Home");
          

        }

        public ActionResult Myorder()
        {

            return View((List<TestsList>)Session["cart"]);

        }

        public ActionResult Remove(TestsList mob)
        {
            List<TestsList> li = (List<TestsList>)Session["cart"];
            li.RemoveAll(x => x.TestId == mob.TestId);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");
            //return View();
        }


    }
}
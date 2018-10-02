using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLT.Models;

namespace NLT.Controllers
{
    public class UserController : Controller
    {
        LocationTestModel objLocationTestModel = new LocationTestModel();
        BAL objBAL = new BAL();

        LoginModel objLoginModel = new LoginModel();
        ResponseModel objResponseModel;

        // GET: User




        public ActionResult dashboardView()
        {
            return View();
        }




        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Errors = "";

            return View(objLoginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {

                Session["UID"]= loginModel.UserId;
                Session["Password"] = loginModel.Password;


                if(loginModel.UserId.ToString()=="Admin" && loginModel.Password.ToString()=="123")
                {
                    return RedirectToAction("Dashboard", "User");

                }
                else
                {
                    ViewBag.Errors = "Please Enter valid UID/Pwd";

                   return  View(loginModel);

                }
               
            }
            else
            {
                Session["UID"] = "";
                Session["Password"] = "";

                return View("Login");
            }


            
        }

        public  ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult SearchTest()
        {
            objLocationTestModel.LocationId = 0;
            objLocationTestModel.Locations = objBAL.getLocations();

            objLocationTestModel.TestId = 0;
            objLocationTestModel.Tests = loadTests();

            return View(objLocationTestModel);
                  
        }

        [HttpPost]
        public ActionResult SearchTest(LocationTestModel locationTestModel)
        {
            LocationTestModel lTM = new LocationTestModel();
            lTM.LocationId = locationTestModel.LocationId;
            lTM.TestId = locationTestModel.TestId;
            lTM.Type = "H";

            return RedirectToAction("Index", "Order", lTM);

        }

        public List<SelectListItem> loadTests()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem
            {
                Text = "-Select-",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "MRI",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "CBP",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "MRI Ls spine",
                Value = "3"
            });
            items.Add(new SelectListItem
            {
                Text = "Thyroid Profile",
                Value = "4"
            });
            items.Add(new SelectListItem
            {
                Text = "FBS",
                Value = "5"

            });
            items.Add(new SelectListItem
            {
                Text = "TSH",
                Value = "6"
            });


            return items;
        }


        public JsonResult getTestsbyLicationId(LocationModel locationModel)
        {
            List<TestModel> objlist = new List<TestModel>();
            try
            {
                int i = 0;

                if (locationModel.LocationId.ToString() != null || locationModel.LocationId.ToString() != "")
                {
                    objlist = objBAL.getTestsByLocationId(locationModel.LocationId);
                }

            }
            catch (Exception ex)
            {

            }

            return Json(objlist, JsonRequestBehavior.DenyGet);
        }


        public ActionResult Test()
        {
            try
            {
                Test objTest = new Test();

                return View(objTest);

            }
            catch (Exception ex)
            {
   
            }

            return View();
        }

        [HttpPost]
        public ActionResult Test(string txtLocation,string txtTest)
        {
            try
            {
                string txtlocation_ = txtLocation;
                string txtTestName_ = txtTest;


                int LocationId = 0;
                int TestId = 0;

                ResponseModel objResponseModel_Test = new ResponseModel();
                objResponseModel_Test = objBAL.getTest(txtTest);
                TestId = Convert.ToInt32(objResponseModel_Test.Result);
                ResponseModel objResponseModel_Location = new ResponseModel();
                objResponseModel_Location = objBAL.getLocation(txtLocation);

                if(objResponseModel_Test.StatusCode==200 && objResponseModel_Location.StatusCode==200)
                {
                     LocationId = Convert.ToInt32(objResponseModel_Test.Result);
                     //TestId = Convert.ToInt32(objResponseModel_Location.Result);


                    LocationTestModel lTM = new LocationTestModel();
                    lTM.LocationId = LocationId;
                    lTM.TestId = TestId;
                    lTM.Type = "H";


                    //objOrderModel.OldLocationId = LocationId.ToString();
                    //objOrderModel.OldTestId = TestId.ToString();

                    //Session["cart"] = "";
                    //Session["count"] = "";
                    // Session["cart"] = null;
                    // Session["count"] = null;
                    Session["UID"] = null;
                    Session["UserName"] = null;
                    Session["PhoneNumber"] = null;
                    Session["Role"] = null;

                    return RedirectToAction("Index", "Order", lTM);

                }
                else
                {
                    ViewBag.Message = "Please Select Valid details";
                }

            }
            catch (Exception ex)
            {
                
            }

            return View();
        }


        [HttpPost]
        public JsonResult getlocations(string Prefix)
        {
        //    //Note : you can bind same list from database
        //    List<Test> ObjList = new List<Test>()
        //    {

        //        new Test {Id=1,Name="Latur" },
        //        new Test {Id=2,Name="Mumbai" },
        //        new Test {Id=3,Name="Pune" },
        //        new Test {Id=4,Name="Delhi" },
        //        new Test {Id=5,Name="Dehradun" },
        //        new Test {Id=6,Name="Noida" },
        //        new Test {Id=7,Name="New Delhi" }

        //};
        //    //Searching records from list using LINQ query
        //    var CityName = (from N in ObjList
        //                    where N.Name.StartsWith(Prefix, StringComparison.CurrentCultureIgnoreCase)
        //                    select new { N.Name });


            List<LocationModel> ObjList = new List<LocationModel>();
            ObjList = objBAL.getLocations_(Prefix);


            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getTests(string Prefix)
        {
            //    //Note : you can bind same list from database
            //    List<Test> ObjList = new List<Test>()
            //    {

            //        new Test {Id=1,Name="Latur" },
            //        new Test {Id=2,Name="Mumbai" },
            //        new Test {Id=3,Name="Pune" },
            //        new Test {Id=4,Name="Delhi" },
            //        new Test {Id=5,Name="Dehradun" },
            //        new Test {Id=6,Name="Noida" },
            //        new Test {Id=7,Name="New Delhi" }

            //};
            //    //Searching records from list using LINQ query
            //    var CityName = (from N in ObjList
            //                    where N.Name.StartsWith(Prefix, StringComparison.CurrentCultureIgnoreCase)
            //                    select new { N.Name });


            List<Test> ObjList = new List<Test>();
            ObjList = objBAL.getTests(Prefix);


            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }


    }
}
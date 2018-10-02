using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLT.Models;

namespace NLT.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        BAL objBal = new BAL();

        public ActionResult Index(int? TestId, int? LocationId, int? CmpId)
        {

            if (Session["cart"] == null)
            {
                List<TestsList> li = new List<TestsList>();
                TestsList obj = new TestsList();

                if (TestId != null)
                {
                    obj.TestId = Convert.ToInt32(TestId);
                }
                else
                {
                    obj.TestId = 0;
                }
                if (LocationId != null)
                {
                    obj.LocationId = Convert.ToInt32(LocationId);
                }
                else
                {
                    obj.LocationId = 0;
                }
                if (CmpId != null)
                {
                    obj.CompanyId = Convert.ToInt32(CmpId);
                }
                else
                {
                    obj.CompanyId = 0;
                }
                li.Add(obj);


                Session["cart"] = li;
                ViewBag.cart = li.Count();

                Session["count"] = 1;

            }
            else
            {
                List<TestsList> li2 = (List<TestsList>)Session["cart"];
                TestsList obj2 = new TestsList();

                if (TestId != null)
                {
                    obj2.TestId = Convert.ToInt32(TestId);
                }
                else
                {
                    obj2.TestId = 0;
                }

                if (LocationId != null)
                {
                    obj2.LocationId = Convert.ToInt32(LocationId);
                }
                else
                {
                    obj2.LocationId = 0;
                }

                if (CmpId != null)
                {
                    obj2.CompanyId = Convert.ToInt32(CmpId);
                }
                else
                {
                    obj2.CompanyId = 0;
                }
                li2.Add(obj2);

                Session["cart"] = li2;
                ViewBag.cart = li2.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }

            // int? locationId, int? TestId, string Type

            return RedirectToAction("Index", "Order", new { locationId = LocationId, TestId = TestId, Type = "H" });


            // return RedirectToAction("CheckOut", "Index");

            // RedirectToAction(, ) ;


            //  return RedirectToAction()

            // TestDisplayModel objTestDisplayModel = objBal.getTestDetails(Convert.ToInt32(LocationId), Convert.ToInt32(TestId), Convert.ToInt32(CmpId));

            //  return View(objTestDisplayModel);
        }

        [HttpPost]
        public ActionResult Index(CPModel CPModel)
        {
            string Res = "";
            ResponseModel objResponseModel = new ResponseModel();
            TestDisplayModel objTDM = new TestDisplayModel();

            try
            {
                //objTDM.YourName = CPModel.YourName;
                //objTDM.PhoneNumber = CPModel.PhoneNumber;
                //objTDM.TestPersonName = CPModel.TestPersonName;
                //objTDM.Age = CPModel.Age;
                //objTDM.Appointmentdate = CPModel.Appointmentdate;

                Guid guid_;

                if (Session["Guid"] != null)
                {
                    //  guid = Session["Guid"].ToString();        
                    guid_ = (Guid)Session["Guid"];
                    CPModel.guid = guid_;

                }

                if (Session["cart"] != null)
                {
                    List<TestsList> li2 = (List<TestsList>)Session["cart"];

                    CPModel.Tests = li2;
                }

                int SavedOrderId = 0;

                objResponseModel = objBal.save_CP(CPModel);

                if (objResponseModel.StatusCode == 200)
                {
                    SavedOrderId = Convert.ToInt32(objResponseModel.Result);

                    CPModel.OrderId = SavedOrderId;

                    //  return RedirectToAction("Index", "Payment");

                    return RedirectToAction("Index", "Payment", new { Id = SavedOrderId });
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {

            }

            return View();

        }


        [HttpPost]
        public ActionResult Index2(TestDisplayModel TestDisplayModel)
        {


            string Res = "";

            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                int SavedOrderId = 0;

                objResponseModel = objBal.saveTestOrder(TestDisplayModel);

                if (objResponseModel.StatusCode == 200)
                {
                    SavedOrderId = Convert.ToInt32(objResponseModel.Result);

                    //  return RedirectToAction("Index", "Payment");

                    return RedirectToAction("Index", "Payment", new { Id = SavedOrderId });


                }

            }
            catch (Exception ex)
            {

            }


            return RedirectToAction("Index", "CheckOut");
        }

        public ActionResult CheckOutProcess(int? L, int? T)
        {
            CPModel objCPModel = new CPModel();
            List<CheckoutProcessModel> objCprocess = new List<CheckoutProcessModel>();
            List<TestsList> lstList = new List<TestsList>();
            try
            {
                if (Session["cart"] != null)
                {
                    List<TestsList> li2 = (List<TestsList>)Session["cart"];
                    TestsList obj2 = new TestsList();

                    CheckOutProcessModel objCheckOutPro = new CheckOutProcessModel();

                    lstList = objBal.getTestPrise(li2);

                    List<int> compList = new List<int>();
                    foreach (var a in lstList)
                    {
                        if (!compList.Contains(a.CompanyId))
                        {
                            compList.Add(a.CompanyId);
                        }
                    }

                    for (int i = 0; i < compList.Count; i++)
                    {
                        int k = compList[i];
                        var lt1 = lstList.Select(x => x.CompanyId = k);

                        List<TestsList> objTests = new List<TestsList>();
                        string CompanyName = "";
                        foreach (var a in lstList)
                        {
                            if (a.CompanyId == k)
                            {
                                if (a.CompanyName == null || a.CompanyName == "")
                                {
                                    CompanyName = "";
                                }
                                else
                                    CompanyName = a.CompanyName;


                                objTests.Add(new TestsList { TestId = a.TestId, TestName = a.TestName, BranchId = a.BranchId });
                            }
                        }

                        objCprocess.Add(new CheckoutProcessModel { CompanyId = compList[i], CompanyName = CompanyName, Tests = objTests });

                    }


                    objCPModel.YourName = "";
                    objCPModel.Age = "";
                    objCPModel.PhoneNumber = "";
                    objCPModel.TestPersonName = "";
                    objCPModel.Tests = lstList;
                    objCPModel.Appointmentdate = DateTime.Now;

                }

            }
            catch (Exception ex)
            {

            }

            return View(objCPModel);
        }


        public JsonResult PhoneNumberVerify(LoginModel loginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                bool Result = objBal.IsphoneNumberExits(loginModel.PhoneNumber);

                if (Result == false)
                {
                    objResponseModel.StatusCode = 400;
                    objResponseModel.ErrorMessage = "Phone Number Not Registered.";
                }
                else
                {
                    objResponseModel.StatusCode = 200;
                    objResponseModel.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {


            }

            return  Json(objResponseModel,JsonRequestBehavior.AllowGet);

        }
    }
}
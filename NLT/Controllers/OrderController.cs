using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NLT.Models;

namespace NLT.Controllers
{
    public class OrderController : Controller
    {
        LocationTestModel objLocationTestModel = new LocationTestModel();

        BAL objBAL = new BAL();

        //public ActionResult Index(int? locationId,int? TestId)
        //{
        //    int i = 0;

        List<TestsList> objTestsList = new List<TestsList>();

        //    objTestsList.Add(new TestsList { TestId = 0, TestName = "MRI", TestAmount="$100", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 1, TestName = "MRI", TestAmount = "$200", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 2, TestName = "CBP", TestAmount = "$300", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 3, TestName = "MRI Ls spine", TestAmount = "$400", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 4, TestName = "Thyroid Profile", TestAmount = "$500", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 5, TestName = "TSH", TestAmount = "$600", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 6, TestName = "FBS", TestAmount= "$700", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });
        //    objTestsList.Add(new TestsList { TestId = 7, TestName = "FBS", TestAmount = "$700", TestLabImagePath = "~\\..\\Images\\Koala.jpg" });

        //    objLocationTestModel.TestsList = objTestsList;

        //    return View(objLocationTestModel);
        //}

        //public ActionResult Index2(int? locationId, int? TestId,string Type)
        //{
        //    List<TestsList> objTestsList = new List<TestsList>();
        //    OrderModel objOrderModel = new OrderModel();

        //    try
        //    {
        //        int LoctionId_ = 0;
        //        LoctionId_ = Convert.ToInt32(locationId);

        //        int TestId_ = 0;
        //        TestId_ = Convert.ToInt32(TestId);

        //        string Type_ = "";
        //        Type_ = Type;

        //        if (locationId != 0 && TestId != 0)
        //        {
        //            objOrderModel = objBAL.getTestsByLocationAndTestName(LoctionId_, TestId_, Type_);
        //        }
        //        else
        //        {
        //            objOrderModel = objBAL.getTestsByLocationAndTestName(0, 0,"H");
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return View(objOrderModel);
        //}

        //[HttpPost]
        //public ActionResult Index(LocationTestModel objLocationTestModel)
        //{
        //    LocationTestModel obj2 = new LocationTestModel();
        //    obj2.LocationId = 100;
        //    obj2.TestId = 200;

        //    return RedirectToAction("Index", "CheckOut", obj2);
        //}

        

        public ActionResult Index(int? locationId, int? TestId, string Type )
        {
            //List<TestsList> objTestsList = new List<TestsList>();

            OrderModel objOrderModel = new OrderModel();

            try
            {
                int LoctionId_ = 0;
                LoctionId_ = Convert.ToInt32(locationId);


                int TestId_ = 0;
                TestId_ = Convert.ToInt32(TestId);

                string Type_ = "";
                Type_ = Type;


               

                if (locationId != 0 && TestId != 0)
                {
                    objOrderModel = objBAL.getTestsByLocationAndTestName(LoctionId_, TestId_, Type_);
                }
                else
                {
                    objOrderModel = objBAL.getTestsByLocationAndTestName(0, 0,"H");
                }

                objOrderModel.OldLocationId = LoctionId_.ToString();
                objOrderModel.OldTestId = TestId_.ToString();
            }
            catch (Exception ex)
            {

            }

            return View(objOrderModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            OrderModel objOrderModel = new OrderModel();

            try
            {
                //int LocationId = orderModel.LocationId;
                //int  TestId = orderModel.TestId;

                string txtTest = form["txtTest"].ToString();
                string txtOldLocationId = form["txtOldLocationId"].ToString();
                string txtOldTestId = form["txtOldTestId"].ToString();

                ResponseModel objResponseModel_Test = new ResponseModel();
                objResponseModel_Test = objBAL.getTest(txtTest);

                int NewTestId = 0;
                if (objResponseModel_Test.StatusCode==200)
                {
                    NewTestId = Convert.ToInt32(objResponseModel_Test.Result);
                }
                else
                {

                }


                List<TestsList> li2 = (List<TestsList>)Session["cart"];

                int loopId = 0;
                int CompanyId = 0;

                if (Session["cart"] !=null)
                {
                    foreach (var a in li2)
                    {
                        if (loopId == 0)
                        {
                            CompanyId = a.CompanyId;
                            loopId = loopId + 1;
                        }
                    }
                    
                }

                

                //string txtNewTestId = objResponseModel_Test.Result;

                if (Convert.ToInt32(txtOldLocationId) != 0 && NewTestId != 0)
                {
                    objOrderModel = objBAL.getTestsByLocationAndTestName(Convert.ToInt32(txtOldLocationId), CompanyId, NewTestId, "H");
                }
                else
                {
                    objOrderModel = objBAL.getTestsByLocationAndTestName(0, 0, "H");
                }

                objOrderModel.OldLocationId = Convert.ToInt32(txtOldLocationId).ToString();
                objOrderModel.OldTestId = NewTestId.ToString();


            }
            catch (Exception ex)
            {
                
            }

            return View(objOrderModel);
        }

        public ActionResult Index2(int? TestId, int? LocationId, int? CmpId, int? BranchId)
        {
            if (Session["cart"] == null)
            {

                Session["Guid"] = new Guid();

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
                if (BranchId != null)
                {
                    obj.BranchId = Convert.ToInt32(BranchId);
                }
                else
                {
                    obj.BranchId = 0;
                }


                li.Add(obj);


                Session["cart"] = li;
                ViewBag.cart = li.Count();

                Session["count"] = 1;

            }
            else
            {
                List<TestsList> listverif = new List<TestsList>();

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

                if (BranchId != null)
                {
                    obj2.BranchId = Convert.ToInt32(BranchId);
                }
                else
                {
                    obj2.BranchId = 0;
                }

                listverif = li2.Where(x => x.TestId == obj2.TestId).ToList();// Select((x => x.TestId = obj2.TestId) && (x.TestId = obj2.TestId));

                if (listverif.Count > 1)
                {
                    ViewBag.message("This Test is already added");
                }
                else
                {
                    li2.Add(obj2);

                    Session["cart"] = li2;
                    ViewBag.cart = li2.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }



            }

            return RedirectToAction("Index", "Order", new { locationId = LocationId, TestId = TestId, Type = "H" });

            //return View();
        }



        public ActionResult OrdPro(int? TestId, int? LocationId, int? CmpId, int? BranchId)
        {
            if (Session["cart"] == null)
            {

                Session["Guid"] = new Guid();

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
                if (BranchId != null)
                {
                    obj.BranchId = Convert.ToInt32(BranchId);
                }
                else
                {
                    obj.BranchId = 0;
                }


                li.Add(obj);


                Session["cart"] = li;
                ViewBag.cart = li.Count();

                Session["count"] = 1;

            }
            else
            {
                List<TestsList> listverif = new List<TestsList>();

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

                if (BranchId != null)
                {
                    obj2.BranchId = Convert.ToInt32(BranchId);
                }
                else
                {
                    obj2.BranchId = 0;
                }

                listverif  = li2.Where(x => x.TestId == obj2.TestId).ToList();// Select((x => x.TestId = obj2.TestId) && (x.TestId = obj2.TestId));

                if(listverif.Count>0)
                {
                    ViewBag.Message = "This Test is already added";
                    //ViewBag.Message = "This Test is already added";
                }
                else
                {
                    li2.Add(obj2);

                    Session["cart"] = li2;
                    ViewBag.cart = li2.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }

                

            }

              return RedirectToAction("Index", "Order", new { locationId = LocationId, TestId = TestId, Type = "H" });

            //return View();
        }



        public JsonResult ClearCart()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                if (Session["cart"] != null)
                {
                    Session["cart"] = null;
                    Session["count"] = null;

                    objResponseModel.StatusCode = 200;
                    objResponseModel.Result = "";
                }
                else
                {
                    Session["cart"] = null;
                    Session["count"] = null;

                    objResponseModel.StatusCode = 200;
                    objResponseModel.Result = "";
                }
            }
            catch (Exception ex)
            {
                objResponseModel.StatusCode = 400;
                objResponseModel.ErrorMessage = ex.Message;
            }


            return Json(objResponseModel, JsonRequestBehavior.AllowGet);
        }
         
        
        public ActionResult OrderList()
        {
            List<OrderModel> objOrderlist = new List<OrderModel>();

            try
            {
                objOrderlist = objBAL.getOrders(0);
            }
            catch (Exception ex)
            {
                
            }

            return View(objOrderlist);
        }

        public ActionResult GOrder(int? Id)
        {
            List<OrderModel> objOrderlist = new List<OrderModel>();
            GOrder objGOrder = new Models.GOrder();
            
            try
            {
                int OrderId = 0;
                if(Id != null)
                {
                    OrderId = Convert.ToInt32(Id);
                }

              objGOrder = objBAL.GOrder(OrderId);
            }
            catch (Exception ex)
            {

            }

            return View(objGOrder);
        }


        [HttpPost]
        public JsonResult getTests(string Prefix)
        {
            List<Test> ObjList = new List<Test>();

            try
            {
                if (Session["cart"] != null)
                {
                    List<TestsList> li2 = (List<TestsList>)Session["cart"];
                    List<TestsList> listverif = new List<TestsList>();

                    int loop = 0;
                    int CompanyId = 0;
                    foreach (var a_ in li2)
                    {
                        if (loop == 0)
                        {
                            CompanyId = a_.CompanyId;
                            loop = loop++;
                        }

                    }


                    ObjList = objBAL.getTests(Prefix, Convert.ToString(CompanyId.ToString()));

                }
                else
                {
                    ObjList = objBAL.getTests(Prefix);
                }

            }
            catch (Exception ex)
            {
                
            }

            






            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }


    }
}
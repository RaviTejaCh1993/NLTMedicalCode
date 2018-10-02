using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NLT.Models;


namespace NLT.Controllers
{
    public class PaymentController : Controller
    {
        BAL objBAL = new BAL();

        // GET: Payment
        //public ActionResult Index(int? Id)
        //{
        //    PaymentModel objPaymentModel = new PaymentModel();

        //    try
        //    {
        //        int OrderId = Convert.ToInt32(Id.ToString());
        //        objPaymentModel = objBAL.getTestOrder(OrderId);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(objPaymentModel);
        //}

            
        public JsonResult DEorderItem(CPModel cPModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                int orderId     = cPModel.OrderId;
                int orderItemId = cPModel.SequenceId;


                objResponseModel = objBAL.Delete_Order_Item(orderId, orderItemId);


            }
            catch (Exception ex)
            {
                
            }

            return Json(objResponseModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int? Id)
        {
            CPModel objCPModel = new CPModel();

            PaymentModel objPaymentModel = new PaymentModel();
            List<CheckoutProcessModel> objCprocess = new List<CheckoutProcessModel>();
            List<TestsList> lstList = new List<TestsList>();
            try
            {
                int OrderId = Convert.ToInt32(Id.ToString());
                objCPModel = objBAL.getCPTestOrder(OrderId);

                objCPModel.OrderId = OrderId;

            }
            catch (Exception ex)
            {

            }
            return View(objCPModel);
        }


        [HttpPost]
        public ActionResult Index(CPModel cpModel)
        //public ActionResult Index(PaymentModel PaymentModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PaymentModel objPaymentModel = new PaymentModel();
            int OrderId = 0;

            try
            {
                OrderId = Convert.ToInt32(cpModel.OrderId);

                objResponseModel = objBAL.confirmTestOrder(OrderId, Convert.ToInt32(PaymentType.CashAtCenter));

                if (objResponseModel.StatusCode == 200)
                {

                    return RedirectToAction("Confirm", "Payment", new { Id = OrderId });

                }
                else if (objResponseModel.StatusCode == 400)
                {
                    return View();
                }

            }
            catch (Exception ex)
            {

            }
            return View(objPaymentModel);
        }


        public ActionResult Confirm(int? Id)
        {
            int OrderId = 0;
            OrderId = Convert.ToInt32(Id);

            OrderModel objOrderModel = new OrderModel();
            objOrderModel.OrderId = OrderId;


            //PaymentModel objPaymentModel = new PaymentModel();
            //objPaymentModel.OrderId = OrderId;

            Session["cart"] = null;
            Session["count"] = null;

            return View(objOrderModel);
        }


        public JsonResult ConfirmPayment(CPModel cpModel)
        //public ActionResult Index(PaymentModel PaymentModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            PaymentModel objPaymentModel = new PaymentModel();
            int OrderId = 0;

            try
            {
                OrderId = Convert.ToInt32(cpModel.OrderId);
                objResponseModel = objBAL.confirmTestOrder(OrderId, Convert.ToInt32(PaymentType.CashAtCenter));

            }
            catch (Exception ex)
            {

            }

            return Json(objResponseModel, JsonRequestBehavior.AllowGet);
        }



    }
}
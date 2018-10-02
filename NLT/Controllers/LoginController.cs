using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NLT.Models;



namespace NLT.Controllers
{
    public class LoginController : Controller
    {
        BAL objBAL = new BAL();
        ResponseModel responseModel;

        // GET: Login
        public ActionResult Index()
        {
            LoginModel objLoginModel = new LoginModel();

            try
            {

                objLoginModel.PhoneNumber = "";
                objLoginModel.OTP = "";
                objLoginModel.OTPdiv = false;
                objLoginModel.PhoneNumberdiv = true;
                objLoginModel.TransType = "PNum";
            }
            catch (Exception ex)
            {

            }

            return View(objLoginModel);
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel, string btnPhoneNum, string btnOTP)
        {
            responseModel = new ResponseModel();

            string Errors = "";
            try
            {

                if (!string.IsNullOrEmpty(btnPhoneNum))
                {
                    if (loginModel.PhoneNumber == null || loginModel.PhoneNumber == "")
                    {
                        Errors = Errors + " PhoneNumber is Mandatory...\n";
                    }
                    else
                    {
                        bool Result = objBAL.IsphoneNumberExits(loginModel.PhoneNumber);

                        if (Result==false)
                        {
                            Errors = Errors + " PhoneNumber Not valid ";
                        }
                    }

                    if (Errors != "")
                    {
                        ViewBag.Message = Errors;

                        loginModel.PhoneNumberdiv = true;
                        loginModel.OTPdiv = false;

                    }
                    else
                    {
                        //objLoginModel.PhoneNumber = "";
                        //objLoginModel.OTP = "";
                        loginModel.OTPdiv = true;
                        loginModel.PhoneNumberdiv = false;
                        loginModel.TransType = "OTP";

                    }

                }
                if (!string.IsNullOrEmpty(btnOTP))
                {

                    if (loginModel.OTP == null || loginModel.OTP == "")
                    {
                        Errors = Errors + " OTP is Mandatory...\n";
                    }

                    if (Errors != "")
                    {
                        ViewBag.Message = Errors;
                    }
                    else
                    {
                        if (loginModel.OTP == "3344")
                        {
                            Session["Role"] = "Admin";
                            Session["UserName"] = "Admin";
                            return RedirectToAction("dashboard", "User");
                        }
                        else
                        {
                            if (loginModel.OTP == "2244")
                            {
                                //objLoginModel.PhoneNumber = "";
                                //loginModel.OTP = "";
                                /// loginModel.OTPdiv = true;
                                // loginModel.PhoneNumberdiv = false;
                                // loginModel.TransType = "OTP";


                                Session["UID"] = null;
                                Session["UserName"] = null;
                                Session["PhoneNumber"] = null;
                                Session["Role"] = null;

                                bool isPhoneNumExists = false;
                                int userId = 0;

                                if (isPhoneNumExists == true)
                                {
                                    Session["Role"] = "Admin";
                                    Session["UID"] = "100";
                                    Session["UserName"] = "Srinivas..";
                                    Session["PhoneNumber"] = "99999";
                                    Session["Role"] = "User";
                                }
                                else
                                {
                                    string Pno = "";
                                    string Otp = "";
                                    string emailId = "";
                                    string Errors2 = "";


                                    if (loginModel.PhoneNumber == null || loginModel.PhoneNumber == "")
                                    {
                                        Errors2 = Errors2 + " PhoneNumber is Mandatory";
                                    }
                                    else
                                    {
                                        Pno = loginModel.PhoneNumber.ToString();
                                    }

                                    if (loginModel.OTP == null || loginModel.OTP == "")
                                    {
                                        Errors2 = Errors2 + " OTP is Mandatory";
                                    }
                                    else
                                    {
                                        Otp = loginModel.OTP.ToString();
                                    }


                                    if (Errors2 == "")
                                    {
                                        responseModel = objBAL.userSignup(Pno, Otp, emailId);
                                    }

                                    if (responseModel.StatusCode == 200)
                                    {
                                        Session["UID"] = responseModel.Result.ToString();

                                        Session["UserName"] = "Srinivas Vadla22";
                                        Session["PhoneNumber"] = "99999";
                                        Session["Role"] = "User";

                                    }
                                    else if (responseModel.StatusCode == 400)
                                    {

                                    }



                                }



                                return RedirectToAction("dashboard", "User");

                                //ViewBag.Message = "OTP is valid";
                            }
                            else
                            {
                                //objLoginModel.PhoneNumber = "";
                                //objLoginModel.OTP = "";
                                loginModel.OTPdiv = true;
                                loginModel.PhoneNumberdiv = false;
                                loginModel.TransType = "OTP";

                                ViewBag.Message = "OTP is Not Valid valid";
                            }


                        }


                    }

                }

                // ViewBag.Message = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Srinivas", DateTime.Now.ToString());

            }
            catch (Exception ex)
            {

            }

            return View(loginModel);
        }

        public JsonResult PostPhoneNumber(LoginModel loginModel)
        {
            LoginModel objLoginModel = new LoginModel();
            string Errors = "";
            try
            {

                if (loginModel.PhoneNumber == "" || loginModel.PhoneNumber == null)
                {
                    Errors = Errors + " Please Enter PhoneNumber \n ";
                }
                else
                {
                    if (loginModel.PhoneNumber.Length < 10)
                    {
                        Errors = Errors + " PhoneNumber length must be 10 degits  \n ";
                    }
                }

                if (Errors == "")
                {
                    //objLoginModel.PhoneNumber = "";
                    //objLoginModel.OTP = "";
                    objLoginModel.OTPdiv = true;
                }
            }
            catch (Exception ex)
            {

            }

            return Json(objLoginModel, JsonRequestBehavior.AllowGet); // View(objLoginModel);

        }


        public JsonResult PostOTP(LoginModel loginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            LoginModel objLoginModel = new LoginModel();
            string Errors = "";
            try
            {

                if (loginModel.PhoneNumber == "" || loginModel.PhoneNumber == null)
                {
                    Errors = Errors + " Please Enter PhoneNumber \n ";
                }
                else
                {
                    if (loginModel.PhoneNumber.Length < 10)
                    {
                        Errors = Errors + " PhoneNumber length must be 10 degits  \n ";
                    }
                }
                //if (loginModel.OTP == "" || loginModel.OTP == null)
                //{
                //    Errors = Errors + " Please Enter PhoneNumber \n ";
                //}
                //else
                //{
                //    if (loginModel.OTP.Length < 5)
                //    {
                //        Errors = Errors + "OTP Not Valid \n ";
                //    }
                //}

                if (Errors == "")
                {
                    //objLoginModel.PhoneNumber = "";
                    //objLoginModel.OTP = "";
                    //objLoginModel.OTPdiv = true;

                    objResponseModel.StatusCode = 200;
                    objResponseModel.Result = true;

                }
                else
                {
                    objResponseModel.Result = false;
                    objResponseModel.StatusCode = 400;
                    objResponseModel.ErrorMessage = Errors;
                }

            }
            catch (Exception ex)
            {

            }

            return Json(objResponseModel, JsonRequestBehavior.AllowGet); // View(objLoginModel);
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLT.Models;
using System.Configuration;
using Newtonsoft.Json;

namespace NLT.Controllers
{
    public class SignupController : Controller
    {
        BAL objBAL = new BAL();
        ResponseModel responseModel;

        string SMSSendingStatus = ConfigurationManager.AppSettings["SMSSendingStatus"].ToString();
        string SMSUserName = ConfigurationManager.AppSettings["SMSUserName"].ToString();
        string SMSPassWord = ConfigurationManager.AppSettings["SMSPassWord"].ToString();
        string SMSSenderId = ConfigurationManager.AppSettings["SMSSenderId"].ToString();
        string SMSFromMobileNumbers = ConfigurationManager.AppSettings["SMSFromMobileNumbers"].ToString();
        string SMSToMobileNumber = ConfigurationManager.AppSettings["SMSToMobileNumber"].ToString();
        string SMSMessageText1 = ConfigurationManager.AppSettings["SMSMessageText1"].ToString();
        string SMSMessageText2 = ConfigurationManager.AppSettings["SMSMessageText2"].ToString();
        string SMSMessageText3 = ConfigurationManager.AppSettings["SMSMessageText3"].ToString();
        string SMSMessageText4 = ConfigurationManager.AppSettings["SMSMessageText4"].ToString();



        // GET: Signup
        public ActionResult Index()
        {
            LoginModel objLoginModel = new LoginModel();

            try
            {
                Session["SentOtp"] = null;

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
                        Errors = Errors + " PhoneNumber is Mandatory \n";
                    }
                    if (loginModel.EmailId == null || loginModel.EmailId == "")
                    {
                        Errors = Errors + " EmailId is Mandatory \n";
                    }

                    if (loginModel.PhoneNumber  != null || loginModel.PhoneNumber != "")
                    {
                        bool Result = objBAL.IsphoneNumberExits(loginModel.PhoneNumber);

                        if(Result)
                        {
                            Errors = Errors + " PhoneNumber already Regisered ";
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

                        string otp_ = sendOtp(loginModel.PhoneNumber);

                        ViewBag.SentOtp = otp_;

                        Session["SentOtp"] = otp_;

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
                        string OT = "";
                        if (Session["SentOtp"] != null)
                        {
                           OT = Session["SentOtp"].ToString();
                        }

                          
                          // if (loginModel.OTP == "2244")
                            if (loginModel.OTP == OT)
                            {

                            string PhoneNum = "";
                            string Email = "";
                            string OTP = "";

                            PhoneNum = loginModel.PhoneNumber;
                            Email = loginModel.EmailId;
                            OTP = loginModel.OTP;

                            ResponseModel objResponseModel = new ResponseModel();
                            objResponseModel =  objBAL.userSignup(PhoneNum, OTP, Email);

                            if(objResponseModel.StatusCode == 200)
                            {
                                return RedirectToAction("Test","User");
                            }
                            else
                            {
                                ViewBag.Message = objResponseModel.ErrorMessage;
                            }

                        }
                        else
                        {
                            ViewBag.Message = "OTP Not Valid";
                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }

            return View(loginModel);
        }




        public string sendOtp(string PhoneNumber)
        {
            string Result = "";

            try
            {
                SMSCAPI obj = new SMSCAPI();
                string strPostResponse;

                string MessageText = "";

                MessageText = "8822";

                strPostResponse = obj.SendSMS(SMSUserName, SMSPassWord, SMSSenderId, PhoneNumber, MessageText);

                SMSResponse sMSResponse_ = JsonConvert.DeserializeObject<SMSResponse>(strPostResponse);

                if (sMSResponse_ != null)
                {
                    if (Convert.ToInt32(sMSResponse_.JobId) > 0)
                    {
                        sMSResponse_.SentStatus = true;

                        //  bool Result = objBal.save_SMS_Status(sMSResponse_, NewUserId);

                       Result =  MessageText;

                    }

                }


            }
            catch (Exception ex)
            {
                
            }

            return Result;
        }

    }
}
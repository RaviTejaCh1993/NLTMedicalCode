using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NLT.Models
{
    public class LoginModel
    {
        [DisplayName("UserId")]
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { set; get; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; }


        public string PhoneNumber { set; get; }
        public string OTP { set; get; }

        public bool OTPdiv { set; get; }
        public bool PhoneNumberdiv { set; get; }
        public string TransType { set; get; }

        public string EmailId { set; get; }


    }
}
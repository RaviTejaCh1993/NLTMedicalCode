using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLT.Models
{
    public class ResponseModel
    {
        public object Result { set; get; }
        public int StatusCode { set; get; }
        public string ErrorMessage { set; get; }

    }
}

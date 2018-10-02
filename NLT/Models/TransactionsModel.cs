using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLT.Models
{
    public class TransactionsModel
    {
    }


    public class ReportsModel
    {
        public string OrderId { set; get; }
        public string PartnerCode { set; get; }
        public string FilePath { set; get; }

    }

    public class FileOrderModel
    {
        public int OrderId { set; get; }
        public int VendorId { set; get; }

        public bool divShowOrder { set; get; }
        public bool divFileUpload { set; get; }

        public List<FileModel> listFiles { set; get; }


    }
    public class FileModel
    {
        public string OrderId { set; get; }
        public string FileName { set; get; }
        public string FilePath { set; get; }


    }

    //public class FileModel
    //{
    //    public string OrderId { set; get; }
    //    public string FileName { set; get; }
    //    public string FilePath { set; get; }


    //}


    public class LocationModel
    {
        public int LocationId { set; get; }
        public string LocationName { set; get; }
    }

    public class SMSResponse
    {
        public string JobId { get; set; }
        public string Ack { get; set; }
        public string NoOfSMS { get; set; }
        public bool SentStatus { get; set; }
        public string ErrorMessage { get; set; }

    }

    public class LocationTestModel
    {
        //public int LocationId { set; get; }
        //public List<LocationModel> LocationList;

        //public int TestId { set; get; }
        //public List<TestModel> TestList;

        public List<SelectListItem> Locations { get; set; }
        public int? LocationId { set; get; }

        public List<SelectListItem> Tests { get; set; }
        public int? TestId { set; get; }

        public string Type { set; get; }

        public List<TestsList> TestsList;

    }

    //public class TestModel
    //{
    //    public int TestId { set; get; }
    //    public string TestName { set; get; }
    //}
    public class PaymentProcess
    {
        public string OrderId { set; get; }
    }

    

    public class CPModel
    {

        public int SequenceId { set; get; }
        public int OrderId { set; get; }
        public Guid guid { set; get; }
        public String YourName { set; get; }
        public String PhoneNumber { set; get; }
        public String TestPersonName { set; get; }
        public String Age { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Date")]
        public DateTime Appointmentdate { get; set; }

        public List<TestsList> Tests;

    }


    public class cart
    {
        //public string Guid { set; get; }
        public List<TestsList> lisTests { set; get; }
        public Guid  guid{ set; get; }
}

    public class CheckoutProcessModel
    {

        public int CompanyId { set; get; }
        public String CompanyName { set; get; }
        public List<TestsList> Tests;


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Date")]
        public DateTime Appointmentdate { get; set; }


        public String YourName { set; get; }
        public String PhoneNumber { set; get; }

        public String TestPersonName { set; get; }
        public String Age { set; get; }
    }




    public class TestsList
    {
        public int SequenceId { set; get; }
        public int TestId { set; get; }
        public string TestLabImagePath { set; get; }
        public string TestAmount { set; get; }
        public string TestName { set; get; }

        public string CompanyName { set; get; }
        public string PlaceName { set; get; }

        public decimal MRP { set; get; }
        public decimal CustomerPrice { set; get; }

        public int CompanyId { set; get; }
        public int LocationId { set; get; }
        public int BranchId { set; get; }


    }

    public class GOrder
    {
        public string OrderId { set; get; }
        public string OrderedPerson { set; get; }
        public string PhoneNumber { set; get; }
        public string TestPersonName { set; get; }
        public string Age { set; get; }
        public string Appointmentdate { set; get; }
        public string ConfirmStatus { set; get; }
        public string PaymentType { set; get; }

        public  List<OrderModel> OrderItems { set; get; }
    }

    public class OrderModel
    {
        public int OrderId { set; get; }

        public int LocationId { set; get; }
        public string LocationName { set; get; }
        public int TestId { set; get; }
        public string TestName { set; get; }
        public List<TestsList> TestsList { set; get; }
        public string DisplayCentersAmount { set; get; }
        public string Type { set; get; }
        public int CompanyId { set; get; }
        public string CompanyName { set; get; }

        public string orderedPerson { set; get; }
        public string PhoneNumber { set; get; }
        public string TestPersonName { set; get; }
        public string Age { set; get; }
        public int ConfirmStatus { set; get; }
        public string PaymentType { set; get; }
        public string Appointmentdate { set; get; }
        public string MRP { set; get; }

        public string SearchTestName { set; get; }


        public string OldTestId { set; get; }
        public string OldLocationId { set; get; }
        public string NewTestName { set; get; }

        public string FileName { set; get; }
        public string FilePath { set; get; }

        //public int OrderId { set; get; }
        public int VendorId { set; get; }

        public bool divShowOrder { set; get; }
        public bool divFileUpload { set; get; }

        //public List<FileModel> listFiles { set; get; }

    }
    //public class FileModel
    //{
    //    public string OrderId { set; get; }
    //    public string FileName { set; get; }
    //    public string FilePath { set; get; }


    //}


    public class TestDisplayModel
    {
        public int OrderId { set; get; }
        public int TestId { set; get; }
        public int LocationId { set; get; }
        public String TestName { set; get; }
        public String CompanyName { set; get; }

        public String Amount { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Date")]
        public DateTime Appointmentdate { get; set; }


        public String YourName { set; get; }
        public String PhoneNumber { set; get; }

        public String TestPersonName { set; get; }
        public String Age { set; get; }


    }

    public class PaymentModel
    {
        public int OrderId { set; get; }
        public int PaymentId { set; get; }
        public string TestPersonName { set; get; }
        public int TestId { set; get; }
        public string TestName { set; get; }
        public int PlaceId { set; get; }
        public string PlaceName { set; get; }
        public string Appointmentdate { set; get; }
        public decimal Total { set; get; }
        public decimal FinalPrice { set; get; }

        public List<TestsList> TestsList { set; get; }

    }

    public enum PaymentType
    { PayTM = 1, CitrusPay = 2, PayYouMoney = 3, CashAtCenter = 4 };


}
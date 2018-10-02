using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NLT.Models
{
    public class MasterModel
    {
    }

    public class MenuViewModel
    {
        public int MenuID { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool IsAction { get; set; }
        public string Link { get; set; }
        public string Class { get; set; }
        public List<MenuViewModel> SubMenu { get; set; }
        //public string SubMenu { get; set; }
    }

    public class Mobiles
    {
        public int? slno { get; set; }
        public string MobileName { get; set; }
        public double Price { get; set; }
        public string Url { get; set; }
        public string ZoomUrl { get; set; }
        public string Description { get; set; }

    }


    public class DepartmentModel
    {
        public int DepartmentId { set; get; }

        [Required(ErrorMessage = "Department Name Required")]
        public string DepartmentName { set; get; }
    }
    public class SpecimenModel
    {
        public int SpecimenId { set; get; }

        [Required(ErrorMessage = "Specimen Name Required")]
        public string SpecimenName { set; get; }
    }

    public class StateModel
    {
        public int StateId { set; get; }

        [Required(ErrorMessage = "stateName Required")]
        public string StateName { set; get; }
    }

    public class CityModel
    {

        public int? StateId { set; get; }
        public string StateName { set; get; }
        public int CityId { set; get; }
        [Required(ErrorMessage = "CityName Required")]
        public string CityName { set; get; }

        public string Pincode { set; get; }
        public decimal Latitude { set; get; }
        public decimal Longitude { set; get; }

        public List<SelectListItem> States { set; get; }

    }

    public class PlaceModel
    {

        public int? StateId { set; get; }
        public string StateName { set; get; }
        public int CityId { set; get; }
        public string CityName { set; get; }

        public int PlaceId { set; get; }
        [Required(ErrorMessage = "PlaceName Required")]
        public string PlaceName { set; get; }

        public string Pincode { set; get; }
        public decimal Latitude { set; get; }
        public decimal Longitude { set; get; }

        public List<SelectListItem> States { set; get; }
        public List<SelectListItem> Citys { set; get; }

    }

    public class CompanyModel
    {
        public int CompanyId { set; get; }
        public string CompanyName { set; get; }
        public string ImagePath { set; get; }
        // public HttpPostedFileWrapper ImageFile { set; get; }

        //[Required(ErrorMessage = "Please select file.")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase File { get; set; }

        public string Remarks { set; get; }

    }

   
    public class CheckOutProcessModel
    {
        public int CompanyId { set; get; }
        public string CompanyName { set; get; }
        public List<TestModel> Tests { get; set; }
        
    }


    public class TestModel
    {
        public int TestId { set; get; }
        public string TestName { set; get; }
        public string DisplayName { set; get; }

        public int DepartmentId { set; get; }
        public string DepartmentName { set; get; }
        public List<SelectListItem> Department { set; get; }

        public int SpecimenId { set; get; }
        public string SpecimenName { set; get; }
        public List<SelectListItem> Specimen { set; get; }

        public int ApplicableTo { set; get; }
        public List<SelectListItem> Applicable { set; get; }

        public int ReportingDays { set; get; }
        public bool HouseVisit { set; get; }
        public bool Nabl { set; get; }
        public bool ActiveStatus { set; get; }
        
        public string Remarks { set; get; }

    }



    public class TestLocationModel
    {
        public int MapId { set; get; }

        public int TestId { set; get; }
        public string TestName { set; get; }
        public List<SelectListItem> Tests { set; get; }

        public int CompanyId { set; get; }
        public string CompanyName { set; get; }
        public List<SelectListItem> Companys { set; get; }

        public int LocationId { set; get; }
        public string LocationName { set; get; }
        public List<SelectListItem> Locations { set; get; }

        public decimal price { set; get; }
        public decimal CustomerPrice { set; get; }
        public decimal CustomerDiscount { set; get; }
        public int CustomerDiscountTypeId { set; get; }
        public List<SelectListItem> CustomerDiscountTypes { set; get; }

        public bool CustomerDiscountApplay { set; get; }

        public bool  ActiveStatus { set; get; }

        public string Remarks { set; get; }

    }


    public class BranchModel
    {
        public int BranchId { set; get; }
        public string BranchName { set; get; }

        public int CompanyId { set; get; }    
        public string CompanyName { set; get; }
        public int PleaseId { set; get; }
        public string PlaceName { set; get; }

        public List<SelectListItem> Companys { set; get; }
        public List<SelectListItem> Places { set; get; }

        public string Address { set; get; }
        public string Phone1 { set; get; }
        public string Phone2 { set; get; }
        public string ContactPerson { set; get; }
        public string PersonalMobile { set; get; }
        public string Latitude { set; get; }
        public string Longitude { set; get; }
        public int WorkingHourseFrom { set; get; }
        public int WorkingHourseTo { set; get; }

        public bool SundayisHoliday { set; get; }
        public bool IsNABL { set; get; }
        public bool IsWheelChair { set; get; }
        public bool IsHomeCollection { set; get; }
        public bool IsParkingAvailable { set; get; }

        public bool IsCreditCardAccet { set; get; }
        public bool IsMoneyBack { set; get; }
        public bool IsOnlinePayment { set; get; }
        public string FirstImage { set; get; }
        public string SecoundImage { set; get; }
        public string ThirdImage { set; get; }

        public List<DepartmentService> DepartmentService { set; get; }

        public List<SelectListItem> Departmetnts {set; get; }
        public int DepartmentId { set; get; }
        public string DepartmentName { set; get; }
        public string FromTime { set; get; }
        public string ToTime { set; get; }


    }


    public class DepartmentService
    {
        public int Id { set; get; }
        public int DepartmentId { set; get; }
        public string DepartmentName { set; get; }
        public string FromTime { set; get; }
        public string ToTime { set; get; }
    }
    public class SchemeDetailsModel
    {
        public int SchemeDetailsId { set; get; }
        public int SchemeId { set; get; }
        public int CompanyId { set; get; }
        public int BranchId { set; get; }
        public int DepartmentId { set; get; }

   
        public string SchemeName { set; get; }
        public string CompanyName { set; get; }
        public string BranchName { set; get; }
        public string DepartmentName { set; get; }

        public List<TestPriceModel> TestPrices { set; get; }


    }


    //public class TestPriceModel
    //{
    //    //public int Id { set; get; }
    //    public int TestId { set; get; }
    //   // public string TestName { set; get; }
    //    public decimal MRP { set; get; }
    //    //public decimal AgrigateRate { set; get; }
    //    //public decimal CustomerDiscountPrice { set; get; }
    //    //public decimal Total { set; get; }

    //}

    public class SchemeModel
    {
        public int SchemeDetailsId { set; get; }
        public int SchemeId { set; get; }
        public string SchemeName { set; get; }
        public int CompanyId { set; get; }
        public int BranchId { set; get; }
        public int DepartmentId { set; get; }

        public string CompanyName { set; get; }
        public string BranchName { set; get; }
        public string DepartmentName { set; get; }

        public List<SelectListItem> Schemes { set; get; }
        public List<SelectListItem> Companys { set; get; }
        public List<SelectListItem> Branches { set; get; }
        public List<SelectListItem> Departments { set; get; }

        public List<TestPriceModel> TestPrices { set; get; }

        //public TestPriceModel NewTestPrices { set; get; }
        //public bool ShowTestProceEdit { set; get; }


        //public int TestRowId { set; get; }
        //public int TestId { set; get; }
        //public string TestName { set; get; }
        //public decimal MRP { set; get; }
        //public decimal AgrigateRate { set; get; }
        //public decimal CustomerDiscountPrice { set; get; }
        //public decimal Total { set; get; }
    }


    public class TestPriceModel
    {
        public int Id { set; get; }
        public int TestId { set; get; }
        public string TestName { set; get; }
        public decimal MRP { set; get; }
        public decimal AgrigateRate { set; get; }
        public int CustomerDiscountType { set; get; }
        public decimal CustomerDiscountPrice { set; get; }
        public decimal Total { set; get; }
        public List<SelectListItem> DiscountTypes { set; get; }
    }


    public class Test
    {
        public int Id { set; get; }
        public string Name { set; get; }

    }




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NLT.Models;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace NLT.Controllers
{
    public class MasterController : Controller
    {
      

        BAL objBal = new BAL();
        ResponseModel objResponseModel = new ResponseModel();
        StateModel objStateModel = new StateModel();
        CityModel objCityModel = new CityModel();
        PlaceModel objPlaceModel = new PlaceModel();
        CompanyModel objCompanyModel = new CompanyModel();
        TestModel objTestModel = new TestModel();
        TestLocationModel objTestLocationModel = new TestLocationModel();
        DepartmentModel objDepartmentModel = new DepartmentModel();
        SpecimenModel objSpecimenModel = new SpecimenModel();
         SchemeModel objSchemeModel = new SchemeModel();




        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Department(int? Id)
        {

            if (Id == null)
            {
                ViewBag.PageHedding = "Create Department";
            }
            else
            {
                int departmentId = 0;
                departmentId = Convert.ToInt32(Id);
                objDepartmentModel = objBal.getDepartment(departmentId);

                ViewBag.PageHedding = "Update Department";

            }

            return View(objDepartmentModel);

        }

        [HttpPost]
        public ActionResult Department(DepartmentModel DepartmentModel)
        {
            if (ModelState.IsValid)
            {

                if (DepartmentModel.DepartmentId != 0)
                {
                    objResponseModel = objBal.saveDepartment(DepartmentModel);

                }
                else
                {
                    DepartmentModel.DepartmentId = 0;
                    objResponseModel = objBal.saveDepartment(DepartmentModel);
                }

                return RedirectToAction("Departments", "Master");

            }
            else
            {
                return RedirectToAction("Departments", "Master");
            }


        }

        public ActionResult Departments()
        {
            List<DepartmentModel> listDepartmentModel = new List<DepartmentModel>();
            listDepartmentModel = objBal.getDepartments(0);

            return View(listDepartmentModel);
        }


        public ActionResult Specimen(int? Id)
        {

            if (Id == null)
            {
                ViewBag.PageHedding = "Create Specimen";
            }
            else
            {
                int SpecimenId = 0;
                SpecimenId = Convert.ToInt32(Id);
                objSpecimenModel = objBal.getSpecimen(SpecimenId);

                ViewBag.PageHedding = "Update Specimen";

            }

            return View(objSpecimenModel);

        }

        [HttpPost]
        public ActionResult Specimen(SpecimenModel specimenModel)
        {
            if (ModelState.IsValid)
            {

                if (specimenModel.SpecimenId != 0)
                {
                    objResponseModel = objBal.saveSpecimen(specimenModel);

                }
                else
                {
                    specimenModel.SpecimenId = 0;
                    objResponseModel = objBal.saveSpecimen(specimenModel);
                }

                return RedirectToAction("Specimens", "Master");

            }
            else
            {
                return RedirectToAction("Specimens", "Master");
            }


        }

        public ActionResult Specimens()
        {
            List<SpecimenModel> listSpecimenModel = new List<SpecimenModel>();
            listSpecimenModel = objBal.getSpecimens(0);

            return View(listSpecimenModel);
        }



        public ActionResult States()
        {
            List<StateModel> listStateModel = new List<StateModel>();
            listStateModel = objBal.getStates(0);

            return View(listStateModel);
        }
        public ActionResult State(int? Id)
        {

            if (Id == null)
            {
                ViewBag.PageHedding = "Create State";
            }
            else
            {
                int stateId = 0;
                stateId = Convert.ToInt32(Id);
                objStateModel = objBal.getState(stateId);

                ViewBag.PageHedding = "Update State";

            }

            return View(objStateModel);
        }

        [HttpPost]
        public ActionResult State(StateModel StateModel)
        {
            if (ModelState.IsValid)
            {

                if (StateModel.StateId != 0)
                {
                    objResponseModel = objBal.saveState(StateModel);

                }
                else
                {
                    StateModel.StateId = 0;
                    objResponseModel = objBal.saveState(StateModel);
                }

                return RedirectToAction("States", "Master");

            }
            else
            {
                return RedirectToAction("State", "Master");
            }


        }

        public ActionResult Citys()
        {
            List<CityModel> listStateModel = new List<CityModel>();
            listStateModel = objBal.getCitys(0);

            return View(listStateModel);
        }


        public ActionResult City(int? Id)
        {
            if (Id == null)
            {
                ViewBag.PageHedding = "Create City";
                int cityId = 0;
                objCityModel = objBal.getCity(cityId);
            }
            else
            {
                int cityId = 0;
                cityId = Convert.ToInt32(Id);
                objCityModel = objBal.getCity(cityId);

                ViewBag.PageHedding = "Update City";
            }

            return View(objCityModel);
        }


        [HttpPost]
        public ActionResult City(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                if (cityModel.CityId != 0)
                {
                    objResponseModel = objBal.saveCity(cityModel);
                }
                else
                {
                    cityModel.CityId = 0;
                    objResponseModel = objBal.saveCity(cityModel);
                }

                return RedirectToAction("Citys", "Master");
            }
            else
            {
                ViewBag.PageHedding = "Create City";

                cityModel.States = objBal.LoadStates();

                return View(cityModel);
            }
        }

        public ActionResult Pleases()
        {
            List<PlaceModel> listPlaceModel = new List<PlaceModel>();
            listPlaceModel = objBal.getPlaces(0);

            return View(listPlaceModel);

        }



        public ActionResult Please(int? Id)
        {
            ViewBag.Errors = "";

            if (Id == null)
            {
                ViewBag.PageHedding = "Create Place";
                int placeId = 0;
                objPlaceModel = objBal.getPlace(placeId);
            }
            else
            {
                int placeId = 0;
                placeId = Convert.ToInt32(Id);
                objPlaceModel = objBal.getPlace(placeId);

                ViewBag.PageHedding = "Update Place";
            }

            return View(objPlaceModel);
        }

        [HttpPost]
        public ActionResult Please(PlaceModel placeModel)
        {
            if (ModelState.IsValid)
            {
                if (placeModel.PlaceId != 0)
                {
                    objResponseModel = objBal.savePlace(placeModel);


                    if (objResponseModel.StatusCode == 400)
                    {
                        ViewBag.Errors = objResponseModel.ErrorMessage;
                        return View(objResponseModel.Result);

                    }
                    else if (objResponseModel.StatusCode == 200)
                    {
                        ViewBag.Errors = "Saved Sucessfully";

                        return Json(objResponseModel, JsonRequestBehavior.AllowGet);
                    }


                    ViewBag.Errors = "Updated Sucessfully";
                    return RedirectToAction("Pleases", "Master");

                }
                else
                {
                    placeModel.PlaceId = 0;
                    objResponseModel = objBal.savePlace(placeModel);

                    ViewBag.Errors = "";

                    if (objResponseModel.StatusCode == 400)
                    {
                        ViewBag.Errors = objResponseModel.ErrorMessage;
                        /// return View(objResponseModel.Result);

                        placeModel.States = objBal.LoadStates();//  objResponseModel.Result
                        placeModel.States = objBal.LoadCitys(Convert.ToInt32(placeModel.StateId)) ;// 
                       // return View(placeModel);

                        return Json(objResponseModel, JsonRequestBehavior.AllowGet);
                    }
                    else if (objResponseModel.StatusCode == 200)
                    {
                        ViewBag.Errors = "Saved Sucessfully";

                        return Json(objResponseModel, JsonRequestBehavior.AllowGet);
                       
                    }

                    return RedirectToAction("Pleases", "Master");


                }


            }
            else
            {
                return View(placeModel);
            }
        }




        public JsonResult GetCitys(int? StateId)
        {
            int SId = 0;
            SId = Convert.ToInt32(StateId);

            ResponseModel objResponseModel = objBal.getCitysByStateId(SId);


            return Json(objResponseModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Companys()
        {
            List<CompanyModel> listCompanyModel = new List<CompanyModel>();
            listCompanyModel = objBal.getCompanys(0);

            return View(listCompanyModel);
        }

        [HttpGet]
        public ActionResult Company(int? Id)
        {
            if (Id == null)
            {
                ViewBag.PageHedding = "Create Company";
                int CompanyId = 0;
                objCompanyModel = objBal.getCompany(CompanyId);
            }
            else
            {
                int CompanyId = 0;
                CompanyId = Convert.ToInt32(Id);
                objCompanyModel = objBal.getCompany(CompanyId);

                ViewBag.PageHedding = "Update Company";
            }

            return View(objCompanyModel);
        }


        

        [HttpPost]
        public ActionResult Company(CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                if (companyModel.CompanyId != 0)
                {
                    if (companyModel.File != null)
                    {

                        string ImageUploadedPath = "";
                        var file = companyModel.File;
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var extention = Path.GetExtension(file.FileName);
                            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

                            if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
                            {

                                string Path = string.Empty;

                                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/UploadedImages/"));
                                if (!dir.Exists)
                                {
                                    dir.Create();
                                }
                                else
                                {

                                }

                                string Key = DateTime.Now.Ticks.ToString();
                                ImageUploadedPath = "/UploadedImages/" + fileNameWithoutExtension + '_' + Key + extention;

                                file.SaveAs(Server.MapPath(ImageUploadedPath));

                                companyModel.ImagePath = ImageUploadedPath;

                            }
                            else
                            {
                                return Content("<script language='javascript' type='text/javascript'>alert('Please Select .jpg or .jpeg or .png ....'); </script>");

                            }

                        }

                    }
                    else
                    {
                        companyModel.ImagePath = "";
                    }

                    objResponseModel = objBal.saveCompany(companyModel);



                    if (objResponseModel.StatusCode == 400)
                    {
                        ViewBag.Errors = objResponseModel.ErrorMessage;
                        return View(objResponseModel.Result);

                    }
                    else if (objResponseModel.StatusCode == 200)
                    {
                        ViewBag.Errors = "Saved Sucessfully";

                        return Content("<script language='javascript' type='text/javascript'>alert('Saved Sucessfully !'); window.location.href = '/Master/Companys';</script>");

                    }

                    return View(objResponseModel);

                }
                else
                {
                    string ImageUploadedPath = "";
                    var file = companyModel.File;
                    if (file != null)
                    {


                        var fileName = Path.GetFileName(file.FileName);
                        var extention = Path.GetExtension(file.FileName);
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

                        if(extention==".jpg" || extention == ".jpeg" || extention == ".png")
                        {

                            DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/UploadedImages/"));
                            if (!dir.Exists)
                            {
                                dir.Create();
                            }
                            else
                            {

                            }

                            string Key = DateTime.Now.Ticks.ToString();
                            ImageUploadedPath = "/UploadedImages/" + fileNameWithoutExtension + '_' + Key + extention;

                            file.SaveAs(Server.MapPath(ImageUploadedPath));
                        }
                        else
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert('Please Select .jpg or .jpeg or .png ....'); </script>");

                        }

                    }

                    companyModel.CompanyId = 0;
                    companyModel.ImagePath = ImageUploadedPath;
                    objResponseModel = objBal.saveCompany(companyModel);

                    // return RedirectToAction("Companys", "Master");
                    // return Json(objResponseModel, JsonRequestBehavior.AllowGet);

                    if (objResponseModel.StatusCode == 400)
                    {
                        ViewBag.Errors = objResponseModel.ErrorMessage;
                        return View(objResponseModel.Result);

                    }
                    else if (objResponseModel.StatusCode == 200)
                    {
                        ViewBag.Errors = "Saved Sucessfully";

                       // return RedirectToAction("Companys", "Master");

                        return Content("<script language='javascript' type='text/javascript'>alert('Saved Sucessfully !'); window.location.href = '/Master/Companys';</script>");

                        // return Json(objResponseModel, JsonRequestBehavior.AllowGet);

                    }

                    // return Json(objResponseModel, JsonRequestBehavior.AllowGet);

                    return View(objResponseModel);
                }


            }
            else
            {
                return View(objCompanyModel);
            }

            // return View();
        }


        //[HttpPost]
        //public ActionResult Company_1(CompanyModel companyModel)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (companyModel.CompanyId != 0)
        //        {
        //            if(companyModel.ImageFile != null)
        //            {

        //                string ImageUploadedPath = "";
        //                var file = companyModel.ImageFile;
        //                if (file != null)
        //                {
        //                    var fileName = Path.GetFileName(file.FileName);
        //                    var extention = Path.GetExtension(file.FileName);
        //                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

        //                    string Key = DateTime.Now.Ticks.ToString();
        //                    ImageUploadedPath = "/UploadedImages/" + fileNameWithoutExtension + '_' + Key + extention;

        //                    file.SaveAs(Server.MapPath(ImageUploadedPath));

        //                    companyModel.ImagePath = ImageUploadedPath;

        //                }

        //            }
        //            else
        //            {
        //                companyModel.ImagePath = "";
        //            }

        //            objResponseModel = objBal.saveCompany(companyModel);



        //            if (objResponseModel.StatusCode == 400)
        //            {
        //                ViewBag.Errors = objResponseModel.ErrorMessage;
        //                return View(objResponseModel.Result);

        //            }
        //            else if (objResponseModel.StatusCode == 200)
        //            {
        //                ViewBag.Errors = "Saved Sucessfully";

        //                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
        //            }

        //            return Json(objResponseModel, JsonRequestBehavior.AllowGet);

        //        }
        //        else
        //        {
        //            string ImageUploadedPath = "";
        //            var file = companyModel.ImageFile;
        //            if(file != null)
        //            {
        //                var fileName = Path.GetFileName(file.FileName);
        //                var extention = Path.GetExtension(file.FileName);
        //                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

        //               string Key= DateTime.Now.Ticks.ToString();
        //               ImageUploadedPath = "/UploadedImages/" + fileNameWithoutExtension + '_' + Key + extention;

        //               file.SaveAs(Server.MapPath(ImageUploadedPath));             
        //            }

        //            companyModel.CompanyId = 0;
        //            companyModel.ImagePath = ImageUploadedPath;
        //            objResponseModel = objBal.saveCompany(companyModel);

        //           // return RedirectToAction("Companys", "Master");
        //           // return Json(objResponseModel, JsonRequestBehavior.AllowGet);

        //            if (objResponseModel.StatusCode == 400)
        //            {
        //                ViewBag.Errors = objResponseModel.ErrorMessage;
        //                return View(objResponseModel.Result);

        //            }
        //            else if (objResponseModel.StatusCode == 200)
        //            {
        //                ViewBag.Errors = "Saved Sucessfully";

        //                return RedirectToAction("Companys", "Master");

        //               // return Json(objResponseModel, JsonRequestBehavior.AllowGet);

        //            }

        //            return Json(objResponseModel, JsonRequestBehavior.AllowGet);
        //        }


        //    }
        //    else
        //    {
        //        return View(objCompanyModel);
        //    }


        //}


       

        public ActionResult TestMaster(int? Id)
        {

            if (Id == null)
            {
                ViewBag.PageHedding = "Create Test";
                int TestId = 0;
                objTestModel = objBal.getTest(TestId);
            }
            else
            {
                int TestId = 0;
                TestId = Convert.ToInt32(Id);
                objTestModel = objBal.getTest(TestId);

                ViewBag.PageHedding = "Update Test";
            }

            return View(objTestModel);

        }

        [HttpPost]
        public ActionResult TestMaster(TestModel testModel)
        {
            if (ModelState.IsValid)
            {
                if (testModel.TestId != 0)
                {
                    objResponseModel = objBal.saveTest(testModel);
                }
                else
                {
                    testModel.TestId = 0;
                    objResponseModel = objBal.saveTest(testModel);
                }

                return RedirectToAction("Tests", "Master");
            }
            else
            {
                return View(testModel);
            }

        }

        public ActionResult Tests()
        {
            List<TestModel> listTestModel = new List<TestModel>();
            listTestModel = objBal.getTets(0);

            return View(listTestModel);
        }

        public ActionResult MapTestLocation(int? Id)
        {

            if (Id == null)
            {
                ViewBag.PageHedding = "Create Map Test Location ";
                int MapId = 0;
                objTestLocationModel = objBal.getMapTestlocation(MapId);
            }
            else
            {
                int MapId = 0;
                MapId = Convert.ToInt32(Id);
                objTestLocationModel = objBal.getMapTestlocation(MapId);

                ViewBag.PageHedding = "Update Map Test Location";
            }

            return View(objTestLocationModel);

        }
  
        [HttpPost]
        public ActionResult MapTestLocation(TestLocationModel testLocationModel)
        {
            if (ModelState.IsValid)
            {
                if (testLocationModel.MapId != 0)
                {
                    objResponseModel = objBal.saveMapTestLocation(testLocationModel);
                }
                else
                {
                    testLocationModel.MapId = 0;
                    objResponseModel = objBal.saveMapTestLocation(testLocationModel);
                }

                return RedirectToAction("MapTestLocations", "Master");
            }
            else
            {
                return View(testLocationModel);
            }

        }


        public ActionResult MapTestLocations()
        {
            List<TestLocationModel> listTestLocationModel = new List<TestLocationModel>();
            listTestLocationModel = objBal.getMapTestLocations(0);

            return View(listTestLocationModel);
        }


        public ActionResult Branch(int? Id)
        {
            BranchModel objBranchModel = new BranchModel();
              
            try
            {
                if(Id == null)
                {
                    ViewBag.PageHedding = "Create Branch ";

                    int BranchId = 0;
                    objBranchModel = objBal.getBranch(BranchId);
                   
                }
                else
                {
                    ViewBag.PageHedding = "Update Branch";

                    int BranchId = 0;
                    BranchId = Convert.ToInt32(Id);
                    objBranchModel = objBal.getBranch(BranchId); 
                }
                 
            }
            catch (Exception ex)
            {
          
            }

            return View(objBranchModel);
        }

        [HttpPost]
        public ActionResult Branch(BranchModel branchModel)
        {
           
                if (ModelState.IsValid)
                {
                    if (branchModel.BranchId != 0)
                    {
                        objResponseModel = objBal.saveBranch(branchModel);
                    }
                    else
                    {
                        branchModel.BranchId = 0;
                        objResponseModel = objBal.saveBranch(branchModel);
                    }

                    return RedirectToAction("Branches", "Master");
                }
                else
                {
                    return View(branchModel);
                }

           

        }


        public ActionResult Branches()
        {
            List<BranchModel> listBranchModel = new List<BranchModel>();
            listBranchModel = objBal.getBranchs(0);

            return View(listBranchModel);
        }

        public JsonResult DeleteDepartmentService(string BranchId,string ServiceId)
        {
            BranchModel objBranchModel = new BranchModel();

            try
            {
                int DeptServiceId = 0;

                if (BranchId != null && ServiceId !=null)
                {
                   DeptServiceId = Convert.ToInt32(ServiceId);

                  DataTable dt =  objBal.DeleteDepartmentService(DeptServiceId);

                    if(Convert.ToInt32(dt.Rows[0]["Result"])==1)
                    {
                        
                    }

                }
                else
                {
                    DeptServiceId = 0;
                }

               
               /// objBranchModel = objBal.getBranch(Convert.ToInt32(BranchId));

                
            }
            catch (Exception ex)
            {
                
            }

            return Json("Sucess",JsonRequestBehavior.AllowGet);

           // return RedirectToAction("Sucess", "Branch", objBranchModel);

        }


        // Scheme Master
        public ActionResult Scheme(int? Id)
        {
            SchemeModel objSchemeModel = new SchemeModel();

            try
            {
                if (Id == null)
                {
                    ViewBag.PageHedding = "Create Scheme ";

                    int SchemeId = 0;
                    objSchemeModel = objBal.getScheme(SchemeId);

                }
                else
                {
                    ViewBag.PageHedding = "Update Scheme";

                    int SchemeId = 0;
                    SchemeId = Convert.ToInt32(Id);
                    objSchemeModel = objBal.getScheme(SchemeId);
                }

            }
            catch (Exception ex)
            {

               
            }

            return View(objSchemeModel);
        }

        [HttpPost]
        public ActionResult Scheme(SchemeModel SchemeModel)
        {
            if (SchemeModel.SchemeId != 0)
            {
                objResponseModel = objBal.saveScheme(SchemeModel);
            }
            else
            {
                SchemeModel.SchemeId = 0;
                objResponseModel = objBal.saveScheme(SchemeModel);
            }

            if(objResponseModel.StatusCode==200)
            {
                return RedirectToAction("Schemes", "Master");
            }
            else
            {
                return View(SchemeModel);
            }
            
        }
        public ActionResult Schemes()
        {
            List<SchemeModel> listSchemeModel = new List<SchemeModel>();
            listSchemeModel = objBal.getSchemes(0);

            return View(listSchemeModel);
        }


        // Scheme Details
        public ActionResult SchemeDetails(int? Id)
        {
            SchemeModel objSchemeModel = new SchemeModel();

            try
            {
                if (Id == null)
                {
                    ViewBag.PageHedding = "Create Scheme Details ";

                    int SchemeDetailsId = 0;
                    objSchemeModel = objBal.getSchemeDetails(SchemeDetailsId);

                }
                else
                {
                    ViewBag.PageHedding = "Update Scheme Details";

                    int SchemeDetailsId = 0;
                    SchemeDetailsId = Convert.ToInt32(Id);
                    objSchemeModel = objBal.getSchemeDetails(SchemeDetailsId);
                }

            }
            catch (Exception ex)
            {


            }

            return View(objSchemeModel);
        }

        [HttpPost]
        public ActionResult SchemeDetails(SchemeModel SchemeModel, FormCollection form, string btnpost)
        {
            try
            {
                // SchemeModel objSchemeModel = new SchemeModel();
                if (btnpost == "Go")
                {

                    if (SchemeModel.SchemeDetailsId > 0)
                    {
                        // ViewBag.PageHedding = "Create Scheme ";

                        int SchemeId = SchemeModel.SchemeId;
                        objSchemeModel = objBal.getSchemeWhenButtonClick(SchemeModel, btnpost);

                    }
                    else
                    {
                        ViewBag.PageHedding = "Update Scheme";

                        //int SchemeId = 0;
                        //SchemeId = Convert.ToInt32(SchemeModel.SchemeId);
                        objSchemeModel = objBal.getSchemeWhenButtonClick(SchemeModel, btnpost);


                    }

                    return View(objSchemeModel);
                }
                //else if (btnpost == "btnSaveTestPrice")
                //{

                //    string str2 = "btnSaveTestPrice";


                //    if (SchemeModel.SchemeId != 0)
                //    {
                //        objResponseModel = objBal.saveScheme(SchemeModel);
                //    }
                //    else
                //    {
                //        SchemeModel.SchemeId = 0;
                //        objResponseModel = objBal.saveScheme(SchemeModel);
                //    }
                //}


            }
            catch (Exception ex)
            {

               
            }

            return View(objSchemeModel);

        }

       // public JsonResult saveSchemeDetails(int? CompanyId)
        public JsonResult saveSchemeDetails(SchemeDetailsModel SchemeDetailsModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {

                string Res = "";

                objResponseModel = objBal.saveSchemeDetails(SchemeDetailsModel);

                if(objResponseModel.StatusCode == 200)
                {

                }
                else
                {

                }


            }
            catch (Exception ex)
            {

               
            }

            return Json(objResponseModel, JsonRequestBehavior.AllowGet);

        }


        public ActionResult SchemeMaster(int? Id)
        {
            SchemeModel objSchemeModel = new SchemeModel();

            try
            {
                if (Id == null)
                {
                    ViewBag.PageHedding = "Create Scheme ";

                    int SchemeId = 0;
                    objSchemeModel = objBal.getScheme(SchemeId);

                }
                else
                {
                    ViewBag.PageHedding = "Update Scheme";

                    int SchemeId = 0;
                    SchemeId = Convert.ToInt32(Id);
                    objSchemeModel = objBal.getScheme(SchemeId);
                }

            }
            catch (Exception ex)
            {

            }

            return View(objSchemeModel);
        }

        [HttpPost]
        public ActionResult SchemeMaster(SchemeModel SchemeModel,FormCollection form,string btnpost)
        {
            // SchemeModel objSchemeModel = new SchemeModel();
            if (btnpost == "Go")
            {

                if (SchemeModel.SchemeId != 0)
                {
                    // ViewBag.PageHedding = "Create Scheme ";


                    int SchemeId = SchemeModel.SchemeId;
                    objSchemeModel = objBal.getSchemeWhenButtonClick(SchemeModel, btnpost);

                }
                else
                {
                    ViewBag.PageHedding = "Update Scheme";

                    int SchemeId = 0;
                    SchemeId = Convert.ToInt32(SchemeModel.SchemeId);
                    objSchemeModel = objBal.getSchemeWhenButtonClick(SchemeModel, btnpost);


                }

                return View(objSchemeModel);
            }
            else if (btnpost == "btnSaveTestPrice")
            {

                string str2 = "btnSaveTestPrice";


                if (SchemeModel.SchemeId != 0)
                {
                    objResponseModel = objBal.saveScheme(SchemeModel);
                }
                else
                {
                    SchemeModel.SchemeId = 0;
                    objResponseModel = objBal.saveScheme(SchemeModel);
                }



            }



            return View(objSchemeModel);
            


            //switch (submitButton)
            //{
            //    case "Send":
            //        // delegate sending to another controller action
            //        return (Send());
            //    case "Cancel":
            //        // call another action to perform the cancellation
            //        return (Cancel());
            //    default:
            //        // If they've submitted the form without a submitButton, 
            //        // just return the view again.
            //        return (View());
            //}


            //if (ModelState.IsValid)
            //{
            //    if (SchemeModel.SchemeId != 0)
            //    {
            //        objResponseModel = objBal.saveScheme(SchemeModel);
            //    }
            //    else
            //    {
            //        SchemeModel.SchemeId = 0;
            //        objResponseModel = objBal.saveScheme(SchemeModel);
            //    }

            //    return RedirectToAction("Schemes", "Master");
            //}
            //else
            //{
            //    return View(objSchemeModel);
            //}

        }

       


        public JsonResult GetDipartmentsByBranchId(int? BranchId)
        {
            
            try
            {
                int BranchId_ = 0;
                BranchId_ = Convert.ToInt32(BranchId);

                objResponseModel = objBal.getDepartmentsByBranchId(BranchId_);

                return Json(objResponseModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                objResponseModel.StatusCode = 400;
                objResponseModel.ErrorMessage = ex.Message;

                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult GetBranchesByCompanyId(int? CompanyId)
        {
           
            try
            {
                int CompanyId_ = 0;
                CompanyId_ = Convert.ToInt32(CompanyId);

                objResponseModel = objBal.getBranchsByCompanyId(CompanyId_);

                return Json(objResponseModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                objResponseModel.StatusCode = 400;
                objResponseModel.ErrorMessage = ex.Message;

                return Json(objResponseModel, JsonRequestBehavior.AllowGet);
            }


        }


       // public void getSchemeDetailsHeadders()

        public ActionResult SchemeDetailsHeader()
        {
            List<SchemeDetailsModel> objList = new List<SchemeDetailsModel>();
            ViewBag.PageHedding = "Scheme Details Headder";

            try
            {
                objList = objBal.getSchemeDetailsHeadders(0);

            }
            catch (Exception ex)
            {

            }

            return View(objList);

        }

        public ActionResult TestPrices()
        {
            SchemeModel objSchemeModel = new SchemeModel();

            try
            {
                ViewBag.PageHedding = "update Test Prices";

                int SchemeDetailsId = 0;
                objSchemeModel = objBal.getSchemeDetails(SchemeDetailsId);


            }
            catch (Exception ex)
            {

            }

            return View(objSchemeModel);
        }


        [HttpPost]
        public ActionResult TestPrices(SchemeModel SchemeModel, FormCollection form, string btnpost)
        {

            try
            {
                if (btnpost == "Go")
                {
                    ViewBag.PageHedding = "Test Prices";
                    objSchemeModel = objBal.getSchemeWhenButtonClick(SchemeModel, btnpost);

                    return View(objSchemeModel);
                }
               
            }
            catch (Exception ex)
            {


            }

            return View(objSchemeModel);

        }


    }
}
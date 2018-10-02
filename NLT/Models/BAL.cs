using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NLT.Models;
using System.Data;

using System.Web.Mvc;

namespace NLT.Models
{
    public class BAL
    {
        DAL objDal = new DAL();
        ResponseModel objResponseModel = new ResponseModel();



        public bool IsphoneNumberExits(string PhoneNumber)
        {
            bool Result = false;

            try
            {
               Result = objDal.IsphoneNumberExits(PhoneNumber);

            }
            catch (Exception ex)
            {

            }

            return Result;
        }

        public GOrder GOrder(int OrderId)
        {
            DataSet dsOrder = new DataSet();
            DataTable dtTest = new DataTable();
            List<OrderModel> objlistOrders = new List<OrderModel>();
            Models.GOrder objGOrder = new Models.GOrder();

            try
            {
                dsOrder = objDal.getOrdersDetails(OrderId);

                if(dsOrder.Tables[0].Rows.Count>0)
                {
                    objGOrder.OrderId = dsOrder.Tables[0].Rows[0]["OrderId"].ToString();
                    objGOrder.OrderedPerson = dsOrder.Tables[0].Rows[0]["OrderedPerson"].ToString();
                    objGOrder.PhoneNumber = dsOrder.Tables[0].Rows[0]["PhoneNumber"].ToString();
                    objGOrder.PaymentType = dsOrder.Tables[0].Rows[0]["PaymentType"].ToString();
                    objGOrder.Appointmentdate = dsOrder.Tables[0].Rows[0]["Appointmentdate"].ToString();
                    objGOrder.ConfirmStatus = dsOrder.Tables[0].Rows[0]["ConfirmStatus"].ToString();
                    objGOrder.PaymentType = dsOrder.Tables[0].Rows[0]["PaymentType"].ToString();

                    objGOrder.TestPersonName = dsOrder.Tables[0].Rows[0]["TestPersonName"].ToString();
                    objGOrder.Age = dsOrder.Tables[0].Rows[0]["Age"].ToString();


                    for (int i = 0; i < dsOrder.Tables[1].Rows.Count; i++)
                    {
                        objlistOrders.Add(new OrderModel { OrderId= Convert.ToInt32(dsOrder.Tables[1].Rows[i]["OrderId"].ToString()), LocationName= dsOrder.Tables[1].Rows[i]["LocationName"].ToString(),CompanyName= dsOrder.Tables[1].Rows[i]["CompanyName"].ToString(),TestName= dsOrder.Tables[1].Rows[i]["TestName"].ToString(),MRP= dsOrder.Tables[1].Rows[i]["MRP"].ToString() });
                    }
                    objGOrder.OrderItems = objlistOrders;

                }

              

            }
            catch (Exception ex)
            {

            }


            return objGOrder;
        }


        public List<OrderModel> getOrders(int OrderId)
        {
            DataTable dtTest = new DataTable();
            List<OrderModel> objlistOrders = new List<OrderModel>();

            try
            {
                dtTest = objDal.getOrders(OrderId);

                for (int i = 0; i < dtTest.Rows.Count; i++)
                {
                    objlistOrders.Add(new OrderModel { OrderId = Convert.ToInt32(dtTest.Rows[i]["OrderId"].ToString()), orderedPerson = dtTest.Rows[i]["OrderedPerson"].ToString(), PhoneNumber = dtTest.Rows[i]["PhoneNumber"].ToString(), PaymentType= dtTest.Rows[i]["PaymentType"].ToString(),Appointmentdate = dtTest.Rows[i]["Appointmentdate"].ToString() });
                }

            }
            catch (Exception ex)
            {

            }


            return objlistOrders;
        }


        public ResponseModel  getLocation(string LocationName)
        {
            DataTable dtL = new DataTable();
            try
            {
                dtL = objDal.getLocation(LocationName);

                if(dtL.Rows.Count>0)
                {
                    objResponseModel.Result = dtL.Rows[0]["LocationId"].ToString();
                    objResponseModel.StatusCode = 200;
                    objResponseModel.ErrorMessage = "";
                }
                else
                {
                    objResponseModel.ErrorMessage = "Location Name Not Valid";
                    objResponseModel.StatusCode = 400;

                }


            }
            catch (Exception ex)
            {

                
            }

            return objResponseModel;
        }
        public ResponseModel getTest(string TestName)
        {
            DataTable dtT = new DataTable();
            try
            {
                dtT = objDal.getTest(TestName);

                if (dtT.Rows.Count > 0)
                {
                    objResponseModel.Result = dtT.Rows[0]["TestId"].ToString();
                    objResponseModel.StatusCode = 200;
                    objResponseModel.ErrorMessage = "";
                }
                else
                {
                    objResponseModel.ErrorMessage = "Test Name Not Valid";
                    objResponseModel.StatusCode = 400;

                }

            }
            catch (Exception ex)
            {
                
            }
            return objResponseModel;
        }


        public ResponseModel userSignup(string PhoneNumber, string OTP, string emailId )
        {
            objResponseModel = new ResponseModel();
            bool Result = false;
            int NewUserId = 0;
            try
            {

                string Errors = objDal.userSignup(PhoneNumber, emailId, OTP, out Result, out NewUserId);

                //DataTable dt = objDal.userSignup(PhoneNumber, emailId, OTP, out Result, out NewUserId);

                if (Result == false)
                {
                    objResponseModel.StatusCode = 400;
                    objResponseModel.ErrorMessage = Errors;

                }
                else
                {
                    objResponseModel.StatusCode = 200;
                    objResponseModel.ErrorMessage = "";
                    objResponseModel.Result = NewUserId;
                }

            }
            catch (Exception ex)
            {
                
            }
            return objResponseModel;

        }

        public List<Test> getTests(string TestName,string CompanyId)
        {
            DataTable dtTest = new DataTable();
            List<Test> objlistTest = new List<Test>();

            try
            {
                dtTest = objDal.getTests(TestName, CompanyId);

                for (int i = 0; i < dtTest.Rows.Count; i++)
                {
                    objlistTest.Add(new Test { Id = Convert.ToInt32(dtTest.Rows[i]["TestId"].ToString()), Name = dtTest.Rows[i]["TestName"].ToString() });
                }


            }
            catch (Exception ex)
            {

            }


            return objlistTest;
        }

        public List<Test> getTests(string TestName)
        {
            DataTable dtTest = new DataTable();
            List<Test> objlistTest = new List<Test>();

            try
            {
                dtTest = objDal.getTests(TestName);

                for(int i=0;i<dtTest.Rows.Count;i++)
                {
                    objlistTest.Add(new Test { Id = Convert.ToInt32(dtTest.Rows[i]["TestId"].ToString()), Name = dtTest.Rows[i]["TestName"].ToString() });
                }

                
            }
            catch (Exception ex)
            {

            }


            return objlistTest;
        }

        public List<LocationModel> getLocations_(string LocationtName)
        {
            DataTable dtTest = new DataTable();
            List<LocationModel> objlistLocation = new List<LocationModel>();

            try
            {
                dtTest = objDal.getLocations_(LocationtName);

                for (int i = 0; i < dtTest.Rows.Count; i++)
                {
                    objlistLocation.Add(new LocationModel { LocationId = Convert.ToInt32(dtTest.Rows[i]["Id"].ToString()), LocationName = dtTest.Rows[i]["Name"].ToString() });
                }


            }
            catch (Exception ex)
            {

            }


            return objlistLocation;
        }


        public ResponseModel save_CP(CPModel cPModel)
        {
            ResponseModel objResponse = new ResponseModel();
            TestDisplayModel objTDM = new TestDisplayModel();

            try
            {

                if (cPModel.YourName == null)
                {
                    cPModel.YourName = "";
                }
                if (cPModel.PhoneNumber == null)
                {
                    cPModel.PhoneNumber = "";
                }
                if (cPModel.TestPersonName == null)
                {
                    cPModel.TestPersonName = "";
                }
                if (cPModel.Age == null)
                {
                    cPModel.Age = "";
                }
                if (cPModel.Appointmentdate == null)
                {
                    DateTime objdate = new DateTime();
                    cPModel.Appointmentdate = objdate;
                }


                DataTable dtResult = new DataTable();

                dtResult = objDal.save_CP(cPModel);

                if (dtResult.Rows.Count > 0)
                {
                    objResponse.StatusCode = 200;
                    objResponse.Result = dtResult.Rows[0]["OrderId"].ToString();
                }

            }
            catch (Exception ex)
            {
            }

            return objResponse;
        }


        public List<TestsList> getTestPrise(List<TestsList> testsList)
        {

            try
            {
                foreach (var a in testsList)
                {
                    //objCheckOutPro.CompanyId = a.CompanyId;
                    //objCheckOutPro.CompanyName = a.CompanyName;
                    //objCheckOutPro.Tests = null;
                    //a.MRP = 100;

                    DataTable dt = new DataTable();
                    dt = objDal.getTestPrise(a.CompanyId, a.LocationId, a.TestId);

                    if (dt.Rows.Count > 0)
                    {



                        if (dt.Rows[0]["MRP"].ToString() == "")
                        {
                            a.MRP = 0;
                        }
                        else
                            a.MRP = Convert.ToDecimal(dt.Rows[0]["MRP"].ToString());

                        if (dt.Rows[0]["CompanyName"].ToString() == "")
                        {
                            a.CompanyName = "";
                        }
                        else
                            a.CompanyName = dt.Rows[0]["CompanyName"].ToString();

                        if (dt.Rows[0]["TestName"].ToString() == "")
                        {
                            a.TestName = "";
                        }
                        else
                            a.TestName = dt.Rows[0]["TestName"].ToString();


                        if (dt.Rows[0]["BranchName"].ToString() == "")
                        {
                            a.PlaceName = "";
                        }
                        else
                            a.PlaceName = dt.Rows[0]["BranchName"].ToString();

                    }

                }



            }
            catch (Exception ex)
            {

            }

            return testsList;
        }

        public ResponseModel saveDepartment(DepartmentModel departmentModel)
        {
            try
            {
                int Result = 0;

                Result = objDal.saveDepartment(departmentModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;

                }
                else
                {

                }
                return objResponseModel;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DepartmentModel getDepartment(int DepartmentId)
        {
            DepartmentModel objDepartment = new DepartmentModel();
            try
            {
                DataSet ds = objDal.getDepartments(DepartmentId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (ds.Tables[0].Rows[i]["Id"].ToString() == "" || ds.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "" || ds.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        }

                        // objList.Add(new StateModel { StateId = Id, StateName = Name });
                        objDepartment.DepartmentId = Id;
                        objDepartment.DepartmentName = Name;

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objDepartment;

        }
        public List<DepartmentModel> getDepartments(int DepartmentId)
        {
            List<DepartmentModel> objList = new List<DepartmentModel>();
            try
            {
                DataSet ds = objDal.getDepartments(DepartmentId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (ds.Tables[0].Rows[i]["Id"].ToString() == "" || ds.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "" || ds.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        }

                        objList.Add(new DepartmentModel { DepartmentId = Id, DepartmentName = Name });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }



        public ResponseModel saveSpecimen(SpecimenModel specimenModel)
        {
            try
            {
                int Result = 0;

                Result = objDal.saveSpecimen(specimenModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;

                }
                else
                {

                }
                return objResponseModel;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public SpecimenModel getSpecimen(int SpecimenId)
        {
            SpecimenModel objSpecimen = new SpecimenModel();
            try
            {
                DataSet ds = objDal.getSpecimens(SpecimenId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (ds.Tables[0].Rows[i]["Id"].ToString() == "" || ds.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "" || ds.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        }

                        // objList.Add(new StateModel { StateId = Id, StateName = Name });
                        objSpecimen.SpecimenId = Id;
                        objSpecimen.SpecimenName = Name;

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objSpecimen;

        }
        public List<SpecimenModel> getSpecimens(int SpecimenId)
        {
            List<SpecimenModel> objList = new List<SpecimenModel>();
            try
            {
                DataSet ds = objDal.getSpecimens(SpecimenId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (ds.Tables[0].Rows[i]["Id"].ToString() == "" || ds.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "" || ds.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        }

                        objList.Add(new SpecimenModel { SpecimenId = Id, SpecimenName = Name });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }

        public ResponseModel saveState(StateModel stateModel)
        {
            try
            {
                int Result = 0;

                Result = objDal.saveState(stateModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;

                }
                else
                {

                }
                return objResponseModel;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StateModel> getStates(int StateId)
        {
            List<StateModel> objList = new List<StateModel>();
            try
            {
                DataSet ds = objDal.getStates(StateId);



                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (ds.Tables[0].Rows[i]["Id"].ToString() == "" || ds.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "" || ds.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        }

                        objList.Add(new StateModel { StateId = Id, StateName = Name });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }

        public StateModel getState(int StateId)
        {
            StateModel objState = new StateModel();
            try
            {
                DataSet ds = objDal.getStates(StateId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (ds.Tables[0].Rows[i]["Id"].ToString() == "" || ds.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "" || ds.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        }

                        // objList.Add(new StateModel { StateId = Id, StateName = Name });
                        objState.StateId = Id;
                        objState.StateName = Name;

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objState;

        }

        public List<SelectListItem> LoadStates()
        {
            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataSet dsStates = objDal.getStates(0);

                if (dsStates.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsStates.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (dsStates.Tables[0].Rows[i]["Id"].ToString() == "" || dsStates.Tables[0].Rows[i]["Id"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dsStates.Tables[0].Rows[i]["Id"].ToString());
                        }

                        if (dsStates.Tables[0].Rows[i]["Name"].ToString() == "" || dsStates.Tables[0].Rows[i]["Name"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dsStates.Tables[0].Rows[i]["Name"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }

        public List<SelectListItem> LoadCitys(int StateId)
        {
            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataSet dsCitys = objDal.getCitysByStateId(StateId);

                if (dsCitys.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsCitys.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";

                        if (dsCitys.Tables[0].Rows[i]["CityId"].ToString() == "" || dsCitys.Tables[0].Rows[i]["CityId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dsCitys.Tables[0].Rows[i]["CityId"].ToString());
                        }

                        if (dsCitys.Tables[0].Rows[i]["CityName"].ToString() == "" || dsCitys.Tables[0].Rows[i]["CityName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dsCitys.Tables[0].Rows[i]["CityName"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }

        public CityModel getCity(int CityId)
        {

            CityModel objCity = new CityModel();
            try
            {
                if (CityId > 0)
                {

                    int Id = 0;
                    string Name = "";
                    string StateName = "";
                    int StateId = 0;

                    string Pincode = "";
                    decimal Latitude = 0;
                    decimal Longitude = 0;


                    DataSet ds = objDal.getCitys(CityId);

                    if (ds.Tables[0].Rows[0]["CityId"].ToString() == "" || ds.Tables[0].Rows[0]["CityId"] == null)
                    {
                        Id = 0;
                    }
                    else
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["CityId"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["CityName"].ToString() == "" || ds.Tables[0].Rows[0]["CityName"] == null)
                    {
                        Name = "";
                    }
                    else
                    {
                        Name = ds.Tables[0].Rows[0]["CityName"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["StateName"].ToString() == "" || ds.Tables[0].Rows[0]["StateName"] == null)
                    {
                        StateName = "";
                    }
                    else
                    {
                        StateName = ds.Tables[0].Rows[0]["StateName"].ToString();
                    }

                    StateId = Convert.ToInt32(ds.Tables[0].Rows[0]["StateId"].ToString());


                    if (ds.Tables[0].Rows[0]["Pincode"].ToString() == "" || ds.Tables[0].Rows[0]["Pincode"] == null)
                    {
                        Pincode = "";
                    }
                    else
                    {
                        Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["Latitude"].ToString() == "" || ds.Tables[0].Rows[0]["Latitude"] == null)
                    {
                        Latitude = 0;
                    }
                    else
                    {
                        Latitude = Convert.ToDecimal(ds.Tables[0].Rows[0]["Latitude"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["Longitude"].ToString() == "" || ds.Tables[0].Rows[0]["Longitude"] == null)
                    {
                        Longitude = 0;
                    }
                    else
                    {
                        Longitude = Convert.ToDecimal(ds.Tables[0].Rows[0]["Longitude"].ToString());
                    }


                    objCity.CityId = CityId;
                    objCity.CityName = Name;
                    objCity.StateId = StateId;
                    objCity.States = LoadStates();

                    objCity.Pincode = Pincode;
                    objCity.Latitude = Latitude;
                    objCity.Longitude = Longitude;

                }
                else
                {
                    objCity.CityId = 0;
                    objCity.StateId = 0;
                    objCity.States = LoadStates();
                    objCity.CityName = "";

                    objCity.Pincode = "";
                    objCity.Latitude = 0;
                    objCity.Longitude = 0;
                }

            }
            catch (Exception ex)
            {

            }

            return objCity;
        }


        public ResponseModel saveCity(CityModel cityModel)
        {
            try
            {
                int Result = 0;

                Result = objDal.saveCity(cityModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;
                }
                else
                {
                    objResponseModel.Result = "Not Saved ";
                    objResponseModel.StatusCode = 400;
                }

            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }


        public List<CityModel> getCitys(int CityId)
        {
            List<CityModel> objList = new List<CityModel>();
            try
            {
                DataSet ds = objDal.getCitys(CityId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        string StateName = "";

                        if (ds.Tables[0].Rows[i]["CityId"].ToString() == "" || ds.Tables[0].Rows[i]["CityId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["CityId"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["CityName"].ToString() == "" || ds.Tables[0].Rows[i]["CityName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["CityName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["StateName"].ToString() == "" || ds.Tables[0].Rows[i]["StateName"] == null)
                        {
                            StateName = "";
                        }
                        else
                        {
                            StateName = ds.Tables[0].Rows[i]["StateName"].ToString();
                        }

                        objList.Add(new CityModel { CityId = Id, CityName = Name, StateName = StateName });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }

        public ResponseModel getCitysByStateId(int StateId)
        {
            ResponseModel objResponseModel = new ResponseModel();

            List<CityModel> objList = new List<CityModel>();
            try
            {
                DataSet ds = objDal.getCitysByStateId(StateId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        string StateName = "";

                        if (ds.Tables[0].Rows[i]["CityId"].ToString() == "" || ds.Tables[0].Rows[i]["CityId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["CityId"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["CityName"].ToString() == "" || ds.Tables[0].Rows[i]["CityName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["CityName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["StateName"].ToString() == "" || ds.Tables[0].Rows[i]["StateName"] == null)
                        {
                            StateName = "";
                        }
                        else
                        {
                            StateName = ds.Tables[0].Rows[i]["StateName"].ToString();
                        }

                        objList.Add(new CityModel { CityId = Id, CityName = Name, StateName = StateName });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            objResponseModel.Result = objList;
            objResponseModel.StatusCode = 200;

            return objResponseModel;

        }


        public PlaceModel getPlace(int PlaceId)
        {

            PlaceModel objPlace = new PlaceModel();
            try
            {
                if (PlaceId > 0)
                {

                    int Id = 0;
                    string Name = "";

                    string Pincode = "";
                    decimal Latitude = 0;
                    decimal Longitude = 0;


                    int StateId = 0;
                    int CityId = 0;

                    DataTable dt = objDal.getPlaces(PlaceId);

                    if (dt.Rows[0]["PlaseId"].ToString() == "" || dt.Rows[0]["PlaseId"] == null)
                    {
                        Id = 0;
                    }
                    else
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["PlaseId"].ToString());
                    }

                    if (dt.Rows[0]["PlaseName"].ToString() == "" || dt.Rows[0]["PlaseName"] == null)
                    {
                        Name = "";
                    }
                    else
                    {
                        Name = dt.Rows[0]["PlaseName"].ToString();
                    }

                    if (dt.Rows[0]["Pincode"].ToString() == "" || dt.Rows[0]["Pincode"] == null)
                    {
                        Pincode = "";
                    }
                    else
                    {
                        Pincode = dt.Rows[0]["Pincode"].ToString();
                    }

                    if (dt.Rows[0]["Latitude"].ToString() == "" || dt.Rows[0]["Latitude"] == null)
                    {
                        Latitude = 0;
                    }
                    else
                    {
                        Latitude = Convert.ToDecimal(dt.Rows[0]["Latitude"].ToString());
                    }

                    if (dt.Rows[0]["Longitude"].ToString() == "" || dt.Rows[0]["Longitude"] == null)
                    {
                        Longitude = 0;
                    }
                    else
                    {
                        Longitude = Convert.ToDecimal(dt.Rows[0]["Longitude"].ToString());
                    }


                    StateId = Convert.ToInt32(dt.Rows[0]["StateId"].ToString());
                    CityId = Convert.ToInt32(dt.Rows[0]["CityId"].ToString());


                    objPlace.PlaceId = Id;
                    objPlace.PlaceName = Name;

                    objPlace.Pincode = Pincode;
                    objPlace.Latitude = Latitude;
                    objPlace.Longitude = Longitude;

                    objPlace.StateId = StateId;
                    objPlace.CityId = CityId;

                    objPlace.States = LoadStates();
                    objPlace.Citys = LoadCitys(StateId);


                }
                else
                {

                    objPlace.CityId = 0;
                    objPlace.CityName = "";

                    objPlace.Pincode = "";
                    objPlace.Latitude = 0;
                    objPlace.Longitude = 0;

                    objPlace.StateId = 0;
                    objPlace.StateName = "";

                    objPlace.PlaceId = 0;
                    objPlace.PlaceName = "";

                    objPlace.States = LoadStates();
                    objPlace.Citys = LoadCitys(0);
                }

            }
            catch (Exception ex)
            {

            }

            return objPlace;
        }

        public ResponseModel savePlace(PlaceModel placeModel)
        {
            try
            {
                int Result = 0;
                int Errors = 0;
                string ErrorMessage = "";

                if (placeModel.PlaceName == "" || placeModel.PlaceName == null)
                {
                    Errors = Errors + 1;
                    ErrorMessage = ErrorMessage + "Please Enter PlaceName \n ";
                }

                if (placeModel.Pincode == "" || placeModel.Pincode == null)
                {
                    Errors = Errors + 1;
                    ErrorMessage = ErrorMessage + "Please Enter Pincode \n ";
                }

                if (placeModel.Latitude < 0)
                {
                    Errors = Errors + 1;
                    ErrorMessage = ErrorMessage + "Please Enter Latitude \n ";
                }

                if (placeModel.Longitude < 0)
                {
                    Errors = Errors + 1;
                    ErrorMessage = ErrorMessage + "Please Enter Longitude \n ";
                }


                if (placeModel.StateId == 0)
                {
                    Errors = Errors + 1;
                    ErrorMessage = ErrorMessage + "Please select State \n";
                }

                if (placeModel.CityId == 0)
                {
                    Errors = Errors + 1;
                    ErrorMessage = ErrorMessage + "Please select City \n";
                }



                if (Errors > 0)
                {
                    placeModel.States = LoadStates();
                    placeModel.Citys = LoadCitys(0);

                    objResponseModel.Result = placeModel;


                    objResponseModel.ErrorMessage = ErrorMessage;
                    objResponseModel.StatusCode = 400;
                }
                else
                {
                    Result = objDal.savePlace(placeModel);

                    if (Result > 0)
                    {
                        objResponseModel.Result = "Saved SucessFully";
                        objResponseModel.StatusCode = 200;
                    }
                    else
                    {

                        objResponseModel.Result = placeModel;
                        objResponseModel.ErrorMessage = "Not Saved "; ;
                        objResponseModel.StatusCode = 400;
                    }
                }



            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }

        public List<PlaceModel> getPlaces(int PlaceId)
        {
            List<PlaceModel> objList = new List<PlaceModel>();
            try
            {
                DataTable dt = objDal.getPlaces(PlaceId);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        string StateName = "";
                        string CityName = "";

                        if (dt.Rows[i]["PlaseId"].ToString() == "" || dt.Rows[i]["PlaseId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["PlaseId"].ToString());
                        }

                        if (dt.Rows[i]["PlaseName"].ToString() == "" || dt.Rows[i]["PlaseName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dt.Rows[i]["PlaseName"].ToString();
                        }



                        if (dt.Rows[i]["CityName"].ToString() == "" || dt.Rows[i]["CityName"] == null)
                        {
                            CityName = "";
                        }
                        else
                        {
                            CityName = dt.Rows[i]["CityName"].ToString();
                        }

                        if (dt.Rows[i]["StateName"].ToString() == "" || dt.Rows[i]["StateName"] == null)
                        {
                            StateName = "";
                        }
                        else
                        {
                            StateName = dt.Rows[i]["StateName"].ToString();
                        }

                        objList.Add(new PlaceModel { PlaceId = Id, PlaceName = Name, StateName = StateName, CityName = CityName });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }


        public ResponseModel saveCompany(CompanyModel companyModel)
        {
            try
            {
                int Result = 0;

                if (companyModel.Remarks == null)
                {
                }

                Result = objDal.saveCompany(companyModel);

                if (Result >= 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;
                }
                else
                {
                    objResponseModel.Result = "Not Saved ";
                    objResponseModel.StatusCode = 400;
                }

            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }

        public List<CompanyModel> getCompanys(int CompanyId)
        {
            List<CompanyModel> objList = new List<CompanyModel>();
            try
            {
                DataTable dt = objDal.getCompanys(CompanyId);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        string Remarks = "";

                        if (dt.Rows[i]["CompanyId"].ToString() == "" || dt.Rows[i]["CompanyId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["CompanyId"].ToString());
                        }

                        if (dt.Rows[i]["CompanyName"].ToString() == "" || dt.Rows[i]["CompanyName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dt.Rows[i]["CompanyName"].ToString();
                        }

                        if (dt.Rows[i]["Remarks"].ToString() == "" || dt.Rows[i]["Remarks"] == null)
                        {
                            Remarks = "";
                        }
                        else
                        {
                            Remarks = dt.Rows[i]["Remarks"].ToString();
                        }

                        objList.Add(new CompanyModel { CompanyId = Id, CompanyName = Name, Remarks = Remarks });
                    }

                }

            }
            catch (Exception ex)
            {

            }

            return objList;
        }


        public CompanyModel getCompany(int CompanyId)
        {

            CompanyModel objCompany = new CompanyModel();
            try
            {
                if (CompanyId > 0)
                {

                    int Id = 0;
                    string Name = "";
                    string ImagePath = "";
                    string Remarks = "";

                    DataTable dt = objDal.getCompanys(CompanyId);

                    if (dt.Rows[0]["CompanyId"].ToString() == "" || dt.Rows[0]["CompanyId"] == null)
                    {
                        Id = 0;
                    }
                    else
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["CompanyId"].ToString());
                    }

                    if (dt.Rows[0]["CompanyName"].ToString() == "" || dt.Rows[0]["CompanyName"] == null)
                    {
                        Name = "";
                    }
                    else
                    {
                        Name = dt.Rows[0]["CompanyName"].ToString();
                    }

                    if (dt.Rows[0]["ImagePath"].ToString() == "" || dt.Rows[0]["ImagePath"] == null)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = dt.Rows[0]["ImagePath"].ToString();
                    }


                    if (dt.Rows[0]["Remarks"].ToString() == "" || dt.Rows[0]["Remarks"] == null)
                    {
                        Remarks = "";
                    }
                    else
                    {
                        Remarks = dt.Rows[0]["Remarks"].ToString();
                    }

                    objCompany.CompanyId = Id;
                    objCompany.CompanyName = Name;
                    objCompany.ImagePath = ImagePath;

                    objCompany.Remarks = Remarks;

                }
                else
                {
                    objCompany.CompanyId = 0;
                    objCompany.CompanyName = "";
                    // objCompany.ImagePath = "/images/concert.jpg";

                    objCompany.Remarks = "";
                }

            }
            catch (Exception ex)
            {

            }

            return objCompany;
        }


        public TestModel getTest(int TestId)
        {

            TestModel objTestModel = new TestModel();
            try
            {
                if (TestId > 0)
                {

                    int Id = 0;
                    string Name = "";
                    string DisplayName = "";
                    int DepartmentId = 0;
                    int SpecimenId = 0;
                    int ApplicableTo = 0;
                    int ReportingDays = 0;
                    bool HouseVisit = false;
                    bool Nabl = false;
                    bool ActiveStatus = false;
                    string Remarks = "";


                    DataSet ds = objDal.getTests(TestId);

                    if (ds.Tables[0].Rows[0]["TestId"].ToString() == "" || ds.Tables[0].Rows[0]["TestId"] == null)
                    {
                        Id = 0;
                    }
                    else
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["TestId"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["TestName"].ToString() == "" || ds.Tables[0].Rows[0]["TestName"] == null)
                    {
                        Name = "";
                    }
                    else
                    {
                        Name = ds.Tables[0].Rows[0]["TestName"].ToString();
                    }



                    if (ds.Tables[0].Rows[0]["DisplayName"].ToString() == "" || ds.Tables[0].Rows[0]["DisplayName"] == null)
                    {
                        DisplayName = "";
                    }
                    else
                    {
                        DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["DepartmentId"].ToString() == "" || ds.Tables[0].Rows[0]["DepartmentId"] == null)
                    {
                        DepartmentId = 0;
                    }
                    else
                    {
                        DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentId"].ToString());
                    }



                    if (ds.Tables[0].Rows[0]["SpecimenId"].ToString() == "" || ds.Tables[0].Rows[0]["SpecimenId"] == null)
                    {
                        SpecimenId = 0;
                    }
                    else
                    {
                        SpecimenId = Convert.ToInt32(ds.Tables[0].Rows[0]["SpecimenId"].ToString());
                    }


                    if (ds.Tables[0].Rows[0]["ApplicableTo"].ToString() == "" || ds.Tables[0].Rows[0]["ApplicableTo"] == null)
                    {
                        ApplicableTo = 0;
                    }
                    else
                    {
                        ApplicableTo = Convert.ToInt32(ds.Tables[0].Rows[0]["ApplicableTo"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["ReportingDays"].ToString() == "" || ds.Tables[0].Rows[0]["ReportingDays"] == null)
                    {
                        ReportingDays = 0;
                    }
                    else
                    {
                        ReportingDays = Convert.ToInt32(ds.Tables[0].Rows[0]["ReportingDays"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["HouseVisit"].ToString() == "" || ds.Tables[0].Rows[0]["HouseVisit"] == null)
                    {
                        HouseVisit = false;
                    }
                    else
                    {
                        HouseVisit = Convert.ToBoolean(ds.Tables[0].Rows[0]["HouseVisit"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["Nabl"].ToString() == "" || ds.Tables[0].Rows[0]["Nabl"] == null)
                    {
                        Nabl = false;
                    }
                    else
                    {
                        Nabl = Convert.ToBoolean(ds.Tables[0].Rows[0]["Nabl"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["ActiveStatus"].ToString() == "" || ds.Tables[0].Rows[0]["ActiveStatus"] == null)
                    {
                        ActiveStatus = false;
                    }
                    else
                    {
                        ActiveStatus = Convert.ToBoolean(ds.Tables[0].Rows[0]["ActiveStatus"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "" || ds.Tables[0].Rows[0]["Remarks"] == null)
                    {
                        Remarks = "";
                    }
                    else
                    {
                        Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    }

                    objTestModel.TestId = Id;
                    objTestModel.TestName = Name;
                    objTestModel.DisplayName = DisplayName;
                    objTestModel.DepartmentId = DepartmentId;
                    objTestModel.Department = loadDepartment();

                    objTestModel.SpecimenId = SpecimenId;
                    objTestModel.Specimen = loadSpecimen();
                    objTestModel.ApplicableTo = ApplicableTo;
                    objTestModel.Applicable = loadApplicable();

                    objTestModel.ReportingDays = ReportingDays;
                    objTestModel.Nabl = Nabl;
                    objTestModel.HouseVisit = HouseVisit;
                    objTestModel.ActiveStatus = ActiveStatus;

                    objTestModel.Remarks = Remarks;

                }
                else
                {
                    objTestModel.TestId = 0;
                    objTestModel.TestName = "";
                    objTestModel.Remarks = "";
                    objTestModel.DisplayName = "";

                    objTestModel.DepartmentId = 0;
                    objTestModel.DepartmentName = "";
                    objTestModel.Department = loadDepartment();

                    objTestModel.SpecimenId = 0;
                    objTestModel.SpecimenName = "";
                    objTestModel.Specimen = loadSpecimen();

                    objTestModel.ApplicableTo = 0;
                    objTestModel.Applicable = loadApplicable();

                    objTestModel.ReportingDays = 0;

                    objTestModel.Nabl = false;
                    objTestModel.HouseVisit = false;
                    objTestModel.ActiveStatus = false;

                    objTestModel.Remarks = "";

                }

            }
            catch (Exception ex)
            {

            }

            return objTestModel;
        }

        public List<SelectListItem> loadSelect()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });
            return objList;
        }



        public List<SelectListItem> loadDepartment()
        {
            DataSet ds = objDal.getDepartments(0);

            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objList.Add(new SelectListItem { Value = ds.Tables[0].Rows[i]["Id"].ToString(), Text = ds.Tables[0].Rows[i]["Name"].ToString() });
                    }


                }

            }
            catch (Exception ex)
            {


            }

            return objList;
        }

        public List<SelectListItem> loadApplicable()
        {

            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "Male", Selected = true });
                objList.Add(new SelectListItem { Value = "1", Text = "Female" });
                objList.Add(new SelectListItem { Value = "2", Text = "Both" });

            }
            catch (Exception ex)
            {


            }

            return objList;
        }

        public List<SelectListItem> loadSpecimen()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            DataSet ds = objDal.getSpecimens(0);
            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objList.Add(new SelectListItem { Value = ds.Tables[0].Rows[i]["Id"].ToString(), Text = ds.Tables[0].Rows[i]["Name"].ToString() });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;
        }

        public ResponseModel saveTest(TestModel testModel)
        {
            try
            {
                int Result = 0;


                if (testModel.Remarks == null)
                {
                    testModel.Remarks = "";
                }

                Result = objDal.saveTest(testModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;
                }
                else
                {
                    objResponseModel.Result = "Not Saved ";
                    objResponseModel.StatusCode = 400;
                }

            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }

        public List<TestModel> getTets(int TestId)
        {
            List<TestModel> objList = new List<TestModel>();
            try
            {
                DataSet ds = objDal.getTests(TestId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";

                        string DisplayName = "";
                        string DepartmentName = "";
                        string SpecimenName = "";

                        int DepartmentId = 0;
                        int SpecimenId = 0;

                        int ApplicableTo = 0;
                        int ReportingDays = 0;
                        bool HouseVisit = false;
                        bool Nabl = false;
                        bool ActiveStatus = false;
                        string Remarks = "";


                        if (ds.Tables[0].Rows[i]["TestId"].ToString() == "" || ds.Tables[0].Rows[i]["TestId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["TestName"].ToString() == "" || ds.Tables[0].Rows[i]["TestName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["TestName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["DisplayName"].ToString() == "" || ds.Tables[0].Rows[i]["DisplayName"] == null)
                        {
                            DisplayName = "";
                        }
                        else
                        {
                            DisplayName = ds.Tables[0].Rows[i]["DisplayName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["DepartmentName"].ToString() == "" || ds.Tables[0].Rows[i]["DepartmentName"] == null)
                        {
                            DepartmentName = "";
                        }
                        else
                        {
                            DepartmentName = ds.Tables[0].Rows[i]["DepartmentName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["SpecimenName"].ToString() == "" || ds.Tables[0].Rows[i]["SpecimenName"] == null)
                        {
                            SpecimenName = "";
                        }
                        else
                        {
                            SpecimenName = ds.Tables[0].Rows[i]["SpecimenName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["DepartmentId"].ToString() == "" || ds.Tables[0].Rows[i]["DepartmentId"] == null)
                        {
                            DepartmentId = 0;
                        }
                        else
                        {
                            DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"].ToString());
                        }


                        if (ds.Tables[0].Rows[i]["SpecimenId"].ToString() == "" || ds.Tables[0].Rows[i]["SpecimenId"] == null)
                        {
                            SpecimenId = 0;
                        }
                        else
                        {
                            SpecimenId = Convert.ToInt32(ds.Tables[0].Rows[i]["SpecimenId"].ToString());
                        }


                        if (ds.Tables[0].Rows[i]["ApplicableTo"].ToString() == "" || ds.Tables[0].Rows[i]["ApplicableTo"] == null)
                        {
                            ApplicableTo = 0;
                        }
                        else
                        {
                            ApplicableTo = Convert.ToInt32(ds.Tables[0].Rows[i]["ApplicableTo"].ToString());
                        }


                        if (ds.Tables[0].Rows[i]["ReportingDays"].ToString() == "" || ds.Tables[0].Rows[i]["ReportingDays"] == null)
                        {
                            ReportingDays = 0;
                        }
                        else
                        {
                            ReportingDays = Convert.ToInt32(ds.Tables[0].Rows[i]["ReportingDays"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["HouseVisit"].ToString() == "" || ds.Tables[0].Rows[i]["HouseVisit"] == null)
                        {
                            HouseVisit = false;
                        }
                        else
                        {
                            HouseVisit = Convert.ToBoolean(ds.Tables[0].Rows[i]["HouseVisit"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Nabl"].ToString() == "" || ds.Tables[0].Rows[i]["Nabl"] == null)
                        {
                            Nabl = false;
                        }
                        else
                        {
                            Nabl = Convert.ToBoolean(ds.Tables[0].Rows[i]["Nabl"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Nabl"].ToString() == "" || ds.Tables[0].Rows[i]["Nabl"] == null)
                        {
                            Nabl = false;
                        }
                        else
                        {
                            Nabl = Convert.ToBoolean(ds.Tables[0].Rows[i]["Nabl"].ToString());
                        }


                        if (ds.Tables[0].Rows[i]["ActiveStatus"].ToString() == "" || ds.Tables[0].Rows[i]["ActiveStatus"] == null)
                        {
                            ActiveStatus = false;
                        }
                        else
                        {
                            ActiveStatus = Convert.ToBoolean(ds.Tables[0].Rows[i]["ActiveStatus"].ToString());
                        }



                        if (ds.Tables[0].Rows[i]["Remarks"].ToString() == "" || ds.Tables[0].Rows[i]["Remarks"] == null)
                        {
                            Remarks = "";
                        }
                        else
                        {
                            Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                        }


                        objList.Add(new TestModel
                        {
                            TestId = Id,
                            TestName = Name,

                            DisplayName = DisplayName,
                            DepartmentName = DepartmentName,
                            SpecimenName = SpecimenName,
                            DepartmentId = DepartmentId,
                            SpecimenId = SpecimenId,
                            ApplicableTo = ApplicableTo,
                            ReportingDays = ReportingDays,
                            HouseVisit = HouseVisit,
                            Nabl = Nabl,
                            ActiveStatus = ActiveStatus,
                            Remarks = Remarks,

                        });


                    }

                }

            }
            catch (Exception ex)
            {

            }

            return objList;
        }

        public TestLocationModel getMapTestlocation(int Id)
        {
            TestLocationModel objTLM = new TestLocationModel();

            try
            {
                if (Id > 0)
                {

                    DataSet ds = objDal.getMapTestLocation(Id); //MapId

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int MapId = 0; //MapID
                            int TestId = 0;
                            string TestName = "";
                            int CompanyId = 0;
                            string CompanyName = "";
                            int LocationId = 0;
                            string LocationName = "";

                            decimal price = 0;
                            decimal CustomerPrice = 0;
                            decimal CustomerDiscount = 0;

                            int CustomerDiscountTypeId = 0;
                            bool CustomerDiscountApplay = false;

                            bool ActiveStatus = false;
                            string Remarks = "";

                            if (ds.Tables[0].Rows[i]["MapId"].ToString() == "" || ds.Tables[0].Rows[i]["MapId"] == null)
                            {
                                MapId = 0;
                            }
                            else
                            {
                                MapId = Convert.ToInt32(ds.Tables[0].Rows[i]["MapId"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["CompanyId"].ToString() == "" || ds.Tables[0].Rows[i]["CompanyId"] == null)
                            {
                                CompanyId = 0;
                            }
                            else
                            {
                                CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["TestId"].ToString() == "" || ds.Tables[0].Rows[i]["TestId"] == null)
                            {
                                TestId = 0;
                            }
                            else
                            {
                                TestId = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["LocationId"].ToString() == "" || ds.Tables[0].Rows[i]["LocationId"] == null)
                            {
                                LocationId = 0;
                            }
                            else
                            {
                                LocationId = Convert.ToInt32(ds.Tables[0].Rows[i]["LocationId"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["price"].ToString() == "" || ds.Tables[0].Rows[i]["price"] == null)
                            {
                                price = 0;
                            }
                            else
                            {
                                price = Convert.ToDecimal(ds.Tables[0].Rows[i]["price"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["ActiveStatus"].ToString() == "" || ds.Tables[0].Rows[i]["ActiveStatus"] == null)
                            {
                                ActiveStatus = false;
                            }
                            else
                            {
                                ActiveStatus = Convert.ToBoolean(ds.Tables[0].Rows[i]["ActiveStatus"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["DiscountApplay"].ToString() == "" || ds.Tables[0].Rows[i]["DiscountApplay"] == null)
                            {
                                CustomerDiscountApplay = false;
                            }
                            else
                            {
                                CustomerDiscountApplay = Convert.ToBoolean(ds.Tables[0].Rows[i]["DiscountApplay"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["CustomerDiscountType"].ToString() == "" || ds.Tables[0].Rows[i]["CustomerDiscountType"] == null)
                            {
                                CustomerDiscountTypeId = 0;
                            }
                            else
                            {
                                CustomerDiscountTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerDiscountType"].ToString());
                            }



                            if (ds.Tables[0].Rows[i]["CustomerDiscount"].ToString() == "" || ds.Tables[0].Rows[i]["CustomerDiscount"] == null)
                            {
                                CustomerDiscount = 0;
                            }
                            else
                            {
                                CustomerDiscount = Convert.ToDecimal(ds.Tables[0].Rows[i]["CustomerDiscount"].ToString());
                            }

                            if (ds.Tables[0].Rows[i]["Remarks"].ToString() == "" || ds.Tables[0].Rows[i]["Remarks"] == null)
                            {
                                Remarks = "";
                            }
                            else
                            {
                                Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                            }

                            objTLM.MapId = MapId; //MapID
                            objTLM.TestId = TestId;
                            objTLM.CompanyId = CompanyId;
                            objTLM.LocationId = LocationId;
                            objTLM.price = price;
                            objTLM.CustomerDiscountApplay = CustomerDiscountApplay;
                            objTLM.ActiveStatus = ActiveStatus;

                            objTLM.CustomerPrice = CustomerPrice;
                            objTLM.CustomerDiscount = CustomerDiscount;
                            objTLM.CustomerDiscountTypeId = CustomerDiscountTypeId;

                            objTLM.Remarks = Remarks;

                            objTLM.Locations = loadLocations();
                            objTLM.Companys = loadCompanys();
                            objTLM.Tests = loadTests();
                            objTLM.CustomerDiscountTypes = loadDiscountTypes();


                        }


                    }


                }
                else
                {
                    objTLM.LocationId = 0;
                    objTLM.LocationName = "";
                    objTLM.Locations = loadLocations();

                    objTLM.CompanyId = 0;
                    objTLM.CompanyName = "";
                    objTLM.Companys = loadCompanys();

                    objTLM.TestId = 0;
                    objTLM.TestName = "";
                    objTLM.Tests = loadTests();

                    objTLM.CustomerDiscountTypeId = 0;
                    objTLM.CustomerDiscountTypes = loadDiscountTypes();

                    objTLM.CustomerPrice = 0;
                    objTLM.CustomerDiscount = 0;

                    objTLM.CustomerDiscountApplay = false;
                    objTLM.ActiveStatus = true;

                    objTLM.Remarks = "";

                }

            }
            catch (Exception ex)
            {

            }
            return objTLM;

        }

        public List<SelectListItem> loadLocations()
        {

            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataTable dt = objDal.getPlaces(0);

                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";

                        if (dt.Rows[i]["PlaseId"].ToString() == "" || dt.Rows[i]["PlaseId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["PlaseId"].ToString());
                        }

                        if (dt.Rows[i]["PlaseName"].ToString() == "" || dt.Rows[i]["PlaseName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dt.Rows[i]["PlaseName"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });


                    }

                }

            }
            catch (Exception ex)
            {


            }

            return objList;

        }

        public List<SelectListItem> loadTests()
        {

            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataSet ds = objDal.getTests(0);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";

                        if (ds.Tables[0].Rows[i]["TestId"].ToString() == "" || ds.Tables[0].Rows[i]["TestId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["TestName"].ToString() == "" || ds.Tables[0].Rows[i]["TestName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = ds.Tables[0].Rows[i]["TestName"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });


                    }

                }

            }
            catch (Exception ex)
            {


            }

            return objList;
        }

        public List<SelectListItem> loadCompanys()
        {

            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataTable dt = objDal.getCompanys(0);

                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";

                        if (dt.Rows[i]["CompanyId"].ToString() == "" || dt.Rows[i]["CompanyId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["CompanyId"].ToString());
                        }

                        if (dt.Rows[i]["CompanyName"].ToString() == "" || dt.Rows[i]["CompanyName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dt.Rows[i]["CompanyName"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });


                    }

                }

            }
            catch (Exception ex)
            {


            }

            return objList;

        }




        public ResponseModel saveMapTestLocation(TestLocationModel testLocationModel)
        {
            try
            {
                int Result = 0;


                if (testLocationModel.Remarks == null)
                {
                    testLocationModel.Remarks = "";

                }


                Result = objDal.saveMapTestLocation(testLocationModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;
                }
                else
                {
                    objResponseModel.Result = "Not Saved ";
                    objResponseModel.StatusCode = 400;
                }

            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }


        public List<TestLocationModel> getMapTestLocations(int MapId)
        {
            List<TestLocationModel> objList = new List<TestLocationModel>();
            try
            {
                DataSet ds = objDal.getMapTestLocation(MapId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Id = 0; //MapID
                        int TestId = 0;
                        string TestName = "";
                        int CompanyId = 0;
                        string CompanyName = "";
                        int LocationId = 0;
                        string LocationName = "";

                        decimal price = 0;
                        decimal CustomerPrice = 0;
                        decimal CustomerDiscount = 0;

                        int CustomerDiscountTypeId = 0;
                        bool CustomerDiscountApplay = false;

                        bool ActiveStatus = false;
                        string Remarks = "";

                        if (ds.Tables[0].Rows[i]["MapId"].ToString() == "" || ds.Tables[0].Rows[i]["MapId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[i]["MapId"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["CompanyName"].ToString() == "" || ds.Tables[0].Rows[i]["CompanyName"] == null)
                        {
                            CompanyName = "";
                        }
                        else
                        {
                            CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["TestName"].ToString() == "" || ds.Tables[0].Rows[i]["TestName"] == null)
                        {
                            TestName = "";
                        }
                        else
                        {
                            TestName = ds.Tables[0].Rows[i]["TestName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["LocationName"].ToString() == "" || ds.Tables[0].Rows[i]["LocationName"] == null)
                        {
                            LocationName = "";
                        }
                        else
                        {
                            LocationName = ds.Tables[0].Rows[i]["LocationName"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["price"].ToString() == "" || ds.Tables[0].Rows[i]["price"] == null)
                        {
                            price = 0;
                        }
                        else
                        {
                            price = Convert.ToDecimal(ds.Tables[0].Rows[i]["price"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["ActiveStatus"].ToString() == "" || ds.Tables[0].Rows[i]["ActiveStatus"] == null)
                        {
                            ActiveStatus = false;
                        }
                        else
                        {
                            ActiveStatus = Convert.ToBoolean(ds.Tables[0].Rows[i]["ActiveStatus"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["Remarks"].ToString() == "" || ds.Tables[0].Rows[i]["Remarks"] == null)
                        {
                            Remarks = "";
                        }
                        else
                        {
                            Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                        }

                        objList.Add(new TestLocationModel
                        {
                            MapId = Id, //MapID
                            TestName = TestName,
                            CompanyName = CompanyName,
                            LocationName = LocationName,
                            price = price,
                            ActiveStatus = ActiveStatus,
                            Remarks = Remarks,

                        });


                    }

                }

            }
            catch (Exception ex)
            {

            }

            return objList;
        }


        public BranchModel getBranch(int BranchId)
        {
            BranchModel objBranchModel = new BranchModel();
            DataSet ds = new DataSet();
            try
            {
                if (BranchId > 0)
                {

                    ds = objDal.getBranches(BranchId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            objBranchModel.BranchId = BranchId;
                            objBranchModel.BranchName = ds.Tables[0].Rows[i]["BranchName"].ToString();
                            objBranchModel.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                            objBranchModel.PleaseId = Convert.ToInt32(ds.Tables[0].Rows[i]["PleaseId"].ToString());

                            objBranchModel.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                            objBranchModel.Phone1 = ds.Tables[0].Rows[i]["Phone1"].ToString();
                            objBranchModel.Phone2 = ds.Tables[0].Rows[i]["Phone2"].ToString();
                            objBranchModel.ContactPerson = ds.Tables[0].Rows[i]["ContactPerson"].ToString();
                            objBranchModel.PersonalMobile = ds.Tables[0].Rows[i]["PersonalMobile"].ToString();
                            objBranchModel.Latitude = ds.Tables[0].Rows[i]["Latitude"].ToString();
                            objBranchModel.Longitude = ds.Tables[0].Rows[i]["Longitude"].ToString();
                            objBranchModel.WorkingHourseFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["WorkingHourseFrom"].ToString());
                            objBranchModel.WorkingHourseTo = Convert.ToInt32(ds.Tables[0].Rows[i]["WorkingHourseTo"].ToString());
                            objBranchModel.SundayisHoliday = Convert.ToBoolean(ds.Tables[0].Rows[i]["SundayisHoliday"].ToString());

                            objBranchModel.IsNABL = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsNABL"].ToString());
                            objBranchModel.IsWheelChair = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsWheelChair"].ToString());
                            objBranchModel.IsHomeCollection = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsHomeCollection"].ToString());
                            objBranchModel.IsParkingAvailable = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsParkingAvailable"].ToString());
                            objBranchModel.IsCreditCardAccet = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsCreditCardAccet"].ToString());
                            objBranchModel.IsMoneyBack = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsMoneyBack"].ToString());
                            objBranchModel.IsOnlinePayment = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsOnlinePayment"].ToString());
                            objBranchModel.FirstImage = ds.Tables[0].Rows[i]["FirstImage"].ToString();
                            objBranchModel.SecoundImage = ds.Tables[0].Rows[i]["SecoundImage"].ToString();
                            objBranchModel.ThirdImage = ds.Tables[0].Rows[i]["ThirdImage"].ToString();


                        }

                        objBranchModel.Companys = LoadCompanys();
                        objBranchModel.Places = LoadPlaces();
                        objBranchModel.DepartmentService = LoadDepartmentService(BranchId);

                        objBranchModel.Departmetnts = loadDepartment();
                        objBranchModel.DepartmentId = 0;
                        objBranchModel.DepartmentName = "";
                        objBranchModel.FromTime = "";
                        objBranchModel.ToTime = "";
                    }

                }
                else
                {
                    objBranchModel.BranchId = 0;
                    objBranchModel.BranchName = "";
                    objBranchModel.CompanyId = 0;
                    objBranchModel.Companys = LoadCompanys();
                    objBranchModel.PleaseId = 0;
                    objBranchModel.Places = LoadPlaces();
                    objBranchModel.DepartmentService = LoadDepartmentService(0);

                    objBranchModel.Departmetnts = loadDepartment();
                    objBranchModel.DepartmentId = 0;
                    objBranchModel.DepartmentName = "";
                    objBranchModel.FromTime = "";
                    objBranchModel.ToTime = "";
                }

            }
            catch (Exception ex)
            {

            }

            return objBranchModel;

        }

        public List<DepartmentService> LoadDepartmentService(int BranchId)
        {
            List<DepartmentService> objList = new List<Models.DepartmentService>();

            try
            {
                DataTable dt = objDal.getDepartmentService(BranchId);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objList.Add(new DepartmentService { Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString()), DepartmentId = Convert.ToInt32(dt.Rows[i]["DepartmentId"].ToString()), DepartmentName = dt.Rows[i]["DepartmentName"].ToString(), FromTime = dt.Rows[i]["FromTime"].ToString(), ToTime = dt.Rows[i]["ToTime"].ToString() });
                    }
                }


            }
            catch (Exception ex)
            {


            }


            return objList;
        }

        public List<SelectListItem> LoadCompanys()
        {
            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataTable dtCompany = objDal.getCompanys(0);

                if (dtCompany.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCompany.Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (dtCompany.Rows[i]["CompanyId"].ToString() == "" || dtCompany.Rows[i]["CompanyId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dtCompany.Rows[i]["CompanyId"].ToString());
                        }

                        if (dtCompany.Rows[i]["CompanyName"].ToString() == "" || dtCompany.Rows[i]["CompanyName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dtCompany.Rows[i]["CompanyName"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }
        public List<SelectListItem> LoadPlaces()
        {
            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });

                DataTable dtPlace = objDal.getPlaces(0);

                if (dtPlace.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPlace.Rows.Count; i++)
                    {
                        int Id = 0;
                        string Name = "";
                        if (dtPlace.Rows[i]["PlaseId"].ToString() == "" || dtPlace.Rows[i]["PlaseId"] == null)
                        {
                            Id = 0;
                        }
                        else
                        {
                            Id = Convert.ToInt32(dtPlace.Rows[i]["PlaseId"].ToString());
                        }

                        if (dtPlace.Rows[i]["PlaseName"].ToString() == "" || dtPlace.Rows[i]["PlaseName"] == null)
                        {
                            Name = "";
                        }
                        else
                        {
                            Name = dtPlace.Rows[i]["PlaseName"].ToString();
                        }

                        objList.Add(new SelectListItem { Value = Id.ToString(), Text = Name });

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return objList;

        }

        public List<BranchModel> getBranchs(int BranchId)
        {
            List<BranchModel> objBranchModelList = new List<BranchModel>();

            try
            {
                if (BranchId == 0)
                {

                    DataSet ds = objDal.getBranches(BranchId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {


                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BranchModel objBranchModel = new BranchModel();

                            objBranchModel.BranchId = Convert.ToInt32(ds.Tables[0].Rows[i]["BranchId"].ToString());
                            objBranchModel.BranchName = ds.Tables[0].Rows[i]["BranchName"].ToString();
                            objBranchModel.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                            objBranchModel.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                            objBranchModel.PleaseId = Convert.ToInt32(ds.Tables[0].Rows[i]["PleaseId"].ToString());
                            objBranchModel.PlaceName = ds.Tables[0].Rows[i]["PlaceName"].ToString();
                            objBranchModel.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                            objBranchModel.Phone1 = ds.Tables[0].Rows[i]["Phone1"].ToString();

                            //objBranchModel.Departmetnts = loadDepartment();
                            //objBranchModel.DepartmentId = 0;
                            //objBranchModel.DepartmentName = "";
                            //objBranchModel.FromTime = "";
                            //objBranchModel.ToTime = "";

                            objBranchModelList.Add(objBranchModel);

                        }


                    }

                }


            }
            catch (Exception ex)
            {

            }

            return objBranchModelList;

        }



        public ResponseModel saveBranch(BranchModel branchModel)
        {
            try
            {
                int Result = 0;

                if (branchModel.BranchName == null)
                {
                    branchModel.BranchName = "";
                }
                if (branchModel.Address == null)
                {
                    branchModel.Address = "";
                }
                if (branchModel.Phone1 == null)
                {
                    branchModel.Phone1 = "";
                }
                if (branchModel.Phone2 == null)
                {
                    branchModel.Phone2 = "";
                }
                if (branchModel.ContactPerson == null)
                {
                    branchModel.ContactPerson = "";
                }
                if (branchModel.PersonalMobile == null)
                {
                    branchModel.PersonalMobile = "";
                }
                if (branchModel.Latitude == null)
                {
                    branchModel.Latitude = "";
                }
                if (branchModel.Longitude == null)
                {
                    branchModel.Longitude = "";
                }

                if (branchModel.FirstImage == null)
                {
                    branchModel.FirstImage = "";
                }
                if (branchModel.SecoundImage == null)
                {
                    branchModel.SecoundImage = "";
                }
                if (branchModel.ThirdImage == null)
                {
                    branchModel.ThirdImage = "";
                }

                if (branchModel.FromTime == null)
                {
                    branchModel.FromTime = "";
                }
                if (branchModel.ToTime == null)
                {
                    branchModel.ToTime = "";
                }

                Result = objDal.saveBranch(branchModel);

                if (Result > 1)
                {
                    objResponseModel.Result = "Saved SucessFully";
                    objResponseModel.StatusCode = 200;
                }
                else
                {
                    objResponseModel.Result = "Not Saved ";
                    objResponseModel.StatusCode = 400;
                }

            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }

        public DataTable DeleteDepartmentService(int ServiceId)
        {
            return objDal.DeleteDepartmentService(ServiceId);
        }




        // Client FrientEnd


        public List<SelectListItem> getLocations()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                DataTable dtLocation = objDal.getLocations();

                objList.Add(new SelectListItem { Text = "-Select-", Value = "0" });

                if (dtLocation.Rows.Count > 0)
                {


                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        int LocationId = 0;
                        string LocationName = "";

                        if (dtLocation.Rows[i]["PlaseId"].ToString() == null || dtLocation.Rows[i]["PlaseId"].ToString() == "")
                        {
                            LocationId = 0;
                        }
                        else
                        {
                            LocationId = Convert.ToInt32(dtLocation.Rows[i]["PlaseId"].ToString());
                        }

                        if (dtLocation.Rows[i]["PlaseName"].ToString() == null || dtLocation.Rows[i]["PlaseName"].ToString() == "")
                        {
                            LocationName = "";
                        }
                        else
                        {
                            LocationName = dtLocation.Rows[i]["PlaseName"].ToString();
                        }

                        objList.Add(new SelectListItem { Text = LocationName, Value = LocationId.ToString() });
                    }


                }


            }
            catch (Exception ex)
            {

            }

            return objList;
        }

        public List<TestModel> getTestsByLocationId(int locationId)
        {
            List<TestModel> objListTestModel = new List<TestModel>();

            try
            {
                if (locationId > 0)
                {
                    DataTable dtTests = objDal.getTestsByLocationId(locationId);

                    if (dtTests.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTests.Rows.Count; i++)
                        {
                            objListTestModel.Add(new TestModel { TestId = Convert.ToInt32(dtTests.Rows[i]["TestId"].ToString()), TestName = dtTests.Rows[i]["TestName"].ToString() });
                        }

                    }


                }

            }
            catch (Exception ex)
            {

            }

            return objListTestModel;
        }

        public OrderModel getTestsByLocationAndTestName(int LocationId, int CompanyId, int TestId, string Type_)
        {
            OrderModel objOrderModel = new OrderModel();

            try
            {
                string DisplayName = "";
                string TestName = "";

                List<TestsList> objlistTestsList = new List<TestsList>();

                objOrderModel.LocationId = LocationId;
                objOrderModel.TestId = TestId;

                if (LocationId != 0 && TestId != 0)
                {
                    DataSet ds = objDal.getTestsByLocationAndTestName(LocationId, CompanyId, TestId, Type_);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //objlistTestsList.Add(new TestsList { BranchId = Convert.ToInt32(ds.Tables[0].Rows[i]["BranchId"].ToString()), TestId = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString()), TestName = ds.Tables[0].Rows[i]["TestName"].ToString(), TestAmount = ds.Tables[0].Rows[i]["Amount"].ToString(), TestLabImagePath = "http://svadla95-001-site1.gtempurl.com/" + ds.Tables[0].Rows[i]["ImagePath"].ToString(), CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString(), PlaceName = ds.Tables[0].Rows[i]["PlaceName"].ToString(), MRP = Convert.ToDecimal(ds.Tables[0].Rows[i]["MRP"].ToString()), CustomerPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["CustPrice"].ToString()), CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString()) });
                            objlistTestsList.Add(new TestsList { BranchId = Convert.ToInt32(ds.Tables[0].Rows[i]["BranchId"].ToString()), TestId = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString()), TestName = ds.Tables[0].Rows[i]["TestName"].ToString(), TestAmount = ds.Tables[0].Rows[i]["Amount"].ToString(), TestLabImagePath = ds.Tables[0].Rows[i]["ImagePath"].ToString(), CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString(), PlaceName = ds.Tables[0].Rows[i]["PlaceName"].ToString(), MRP = Convert.ToDecimal(ds.Tables[0].Rows[i]["MRP"].ToString()), CustomerPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["CustPrice"].ToString()), CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString()) });

                            //  "http://svadla95-001-site1.gtempurl.com/" +
                        }
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DisplayName = ds.Tables[1].Rows[0]["DisplayCentersTestAmount"].ToString();

                        TestName = ds.Tables[1].Rows[0]["TestName"].ToString();

                    }

                }
                else
                {


                }

                objOrderModel.TestName = TestName;
                objOrderModel.DisplayCentersAmount = DisplayName;
                objOrderModel.TestsList = objlistTestsList;

            }
            catch (Exception ex)
            {

            }

            return objOrderModel;

        }

        public OrderModel getTestsByLocationAndTestName(int LocationId, int TestId, string Type_)
        {
            OrderModel objOrderModel = new OrderModel();

            try
            {
                string DisplayName = "";
                string TestName = "";

                List<TestsList> objlistTestsList = new List<TestsList>();

                objOrderModel.LocationId = LocationId;
                objOrderModel.TestId = TestId;

                if (LocationId != 0 && TestId != 0)
                {
                    DataSet ds = objDal.getTestsByLocationAndTestName(LocationId, TestId, Type_);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //objlistTestsList.Add(new TestsList { BranchId = Convert.ToInt32(ds.Tables[0].Rows[i]["BranchId"].ToString()), TestId = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString()), TestName = ds.Tables[0].Rows[i]["TestName"].ToString(), TestAmount = ds.Tables[0].Rows[i]["Amount"].ToString(), TestLabImagePath = "http://svadla95-001-site1.gtempurl.com/" + ds.Tables[0].Rows[i]["ImagePath"].ToString(), CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString(), PlaceName = ds.Tables[0].Rows[i]["PlaceName"].ToString(), MRP = Convert.ToDecimal(ds.Tables[0].Rows[i]["MRP"].ToString()), CustomerPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["CustPrice"].ToString()), CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString()) });
                            objlistTestsList.Add(new TestsList { BranchId = Convert.ToInt32(ds.Tables[0].Rows[i]["BranchId"].ToString()), TestId = Convert.ToInt32(ds.Tables[0].Rows[i]["TestId"].ToString()), TestName = ds.Tables[0].Rows[i]["TestName"].ToString(), TestAmount = ds.Tables[0].Rows[i]["Amount"].ToString(), TestLabImagePath = ds.Tables[0].Rows[i]["ImagePath"].ToString(), CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString(), PlaceName = ds.Tables[0].Rows[i]["PlaceName"].ToString(), MRP = Convert.ToDecimal(ds.Tables[0].Rows[i]["MRP"].ToString()), CustomerPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["CustPrice"].ToString()), CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString()) });

                            //  "http://svadla95-001-site1.gtempurl.com/" +
                        }
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DisplayName = ds.Tables[1].Rows[0]["DisplayCentersTestAmount"].ToString();

                        TestName = ds.Tables[1].Rows[0]["TestName"].ToString();

                    }

                }
                else
                {


                }

                objOrderModel.TestName = TestName;
                objOrderModel.DisplayCentersAmount = DisplayName;
                objOrderModel.TestsList = objlistTestsList;

            }
            catch (Exception ex)
            {

            }

            return objOrderModel;

        }


        public TestDisplayModel getTestDetails(int LocationId, int TestId, int CmpId)
        {
            TestDisplayModel objTModel = new TestDisplayModel();
            try
            {

                DataTable dt = objDal.getTestDetails(LocationId, TestId, CmpId);

                if (dt.Rows.Count > 0)
                {
                    objTModel.LocationId = LocationId;
                    objTModel.TestId = TestId;
                    objTModel.CompanyName = dt.Rows[0]["companyName"].ToString();
                    objTModel.TestName = dt.Rows[0]["TestName"].ToString();
                    objTModel.Amount = dt.Rows[0]["Amount"].ToString();
                    objTModel.Appointmentdate = DateTime.Now;
                }

            }
            catch (Exception ex)
            {

            }

            return objTModel;
        }

        public ResponseModel saveTestOrder(TestDisplayModel TestDisplayModel)
        {
            ResponseModel objResponse = new ResponseModel();

            try
            {
                if (TestDisplayModel.YourName == null)
                {
                    TestDisplayModel.YourName = "";
                }
                if (TestDisplayModel.PhoneNumber == null)
                {
                    TestDisplayModel.PhoneNumber = "";
                }
                if (TestDisplayModel.TestPersonName == null)
                {
                    TestDisplayModel.TestPersonName = "";
                }
                if (TestDisplayModel.Age == null)
                {
                    TestDisplayModel.Age = "";
                }
                if (TestDisplayModel.Appointmentdate == null)
                {
                    DateTime objdate = new DateTime();
                    TestDisplayModel.Appointmentdate = objdate;
                }

                DataTable dtResult = new DataTable();

                dtResult = objDal.saveTestOrder(TestDisplayModel);

                if (dtResult.Rows.Count > 0)
                {
                    objResponse.StatusCode = 200;
                    objResponse.Result = dtResult.Rows[0]["OrderId"].ToString();
                }

            }
            catch (Exception ex)
            {
            }

            return objResponse;
        }


        public ResponseModel Delete_Order_Item(int orderId, int orderItemId)
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                bool Result = objDal.Delete_Order_Item(orderId, orderItemId);

                if(Result==true)
                {
                    objResponseModel.StatusCode = 200;
                    objResponseModel.Result = orderId;

                }
                else
                {
                    objResponseModel.StatusCode = 400;
                    objResponseModel.Result = orderId;
                    objResponseModel.ErrorMessage = "Order Item Not Deleted";
                }

            }
            catch (Exception ex)
            {

            }

            return objResponseModel;
        }


        public CPModel getCPTestOrder(int TestOrderId)
        {
            CPModel objCPModel = new CPModel();
            List<TestsList> objTestList = new List<TestsList>();

            try
            {
                if (TestOrderId != 0)
                {
                    DataSet ds = objDal.getCPTestOrder(TestOrderId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objCPModel.TestPersonName = ds.Tables[0].Rows[0]["TestPersonName"].ToString();
                        objCPModel.YourName = ds.Tables[0].Rows[0]["YourName"].ToString();
                        objCPModel.PhoneNumber = "";
                        objCPModel.Age = "";
                        objCPModel.Appointmentdate = Convert.ToDateTime(DateTime.Now);

                        objCPModel.Tests = objTestList;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            objTestList.Add(new TestsList { SequenceId = Convert.ToInt32(ds.Tables[1].Rows[i]["SequenceId"].ToString()), CompanyName = ds.Tables[1].Rows[i]["Company"].ToString(), TestName = ds.Tables[1].Rows[i]["TestName"].ToString(), MRP = Convert.ToDecimal(ds.Tables[1].Rows[i]["MRP"].ToString()) });
                        }

                        objCPModel.Tests = objTestList;
                    }

                }

            }
            catch (Exception ex)
            {

            }

            return objCPModel;
        }

        public PaymentModel getTestOrder(int TestOrderId)
        {
            PaymentModel objPaymentModel = new PaymentModel();
            try
            {
                if (TestOrderId != 0)
                {
                    DataTable dt = objDal.getTestOrder(TestOrderId);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objPaymentModel.OrderId = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                            objPaymentModel.TestPersonName = dt.Rows[i]["TestPersonName"].ToString();
                            objPaymentModel.PlaceName = dt.Rows[i]["PlaceName"].ToString();
                            objPaymentModel.Appointmentdate = dt.Rows[i]["Appointmentdate"].ToString();
                            objPaymentModel.TestName = dt.Rows[i]["TestName"].ToString();
                            objPaymentModel.Total = Convert.ToDecimal(dt.Rows[i]["TotalPrice"].ToString());
                            objPaymentModel.FinalPrice = Convert.ToDecimal(dt.Rows[i]["FinalPrice"].ToString());
                        }
                    }



                }
                else
                {


                }



            }
            catch (Exception ex)
            {

            }
            return objPaymentModel;
        }


        public ResponseModel confirmTestOrder(int OrderId, int PaymentType)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                DataTable dt = objDal.confirmTestOrder(OrderId, PaymentType);

                if (dt.Rows[0]["ResultStatus"].ToString() == "1")
                {
                    objResponseModel.StatusCode = 200;
                    objResponseModel.ErrorMessage = "";

                    objResponseModel.Result = dt.Rows[0]["OrderId"].ToString();
                }

                if (dt.Rows[0]["ResultStatus"].ToString() == "0")
                {
                    objResponseModel.StatusCode = 400;
                    objResponseModel.ErrorMessage = "";
                    objResponseModel.Result = dt.Rows[0]["Result"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            return objResponseModel;
        }


        // Scheme Master
        public SchemeModel getScheme(int schemeId_)
        {
            SchemeModel objSchemeModel = new SchemeModel();

            try
            {
                if (schemeId_ > 0)
                {

                    int SchemeId = 0;
                    string SchemeName = "";

                    DataSet ds = objDal.getSchemes(schemeId_);

                    if (ds.Tables[0].Rows[0]["SchemeId"].ToString() == "" || ds.Tables[0].Rows[0]["SchemeId"] == null)
                    {
                        SchemeId = 0;
                    }
                    else
                    {
                        SchemeId = Convert.ToInt32(ds.Tables[0].Rows[0]["SchemeId"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["SchemeName"].ToString() == "" || ds.Tables[0].Rows[0]["SchemeName"] == null)
                    {
                        SchemeName = "";
                    }
                    else
                    {
                        SchemeName = ds.Tables[0].Rows[0]["SchemeName"].ToString();
                    }

                    objSchemeModel.SchemeId = SchemeId;
                    objSchemeModel.SchemeName = SchemeName;

                }
                else
                {
                    objSchemeModel.SchemeId = 0;
                    objSchemeModel.SchemeName = "";

                }

            }
            catch (Exception ex)
            {

            }

            return objSchemeModel;
        }
        public ResponseModel saveScheme(SchemeModel SchemeModel)
        {
            ResponseModel objResponse = new ResponseModel();

            try
            {
                if (SchemeModel.SchemeName == null)
                {
                    SchemeModel.SchemeName = "";
                }

                DataTable dtResult = new DataTable();

                dtResult = objDal.saveScheme(SchemeModel);

                if (dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows[0]["Result"].ToString() == "0")
                    {
                        objResponse.StatusCode = 400;
                        objResponse.Result = dtResult.Rows[0]["Result"].ToString();
                    }
                    else if (dtResult.Rows[0]["Result"].ToString() == "1")
                    {
                        objResponse.StatusCode = 200;
                        objResponse.Result = dtResult.Rows[0]["Result"].ToString();
                    }


                }

            }
            catch (Exception ex)
            {
            }

            return objResponse;
        }
        public List<SchemeModel> getSchemes(int SchemeId)
        {
            List<SchemeModel> objSchemeModelList = new List<SchemeModel>();

            try
            {
                if (SchemeId == 0)
                {

                    DataSet ds = objDal.getSchemes(SchemeId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            SchemeModel objSchemeModel = new SchemeModel();

                            objSchemeModel.SchemeId = Convert.ToInt32(ds.Tables[0].Rows[i]["SchemeId"].ToString());
                            objSchemeModel.SchemeName = ds.Tables[0].Rows[i]["SchemeName"].ToString();
                            //objSchemeModel.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                            //objSchemeModel.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                            //objSchemeModel.DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"].ToString());
                            //objSchemeModel.DepartmentName = ds.Tables[0].Rows[i]["DepartmentName"].ToString();

                            objSchemeModelList.Add(objSchemeModel);

                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }

            return objSchemeModelList;

        }



        public SchemeModel getSchemeWhenButtonClick(SchemeModel objSchemeModel, string btnpost)
        {
            List<TestPriceModel> objTestPriceModellist = new List<TestPriceModel>();
            try
            {
                if (btnpost == "Go")
                {

                    DataTable dt = objDal.getTestsForScheme(objSchemeModel.SchemeDetailsId, objSchemeModel.CompanyId, objSchemeModel.BranchId, objSchemeModel.DepartmentId);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objTestPriceModellist.Add(new TestPriceModel { Id = 0, TestId = Convert.ToInt32(dt.Rows[i]["TestId"].ToString()), TestName = dt.Rows[i]["TestName"].ToString(), MRP = Convert.ToDecimal(dt.Rows[i]["MRP"].ToString()), AgrigateRate = Convert.ToDecimal(dt.Rows[i]["AgrigateRate"].ToString()), CustomerDiscountType = Convert.ToInt32(dt.Rows[i]["CustomerDiscountType"].ToString()), CustomerDiscountPrice = Convert.ToDecimal(dt.Rows[i]["CustomerDiscountPrice"].ToString()), Total = Convert.ToDecimal(dt.Rows[i]["Total"].ToString()), DiscountTypes = loadDiscountTypes() });
                        }


                        //if (objSchemeModel.SchemeId > 0)
                        //{
                        //    objSchemeModel.ShowTestProceEdit = true;
                        //}
                        //else
                        //{
                        //    objSchemeModel.ShowTestProceEdit = true;
                        //    objSchemeModel.TestRowId = 0;
                        //    objSchemeModel.TestId = 0;
                        //    objSchemeModel.TestName = "";
                        //    objSchemeModel.MRP = 0;
                        //    objSchemeModel.AgrigateRate = 0;
                        //    objSchemeModel.CustomerDiscountPrice = 0;
                        //    objSchemeModel.Total = 0;
                        //}
                    }

                    objSchemeModel.TestPrices = objTestPriceModellist;


                    //if (objSchemeModel.CompanyId > 0)
                    //{
                    //    objSchemeModel.Companys = loadCompanys();
                    //}
                    //if (objSchemeModel.BranchId > 0)
                    //{
                    //    objSchemeModel.Branches = loadBranches(objSchemeModel.CompanyId);
                    //}
                    //if (objSchemeModel.DepartmentId > 0)
                    //{
                    //    objSchemeModel.Departments = loadDepartments(objSchemeModel.BranchId);
                    //}
                    objSchemeModel.Companys = loadCompanys();
                    objSchemeModel.Schemes = loadSchemes(0);

                    if (objSchemeModel.CompanyId > 0)
                    {
                        objSchemeModel.Branches = loadBranches(objSchemeModel.CompanyId);
                    }
                    else
                    {
                        objSchemeModel.Branches = loadBranches(0);
                    }

                    if (objSchemeModel.BranchId > 0)
                    {
                        objSchemeModel.Departments = loadDepartments(objSchemeModel.BranchId);
                    }
                    else
                    {
                        objSchemeModel.Departments = loadDepartments(0);
                    }




                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            return objSchemeModel;

        }


        public SchemeModel getSchemeDetails(int schemeDetailsId)
        {
            SchemeModel objSchemeModel = new SchemeModel();
            // TestModel objTestModel = new TestModel();
            try
            {

                if (schemeDetailsId > 0)
                {
                    int SchemeDetailsId = 0;
                    int SchemeId = 0;
                    string SchemeName = "";

                    int CompanyId = 0;
                    int BranchId = 0;
                    int DepartmentId = 0;

                    DataSet ds = objDal.getSchemeDetails(schemeDetailsId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["SchemeDetailsId"].ToString() == "" || ds.Tables[0].Rows[0]["SchemeDetailsId"] == null)
                        {
                            SchemeDetailsId = 0;
                        }
                        else
                        {
                            SchemeDetailsId = Convert.ToInt32(ds.Tables[0].Rows[0]["SchemeDetailsId"].ToString());
                        }


                        if (ds.Tables[0].Rows[0]["SchemeId"].ToString() == "" || ds.Tables[0].Rows[0]["SchemeId"] == null)
                        {
                            SchemeId = 0;
                        }
                        else
                        {
                            SchemeId = Convert.ToInt32(ds.Tables[0].Rows[0]["SchemeId"].ToString());
                        }

                        //if (ds.Tables[0].Rows[0]["SchemeName"].ToString() == "" || ds.Tables[0].Rows[0]["SchemeName"] == null)
                        //{
                        //    SchemeName = "";
                        //}
                        //else
                        //{
                        //    SchemeName = ds.Tables[0].Rows[0]["SchemeName"].ToString();
                        //}

                        if (ds.Tables[0].Rows[0]["BranchId"].ToString() == "" || ds.Tables[0].Rows[0]["BranchId"] == null)
                        {
                            BranchId = 0;
                        }
                        else
                        {
                            BranchId = Convert.ToInt32(ds.Tables[0].Rows[0]["BranchId"].ToString());
                        }

                        if (ds.Tables[0].Rows[0]["CompanyId"].ToString() == "" || ds.Tables[0].Rows[0]["CompanyId"] == null)
                        {
                            CompanyId = 0;
                        }
                        else
                        {
                            CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString());
                        }

                        if (ds.Tables[0].Rows[0]["DepartmentId"].ToString() == "" || ds.Tables[0].Rows[0]["DepartmentId"] == null)
                        {
                            DepartmentId = 0;
                        }
                        else
                        {
                            DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentId"].ToString());
                        }

                        objSchemeModel.SchemeDetailsId = SchemeDetailsId;
                        objSchemeModel.SchemeId = SchemeId;
                        objSchemeModel.SchemeName = SchemeName;

                        objSchemeModel.CompanyId = CompanyId;
                        objSchemeModel.BranchId = BranchId;
                        objSchemeModel.DepartmentId = DepartmentId;


                        /// objSchemeModel.Departments = loadDepartment();
                        // objSchemeModel.Companys = loadCompanys();
                        // objSchemeModel.Branches =  loadSelect();

                        //objSchemeModel.Schemes = loadSchemes(0);
                        //objSchemeModel.Departments = loadSelect();
                        //objSchemeModel.Companys = loadCompanys();
                        //objSchemeModel.Branches = loadSelect();



                        objSchemeModel.Companys = loadCompanys();
                        objSchemeModel.Schemes = loadSchemes(0);

                        if (objSchemeModel.CompanyId > 0)
                        {
                            objSchemeModel.Branches = loadBranches(objSchemeModel.CompanyId);
                        }
                        else
                        {
                            objSchemeModel.Branches = loadBranches(0);
                        }

                        if (objSchemeModel.BranchId > 0)
                        {
                            objSchemeModel.Departments = loadDepartments(objSchemeModel.BranchId);
                        }
                        else
                        {
                            objSchemeModel.Departments = loadDepartments(0);
                        }
                    }

                    List<TestPriceModel> objTestPriceModel = new List<TestPriceModel>();
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            objTestPriceModel.Add(new TestPriceModel { Id = Convert.ToInt32(ds.Tables[1].Rows[i]["TestPriceId"].ToString()), TestId = Convert.ToInt32(ds.Tables[1].Rows[i]["TestId"].ToString()), TestName = ds.Tables[1].Rows[i]["TestName"].ToString(), MRP = Convert.ToDecimal(ds.Tables[1].Rows[i]["MRP"].ToString()), AgrigateRate = Convert.ToDecimal(ds.Tables[1].Rows[i]["AgrigateRate"].ToString()), CustomerDiscountType = Convert.ToInt32(ds.Tables[1].Rows[i]["CustomerDiscountType"].ToString()), CustomerDiscountPrice = Convert.ToDecimal(ds.Tables[1].Rows[i]["CustomerDiscountPrice"].ToString()), Total = Convert.ToDecimal(ds.Tables[1].Rows[i]["Total"].ToString()), DiscountTypes = loadDiscountTypes() });
                        }

                    }

                    objSchemeModel.TestPrices = objTestPriceModel;

                }
                else
                {

                    objSchemeModel.SchemeDetailsId = 0;
                    objSchemeModel.SchemeId = 0;
                    objSchemeModel.SchemeName = "";
                    objSchemeModel.DepartmentId = 0;
                    objSchemeModel.CompanyId = 0;

                    objSchemeModel.Schemes = loadSchemes(0);
                    objSchemeModel.Departments = loadSelect();
                    objSchemeModel.Companys = loadCompanys();
                    objSchemeModel.Branches = loadSelect();

                    //objSchemeModel.NewTestPrices = new TestPriceModel();
                    //objSchemeModel.ShowTestProceEdit = false;
                    //objSchemeModel.TestRowId = 0;
                    //objSchemeModel.TestId = 0;
                    //objSchemeModel.TestName = "";
                    //objSchemeModel.MRP = 0;
                    //objSchemeModel.AgrigateRate = 0;
                    //objSchemeModel.CustomerDiscountPrice = 0;
                    //objSchemeModel.Total = 0;


                }

            }
            catch (Exception ex)
            {

            }

            return objSchemeModel;
        }



        public List<SelectListItem> loadSchemes(int SchemeId)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();

            DataSet ds = objDal.getSchemes(SchemeId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    objlist.Add(new SelectListItem { Value = ds.Tables[0].Rows[i]["SchemeId"].ToString(), Text = ds.Tables[0].Rows[i]["SchemeName"].ToString() });
                }
            }
            else
            {
                objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });
            }

            return objlist;
        }

        public List<SelectListItem> loadBranches(int CompanyId)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();

            if (CompanyId > 0)
            {

                DataTable dt = objDal.getBranchsByCompanyId(CompanyId);

                if (dt.Rows.Count > 0)
                {
                    objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objlist.Add(new SelectListItem { Value = dt.Rows[i]["BranchId"].ToString(), Text = dt.Rows[i]["BranchName"].ToString() });

                    }
                }
                else
                {
                    objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });
                }

            }

            return objlist;

        }

        public List<SelectListItem> loadDepartments(int BranchId)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();

            if (BranchId > 0)
            {

                DataTable dt = objDal.getDepartmentsByBranchId(BranchId);

                if (dt.Rows.Count > 0)
                {
                    objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objlist.Add(new SelectListItem { Value = dt.Rows[i]["DepartmentId"].ToString(), Text = dt.Rows[i]["DepartmentName"].ToString() });
                    }
                }
                else
                {
                    objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });

                }

            }
            else
            {
                objlist.Add(new SelectListItem { Value = "0", Text = "-Select-" });

            }

            return objlist;
        }

        public List<SelectListItem> loadDiscountTypes()
        {

            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                //objList.Add(new SelectListItem { Value = "0", Text = "-Select-", Selected = true });
                objList.Add(new SelectListItem { Value = "1", Text = "Amount" });
                objList.Add(new SelectListItem { Value = "2", Text = "Percentage(%)" });
            }
            catch (Exception ex)
            {


            }

            return objList;
        }


        //public ResponseModel getDepartmentByCompanyId(int compantId)
        //{
        //    ResponseModel objResponseModel = new ResponseModel();

        //    List<CityModel> objList = new List<CityModel>();
        //    try
        //    {
        //        DataSet ds = objDal.getDepartmentByCompanyId(compantId);

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                int Id = 0;
        //                string Name = "";
        //                string StateName = "";

        //                if (ds.Tables[0].Rows[i]["CityId"].ToString() == "" || ds.Tables[0].Rows[i]["CityId"] == null)
        //                {
        //                    Id = 0;
        //                }
        //                else
        //                {
        //                    Id = Convert.ToInt32(ds.Tables[0].Rows[i]["CityId"].ToString());
        //                }

        //                if (ds.Tables[0].Rows[i]["CityName"].ToString() == "" || ds.Tables[0].Rows[i]["CityName"] == null)
        //                {
        //                    Name = "";
        //                }
        //                else
        //                {
        //                    Name = ds.Tables[0].Rows[i]["CityName"].ToString();
        //                }

        //                if (ds.Tables[0].Rows[i]["StateName"].ToString() == "" || ds.Tables[0].Rows[i]["StateName"] == null)
        //                {
        //                    StateName = "";
        //                }
        //                else
        //                {
        //                    StateName = ds.Tables[0].Rows[i]["StateName"].ToString();
        //                }

        //                objList.Add(new CityModel { CityId = Id, CityName = Name, StateName = StateName });
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    objResponseModel.Result = objList;
        //    objResponseModel.StatusCode = 200;

        //    return objResponseModel;

        //}

        public ResponseModel getDepartmentsByBranchId(int BranchId)
        {
            List<DepartmentService> objList = new List<Models.DepartmentService>();

            try
            {
                DataTable dt = objDal.getDepartmentsByBranchId(BranchId);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objList.Add(new DepartmentService { DepartmentId = Convert.ToInt32(dt.Rows[i]["DepartmentId"].ToString()), DepartmentName = dt.Rows[i]["DepartmentName"].ToString() });
                    }
                }

                objResponseModel.Result = objList;
                objResponseModel.StatusCode = 200;

            }
            catch (Exception ex)
            {
                objResponseModel.Result = objList;
                objResponseModel.StatusCode = 400;
                objResponseModel.ErrorMessage = ex.Message;
            }

            return objResponseModel;
        }


        public ResponseModel getBranchsByCompanyId(int CompanyId)
        {

            List<BranchModel> objBranchModelList = new List<BranchModel>();

            try
            {
                if (CompanyId > 0)
                {

                    DataTable dt = objDal.getBranchsByCompanyId(CompanyId);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BranchModel objBranchModel = new BranchModel();

                            objBranchModel.BranchId = Convert.ToInt32(dt.Rows[i]["BranchId"].ToString());
                            objBranchModel.BranchName = dt.Rows[i]["BranchName"].ToString();

                            objBranchModelList.Add(objBranchModel);

                        }
                    }

                    objResponseModel.Result = objBranchModelList;
                    objResponseModel.StatusCode = 200;


                }


            }
            catch (Exception ex)
            {
                objResponseModel.Result = objBranchModelList;
                objResponseModel.StatusCode = 400;
                objResponseModel.ErrorMessage = ex.Message;
            }

            return objResponseModel;

        }


        public ResponseModel saveSchemeDetails(SchemeDetailsModel schemeDetailsModel)
        {
            ResponseModel objResponse = new ResponseModel();

            try
            {

                DataTable dtResult = new DataTable();

                dtResult = objDal.saveSchemeDetails(schemeDetailsModel);

                if (dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows[0]["Result"].ToString() == "0")
                    {
                        objResponse.StatusCode = 400;
                        objResponse.Result = dtResult.Rows[0]["Result"].ToString();
                    }
                    else if (dtResult.Rows[0]["Result"].ToString() == "1")
                    {
                        objResponse.StatusCode = 200;
                        objResponse.Result = dtResult.Rows[0]["Result"].ToString();
                    }


                }

            }
            catch (Exception ex)
            {
            }

            return objResponse;
        }

        public List<SchemeDetailsModel> getSchemeDetailsHeadders(int SchemeDetailsId)
        {
            List<SchemeDetailsModel> objSchemeDetailsList = new List<SchemeDetailsModel>();

            try
            {
                DataTable dtDH = new DataTable();
                dtDH = objDal.getSchemeDetailsHeadders(SchemeDetailsId);

                for (int i = 0; i < dtDH.Rows.Count; i++)
                {
                    objSchemeDetailsList.Add(new SchemeDetailsModel { SchemeDetailsId = Convert.ToInt32(dtDH.Rows[i]["SchemeDetailsId"].ToString()), CompanyName = dtDH.Rows[i]["CompanyName"].ToString(), BranchName = dtDH.Rows[i]["BranchName"].ToString(), DepartmentName = dtDH.Rows[i]["DepartmentName"].ToString() });
                }

            }
            catch (Exception ex)
            {

            }

            return objSchemeDetailsList;

        }




    }
}
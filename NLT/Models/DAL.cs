using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NLT.Models;

namespace NLT.Models
{
    public class DAL
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["INV"].ToString();

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;



        public DAL()
        {
            conn = new SqlConnection(ConnStr);
            ds = new DataSet();
            dt = new DataTable();
        }


        public bool IsphoneNumberExits(string PhoneNumber)
        {
            bool Result = true;
            DataSet dsPhoneNum = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_IsPhoneNumberValid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PhoneNumber", typeof(string)).Value = PhoneNumber;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsPhoneNum);
                DataTable dtPhone = new DataTable();
                dtPhone = dsPhoneNum.Tables[0];

                conn.Close();

                if(dtPhone.Rows.Count>0)
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            catch (Exception ex)
            {
                
            }

            return Result;
        }


        // Phone Numner Save
        public DataTable getOrders(int OrderId)
        {
            DataTable dtTest = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getOrders", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", typeof(int)).Value = OrderId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtTest = ds.Tables[0];

                conn.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return dtTest;
        }

        public DataSet getOrdersDetails(int OrderId)
        {
            DataSet dsOrder = new DataSet();
            DataTable dtTest = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getOrderDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", typeof(int)).Value = OrderId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsOrder);

               // dtTest = ds.Tables[0];

                conn.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return dsOrder;
        }


        public DataTable userSignup_(string PhoneNumber, string emailId, string OTP, out bool Result, out int NewUserId)
        {
            DataTable dtTest = new DataTable();
            Result = false;
            NewUserId = 0;

            string ParameterError = "";
            bool ParameterResult = false;
            string str = "";
            str = ConfigurationManager.ConnectionStrings["INV"].ToString();
            SqlConnection conn_ = new SqlConnection(str);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();

                //SqlCommand cmd = new SqlCommand("usp_userSignup", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@stateId", typeof(int)).Value = StateId;

                SqlCommand cmd = new SqlCommand("usp_userSignup", conn_);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PhoneNumber", typeof(string)).Value = PhoneNumber;
                cmd.Parameters.AddWithValue("@emailId", typeof(string)).Value = emailId;
                cmd.Parameters.AddWithValue("@OTP", typeof(string)).Value = OTP;


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }


        public DataTable getUserDetails(int SequenceId)
        {
            DataTable dtTest = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select SequenceId,CreatedDate,UserId,UserName,PhoneNumber,EmailId,OTP from tbl_SignUp where UserId=" + SequenceId, conn);
               // cmd.CommandType = CommandType.StoredProcedure;
             //   cmd.Parameters.AddWithValue("@SequenceId", typeof(string)).Value = SequenceId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtTest = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


            return dtTest;
        }


        public string userSignup(string PhoneNumber, string emailId, string OTP,out bool Result, out int NewUserId)
        {
            DataTable dtTest = new DataTable();
            Result = false;
            NewUserId = 0;

            string ParameterError = "";
            bool ParameterResult = false;
            string str = "";
            str = ConfigurationManager.ConnectionStrings["INV"].ToString();
            SqlConnection conn_ = new SqlConnection(str);

            int UserId = 0;

            try
            {
               
                conn_.Open();

                SqlCommand cmd = new SqlCommand("usp_userSignup", conn_);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PhoneNumber", typeof(string)).Value = PhoneNumber;
                cmd.Parameters.AddWithValue("@emailId", typeof(string)).Value = emailId;
                cmd.Parameters.AddWithValue("@OTP", typeof(string)).Value = OTP;

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                //dtTest = ds.Tables[0];

                //SqlParameter ourParameterResult = new SqlParameter();
                //ourParameterResult.ParameterName = "@Result";
                //ourParameterResult.SqlDbType = SqlDbType.Bit;
                //ourParameterResult.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(ourParameterResult);

                SqlParameter ourParameterNewUserId = new SqlParameter();
                ourParameterNewUserId.ParameterName = "@NewUserId";
                ourParameterNewUserId.SqlDbType = SqlDbType.Int;
                ourParameterNewUserId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ourParameterNewUserId);

                //SqlParameter ourParameterError = new SqlParameter();
                //ourParameterError.ParameterName = "@Error";
                //ourParameterError.SqlDbType = SqlDbType.NVarChar;
                //ourParameterError.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(ourParameterError);

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                //dtTest = ds.Tables[0];

                int Res = cmd.ExecuteNonQuery();

                if(Res !=0)
                {
                    Result = true;
                    ParameterError = "";
                    //ParameterResult = (bool)cmd.Parameters["@Result"].Value;
                    UserId = (int)cmd.Parameters["@NewUserId"].Value;

                    DataTable dt = getUserDetails(UserId);

                    if(dt.Rows.Count>0)
                    {
                        NewUserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    }
                }
                else
                {
                    Result = false;
                }

                //NewUserId = (int)cmd.Parameters["@NewUserId"].Value;
                //ParameterResult = (bool)cmd.Parameters["@Result"].Value;
                //ParameterError = (string)cmd.Parameters["@Error"].Value;

                //if (ParameterResult == true)
                //{
                //    Result = true;
                //    ParameterError = "";
                //}
                //else
                //{
                //    Result = false;

                //}

                conn_.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn_.State == ConnectionState.Open)
                {
                    conn_.Close();
                }

            }


            return ParameterError;
        }



        public DataTable getTest(string TestName)
        {
            DataTable dtTest = new DataTable();
            DataSet dsT = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getTest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestName", typeof(string)).Value = TestName;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsT);
                dtTest = dsT.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


            return dtTest;
        }

        public DataTable getLocation(string LocationName)
        {
            DataTable dtLocation = new DataTable();
            DataSet dsLocation = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlaceName", typeof(string)).Value = LocationName;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsLocation);
                dtLocation = dsLocation.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


            return dtLocation;
        }
        public DataTable getLocations_(string LocationName)
        {
            DataTable dtTest = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getLocations", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlaceName", typeof(string)).Value = LocationName;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtTest = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


            return dtTest;
        }

        public DataTable getTests(string TestName,string CompanyId)
        {
            DataTable dtTest = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getTests", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestName", typeof(string)).Value = TestName;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(string)).Value = CompanyId;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtTest = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


            return dtTest;
        }
        public DataTable getTests(string TestName)
        {
            DataTable dtTest = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_getTests", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestName", typeof(string)).Value = TestName;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(string)).Value = "0";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtTest = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


            return dtTest;
        }


        public bool Delete_Order_Item(int orderId, int orderItemId)
        {
            bool Resonse = false;

            try
            {
                conn.Open();
                cmd = new SqlCommand("usp_Delete_Order_Item", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", typeof(int)).Value = orderId;
                cmd.Parameters.AddWithValue("@orderItemId", typeof(int)).Value = orderItemId;

                int Res = cmd.ExecuteNonQuery();

                if (Res == 1)
                {
                    Resonse = true;
                }
                else
                {
                    Resonse = false;
                }

                conn.Close();

            }
            catch (Exception ex)
            {
            }

            return Resonse;


        }





        public DataSet getDepartments(int DepartmentId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getDepartments", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = DepartmentId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataTable getTestPrise(int CompanyId, int LocationId, int TestId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getTestPrise", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = CompanyId;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = LocationId;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = TestId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];
                conn.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return dt;
        }




        public int saveDepartment(DepartmentModel departmentModel)
        {
            int Result = 0;

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveDepartment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = departmentModel.DepartmentId;
                cmd.Parameters.AddWithValue("@DepartmentName", typeof(string)).Value = departmentModel.DepartmentName;

                Result = cmd.ExecuteNonQuery();

                conn.Close();

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);

            }
            catch (Exception ex)
            {

                throw;
            }


            return Result;
        }


        public DataSet getSpecimens(int SpecimenId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getSpecimens", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpecimenId", typeof(int)).Value = SpecimenId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public int saveSpecimen(SpecimenModel specimenModel)
        {
            int Result = 0;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveSpecimen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpecimenId", typeof(int)).Value = specimenModel.SpecimenId;
                cmd.Parameters.AddWithValue("@SpecimenName", typeof(string)).Value = specimenModel.SpecimenName;

                Result = cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {


            }


            return Result;
        }



        public int saveState(StateModel stateModel)
        {
            int Result = 0;

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveSate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stateId", typeof(int)).Value = stateModel.StateId;
                cmd.Parameters.AddWithValue("@stateName", typeof(string)).Value = stateModel.StateName;

                Result = cmd.ExecuteNonQuery();

                conn.Close();

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);

            }
            catch (Exception ex)
            {

                throw;
            }


            return Result;
        }

        public DataSet getStates(int StateId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getStates", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stateId", typeof(int)).Value = StateId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }



        public int saveCity(CityModel cityModel)
        {
            int Result = 0;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveCity", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CityId", typeof(int)).Value = cityModel.CityId;
                cmd.Parameters.AddWithValue("@CityName", typeof(string)).Value = cityModel.CityName;
                cmd.Parameters.AddWithValue("@StateId", typeof(int)).Value = cityModel.StateId;

                cmd.Parameters.AddWithValue("@Pincode", typeof(string)).Value = cityModel.Pincode;
                cmd.Parameters.AddWithValue("@Latitude", typeof(string)).Value = cityModel.Latitude.ToString();
                cmd.Parameters.AddWithValue("@Longitude", typeof(string)).Value = cityModel.Longitude.ToString();


                Result = cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {

                throw;
            }


            return Result;
        }

        public DataSet getCitys(int CityId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getCitys", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CityId", typeof(int)).Value = CityId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet getCitysByStateId(int StateId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getCitysByStateId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", typeof(int)).Value = StateId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }


        public int savePlace(PlaceModel placeModel)
        {
            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_savePlace", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlaceId", typeof(int)).Value = placeModel.PlaceId;
                cmd.Parameters.AddWithValue("@PlaceName", typeof(string)).Value = placeModel.PlaceName;

                cmd.Parameters.AddWithValue("@CityId", typeof(int)).Value = placeModel.CityId;
                cmd.Parameters.AddWithValue("@StateId", typeof(int)).Value = placeModel.StateId;

                cmd.Parameters.AddWithValue("@Pincode", typeof(string)).Value = placeModel.Pincode;
                cmd.Parameters.AddWithValue("@Latitude", typeof(string)).Value = placeModel.Latitude.ToString();
                cmd.Parameters.AddWithValue("@Longitude", typeof(string)).Value = placeModel.Longitude.ToString();

                Result = cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {

            }


            return Result;
        }


        public DataTable getPlaces(int PlaceId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getPlaces", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlaceId", typeof(int)).Value = PlaceId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            return ds.Tables[0];
        }



        public int saveCompany(CompanyModel companyModel)
        {
            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveCompany", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = companyModel.CompanyId;
                cmd.Parameters.AddWithValue("@CompanyName", typeof(string)).Value = companyModel.CompanyName;
                cmd.Parameters.AddWithValue("@ImagePath", typeof(string)).Value = companyModel.ImagePath;
                cmd.Parameters.AddWithValue("@Remarks", typeof(string)).Value = companyModel.Remarks;
                Result = cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return Result;
        }



        public DataTable getCompanys(int CompanyId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getCompanys", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = CompanyId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            return ds.Tables[0];
        }


        public int saveTest(TestModel testModel)
        {
            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveTest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = testModel.TestId;
                cmd.Parameters.AddWithValue("@TestName", typeof(string)).Value = testModel.TestName;
                cmd.Parameters.AddWithValue("@DisplayName", typeof(string)).Value = testModel.DisplayName;
                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = testModel.DepartmentId;
                cmd.Parameters.AddWithValue("@SpecimenId", typeof(int)).Value = testModel.SpecimenId;
                cmd.Parameters.AddWithValue("@ApplicableTo", typeof(int)).Value = testModel.ApplicableTo;
                cmd.Parameters.AddWithValue("@ReportingDays", typeof(int)).Value = testModel.ReportingDays;

                cmd.Parameters.AddWithValue("@HouseVisit", typeof(bool)).Value = testModel.HouseVisit;
                cmd.Parameters.AddWithValue("@Nabl", typeof(bool)).Value = testModel.Nabl;
                cmd.Parameters.AddWithValue("@ActiveStatus", typeof(bool)).Value = testModel.ActiveStatus;

                cmd.Parameters.AddWithValue("@Remarks", typeof(string)).Value = testModel.Remarks;

                Result = cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return Result;
        }

        public DataSet getTests(int TestId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getTests", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = TestId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }


        public int saveMapTestLocation(TestLocationModel testLocationModel)
        {
            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_MapTestLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MapId", typeof(int)).Value = testLocationModel.MapId;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = testLocationModel.CompanyId;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = testLocationModel.LocationId;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = testLocationModel.TestId;

                cmd.Parameters.AddWithValue("@Price", typeof(decimal)).Value = testLocationModel.price;
                cmd.Parameters.AddWithValue("@DiscountApplay", typeof(bool)).Value = testLocationModel.CustomerDiscountApplay;
                cmd.Parameters.AddWithValue("@CustomerDiscountType", typeof(int)).Value = testLocationModel.CustomerDiscountTypeId;

                cmd.Parameters.AddWithValue("@CustomerDiscount", typeof(decimal)).Value = testLocationModel.CustomerDiscount;
                cmd.Parameters.AddWithValue("@ActiveStatus", typeof(bool)).Value = testLocationModel.ActiveStatus;
                cmd.Parameters.AddWithValue("@Remarks", typeof(string)).Value = testLocationModel.Remarks;

                Result = cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {

            }

            return Result;
        }

        public DataSet getMapTestLocation(int MapId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getMapTestLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MapId", typeof(int)).Value = MapId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }


        public int saveBranch(BranchModel branchModel)
        {
            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = branchModel.BranchId;
                cmd.Parameters.AddWithValue("@BranchName", typeof(string)).Value = branchModel.BranchName;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = branchModel.CompanyId;
                cmd.Parameters.AddWithValue("@PleaseId", typeof(int)).Value = branchModel.PleaseId;

                cmd.Parameters.AddWithValue("@Address", typeof(string)).Value = branchModel.Address;
                cmd.Parameters.AddWithValue("@Phone1", typeof(string)).Value = branchModel.Phone1;
                cmd.Parameters.AddWithValue("@Phone2", typeof(string)).Value = branchModel.Phone2;

                cmd.Parameters.AddWithValue("@ContactPerson", typeof(string)).Value = branchModel.ContactPerson;
                cmd.Parameters.AddWithValue("@PersonalMobile", typeof(string)).Value = branchModel.PersonalMobile;
                cmd.Parameters.AddWithValue("@Latitude", typeof(string)).Value = branchModel.Latitude;

                cmd.Parameters.AddWithValue("@Longitude", typeof(string)).Value = branchModel.Longitude;

                cmd.Parameters.AddWithValue("@WorkingHourseFrom", typeof(int)).Value = branchModel.WorkingHourseFrom;
                cmd.Parameters.AddWithValue("@WorkingHourseTo", typeof(int)).Value = branchModel.WorkingHourseTo;
                cmd.Parameters.AddWithValue("@SundayisHoliday", typeof(bool)).Value = branchModel.SundayisHoliday;

                cmd.Parameters.AddWithValue("@IsNABL", typeof(bool)).Value = branchModel.IsNABL;
                cmd.Parameters.AddWithValue("@IsWheelChair", typeof(bool)).Value = branchModel.IsWheelChair;
                cmd.Parameters.AddWithValue("@IsHomeCollection", typeof(bool)).Value = branchModel.IsHomeCollection;
                cmd.Parameters.AddWithValue("@IsParkingAvailable", typeof(bool)).Value = branchModel.IsParkingAvailable;
                cmd.Parameters.AddWithValue("@IsCreditCardAccet", typeof(bool)).Value = branchModel.IsCreditCardAccet;
                cmd.Parameters.AddWithValue("@IsMoneyBack", typeof(bool)).Value = branchModel.IsMoneyBack;
                cmd.Parameters.AddWithValue("@IsOnlinePayment", typeof(bool)).Value = branchModel.IsOnlinePayment;
                cmd.Parameters.AddWithValue("@FirstImage", typeof(string)).Value = branchModel.FirstImage;
                cmd.Parameters.AddWithValue("@SecoundImage", typeof(string)).Value = branchModel.SecoundImage;
                cmd.Parameters.AddWithValue("@ThirdImage", typeof(string)).Value = branchModel.ThirdImage;

                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = branchModel.DepartmentId;
                cmd.Parameters.AddWithValue("@FromTime", typeof(string)).Value = branchModel.FromTime;
                cmd.Parameters.AddWithValue("@ToTime", typeof(string)).Value = branchModel.ToTime;

                Result = cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {

            }

            return Result;
        }

        public DataSet getBranches(int BranchId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getBranches", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = BranchId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataTable getDepartmentService(int BranchId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getDepartmentService", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = BranchId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public DataTable DeleteDepartmentService(int ServiceId)
        {
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteDepartmentService", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServiceId", typeof(int)).Value = ServiceId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();

            }
            catch (Exception ex)
            {

            }

            return ds.Tables[0];

        }



        // Client FrientEnd

        public DataTable getLocations()
        {
            try
            {
                conn = new SqlConnection(ConnStr);
                conn.Open();
                cmd = new SqlCommand("sp_getPlaces", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlaceId", typeof(int)).Value = 0;

                da = new SqlDataAdapter(cmd);

                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {

            }

            return ds.Tables[0];
        }

        public DataTable getTestsByLocationId(int LocationId)
        {
            try
            {
                //conn = new SqlConnection(connstrAdminPanel);
                conn.Open();
                cmd = new SqlCommand("sp_getTestsByLocationId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = LocationId;

                da = new SqlDataAdapter(cmd);

                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {

            }

            return ds.Tables[0];
        }


        public DataSet getTestsByLocationAndTestName(int LocationId,int CompanyId, int TestId, string Type)
        {
            try
            {
                // conn = new SqlConnection(connstrAdminPanel);
                conn.Open();
                cmd = new SqlCommand("sp_getTestsByLocationAndTest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = LocationId;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = CompanyId;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = TestId;
                cmd.Parameters.AddWithValue("@Type", typeof(string)).Value = Type;

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        public DataSet getTestsByLocationAndTestName(int LocationId, int TestId, string Type)
        {
            try
            {
                // conn = new SqlConnection(connstrAdminPanel);
                conn.Open();
                cmd = new SqlCommand("sp_getTestsByLocationAndTest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = LocationId;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = 0;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = TestId;
                cmd.Parameters.AddWithValue("@Type", typeof(string)).Value = Type;

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {

            }

            return ds;
        }


        public DataTable getTestDetails(int LocationId, int TestId, int CmpId)
        {
            try
            {
                // conn = new SqlConnection(connstrAdminPanel);
                conn.Open();
                cmd = new SqlCommand("sp_GetTestDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = LocationId;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = TestId;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = CmpId;

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {

            }

            return ds.Tables[0];
        }

        public DataTable save_CP(CPModel cPModel)
        {
            int Result = 0;
            try
            {
                conn.Open();

                DataTable dt = new DataTable();
                dt.Columns.Add("LocationId", typeof(int));
                dt.Columns.Add("CompanyId", typeof(int));
                dt.Columns.Add("TestId", typeof(int));
                dt.Columns.Add("MRP", typeof(string));
               

                foreach (var T in cPModel.Tests)
                {
                    DataRow dr = dt.NewRow();
                    dr["LocationId"] = T.BranchId;
                    dr["CompanyId"] = T.CompanyId;
                    dr["TestId"] = T.TestId;
                    dr["MRP"] = T.MRP;
                    dt.Rows.Add(dr);
                }



                SqlCommand cmd = new SqlCommand("usp_Save_CP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", typeof(int)).Value = 0;
                //cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = cPModel.TestId;
                //cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = cPModel.LocationId;

                cmd.Parameters.AddWithValue("@guid", typeof(string)).Value = cPModel.guid;
                cmd.Parameters.AddWithValue("@YourName", typeof(string)).Value = cPModel.YourName;
                cmd.Parameters.AddWithValue("@PhoneNumber", typeof(string)).Value = cPModel.PhoneNumber;
                cmd.Parameters.AddWithValue("@TestPersonName", typeof(string)).Value = cPModel.TestPersonName;
                cmd.Parameters.AddWithValue("@Age", typeof(int)).Value = Convert.ToInt32(cPModel.Age);
                cmd.Parameters.AddWithValue("@Appointmentdate", typeof(string)).Value = cPModel.Appointmentdate.ToString(); //DateTime.Now.ToString();

                cmd.Parameters.AddWithValue("@tbl_Tests", dt);

                // Result = cmd.ExecuteNonQuery();

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);


                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }


            return ds.Tables[0];
        }


        public DataTable saveTestOrder(TestDisplayModel TestDisplayModel)
        {

            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveTestOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", typeof(int)).Value = TestDisplayModel.OrderId;
                cmd.Parameters.AddWithValue("@TestId", typeof(int)).Value = TestDisplayModel.TestId;
                cmd.Parameters.AddWithValue("@LocationId", typeof(int)).Value = TestDisplayModel.LocationId;
                cmd.Parameters.AddWithValue("@YourName", typeof(string)).Value = TestDisplayModel.YourName;
                cmd.Parameters.AddWithValue("@PhoneNumber", typeof(string)).Value = TestDisplayModel.PhoneNumber;
                cmd.Parameters.AddWithValue("@TestPersonName", typeof(string)).Value = TestDisplayModel.TestPersonName;
                cmd.Parameters.AddWithValue("@Age", typeof(int)).Value = Convert.ToInt32(TestDisplayModel.Age);
                cmd.Parameters.AddWithValue("@Appointmentdate", typeof(string)).Value = TestDisplayModel.Appointmentdate;

                // Result = cmd.ExecuteNonQuery();

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);


                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }


            return ds.Tables[0];

        }


        public DataSet getCPTestOrder(int TestOrderId)
        {
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_getCPTestOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestOrderId", typeof(int)).Value = TestOrderId;

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {
            }

            return ds;


        }
        public DataTable getTestOrder(int TestOrderId)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_getCPTestOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TestOrderId", typeof(int)).Value = TestOrderId;


                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {


            }
            return ds.Tables[0];
        }


        public DataTable confirmTestOrder(int OrderId, int PaymentType)
        {
            try
            {
                // conn = new SqlConnection(connstrAdminPanel);
                conn.Open();
                cmd = new SqlCommand("sp_confirmTestOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", typeof(int)).Value = OrderId;
                cmd.Parameters.AddWithValue("@PaymentType", typeof(int)).Value = PaymentType;

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return ds.Tables[0];
        }



        // Scheme 
        public DataTable saveScheme(SchemeModel schemeModel)
        {

            int Result = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveScheme", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemeId", typeof(int)).Value = schemeModel.SchemeId;
                cmd.Parameters.AddWithValue("@SchemeName", typeof(int)).Value = schemeModel.SchemeName;
                //cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = schemeModel.DepartmentId;
                //cmd.Parameters.AddWithValue("@CompanyId", typeof(string)).Value = schemeModel.CompanyId;

                // Result = cmd.ExecuteNonQuery();

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return ds.Tables[0];

        }


        public DataSet getSchemes(int SchemeId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getSchemes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemeId", typeof(int)).Value = SchemeId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }



        public DataTable getDepartmentsByBranchId(int branchId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getDepartmentByBranchId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = branchId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }


        public DataTable getBranchsByCompanyId(int CompanyId)
        {
            DataTable dt = new DataTable();
            DataSet ds2 = new DataSet();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getBranchsByCompanyId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = CompanyId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds2);
                dt = ds2.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }


        public DataTable getTestsForPriceUpdate(int BranchId, int DepartmentId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getTestsByBranchAndDept", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = BranchId;
                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = DepartmentId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public DataTable getTestsForScheme(int SchemeDetailsId, int CompanyId, int BranchId, int DepartmentId)
        {

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getTestsForScheme", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemeDetailsId", typeof(int)).Value = SchemeDetailsId;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = CompanyId;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = BranchId;
                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = DepartmentId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }


        public DataTable saveSchemeDetails(SchemeDetailsModel schemeDetailsModel)
        {

            int Result = 0;
            try
            {

                DataTable dtTests = new DataTable();
                dtTests.Columns.Add("Id", typeof(int));
                dtTests.Columns.Add("TestId", typeof(int));
                dtTests.Columns.Add("TestName", typeof(string));
                dtTests.Columns.Add("MRP", typeof(decimal));
                dtTests.Columns.Add("AgrigateRate", typeof(decimal));
                dtTests.Columns.Add("CustomerDiscountType", typeof(int));
                dtTests.Columns.Add("CustomerDiscountPrice", typeof(decimal));
                dtTests.Columns.Add("Total", typeof(decimal));


                foreach (var Test in schemeDetailsModel.TestPrices)
                {
                    dtTests.Rows.Add(Test.Id, Test.TestId, "", Test.MRP, Test.AgrigateRate, Test.CustomerDiscountType, Test.CustomerDiscountPrice, Test.Total);
                }

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_saveSchemeDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemeDetailsId", typeof(int)).Value = schemeDetailsModel.SchemeDetailsId;
                cmd.Parameters.AddWithValue("@SchemeId", typeof(int)).Value = schemeDetailsModel.SchemeId;
                cmd.Parameters.AddWithValue("@CompanyId", typeof(int)).Value = schemeDetailsModel.CompanyId;
                cmd.Parameters.AddWithValue("@BranchId", typeof(int)).Value = schemeDetailsModel.BranchId;
                cmd.Parameters.AddWithValue("@DepartmentId", typeof(int)).Value = schemeDetailsModel.DepartmentId;

                cmd.Parameters.AddWithValue("@tblSchemeTests", dtTests);

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                int Result_SchemeDetails = 0;
                Result_SchemeDetails = Convert.ToInt32(ds.Tables[0].Rows[0]["Result"].ToString());


                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return ds.Tables[0];

        }


        public DataTable getSchemeDetailsHeadders(int SchemeDetailsId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getSchemeDetailsHeadder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemeDetailsId", typeof(int)).Value = SchemeDetailsId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;

        }


        public DataSet getSchemeDetails(int SchemeDetailsId)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getSchemeDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemeDetailsId", typeof(int)).Value = SchemeDetailsId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }






    }

}
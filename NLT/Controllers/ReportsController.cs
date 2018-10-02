using iTextSharp.text;
using iTextSharp.text.pdf;
using NLT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NLT.Controllers
{
    public class ReportsController : Controller
    {


        string ConnStr = ConfigurationManager.ConnectionStrings["INV"].ToString();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {

            //ReportsModel objReports = new ReportsModel();

            //return View(objReports);


            FileOrderModel objOrderModel = new FileOrderModel();
            objOrderModel.divFileUpload = false;



            return View(objOrderModel);

        }
        [HttpPost]
        public ActionResult UploadFile(FormCollection form, string btnShow, string btnPost, string btnView, HttpPostedFileBase file, string OrderId)
        {

            FileOrderModel objOrderModel = new FileOrderModel();

            if (!string.IsNullOrEmpty(btnView))
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"800px\" height=\"300px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/UploadedFiles/Srinivas_VRO_2018_.pdf"));

                objOrderModel.VendorId = 100;
                objOrderModel.divFileUpload = true;

                SqlConnection con = new SqlConnection(ConnStr);
                //string OrderId = Convert.ToString(form["OrderId"]);
                string str = "select OrderId,pdfpath from ReportPdf where OrderId='" + OrderId + "'";
                SqlCommand cmdins = new SqlCommand(str, con);
                //cmdins.CommandType = CommandType.StoredProcedure;
                // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                con.Open();

                DataTable dt_ = new DataTable();
                DataSet ds_ = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdins);
                da.Fill(ds_);
                //xlitdesc.Text = "Data Saved";
                dt_ = ds_.Tables[0];

                con.Close();

                List<FileModel> objFileList = new List<FileModel>();
                if (dt_.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_.Rows.Count; i++)
                    {
                        objFileList.Add(new FileModel { OrderId = dt_.Rows[i]["OrderId"].ToString(), FilePath = dt_.Rows[i]["pdfpath"].ToString() });
                    }

                    objOrderModel.listFiles = objFileList;
                }


            }
            if (!string.IsNullOrEmpty(btnShow))
            {
                objOrderModel.VendorId = 100;
                objOrderModel.divFileUpload = true;

                SqlConnection con = new SqlConnection(ConnStr);
                //string OrderId = Convert.ToString(form["OrderId"]);
                string str = "select OrderId,pdfpath from ReportPdf where OrderId='" + OrderId + "'";
                SqlCommand cmdins = new SqlCommand(str, con);
                //cmdins.CommandType = CommandType.StoredProcedure;
                // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                con.Open();

                DataTable dt_ = new DataTable();
                DataSet ds_ = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdins);
                da.Fill(ds_);
                //xlitdesc.Text = "Data Saved";
                dt_ = ds_.Tables[0];

                con.Close();

                List<FileModel> objFileList = new List<FileModel>();
                if (dt_.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_.Rows.Count; i++)
                    {
                        objFileList.Add(new FileModel { OrderId = dt_.Rows[i]["OrderId"].ToString(), FilePath = dt_.Rows[i]["pdfpath"].ToString() });
                    }

                    objOrderModel.listFiles = objFileList;
                }


                //  ViewBag.Message = "Customer saved successfully!";
            }
            if (!string.IsNullOrEmpty(btnPost))
            {

                try
                {
                    if (file.ContentLength > 0)
                    {
                        //string _FileName = Path.GetFileName(file.FileName);
                        //string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        //file.SaveAs(_path);

                        string Name = "Test_" + OrderId + "_" + DateTime.Now.Ticks;

                        string extension = Path.GetExtension(file.FileName);
                        string filename = Path.GetFileName(file.FileName);
                        string Extention = Path.GetExtension(file.FileName);

                        string uploadFolder = Request.PhysicalApplicationPath + "UploadedFiles\\"; // This will get the actual path for upload.
                        string upload = "~/UploadedFiles/" + Name + Extention; // + filename;   // This path will go in database because asp.net requires virtual path.
                        file.SaveAs(uploadFolder + Name + Extention);
                        string getfile = upload;

                        SqlConnection con = new SqlConnection(ConnStr);
                        //string OrderId = Convert.ToString(form["OrderId"]);
                        string str = "Insert into ReportPdf(pdfpath,OrderId)values('" + upload + "','" + OrderId + "')";
                        SqlCommand cmdins = new SqlCommand(str, con);
                        //cmdins.CommandType = CommandType.StoredProcedure;
                        // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                        con.Open();
                        cmdins.ExecuteNonQuery();
                        //xlitdesc.Text = "Data Saved";
                        con.Close();

                        //Response.ContentType = "Application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=Test_PDF.pdf");
                        //Response.TransmitFile(upload);
                        ////Response.TransmitFile(Server.MapPath("~/Files/Test_PDF.pdf"));
                        //Response.End();

                        //WebClient client = new WebClient();
                        //Byte[] buffer = client.DownloadData(uploadFolder + filename);


                        //if (buffer != null)
                        //{
                        //    Response.ContentType = "application/pdf";
                        //    Response.AddHeader("content-length", buffer.Length.ToString());
                        //    Response.BinaryWrite(buffer);
                        //}


                        //WebClient client = new WebClient();
                        //Byte[] buffer = client.DownloadData(upload);

                        //PdfReader reader1 = new PdfReader(buffer);
                        //MemoryStream ms = new MemoryStream();
                        //byte[] mergedPdf = null;

                        //Document document = new Document();
                        //PdfCopy copy = new PdfCopy(document, ms);
                        //copy.AddPage(copy.GetImportedPage(reader1,1));

                        //Response.ContentType = "application/pdf";
                        ////Response.AddHeader("content-length", buffer2.ToString());
                        //// Response.AppendHeader("Content-Disposition", "inline;filename=data.pdf");

                        ////Response.BinaryWrite(buffer2);
                        //Response.BinaryWrite(mergedPdf);
                        ////Response.End();


                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("Content-Disposition", "attachment; filename="+ upload + "");

                        //FileStream fs = new FileStream(Server.MapPath(@"~\HTP.pdf"), FileMode.Open, FileAccess.Read);
                        //return File(fs, "application/pdf");

                        //FileStream fs = new FileStream(Server.MapPath(@upload), FileMode.Open, FileAccess.Read);
                        //return File(fs, "application/pdf");


                        //string filepath = Server.MapPath("/PDF/Aptitude Formulas.pdf");
                        //byte[] pdfByte = GetBytesFromFile(filepath);
                        //return File(pdfByte, "application/pdf");

                        //string filepath = Server.MapPath(upload);
                        //byte[] pdfByte = GetBytesFromFile("~/UploadedFiles/" + filename);
                        //return File(pdfByte, "application/pdf");


                        SqlConnection con2 = new SqlConnection(ConnStr);
                        //string OrderId = Convert.ToString(form["OrderId"]);
                        string str2 = "select pdfpath,OrderId from ReportPdf where OrderId='" + OrderId + "'";
                        SqlCommand cmdins2 = new SqlCommand(str2, con2);
                        //cmdins.CommandType = CommandType.StoredProcedure;
                        // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                        con2.Open();

                        DataTable dt2_ = new DataTable();
                        DataSet ds2_ = new DataSet();
                        SqlDataAdapter da2 = new SqlDataAdapter(cmdins2);
                        da2.Fill(ds2_);
                        //xlitdesc.Text = "Data Saved";
                        dt2_ = ds2_.Tables[0];

                        con2.Close();

                        List<FileModel> objFileList = new List<FileModel>();
                        if (dt2_.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2_.Rows.Count; i++)
                            {
                                objFileList.Add(new FileModel { OrderId = dt2_.Rows[i]["OrderId"].ToString(), FilePath = dt2_.Rows[i]["pdfpath"].ToString() });
                            }

                            objOrderModel.listFiles = objFileList;
                        }
                        objOrderModel.divFileUpload = true;
                        string A = "";

                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return View(objOrderModel);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }

                // ViewBag.Message = "The operation was cancelled!";
            }



            return View(objOrderModel);

        }



        public ActionResult DownloadPdf(string FilePath)
        { // Find user by passed id
          // var file = db.EmailAttachmentReceived.FirstOrDefault(x => x.LisaId == studentId);

            string path = FilePath;
            //string s = FileModel.FileName;

            string path_ = Server.MapPath(path);

            byte[] fileBytes = System.IO.File.ReadAllBytes(path_);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "TestPdf2.pdf");
        }


        public byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            FileStream fs = null;
            try
            {
                fs = new FileStream(Server.MapPath(@fullFilePath), FileMode.Open, FileAccess.Read);
                //return File(fs, "application/pdf");

                //fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        ///-- In Controller
        public FileStreamResult GetPDF()
        {
            FileStream fs = new FileStream(Server.MapPath(@"~\HTP.pdf"), FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }



        public ActionResult Testpage()
        {
            ReportsModel objReports = new ReportsModel();
            return View(objReports);
        }



        public ActionResult Upload()
        {
            OrderModel objOrderModel = new OrderModel();
            objOrderModel.divFileUpload = false;



            return View(objOrderModel);

        }

        [HttpPost]
        public ActionResult Upload(FormCollection form, string btnShow, string btnPost, HttpPostedFileBase file, string OrderId)
        {

            FileOrderModel objFileOrderModel = new FileOrderModel();

            if (!string.IsNullOrEmpty(btnShow))
            {
                objFileOrderModel.VendorId = 100;
                objFileOrderModel.divFileUpload = true;

                SqlConnection con = new SqlConnection(ConnStr);
                //string OrderId = Convert.ToString(form["OrderId"]);
                string str = "select filePath,OrderId from TestFileUpload where OrderId='" + OrderId + "'";
                SqlCommand cmdins = new SqlCommand(str, con);
                //cmdins.CommandType = CommandType.StoredProcedure;
                // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                con.Open();

                DataTable dt_ = new DataTable();
                DataSet ds_ = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdins);
                da.Fill(ds_);
                //xlitdesc.Text = "Data Saved";
                dt_ = ds_.Tables[0];

                con.Close();

                List<FileModel> objFileList = new List<FileModel>();
                if (dt_.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_.Rows.Count; i++)
                    {
                        objFileList.Add(new FileModel { OrderId = dt_.Rows[i]["OrderId"].ToString(), FilePath = dt_.Rows[i]["filePath"].ToString() });
                    }

                    objFileOrderModel.listFiles = objFileList;
                }


                //  ViewBag.Message = "Customer saved successfully!";
            }
            if (!string.IsNullOrEmpty(btnPost))
            {

                try
                {
                    if (file.ContentLength > 0)
                    {
                        //string _FileName = Path.GetFileName(file.FileName);
                        //string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        //file.SaveAs(_path);

                        string extension = Path.GetExtension(file.FileName);
                        string filename = Path.GetFileName(file.FileName);
                        string uploadFolder = Request.PhysicalApplicationPath + "UploadedFiles\\"; // This will get the actual path for upload.
                        string upload = "~/UploadedFiles/" + filename;   // This path will go in database because asp.net requires virtual path.
                        file.SaveAs(uploadFolder + filename);
                        string getfile = upload;

                        SqlConnection con = new SqlConnection(ConnStr);
                        //string OrderId = Convert.ToString(form["OrderId"]);
                        string str = "Insert into TestFileUpload(filePath,OrderId)values('" + upload + "','" + OrderId + "')";
                        SqlCommand cmdins = new SqlCommand(str, con);
                        //cmdins.CommandType = CommandType.StoredProcedure;
                        // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                        con.Open();
                        cmdins.ExecuteNonQuery();
                        //xlitdesc.Text = "Data Saved";
                        con.Close();

                        //Response.ContentType = "Application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=Test_PDF.pdf");
                        //Response.TransmitFile(upload);
                        ////Response.TransmitFile(Server.MapPath("~/Files/Test_PDF.pdf"));
                        //Response.End();

                        //WebClient client = new WebClient();
                        //Byte[] buffer = client.DownloadData(uploadFolder + filename);


                        //if (buffer != null)
                        //{
                        //    Response.ContentType = "application/pdf";
                        //    Response.AddHeader("content-length", buffer.Length.ToString());
                        //    Response.BinaryWrite(buffer);
                        //}


                        //WebClient client = new WebClient();
                        //Byte[] buffer = client.DownloadData(upload);

                        //PdfReader reader1 = new PdfReader(buffer);
                        //MemoryStream ms = new MemoryStream();
                        //byte[] mergedPdf = null;

                        //Document document = new Document();
                        //PdfCopy copy = new PdfCopy(document, ms);
                        //copy.AddPage(copy.GetImportedPage(reader1,1));

                        //Response.ContentType = "application/pdf";
                        ////Response.AddHeader("content-length", buffer2.ToString());
                        //// Response.AppendHeader("Content-Disposition", "inline;filename=data.pdf");

                        ////Response.BinaryWrite(buffer2);
                        //Response.BinaryWrite(mergedPdf);
                        ////Response.End();


                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("Content-Disposition", "attachment; filename="+ upload + "");

                        //FileStream fs = new FileStream(Server.MapPath(@"~\HTP.pdf"), FileMode.Open, FileAccess.Read);
                        //return File(fs, "application/pdf");

                        //FileStream fs = new FileStream(Server.MapPath(@upload), FileMode.Open, FileAccess.Read);
                        //return File(fs, "application/pdf");


                        //string filepath = Server.MapPath("/PDF/Aptitude Formulas.pdf");
                        //byte[] pdfByte = GetBytesFromFile(filepath);
                        //return File(pdfByte, "application/pdf");

                        //string filepath = Server.MapPath(upload);
                        //byte[] pdfByte = GetBytesFromFile("~/UploadedFiles/" + filename);
                        //return File(pdfByte, "application/pdf");


                        SqlConnection con2 = new SqlConnection(ConnStr);
                        //string OrderId = Convert.ToString(form["OrderId"]);
                        string str2 = "select filePath,OrderId from TestFileUpload where OrderId='" + OrderId + "'";
                        SqlCommand cmdins2 = new SqlCommand(str2, con2);
                        //cmdins.CommandType = CommandType.StoredProcedure;
                        // cmdins.Parameters.AddWithValue("@pdfpath", getfile);
                        con2.Open();

                        DataTable dt2_ = new DataTable();
                        DataSet ds2_ = new DataSet();
                        SqlDataAdapter da2 = new SqlDataAdapter(cmdins2);
                        da2.Fill(ds2_);
                        //xlitdesc.Text = "Data Saved";
                        dt2_ = ds2_.Tables[0];

                        con2.Close();

                        List<FileModel> objFileList = new List<FileModel>();
                        if (dt2_.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2_.Rows.Count; i++)
                            {
                                objFileList.Add(new FileModel { OrderId = dt2_.Rows[i]["OrderId"].ToString(), FilePath = dt2_.Rows[i]["filePath"].ToString() });
                            }

                            objFileOrderModel.listFiles = objFileList;
                        }
                        objFileOrderModel.divFileUpload = true;
                        string A = "";

                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return View(objFileOrderModel);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }

                // ViewBag.Message = "The operation was cancelled!";
            }



            return View(objFileOrderModel);
        }

        public JsonResult Getdata(OrderModel orderModel)
        {
            OrderModel objOrderModel = new OrderModel();
            objOrderModel.VendorId = 100;

            return Json(objOrderModel, JsonRequestBehavior.AllowGet);


        }


    }
}
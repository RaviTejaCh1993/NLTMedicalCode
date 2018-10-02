using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLT.Controllers
{
    public class Test2Controller : Controller
    {
        // GET: Test2
        public ActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public JsonResult AutoComplete(string prefix)
        //{
        //    NorthwindEntities entities = new NorthwindEntities();
        //    var customers = (from customer in entities.Customers
        //                     where customer.ContactName.StartsWith(prefix)
        //                     select new
        //                     {
        //                         label = customer.ContactName,
        //                         val = customer.CustomerID
        //                     }).ToList();

        //    return Json(customers);
        //}

        //[HttpPost]
        //public ActionResult Index(string CustomerName, string CustomerId)
        //{
        //    ViewBag.Message = "CustomerName: " + CustomerName + " CustomerId: " + CustomerId;
        //    return View();
        //}

      
	 
        public JsonResult Index2(string prefix)
        {
            //SampleEntities entities = new SampleEntities();
            //var students = (from student in entities.studentsses
            //                where student.Name.StartsWith(prefix)
            //                select new
            //                {
            //                    label = student.Name,
            //                    val = student.id
            //                }).ToList();

           List<student> objstudentList = new List<student>();
            objstudentList.Add(new student { Id= "1", Name="Srinivas" });
            objstudentList.Add(new student { Id = "2", Name = "Sai" });
            objstudentList.Add(new student { Id = "3", Name = "Raj" });

            objstudentList.Add(new student { Id = "4", Name = "Praveen" });
            objstudentList.Add(new student { Id = "5", Name = "Pavan" });
            objstudentList.Add(new student { Id = "6", Name = "Pradeep" });

            return Json(objstudentList,JsonRequestBehavior.AllowGet);
        }


    }

    public class student
    {
        public string Id { set; get; }
        public string Name { set; get; }

    }
}
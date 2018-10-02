using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NLT.Models;

namespace NLT.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            List<MenuViewModel> menuViewModel = new List<MenuViewModel>();
            
            if(Session["Role"] !=null)
            {
                // menuViewModel.Add(new MenuViewModel { MenuID=1, Title="Signup" });
                MenuViewModel menu = new MenuViewModel() { MenuID = 1, Action = "dashboardView", Controller = "User", IsAction = true, Class = "class", SubMenu = null, Title = "Dashboard" };
                menuViewModel.Add(menu);


                if (Session["Role"].ToString() == "Admin")
                {

                    menu = new MenuViewModel() { MenuID = 2, Action = "States", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "States" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 4, Action = "Citys", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Citys" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 5, Action = "Pleases", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Pleases" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 6, Action = "Companys", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Companys" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 7, Action = "Departments", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Departments" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 8, Action = "Branches", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Branches" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 9, Action = "Specimens", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Specimens" };
                    menuViewModel.Add(menu);


                    menu = new MenuViewModel() { MenuID = 10, Action = "Tests", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "Tests" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 11, Action = "TestPrices", Controller = "Master", IsAction = true, Class = "class", SubMenu = null, Title = "TestPrices" };
                    menuViewModel.Add(menu);
                    

                    menu = new MenuViewModel() { MenuID = 12, Action = "Index", Controller = "Logout", IsAction = true, Class = "class", SubMenu = null, Title = "Logout" };
                    menuViewModel.Add(menu);
                }
                else if (Session["Role"].ToString() == "User")
                {
                    menu = new MenuViewModel() { MenuID = 2, Action = "OrderList", Controller = "Order", IsAction = true, Class = "class", SubMenu = null, Title = "Orders" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 3, Action = "UploadFile", Controller = "Reports", IsAction = true, Class = "class", SubMenu = null, Title = "Reports" };
                    menuViewModel.Add(menu);

                    menu = new MenuViewModel() { MenuID = 4, Action = "Index", Controller = "Logout", IsAction = true, Class = "class", SubMenu = null, Title = "Logout" };
                    menuViewModel.Add(menu);


                    //menu = new MenuViewModel() { MenuID = 3, IsAction = false, Class = "class", Link = "javascript:void(0);", Title = "My Network" };
                    //menuViewModel.Add(menu);

                    //menu.SubMenu = new List<MenuViewModel>();
                    //MenuViewModel subMenu = new MenuViewModel() { Action = "Index", Controller = "TreeView", IsAction = true, Class = "", SubMenu = null, Title = "Tree View" };
                    //menu.SubMenu.Add(subMenu);

                }

                return PartialView("_Navigation", menuViewModel);
            }
            else
            {
                return RedirectToAction("Test","User");
            }

            

        }

            //IEnumerable<NLT.Models.MenuViewModel> obj;
            //obj = menuViewModel;

            //object var = menuViewModel.ToList();

            //MenuViewModelNew obj = new MenuViewModelNew();
            //obj.IEMMenu = menuViewModel;

            // IList<menuViewModel> obj =new    menuViewModel;

            // return PartialView("_Navigation123");
            // return View(menu);

            // Partial View  Menu 
            //http://rizwanansari.net/navigation-menu-using-viewmodel-asp-net-mvc/
            // http://www.asparticles.com/2017/10/bind-menu-and-sub-menu-dynamically-in-mvc-from-database.html


        }

}
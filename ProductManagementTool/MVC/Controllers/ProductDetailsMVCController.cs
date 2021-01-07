using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.IO;
using RestSharp;
using PagedList;
using log4net;

namespace MVC.Controllers
{
    
    public class ProductDetailsMVCController : Controller
    {
        readonly ILog log = log4net.LogManager.GetLogger(typeof(ProductDetailsMVCController));
        private readonly ProductmanagmentDBEntities db = new Models.ProductmanagmentDBEntities();
        public ActionResult Welcome()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: ProductDetailsMVC
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Category" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = from s in db.ProductDetails
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString)
                                       || s.Category.Contains(searchString) || s.Short_Description.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "Price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case "Category":
                    products = products.OrderBy(s => s.Category);
                    break;
                case "Quantity":
                    products = products.OrderByDescending(s => s.Quantity);
                    break;
                default:  // Name ascending 
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AddorEdit(int id = 0)
        {
            var categoryList = new List<string>() { "Groceries", "COVID-19 Essentials", "Biscuits & cookies", "Fruits & Vegetables", "Atta Flour Sooji", "Shampoo & Conditioner" };
            var quantityList = new List<string>() { "1","2","3","4","5","6","7","8","9","10" };
            ViewBag.Categorylist = categoryList;
            ViewBag.Quantitylist = quantityList;
            if (id == 0)
            {
                return View(new ProductDetailMVC());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.webApiclient.GetAsync("ProductDetails/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<ProductDetailMVC>().Result);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit(ProductDetailMVC pd, HttpPostedFileBase[] file)
        {
            try
            {
                String[] path = new String[2];
                int i = 0;
                foreach (HttpPostedFileBase image in file)
                {
                   
                    if (image != null)
                    {
                        if (image.ContentLength <= 1024*1024*2)
                        {
                            string imagename = System.IO.Path.GetFileName(image.FileName);
                            path[i] = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(image.FileName));
                            System.Diagnostics.Debug.WriteLine(path[i]);
                            image.SaveAs(path[i]);

                            var client = new RestClient("http://localhost:59368/user/PostUserImage")
                            {
                                Timeout = -1
                            };
                            var request = new RestRequest(Method.POST);
                            request.AddFile("", path[i]);
                            IRestResponse response = client.Execute(request);
                            Console.WriteLine(response.Content);
                            System.IO.File.Delete(path[i]);
                            i++;
                        }
                        else
                        {
                            ModelState.AddModelError("", "2mb");
                            return View();
                        }
                        
                    }
                    

                }

                if (pd.ProductID == 0)
                {
                    if (file[1] == null)
                    {
                        ProductDetailMVC pdata = new ProductDetailMVC { ProductID = pd.ProductID, Name = pd.Name, Category = pd.Category, Price = pd.Price, Quantity = pd.Quantity, Short_Description = pd.Short_Description, Long_Description = pd.Long_Description, Small_Image_Path = "http://localhost:59368/Userimage/" + file[0].FileName, Large_Image_Path = "" };
                        HttpResponseMessage response = GlobalVariables.webApiclient.PostAsJsonAsync("ProductDetails", pdata).Result;
                        System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
                        System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result == "\"Product Exists\"");
                        if (response.Content.ReadAsStringAsync().Result == "\"Product Exists\"")
                        {
                            TempData["SuccessMessage"] = "This Product is Already added into the list.";
                        }
                        else
                        {
                            log.Info("User: Added " + pd.Name + " Product Successfully at " + DateTime.Now.ToString());
                            TempData["SuccessMessage"] = "Saved Successfully";
                        }
                    }
                    else
                    {
                        ProductDetailMVC pdata = new ProductDetailMVC { ProductID = pd.ProductID, Name = pd.Name, Category = pd.Category, Price = pd.Price, Quantity = pd.Quantity, Short_Description = pd.Short_Description, Long_Description = pd.Long_Description, Small_Image_Path = "http://localhost:59368/Userimage/" + file[0].FileName, Large_Image_Path = "http://localhost:59368/Userimage/" + file[1].FileName };
                        HttpResponseMessage response = GlobalVariables.webApiclient.PostAsJsonAsync("ProductDetails", pdata).Result;
                        log.Info("User: Added " + pd.Name + " Product Successfully at " + DateTime.Now.ToString());
                        TempData["SuccessMessage"] = "Saved Successfully";
                        if (response.Content.ReadAsStringAsync().Result == "\"Product Exists\"")
                        {
                            TempData["SuccessMessage"] = "This Product is Already added into the list.";
                        }
                        else
                        {
                            log.Info("User: Added " + pd.Name + " Product Successfully at " + DateTime.Now.ToString());
                            TempData["SuccessMessage"] = "Saved Successfully";
                        }
                    }
                    
                }
                else
                {
                    if (file[1] == null)
                    {
                        ProductDetailMVC pdata = new ProductDetailMVC { ProductID = pd.ProductID, Name = pd.Name, Category = pd.Category, Price = pd.Price, Quantity = pd.Quantity, Short_Description = pd.Short_Description, Long_Description = pd.Long_Description, Small_Image_Path = "http://localhost:59368/Userimage/" + file[0].FileName, Large_Image_Path = "" };
                        HttpResponseMessage response = GlobalVariables.webApiclient.PutAsJsonAsync("ProductDetails/" + pd.ProductID, pdata).Result;
                        log.Info("User: Updated " + pd.Name + " Product Successfully at " + DateTime.Now.ToString());
                        TempData["SuccessMessage"] = "Updated Successfully";
                    }
                    else
                    {
                        ProductDetailMVC pdata = new ProductDetailMVC { ProductID = pd.ProductID, Name = pd.Name, Category = pd.Category, Price = pd.Price, Quantity = pd.Quantity, Short_Description = pd.Short_Description, Long_Description = pd.Long_Description, Small_Image_Path = "http://localhost:59368/Userimage/" + file[0].FileName, Large_Image_Path = "http://localhost:59368/Userimage/" + file[1].FileName };
                        HttpResponseMessage response = GlobalVariables.webApiclient.PutAsJsonAsync("ProductDetails/" + pd.ProductID, pdata).Result;
                        log.Info("User: Updated " + pd.Name + " Product Successfully at " + DateTime.Now.ToString());
                        TempData["SuccessMessage"] = "Updated Successfully";
                    }
                    
                }

            }
            catch (Exception e)
            {
                log.Error("Exception:" + e.Message);
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _ = GlobalVariables.webApiclient.DeleteAsync("ProductDetails/" + id.ToString()).Result;
            log.Info("User: Deleted Product with ProductID:" + id.ToString() + " Successfully at " + DateTime.Now.ToString());
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
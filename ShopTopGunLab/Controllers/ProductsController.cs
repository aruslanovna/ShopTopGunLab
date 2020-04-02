using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

using ShopTopGunLab.SessionConfig;
using Microsoft.AspNetCore.Http;

namespace ShopTopGunLab.Controllers
{
   
    public class ProductsController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpContext _context;
        public ProductsController(IHttpContextAccessor contextAccessor)
        {
          
             _contextAccessor = contextAccessor;
            SetComplexData();
        }

        [Route("Products/SetComplexData")]
        public void SetComplexData()        
        {
            if (_contextAccessor.HttpContext.Session.Keys.Contains("ProductData") == true)
            {
                _contextAccessor.HttpContext.Session.GetList<List<Product>>("ProductData");
            }
            else
            {
                List<Product> productList = new List<Product>();
                Product user = new Product();
                user.Name = "Bread";
                user.Quantity = 5;
                productList.Add(user);
                _contextAccessor.HttpContext.Session.SetList("ProductData", productList);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void SetComplexData(Product product)
        {
            List<Product> productList = HttpContext.Session.GetList<List<Product>>("ProductData");
            Product product1 = new Product();
            product1.Name = product.Name;
            product1.Quantity = product.Quantity;
            product1.dimension = product.dimension;
            product1.ProductId = productList.Count();
            productList.Add(product1);
            HttpContext.Session.SetList("ProductData", productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public void UpdateComplexData(Product product)
        {
            
            RemoveComplexData(product.ProductId);
            List<Product> productList = HttpContext.Session.GetList<List<Product>>("ProductData");
            Product product1 = new Product();
           
            product1.ProductId = product.ProductId;
            product1.Name = product.Name;
            product1.Quantity = product.Quantity;
            product1.dimension = product.dimension;           
            productList.Add(product1);
            HttpContext.Session.SetList("ProductData", productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public void RemoveComplexData(int id)
        {
            List<Product> productList = HttpContext.Session.GetList<List<Product>>("ProductData");

           productList.Remove(productList.Where(x => x.ProductId == id).FirstOrDefault());

            HttpContext.Session.SetList("ProductData", productList);

           
        }
        [Route("Products/GetComplexData")]
        public IEnumerable<Product> GetComplexData()
        {
            IEnumerable<Product> productList =  HttpContext.Session.GetList<List<Product>>("ProductData");                    
            return productList;
        }
  
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewData["CurrentFilter"] = searchString;

            var productSearch = GetComplexData();
            if (!String.IsNullOrEmpty(searchString))
            {
                productSearch = productSearch.Where(s => s.Name.Contains(searchString));
                productSearch = productSearch.OrderBy(s => s.Name);
                return View(productSearch);
            }
            return View(GetComplexData());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var product = GetComplexData().FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                SetComplexData(product);
              
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = GetComplexData().FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Quantity")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UpdateComplexData(product);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        //[HttpGet]
        //public ActionResult Delete2(int id)
        //{
        //    var model = productData.Get(id, (List<Product>)Session["Products"]);

        //    if (model == null)
        //    {
        //        return View("NotFound");
        //    }

        //    return PartialView(model);
        //}

        //[HttpPost]
        //public ActionResult Delete2(int id, FormCollection form)
        //{
        //    List<Product> list = (List<Product>)Session["Products"];
        //    list.RemoveAll(l => l.Id == id);
        //    Session["Products"] = list;
        //    Session["Count"] = Convert.ToInt32(Session["Count"]) - 1;
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = GetComplexData().FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return PartialView(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           
            RemoveComplexData(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return GetComplexData().Any(e => e.ProductId == id);
        }
    }
}

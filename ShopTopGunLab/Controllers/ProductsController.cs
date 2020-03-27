using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

using ShopTopGunLab.SessionConfig;

namespace ShopTopGunLab.Controllers
{
   
    public class ProductsController : Controller
    {
      

        public ProductsController()
        {

           
        }
        List<Product> productList = new List<Product>();
        
        [Route("Products/SetComplexData")]
        public void SetComplexData()        
        {
            List<Product> productList = new List<Product>();
            Product user = new Product();
            user.Name = "Bread";
            user.Quantity = 5;
            productList.Add(user);
            HttpContext.Session.SetList("ProductData", productList);
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

        public void RemoveComplexData(int id)
        {
            List<Product> productList = HttpContext.Session.GetList<List<Product>>("ProductData");

           productList.Remove(productList.Where(x => x.ProductId == id).FirstOrDefault());

            //Product product1 = new Product();
            //product1.Name = product.Name;
            //product1.Quantity = product.Quantity;
            //product1.dimension = product.dimension;
            //product1.ProductId = product.ProductId;
            //productList.Remove(product1);
            HttpContext.Session.SetList("ProductData", productList);

           
        }
        [Route("Products/GetComplexData")]
        public IEnumerable<Product> GetComplexData()
        {
            IEnumerable<Product> productList =  HttpContext.Session.GetList<List<Product>>("ProductData");
                     
            return productList;
        }

        // GET: Products
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


        // GET: Products/Details/5
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

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
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

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Quantity")] Product product)
        //{
        //    if (id != product.ProductId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.ProductId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  GetComplexData().FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = GetComplexData().ToList();
            Product product = products.Where(product=>product.ProductId==id).First();
            
            RemoveComplexData(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return GetComplexData().Any(e => e.ProductId == id);
        }
    }
}

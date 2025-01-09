using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly OnlineShoppingContext _context;

        public ProductController(OnlineShoppingContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var onlineShoppingContext = _context.Product.Include(p => p.Category);
            return View(await onlineShoppingContext.ToListAsync());
        }
        public async Task<IActionResult> ProductDashboard(String? Title)
        {
            if (!string.IsNullOrEmpty(Title)){
                //kam garxa jaba samma response aaudaina till then wait garera basxa
             return View(await _context.Product.Where(p => p.Title.Contains(Title)).ToListAsync());

            }
            else {
                var onlineShoppingContext = _context.Product.Include(p => p.Category);
                return View(await onlineShoppingContext.ToListAsync());
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,CategoryId,ProductIcon")] Product product,IFormFile ProductIcon)
        {
            //if (ModelState.IsValid)
            //{
            //    Guid guid = Guid.NewGuid();
            //    string fileEx = ProductIcon.FileName.Substring(ProductIcon.FileName.LastIndexOf('.'));
            //    string path = Environment.CurrentDirectory + "/wwwroot/ProductImages/" + guid + fileEx;
            //    FileStream fileStream = new FileStream(path, FileMode.Create);
            //    await ProductIcon.CopyToAsync(fileStream);
            //    product.ProductIcon = guid + fileEx;

            //    _context.Add(product);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                string fileEx = ProductIcon.FileName.Substring(ProductIcon.FileName.LastIndexOf('.'));
                string path = Environment.CurrentDirectory + "/wwwroot/ProductImages/" + guid + fileEx;

                // Ensure the directory exists and if doesnt then creates thhe directory
                Directory.CreateDirectory(Environment.CurrentDirectory + "/wwwroot/ProductImages/");

                FileStream fileStream = new FileStream(path, FileMode.Create);
                await ProductIcon.CopyToAsync(fileStream);
                product.ProductIcon = guid + fileEx;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,CategoryId,ProductIcon")] Product product,IFormFile ProductIcon)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}

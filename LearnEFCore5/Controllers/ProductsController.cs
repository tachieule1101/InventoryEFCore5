using LearnEFCore5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEFCore5.Models;

namespace LearnEFCore5.Controllers
{
    public class ProductsController : Controller
    {
        //Thêm Controller action
        private readonly InventoryContext _context;
        public ProductsController(InventoryContext context)
        {
            _context = context;
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

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

        // POST: ProductsController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to. For 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Category,Color,UnitPrice,AvailableQuantity")] Products products) //Bind: liên kết các thuộc tính từ UI
        {
            //ModelState.IsValid kiểm tra người dùng nhập đúng theo quy tắc dữ liệu đã thêm vào
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Category,Color,UnitPrice,AvailableQuantity")] Products updatedProductDetails) //Bind: liên kết các thuộc tính từ UI
        {
            if (id != updatedProductDetails.ProductId)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.Name = updatedProductDetails.Name;
                    product.Category = updatedProductDetails.Category;
                    product.Color = updatedProductDetails.Color;
                    product.UnitPrice = updatedProductDetails.UnitPrice;
                    product.AvailableQuantity = updatedProductDetails.AvailableQuantity;

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(updatedProductDetails.ProductId))
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

            return View(updatedProductDetails);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(long id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}

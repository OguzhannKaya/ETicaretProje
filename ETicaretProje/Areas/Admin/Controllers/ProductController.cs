using ETicaret.Entities;
using ETicaret.Service.Abstract;
using ETicaretProje.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETicaretProje.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IService<Category> _serviceCat;

        public ProductController(IProductService service, IService<Category> serviceCat)
        {
            _service = service;
            _serviceCat = serviceCat;
        }

        // GET: ProductController
        public async Task<ActionResult> IndexAsync()
        {
            var urunler = await _service.GetCustomProductList();
            return View(urunler);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.CategoryId = new SelectList(await _serviceCat.GetAllAsync(),"Id","Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product product, IFormFile? Image1, IFormFile? Image2)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    product.Image1 = await ImageHelp.ImageLoaderAsync(Image1, "/Img/Products/");
                    product.Image2 = await ImageHelp.ImageLoaderAsync(Image2, "/Img/Products/");
                    await _service.AddAsync(product);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.CategoryId = new SelectList(await _serviceCat.GetAllAsync(), "Id", "Name");
            var product =  await _service.FindAsync(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product product, IFormFile? Image1, IFormFile? Image2)
        {
            try
            {
                if(Image1 != null)
                {
                    product.Image1 = await ImageHelp.ImageLoaderAsync(Image1, "/Img/Products/");
                }
                if(Image2 != null)
                {
                    product.Image2 = await ImageHelp.ImageLoaderAsync(Image2, "/Img/Products/");
                }
                _service.Update(product);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var product = await _service.FindAsync(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                _service.Delete(product);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using ETicaret.Entities;
using ETicaret.Service.Abstract;
using ETicaret.Service.Concrete;
using ETicaretProje.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETicaretProje.Areas.Admin.Controllers
{
    [Area("Admin") , Authorize(Policy ="AdminPolicy")]
    public class CategoryController : Controller
    {
        private readonly IService<Category> _service;

        public CategoryController(IService<Category> service)
        {
            _service = service;
        }

        // GET: CategoryController
        public async Task<ActionResult> IndexAsync()
        {
            var kategoriler = await _service.GetAllAsync();
            return View(kategoriler);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Category kategori,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    kategori.Image= await ImageHelp.ImageLoaderAsync(Image,"/Img/Categories/");
                    await _service.AddAsync(kategori);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştu");
                }
            }
            
            return View(kategori);
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var kategori = await _service.FindAsync(id);
            return View(kategori);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category kategori, IFormFile? Image)
        {
            
                try
                {
                    if(Image!=null)
                    {
                      kategori.Image = await ImageHelp.ImageLoaderAsync(Image,"/Img/Categories/");
                     }
                      _service.Update(kategori);
                      await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    
                }
                return View(kategori);
            
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var kategori = await _service.FindAsync(id);
            return View(kategori);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category kategori)
        {
            try
            {
                _service.Delete(kategori);
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

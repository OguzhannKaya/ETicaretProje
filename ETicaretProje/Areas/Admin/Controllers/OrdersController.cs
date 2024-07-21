using ETicaret.Entities;
using ETicaret.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETicaretProje.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy ="AdminPolicy")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;
        private readonly IService<User> _serviceUser;
        private readonly ICategoryService _categoryService;

        public OrdersController(IOrderService service, IService<User> serviceUser, ICategoryService categoryService)
        {
            _service = service;
            _serviceUser = serviceUser;
            _categoryService = categoryService;
        }

        // GET: OrdersController
        public async Task<ActionResult> IndexAsync()
        {
            var satis = await _service.GetCustomOrderListAsync();
            return View(satis);
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrdersController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(),"Id","Name");
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Orders order)
        {
            try
            {
                await _service.AddAsync(order);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Name");
            var satis = await _service.FindAsync(id);
            return View(satis);
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Orders order)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _service.Update(order);
                     await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
                
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Name");
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            return View(order);
        }

        // GET: OrdersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var satis = await _service.FindAsync(id);
            return View(satis);
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Orders order)
        {
            try
            {
                _service.Delete(order);
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

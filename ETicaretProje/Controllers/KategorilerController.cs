using ETicaret.Entities;
using ETicaret.Service.Abstract;
using ETicaret.Service.Concrete;
using ETicaretProje.Models;
using ETicaretProje.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.WebSockets;
using System.Security.Claims;

namespace ETicaretProje.Controllers
{
    public class KategorilerController : Controller
    {
        private readonly ICategoryService _service;
        private readonly IService<Category> _serviceCategory;
        private readonly IProductService _serviceProduct;
        private readonly IUserService _serviceUser;
        private readonly IOrderService _orderService;

        public KategorilerController(ICategoryService service, IService<Category> serviceCategory, IProductService serviceProduct, IUserService serviceUser, IOrderService orderService)
        {
            _service = service;
            _serviceCategory = serviceCategory;
            _serviceProduct = serviceProduct;
            _serviceUser = serviceUser;
            _orderService = orderService;
        }
        [Route("Kategoriler")]
        public async Task<IActionResult> Categories()
        {
            var model = await _service.GetCustomCategoryListAsync();
            return View(model);
        }
        public async Task<IActionResult> IndexAsync(int? id)
        {
            var category = await _service.GetCategorybyId(id.Value);
            var product = await _serviceProduct.GetCustomProduct(id.Value);
            var orderModel = new OrderFormViewModel();
            var userModel = new User();

            if (User.Identity.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                userModel = _serviceUser.Get(u => u.Email == email && u.UserGuid.ToString() == uguid);
                orderModel.User = userModel;
            }            
            
            orderModel.CategoryId = category.Id;

            var model = new MultiEntityViewModel()
            {
                Category = category,
                Product = product,
                Users = userModel
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Siparis(OrderFormViewModel model)
        {
            
                try
                {
                var cate = await _service.GetCategorybyId(model.CategoryId);
                    var user = model.User;
                    // Eğer kullanıcı zaten veritabanında varsa, user.Id'yi ayarlayın
                    var existingUser = await _serviceUser.GetUserByEmailAsync(user.Email);
                    if (existingUser != null)
                    {
                        user = existingUser;
                    }
                    else
                    {
                      TempData["Message"] = "<div class = 'alert alert-danger'>Üye Olmanız Gerekli. Teşekkürler..</div>";
                      return Redirect("/Account/Register");
                    }

                var order = new Orders
                {
                    UserId = user.Id,
                    Notes = model.Order.Notes,
                    Amount = model.Order.Amount,
                    DeliveryDate = model.Order.DeliveryDate,
                    OrderDate = DateTime.Now,
                    Adress = model.Order.Adress,
                    CategoryId = cate.Id,
                };
                    await _orderService.AddAsync(order);
                    await _orderService.SaveAsync();
                    //await MailHelper.SendMailAsync(model.User);
                    TempData["Message"] = "<div class = 'alert alert-success'>Talebiniz Alınmıştır. Teşekkürler..</div>";
                    return RedirectToAction("Categories");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            return View();
        }
    }
}

using ETicaret.Entities;
using ETicaret.Service.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<User> _service;
        private readonly IService<Role> _serviceRole;

        public LoginController(IService<User> service, IService<Role> serviceRole)
        {
            _service = service;
            _serviceRole = serviceRole;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email,string password)
        {
            try
            {
                var account = _service.Get(k=> k.Email == email && k.Password == password);
                if (account == null)
                {
                    TempData["Mesaj"] = "Giriş Başarısız";
                }
                else
                {
                    var role = _serviceRole.Get(r => r.Id == account.RoleId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Name),
                        
                    };
                    if (role != null)
                    {
                        claims.Add(new Claim("Role", role.Name));
                    }
                    var UIdentity = new ClaimsIdentity(claims,"Login");
                    ClaimsPrincipal principle = new ClaimsPrincipal(UIdentity);
                    await HttpContext.SignInAsync(principle);
                    return Redirect("/Admin");
                }
            }
            catch
            {
                TempData["Mesaj"] = "Hata Oluştu";
            }
            return View();
        }
    }
}

using ETicaret.Entities;
using ETicaret.Service.Abstract;
using ETicaretProje.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretProje.Controllers
{
    public class AccountController : Controller
    {
        public readonly IUserService _userService;
        public readonly IService<Role> _Roleservice;
        public AccountController(IUserService userService, IService<Role> roleservice)
        {
            _userService = userService;
            _Roleservice = roleservice;
        }
        [Authorize(Policy ="CustomerPolicy")]
        public IActionResult Index()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
            if(!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
            {
                var user = _userService.Get(u => u.Email ==email && u.UserGuid.ToString() == uguid);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult UserUpdate(User kullanici)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
                {
                    var user = _userService.Get(u => u.Email == email && u.UserGuid.ToString() == uguid);
                    if (user != null)
                    {
                        user.Name = kullanici.Name;
                        user.Surname = kullanici.Surname;
                        user.Email = kullanici.Email;
                        user.Phone = kullanici.Phone;
                        user.Password = kullanici.Password;
                        user.CreatedAt = kullanici.CreatedAt;
                        user.Id = user.Id;
                        user.RoleId = user.RoleId;
                        user.UserGuid = user.UserGuid;
                        _userService.Update(user);
                        _userService.Save();
                    }
                }
            }
            catch 
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rol = await _Roleservice.GetAsync(r => r.Name == "Customer");
                    if (rol == null)
                    {
                        ModelState.AddModelError("", "Kayıt Başarısız");
                        return View();
                    }
                    user.RoleId = rol.Id;
                    await _userService.AddAsync(user);
                    await _userService.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync( CustomerLoginViewModel cvm)
        {
            try
            {
                var account = await _userService.GetAsync(u => u.Email == cvm.Email && u.Password == cvm.Password);
                if(account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız");
                }
                else
                {
                    var role = _Roleservice.Get(r => r.Id == account.RoleId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Name),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.UserData, account.UserGuid.ToString()),

                    };
                    if (role != null)
                    {
                        claims.Add(new Claim("Role", role.Name));
                    }
                    var UIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principle = new ClaimsPrincipal(UIdentity);
                    await HttpContext.SignInAsync(principle);
                    if(role.Name=="Admin")
                    {
                        return Redirect("/Admin");
                    }
                    return Redirect("/Account");
                }
            }
            catch 
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}

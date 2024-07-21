using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Newtonsoft.Json;
using System.Security.Cryptography.Pkcs;
using System.Net.Http;
using System.Text;

namespace ETicaretProje.Controllers
{
    public class UserTestController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync("https://localhost:7241/api/Default");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(Class1 user)
        {
            var httpclient = new HttpClient();
            var jsonUser = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser,Encoding.UTF8,"application/json");
            var respongeMessage = await httpclient.PostAsync("https://localhost:7241/api/Default", content);
            if (respongeMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync("https://localhost:7241/api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonUser = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Class1>(jsonUser);
                return View(values);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(Class1 user)
        {
            var httpclient = new HttpClient();
            var jsonUser = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonUser, Encoding.UTF8,"application/json");
            var responseMessage = await httpclient.PutAsync("https://localhost:7241/api/Default/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(int id)
        {
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync("https://localhost:7241/api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }

    public class Class1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

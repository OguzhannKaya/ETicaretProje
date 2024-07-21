using ETicaretApi.Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult UserApiList()
        {
            using var c = new Context();
            var values = c.UsersApi.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult UserApiAdd(UserApi user)
        {
            using var c = new Context();
            c.Add(user);
            c.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult UserApiGet(int id)
        {
            using var c = new Context();
            var user = c.UsersApi.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult UserApiDelete(int id)
        {
            using var c = new Context();
            var user = c.UsersApi.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                c.Remove(user);
                c.SaveChanges();
                return Ok() ;
            }
        }
        [HttpPut]
        public IActionResult UserApiUpdate(UserApi user)
        {
            using var c = new Context();
            var usr = c.Find<UserApi>(user.Id);
            if (usr == null)
            {
                return NotFound();
            }
            else
            {
                usr.Name = user.Name;
                c.Update(usr);
                c.SaveChanges();
                return Ok();
            }
        }
    }
}

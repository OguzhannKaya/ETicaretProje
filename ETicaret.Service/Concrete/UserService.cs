using ETicaret.Data;
using ETicaret.Data.Concrete;
using ETicaret.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Service.Concrete
{
    public class UserService : UserRepository, IUserService
    {
        public UserService(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}

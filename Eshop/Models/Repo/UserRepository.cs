using Eshop.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models.Repo
{
    public class UserRepository
    {
        private readonly MyContext _context = MyContext.GetInstance();
        public User CheckIfUserExists(User user)
        {
            User result = _context.User.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            return result;
        }
    }
}
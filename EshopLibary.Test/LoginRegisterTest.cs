using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Eshop.Models.Repo;
using Eshop.Models.Tables;
namespace EshopLibary.Test
{
    public class LoginRegisterTest
    {
        private readonly UserRepository _repository = new UserRepository();
        [Fact]
        public void ValidationCheck()
        {
            User user = new User() { UserName = "Filip", Password = "bbb" };
            User result = _repository.CheckIfUserExists(user);
            Assert.NotNull(result);
        }
       
            



    }
}

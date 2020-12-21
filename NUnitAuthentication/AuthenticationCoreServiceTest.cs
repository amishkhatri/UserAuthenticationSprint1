using NUnit.Framework;
using xp.auth.core.integration.Interfaces;
using xp.auth.core.services;
using xp.auth.core.integration.Domain;
using xp.auth.console.ui;
using xp.auth.core.services.Validation;

namespace NUnitAuthentication
{
    public class AuthenticationCoreServiceTest
    {
        private readonly DbContextService _dbContextService;
        private readonly ConsoleAdaptor _consoleAdaptor;
        User _user;

       public  AuthenticationCoreServiceTest()
        {
            _dbContextService = new DbContextService();
            _consoleAdaptor = new ConsoleAdaptor(new Validator());
        }


        //invalid user
        [Test]
        public void ShouldValidateAddNewUserDev123()
        {

            _consoleAdaptor.User = new User
            {
                username = "Dev123",
                password = "test123",
                usertype = (int)UserRoleEnum.Developer
            };

             Assert.IsFalse(_consoleAdaptor.ValidateUser());

        }


        //valid user
        [Test]
        public void ShouldValidateAddNewUserTester()
        {

            _consoleAdaptor.User = new User
            {
                username = "Tester",
                password = "test123",
                usertype = (int)UserRoleEnum.Tester
            };

            Assert.IsTrue(_consoleAdaptor.ValidateUser());

        }

        // insert a new user with valid user name
        [Test]
        public void ShouldAddNewUserByAdmin()
        {
            bool result = _dbContextService.AddUser(new xp.auth.core.integration.Domain.User { username = "dev", password = "test123", usertype = (int)UserRoleEnum.Developer });

            if (result)
            {
                _user = _dbContextService.GetUser("dev");
                StringAssert.AreEqualIgnoringCase("dev", _user.username);
            }

        }

    }
}
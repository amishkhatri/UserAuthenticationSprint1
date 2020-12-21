using xp.auth.core.integration.Domain;
using xp.auth.core.integration.Interfaces;
using xp.auth.core.services;

namespace xp.auth.console.ui
{
    public class ConsoleAdaptor
    {
        
        private readonly IValidate Validator;
        private readonly DbContextService _dbContext;

        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _operationType;

        public string OperationType
        {
            get { return _operationType; }
            set { _operationType = value; }
        }

        public ConsoleAdaptor(IValidate validator)
        {
            Validator = validator;
            User = new User();
            _dbContext = new DbContextService();
            StartOperation();
        }

        public bool ValidateUser()
        {
            return Validator.Validate(this.User.username);
        }

        public void StartOperation()
        {
            try
            {
                System.Console.WriteLine("1.Add User");
                System.Console.WriteLine("Please enter your choice");
                this.OperationType = System.Console.ReadLine();

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public bool ValidateOperation()
        {
            return Validator.IsValidOpertation(this.OperationType);
        }

        public void GetUserDetails()
        {
            try
            {
                System.Console.WriteLine("Please enter user name");
                this.User.username = System.Console.ReadLine();

                System.Console.WriteLine("Please enter password");
                this.User.password = System.Console.ReadLine();

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public bool AddUser(bool IsAdmin)
        {
            bool result=false;
            try
            {
                //( userExists.username.Trim().ToUpper() != this.User.username.Trim().ToUpper())

                this.User.usertype = (!IsAdmin) ? (int) UserRoleEnum.Developer : (int) UserRoleEnum.Administrator;

                User userExists = _dbContext.GetUser(this.User.username);
                if (string.IsNullOrEmpty(userExists.username))
                {
                    result = _dbContext.AddUser(this.User);
                }                
                                
            }
            catch(System.Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}

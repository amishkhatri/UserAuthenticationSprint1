using System;
using xp.auth.core.services.Validation;

namespace xp.auth.console.ui
{

    public class ConsoleAuthernticationUI
    {
        [STAThread]
        private static void Main(string[] args)
        {

            ConsoleAdaptor adptor = new ConsoleAdaptor(new Validator());
            bool result;

            if (adptor.ValidateOperation())
            {
                adptor.GetUserDetails();

                if (adptor.ValidateUser())
                { 
                    result = adptor.AddUser(false); 

                    if (result)
                        Console.WriteLine("The user : " + adptor.User.username + " added successfully");
                    else
                        Console.WriteLine("Failed to add a new user");
                }
                  
                else
                    Console.WriteLine("Please enter a valid user name");
            }

            //   Console.WriteLine("Hello World!");
        }
    }
}

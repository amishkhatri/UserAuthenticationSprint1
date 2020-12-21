using xp.auth.core.integration.Interfaces;
using System.Text.RegularExpressions;
using System;

namespace xp.auth.core.services.Validation
{
    public class Validator : IValidate
    {        
        public static string userPattern = @"^[a-zA-Z]+$";

        private bool _isValid;

        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public bool Validate(string validate)
        {
            bool result = false;
            //string userPattern = @"^[a-zA-Z]+$";
            try
            {
                result = System.Text.RegularExpressions.Regex.IsMatch(validate, userPattern) ? true:false;               
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
             }
            return result;
        }


        bool IValidate.IsValidOpertation(string OperationType)
        {
            int readInput = 0;

            Func<string, bool> func1 = x => int.TryParse(x, out readInput);
            Func<int, bool> func2 = x => { return (x == 1 ); };
            return func1(OperationType) ;

        }
    }
}

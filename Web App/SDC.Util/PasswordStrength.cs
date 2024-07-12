using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace SystemSecurityUtil
{
    public class PasswordStrengthUtil
    {
        //Default Password Stregth Settings
        int _minLength = 6;
        bool _digitRequired = true;
        bool _lowerCaseRequired = true;
        bool _upperCaseRequired = true;
        bool _specialCharRequired = true;
        public PasswordStrengthUtil()
        {
        }
        public PasswordStrengthUtil(int minLength, bool digitRequired, bool lowerCaseRequired,
            bool upperCaseRequired, bool specialCharRequired)
        {
            _minLength = minLength;
            _digitRequired = digitRequired;
            _lowerCaseRequired = lowerCaseRequired;
            _upperCaseRequired = upperCaseRequired;
            _specialCharRequired = specialCharRequired;
        }
        #region Properties
        //Property for getting password strength validation 
        // errors
        private List<string> _validationErrors = new List<string>();
        public List<string> ValidationErrors
        {
            get { return _validationErrors; }
        }
        #endregion Properties
        #region Static Methods
        public static bool HasDigit(string password)
        {
            return Regex.IsMatch(password, @"(?=.*[\d])");
        }
        public static bool HasLowerCase(string password)
        {
            return Regex.IsMatch(password, @"(?=.*[a-z])");
        }
        public static bool HasUpperCase(string password)
        {
            return Regex.IsMatch(password, @"(?=.*[A-Z])");
        }
        public static bool HasSpecialChar(string password)
        {
            return Regex.IsMatch(password, @"(?=.*[\W])");
        }
        #endregion Static Methods
        public bool HasValidLengh(string password)
        {
            return (password.Length >= _minLength);
        }
        public bool IsStrong(string password)
        {
            if (_validationErrors != null)
                _validationErrors.Clear();
            if (!HasValidLengh(password))
                _validationErrors.Add("Invalid password length, minimum password length is " + _minLength.ToString());
            if (_digitRequired && !HasDigit(password))
                _validationErrors.Add("Password requires at least one digit");
            if (_lowerCaseRequired && !HasLowerCase(password))
                ValidationErrors.Add("Password requires at least one lower case character");
            if (_upperCaseRequired && !HasUpperCase(password))
                _validationErrors.Add("Password requires at least one upper case character");
            if (_specialCharRequired && !HasSpecialChar(password))
                _validationErrors.Add("Password requires at least one special character");
            if (_validationErrors.Count == 0)
                return true;
            else
                return false;
        }
    }
}

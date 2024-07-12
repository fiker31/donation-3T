using System.Collections.Generic;
namespace BLL.Framework
{
    #region EntValidationError
    /// <summary>
    /// This class contains the error message when a validation rule is broken
    /// A validation error object should be created when validating input from the user and 
    /// you want to display a message back to the user.
    /// </summary>
    public class EntValidationError
    {
        public EntValidationError() { }
        public string ErrorMessage { get; set; }
    }
    #endregion EntValidationError
    #region EntValidationErrors
    /// <summary>
    /// This class contains a list of validation errors.  This allows you to report back multiple errors.
    /// </summary>
    public class EntValidationErrors : List<EntValidationError>
    {
        public void Add(string errorMessage)
        {
            base.Add(new EntValidationError { ErrorMessage = errorMessage });
        }
    }
    #endregion ValidationErrors
}

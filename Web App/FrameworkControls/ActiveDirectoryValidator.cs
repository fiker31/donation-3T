using BLL.Framework;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrameworkControls
{
    public class ActiveDirectoryValidator : System.Web.UI.Page
    {
        private string _path;
        private string _filterAttribute;



        public ActiveDirectoryValidator(string path)
        {
            _path = path;
        }

        [Obsolete]
        public bool IsAuthenticated(string userName, string password)
        {
            string domainName = System.Configuration.ConfigurationSettings.AppSettings["DefaultActiveDirectoryServerIP"];

            string domainAndUsername = domainName + @"\" + userName;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + userName + ")";
                search.PropertiesToLoad.Add("SAMAccountName");
                //search.PropertiesToLoad.Add("employeeID");
                search.PropertiesToLoad.Add("description");
                search.PropertiesToLoad.Add("name");
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("title");
                search.PropertiesToLoad.Add("department");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("company");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];

                UserAccountBO userAccountBO = new UserAccountBO();


                string FullName = (String)result.Properties["name"][0];
                string[] Names = FullName.Split(null);
                userAccountBO.UserAccountName = (String)result.Properties["SAMAccountName"][0];
                userAccountBO.FirstName = Names[0];
                userAccountBO.LastName = Names[1];
                userAccountBO.Position = (String)result.Properties["title"][0];
                userAccountBO.Department = (String)result.Properties["department"][0];
                userAccountBO.Email = (String)result.Properties["mail"][0];
                userAccountBO.BranchCode = (String)result.Properties["company"][0];

                userAccountBO.UserPassword = "Unchanged";
                userAccountBO.UserConfirmPassword = "Unchanged";
                userAccountBO.PasswordChangeDate = DateTime.Now;
                userAccountBO.IsNewPassword = false;

                userAccountBO.IsActive = true;
                userAccountBO.IsLocked = false;

                EntValidationErrors validationErrors = new EntValidationErrors();


                bool User = userAccountBO.Load(userAccountBO.UserAccountName);
                if (userAccountBO.Load(userAccountBO.UserAccountName) == false)
                {
                    userAccountBO.Save(ref validationErrors, userAccountBO.UserAccountName);
                }

                Session["LoginId"] = userName;
                Session["UserId"] = userName;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}

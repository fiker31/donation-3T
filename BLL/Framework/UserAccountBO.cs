using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SystemSecurityUtil;
using DAL;
using DAL.Framework;
namespace BLL.Framework
{
    #region UserAccountBO
    [Serializable()]
    public class UserAccountBO : BaseEO
    {
        public UserAccountBO()
        {
            Roles = new RoleBOList();
        }
        #region Properties
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public string UserAccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { private get; set; }
        public bool IsNewPassword { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public bool IsLocked { get; set; }
        public byte PasswordAttempt { get; set; }
        public string BranchCode { get; set; }
        public RoleBOList Roles { get; private set; }
        //public int passwordlength { get; set; }//Used to get the length of the user password before encription.
        #endregion Properties
        #region Overrides
        public override bool Load(string userAccountName)
        {
            EntUserAccount userAccount = new UserAccountData().Select(userAccountName);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Load(long id)
        {
            EntUserAccount userAccount = new UserAccountData().Select(id);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Save(DBDataContext db, ref EntValidationErrors validationErrors, string userAccountId)
        {
            if (DBAction == DBActionEnum.Insert || DBAction == DBActionEnum.Update)
            {
                //Validate the object
                Validate(db, ref validationErrors);
                //Check if there were any validation errors
                if (validationErrors.Count == 0)
                {
                    if (DBAction == DBActionEnum.Insert)
                    {
                        //Add
                        Id = new UserAccountData().Insert(db, UserAccountName, FirstName, LastName, Position, Department, Email, IsActive, UserPassword, BranchCode, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new UserAccountData().Update(db, Id, UserAccountName, FirstName, LastName, Position, Department, Email, IsActive, UserPassword, BranchCode, IsNewPassword, PasswordChangeDate, PasswordAttempt, IsLocked, InsertUserId, EntryDate, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //Didn't pass validation.
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not save.");
            }
        }
        protected override void Validate(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            UserAccountData userAccountData = new UserAccountData();
            SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
            //User Account Name is required.
            if (BranchCode.Trim().Length == 0 || BranchCode == "0")
            {
                validationErrors.Add("Branch name is required.");
            }  //User Account Name is required.
            else if (UserAccountName.Trim().Length == 0)
            {
                validationErrors.Add("User account name is required.");
            }
            //User Account Name length.
            else if (UserAccountName.Trim().Length < sp.LoginIdMinLength)
            {
                validationErrors.Add("The length of user account name can not be less than " + sp.LoginIdMinLength.ToString() + ".");
            }
            ////The User account name must be unique.
            else if (userAccountData.IsDuplicateUserAccountName(db, Id, UserAccountName))
            {
                validationErrors.Add("User account name must be unique.");
            }
            //TODO: VV Validate agains AD
            //First name, last name, and email are required.
            else if (FirstName.Trim().Length == 0)
            {
                validationErrors.Add("First name is required.");
            }
            //Last Name is required
            else if (LastName.Trim().Length == 0)
            {
                validationErrors.Add("Last name is required.");
            }
            //User Account Password is required.
            else if (UserPassword == null || UserPassword.Trim().Length == 0)
            {
                validationErrors.Add("User account password is required.");
            }
            //User Account Password lenght <=6.
            //if (passwordlength  < SysParmBO.MinPasswordLen)
            //{
            //    validationErrors.Add("Password length must be greater than " + SysParmBO.MinPasswordLen );
            //}
            //User Account Password confirmation.
            else if (UserPassword != UserConfirmPassword)
            {
                validationErrors.Add("Password and Confirm Passwords are not the same.");
            }
            //Email is required
            //if (Email.Trim().Length == 0)
            //{
            //    validationErrors.Add("The email address is required.");
            //}
            //else
            //{
            //    if (entUserAccountData.IsDuplicateEmail(db, ID, Email))
            //    {
            //        validationErrors.Add("The email address must be unique.");
            //    }
            //    else
            //    {
            //        if (Email.IndexOf("@") < 0)
            //        {
            //            validationErrors.Add("The email address must contain the @ sign.");
            //        }
            //        else
            //        {
            //            string[] emailParts = Email.Split(new char[] { '@' });
            //            if ((emailParts.Length != 2) ||
            //               (emailParts[0].Length < 2) ||
            //               (emailParts[1].ToUpper() != "POWEREDBYV2.COM"))
            //            {
            //                validationErrors.Add("The email address must be in the format XX@V2.com");
            //            }
            //        }
            //    }
            //}
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            throw new NotImplementedException();
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }
        public override void Init()
        {
            IsActive = true;
            IsNewPassword = true;
            UserConfirmPassword = "";
            UserPassword = "";
            IsLocked = false;
            PasswordAttempt = 0;
        }
        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            EntUserAccount userAccount = (EntUserAccount)entity;
            Id = userAccount.UserAccountId;
            UserAccountName = userAccount.UserAccountName;
            FirstName = userAccount.FirstName;
            LastName = userAccount.LastName;
            Position = userAccount.Position;
            Department = userAccount.Department;
            UserFullName = userAccount.FirstName + " " + userAccount.LastName;
            Email = userAccount.Email;
            IsActive = userAccount.IsActive;
            UserPassword = userAccount.UserPassword;
            UserConfirmPassword = userAccount.UserPassword;
            PasswordChangeDate = userAccount.PWDChangeDate;
            IsNewPassword = userAccount.IsNewPassword;
            PasswordAttempt = userAccount.PAsswordAttemptNo;
            IsLocked = userAccount.IsLocked;
            BranchCode = userAccount.BranchID.ToString();
        }
        protected override string GetDisplayText()
        {
            return FirstName + " " + LastName;
        }
        #endregion Overrides
        #region Public Methods
        /// <summary>
        /// The capabilities are least restrictive.  If a user is in more then one role and one has edit and the other is read only
        /// then edit is returned.
        /// </summary>
        /// <param name="capabilityId"></param>
        /// <param name="rolesWithCapabilities"></param>
        /// <returns></returns>
        public RoleCapabilityBO.CapabiiltyAccessFlagEnum GetCapabilityAccess(long capabilityId, RoleBOList rolesWithCapabilities)
        {
            RoleCapabilityBO.CapabiiltyAccessFlagEnum retVal = RoleCapabilityBO.CapabiiltyAccessFlagEnum.None;
            //The roles in the user object do not include the capabilities.
            foreach (RoleBO role in Roles)
            {
                RoleBO roleWithCapabilities = rolesWithCapabilities.GetByRoleId(role.Id);
                foreach (RoleCapabilityBO capability in roleWithCapabilities.RoleCapabilities)
                {
                    if (capability.Capability.Id == capabilityId)
                    {
                        if (capability.AccessFlag == RoleCapabilityBO.CapabiiltyAccessFlagEnum.Edit)
                        {
                            return RoleCapabilityBO.CapabiiltyAccessFlagEnum.Edit;
                        }
                        else if (capability.AccessFlag == RoleCapabilityBO.CapabiiltyAccessFlagEnum.ReadOnly)
                        {
                            //Since this is least restrictive temporarirly set the return value to read only.
                            retVal = RoleCapabilityBO.CapabiiltyAccessFlagEnum.ReadOnly;
                        }
                        else if (capability.AccessFlag == RoleCapabilityBO.CapabiiltyAccessFlagEnum.EditWithDelete)
                        {
                            //Since this is least restrictive temporarirly set the return value to read only.
                            retVal = RoleCapabilityBO.CapabiiltyAccessFlagEnum.EditWithDelete;
                        }
                    }
                }
            }
            return retVal;
        }
        #region Old Change Password
        //public bool ChangePassword(ref EntValidationErrors validationErrors, string newPassword, string confirmPassword,string oldPassword, string userAccountId, bool forReset)
        //{
        //    DBAction = DBActionEnum.Update;
        //    PasswordChangeDate = DateTime.Now;
        //    IsNewPassword = forReset;
        //    UserPassword = newPassword;
        //    UserConfirmPassword = confirmPassword;
        //    PasswordHistoryData ph = new PasswordHistoryData(SecurityParmBO.PasswordHistory);
        //    if (!forReset && IsPasswordUsedByUser(newPassword))
        //    {
        //        validationErrors.Add("Invalid password! This password is previously used by the user.");
        //        return false;
        //    }
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        //Create a connection
        //        using (SMSDataContext db = new SMSDataContext(DBHelper.GetCreditDBConnectionString()))
        //        {
        //            //Update the User account
        //            if (Save(db, ref validationErrors, userAccountId))
        //            {
        //                //If the password is not resseted by the administrator and
        //                //Password history record is required then maintain the password history
        //                if (!forReset && SecurityParmBO.PasswordHistory > 0)
        //                {
        //                    //Insert to the password history
        //                    if (ph.Insert(db, Id, oldPassword, userAccountId) == 0)
        //                    {
        //                        validationErrors.Add("Password history record not saved");
        //                        return false;
        //                    }
        //                    //Delete history records that are not required.
        //                    ph.DeleteInactiveHistories(db, Id);
        //                }
        //                ts.Complete();
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}
        #endregion Old Change Password
        public bool ChangePassword(ref EntValidationErrors validationErrors, string newPassword, string confirmPassword, string oldPassword, string userAccountId, bool forReset)
        {
            DBAction = DBActionEnum.Update;
            PasswordChangeDate = DateTime.Now;
            IsNewPassword = forReset;
            UserPassword = newPassword;
            UserConfirmPassword = confirmPassword;
            SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
            SystemSecurityDAC ph = new SystemSecurityDAC(sp.PasswordHistory);
            if (!forReset && IsPasswordUsedByUser(newPassword))
            {
                validationErrors.Add("Invalid password! This password is previously used by the user.");
                return false;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                //Create a connection
                using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
                {
                    //Update the User account
                    if (Save(db, ref validationErrors, userAccountId))
                    {
                        //If the password is not resseted by the administrator and
                        //Password history record is required then maintain the password history
                        if (!forReset && sp.PasswordHistory > 0)
                        {
                            //Insert to the password history
                            if (ph.InsertPasswordHistory(Id, oldPassword, userAccountId) == false)
                            {
                                validationErrors.Add("Password history record not saved");
                                return false;
                            }
                            //Delete history records that are not required.
                            ph.DeleteInactiveHistories(Id);
                        }
                        ts.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool Lock(ref EntValidationErrors validationErrors)
        {
            DBAction = DBActionEnum.Update;
            IsLocked = true;
            return Save(ref validationErrors, UserAccountName);
        }
        public bool UnLock(ref EntValidationErrors validationErrors, string userAccountId)
        {
            DBAction = DBActionEnum.Update;
            PasswordAttempt = 0;
            IsLocked = false;
            return Save(ref validationErrors, userAccountId);
        }
        public bool RessetPasswordAttempt(ref EntValidationErrors validationErrors)
        {
            DBAction = DBActionEnum.Update;
            if (PasswordAttempt > 0)
            {
                PasswordAttempt = 0;
                return Save(ref validationErrors, UserAccountName);
            }
            else
            {
                return true;
            }
        }
        public bool IncrementPasswordAttempt(ref EntValidationErrors validationErrors)
        {
            SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
            DBAction = DBActionEnum.Update;
            PasswordAttempt++;
            if (PasswordAttempt > sp.NoOfUnsuccessfulAttempts)
                IsLocked = true;
            return Save(ref validationErrors, UserAccountName);
        }
        public bool IsPasswordExpired()
        {
            SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
            if (sp.PasswordInterval == 0)
                return false;
            else
            {
                return (DateTime.Compare(DateTime.Today, PasswordChangeDate.AddDays(sp.PasswordInterval)) > 0);
            }
        }
        public long UpdateUserLogInHistory(string userHostName)
        {
            return new LogInOutBO().UpdateUserLogInHistory(UserAccountName, userHostName);
        }
        public bool UpdateLogOutTime(long logInOutId)
        {
            return new LogInOutBO().UpdateLogOutTime(logInOutId);
        }
        #endregion Public Methods
        private bool IsPasswordUsedByUser(string password)
        {
            SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
            if (sp.PasswordHistory > 0)
            {
                PasswordHistory ph = SystemSecurityDAC.DbHasSecuritySetupTables() ?
                    SystemSecurityDAC.GetPasswordHistoryObject(Id, password) : SystemSecurityDAC.GetEmpityPasswordHistoryObject(Id, password);
                return (ph != null);
            }
            else
            {
                return false;
            }
        }
        #region Static Methods
        public static long GetUserIdByUserName(string useraccountname)
        {
            return UserAccountData.GetUserIdByUserName(useraccountname);
        }
        #endregion Static Methods
        //internal bool IsValidLogin(ref EntValidationErrors validationErrors, string userAccountName, string userPassword )
        //{
        //    EntUserAccount userAccount = new UserAccountData().Select(userAccountName);
        //    if (userAccount != null)
        //    {
        //        if (userAccount.UserPassword != userPassword)
        //        {
        //            validationErrors.Add("Invalid Password");
        //            return false;
        //        }
        //        if (userAccount.i != userPassword)
        //        {
        //            validationErrors.Add("Invalid Password");
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        validationErrors.Add("Invalid User/Login Id");
        //        return false;
        //    }
        //    return true;
        //}
    }
    #endregion UserAccountBOList
    #region UserAccountBOList
    [Serializable()]
    public class UserAccountBOList : BaseEOList<UserAccountBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new UserAccountData().Select());
        }
        #endregion Overrides
        #region Private Methods
        protected void LoadFromList(List<EntUserAccount> users)
        {
            foreach (EntUserAccount user in users)
            {
                UserAccountBO newUserAccountEO = new UserAccountBO();
                newUserAccountEO.MapEntityToProperties(user);
                this.Add(newUserAccountEO);
            }
        }
        #endregion Private Methods
        #region Public Methods
        public void LoadWithRoles()
        {
            Load();
            foreach (UserAccountBO user in this)
            {
                user.Roles.LoadByUserAccountId(user.Id);
            }
        }
        public UserAccountBO GetByUserAccountName(string userAccountName)
        {
            return this.SingleOrDefault(u => u.UserAccountName.ToUpper() == userAccountName.ToUpper());
        }
        public UserAccountBO GetByID(long id)
        {
            return this.SingleOrDefault(u => u.Id == id);
        }
        public void Load(string userAccountName, string firstName, string lastName)
        {
            LoadFromList(new UserAccountData().Select(userAccountName, firstName, lastName));
        }
        #endregion Public Methods
        //public void LoadByWFOwnerGroupId(int entWFOwnerGroupId)
        //{
        //    LoadFromList(new ENTUserAccountData().SelectByWFOwnerGroupId(entWFOwnerGroupId));
        //}
    }
    #endregion UserAccountBOList
}

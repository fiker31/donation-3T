using DAL;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL.Framework
{
    public class UserAccountData : BaseData<EntUserAccount>
    {
        #region Overrides
        public override List<EntUserAccount> Select()
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntUserAccount> UA = (from ua in db.EntUserAccounts
                                           select ua).ToList();
                return UA;
            }
        }
        public override EntUserAccount Select(long id)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var UA = (from ua in db.EntUserAccounts
                          where ua.UserAccountId == id
                          select ua);
                if (UA.Count() != 0)
                {
                    return UA.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public override EntUserAccount Select(string userAccountName)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var UA = (from ua in db.EntUserAccounts
                          where ua.UserAccountName == userAccountName
                          select ua);
                if (UA.Count() != 0)
                {
                    return UA.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public override void Delete(AppDataContext db, long id, Binary version)
        {
            //Create User Account object
            EntUserAccount UA = new EntUserAccount();
            UA.UserAccountId = id;
            UA.Version = version;
            db.EntUserAccounts.Attach(UA);
            db.EntUserAccounts.DeleteOnSubmit(UA);
            db.SubmitChanges();
        }
        public override void Delete(AppDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string userAccountName, string firstName,
            string lastName, string Position, string Department, string email, bool isActive, string userPassword, string branchCode, string userId)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return Insert(db, userAccountName, firstName,
                        lastName, Position, Department, email, isActive, userPassword, branchCode, userId);
            }
        }
        public long Insert(AppDataContext db, string userAccountName, string firstName,
            string lastName, string Position, string Department, string email, bool isActive, string userPassword, string branchCode, string userId)
        {
            //Create a new User Account object
            EntUserAccount UA = new EntUserAccount
            {
                UserAccountName = userAccountName,
                FirstName = firstName,
                LastName = lastName,
                Position = Position,
                Department = Department,
                Email = email,
                IsActive = isActive,
                UserPassword = userPassword,
                BranchID = branchCode,
                IsNewPassword = true,
                PWDChangeDate = DateTime.Now,
                IsLocked = false,
                PAsswordAttemptNo = 0,
                InsertUserId = userId,
                EntryDate = DateTime.Now,
                UpdateUserId = userId,
                UpdateDate = DateTime.Now
            };
            //save the record to the object model
            db.EntUserAccounts.InsertOnSubmit(UA);
            //send changes to the database
            db.SubmitChanges();
            return UA.UserAccountId;
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long id, string userAccountName, string firstName,
            string lastName, string Position, string Department, string email, bool isActive, string userPassword, string branchCode, bool isNewPassord, DateTime passwordChangeDate,
            byte passwordAttempt, bool isLocked, string insertUserId, DateTime entryDate, string userId, Binary version)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return Update(db, id, userAccountName, firstName, lastName, Position, Department, email, isActive, userPassword, branchCode, isNewPassord, passwordChangeDate,
                    passwordAttempt, isLocked, insertUserId, entryDate, userId, version);
            }
        }
        public bool Update(AppDataContext db, long id, string userAccountName, string firstName,
            string lastName, string Position, string Department, string email, bool isActive, string userPassword, string branchCode, bool isNewPassord, DateTime passwordChangeDate,
            byte passwordAttempt, bool isLocked, string insertUserId, DateTime entryDate, string userId, Binary version)
        {
            //Create a new User Account object
            EntUserAccount UA = new EntUserAccount
            {
                UserAccountId = id,
                UserAccountName = userAccountName,
                FirstName = firstName,
                LastName = lastName,
                Position = Position,
                Department = Department,
                Email = email,
                IsActive = isActive,
                UserPassword = userPassword,
                BranchID = branchCode,
                IsNewPassword = isNewPassord,
                PWDChangeDate = passwordChangeDate,
                IsLocked = isLocked,
                PAsswordAttemptNo = passwordAttempt,
                InsertUserId = insertUserId,
                EntryDate = entryDate,
                UpdateUserId = userId,
                UpdateDate = DateTime.Now,
                Version = version
            };
            //save the record to the object model
            db.EntUserAccounts.Attach(UA, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Static Methods
        public static long GetUserIdByUserName(string useraccountname)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from ua in db.EntUserAccounts
                          where ua.UserAccountName == useraccountname
                          select ua.UserAccountId);
                if (TD.Count() != 0)
                {
                    return TD.Single();
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion Static Methods
        #region Utility Methods
        /// <summary>
        /// Checks to see that window account name is unique.
        /// </summary>        
        /// <returns>Returns true if the windows account name is already in database</returns>
        public bool IsDuplicateUserAccountName(AppDataContext db, long userAccountId, string userAccountName)
        {
            return IsDuplicate(db, "EntUserAccount", "UserAccountName", "UserAccountId", userAccountName, userAccountId);
        }
        public bool IsDuplicateEmail(AppDataContext db, int userAccountId, string email)
        {
            return IsDuplicate(db, "EntUserAccount", "email", "UserAccountId", email, userAccountId);
        }
        public List<EntUserAccount> Select(string userAccountName, string firstName, string lastName)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntUserAccount> UA = (from ua in db.EntUserAccounts
                                           where (ua.UserAccountName.StartsWith(userAccountName))
                                           && (firstName.ToString() != "" ? ua.FirstName.StartsWith(firstName) : true)
                                           && (lastName.ToString() != "" ? ua.LastName.StartsWith(lastName) : true)
                                           select ua).ToList();
                return UA;
            }
        }
        public EntUserAccount SelectUserByUserId(long UserAccountId)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                EntUserAccount UA = (from ua in db.EntUserAccounts
                                     where ua.UserAccountId == UserAccountId
                                     select ua).FirstOrDefault();
                return UA;
            }
        }
        public EntUserAccount SelectUserByUserAccountName(string UserAccountName)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                EntUserAccount UA = (from ua in db.EntUserAccounts
                                     where ua.UserAccountName == UserAccountName
                                     select ua).FirstOrDefault();
                return UA;
            }
        }
        public List<EntUserAccount> SelectAllUser()
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntUserAccount> UA = (from ua in db.EntUserAccounts
                                           select ua).ToList();
                return UA ?? null;
            }
        }
        #endregion Utility Methods
    }
}

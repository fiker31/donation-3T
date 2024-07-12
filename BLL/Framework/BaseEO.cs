using DAL;
using DAL.Framework;
using System;
using System.Transactions;
namespace BLL.Framework
{
    /// <summary>
    /// This class is the base for any BLL class that will perform insert, update, or delete actions on a table.
    /// </summary>    
    [Serializable()]
    public abstract class BaseEO : BaseBO
    {
        #region Enumerations
        public enum DBNameEnum
        {
            Gym_Default,
            Gym_Migrate,
            Gym_TS
        }
        public enum DBActionEnum
        {
            Insert,
            Update,
            Delete
        }
        #endregion Enumerations
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEO()
            : base()
        {
            //Default the action to save.
            DBName = DBNameEnum.Gym_Default;
        }
        #endregion Constructor
        #region Properties
        public DBActionEnum DBAction { get; set; }
        public DBNameEnum DBName { get; set; }
        #endregion Properties
        #region Abstract Methods
        /// <summary>
        /// This method will add or update a record.
        /// </summary>
        public abstract bool Save(DBDataContext db, ref EntValidationErrors validationErrors, string userAccountId);
        /// <summary>
        /// This method validates the object's data before trying to save the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void Validate(DBDataContext db, ref EntValidationErrors validationErrors);
        /// <summary>
        /// This should call the business object's data class to delete the record.  The only method that should call this 
        /// is the virtual method "Delete(SqlTransaction tn, ref ValidationErrorAL validationErrors, int id)" in this class.
        /// </summary>
        protected abstract void DeleteForReal(DBDataContext db);
        /// <summary>
        /// This method validates the object's data before trying to delete the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors);
        /// <summary>
        /// This will load the object with the default properties.
        /// </summary>
        public abstract void Init();
        #endregion Abstratct Methods
        #region Protected\Public Methods
        /*public bool IsNewRecord()
        {
            return ID == 0;
        }*/
        /// <summary>
        /// This is used to save a record and start a new transaction.
        /// The implementor of BaseEO needs to create their own Save method that expects the 
        /// transaction to be passed in.
        /// </summary>
        public bool Save(ref EntValidationErrors validationErrors, string userAccountId)
        {
            if (DBAction == DBActionEnum.Insert || DBAction == DBActionEnum.Update)
            {
                //Set Databse Action mode to insert or update Based  on the value of
                //input parameter IsNewRecord
                //DBAction = IsNewRecord ? DBActionEnum.Insert : DBActionEnum.Update;
                // Begin database transaction
                using (TransactionScope ts = new TransactionScope())
                {
                    // Create connection 
                    using (DBDataContext db = new DBDataContext(GetConnectionString()))
                    {
                        //Now save the record
                        if (this.Save(db, ref validationErrors, userAccountId))
                        {
                            // Commit transaction if update was successful
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
            else
            {
                throw new Exception("DBAction not Save.");
            }
        }
        /// <summary>
        /// This method will connect to the database and start a transaction.
        /// </summary>
        public bool Delete(ref EntValidationErrors validationErrors, string userAccountId)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                // Begin database transaction
                using (TransactionScope ts = new TransactionScope())
                {
                    // Create connection
                    using (DBDataContext db = new DBDataContext(GetConnectionString()))
                    {
                        this.Delete(db, ref validationErrors, userAccountId);
                        if (validationErrors.Count == 0)
                        {
                            //Commit transaction since the delete was successful
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            //Rollback since the delete was not successful
                            return false;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }
        /// <summary>
        /// Deletes the record.
        /// </summary>
        internal virtual bool Delete(DBDataContext db, ref EntValidationErrors validationErrors, string userAccountId)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                //Check if this record can be deleted.  There may be referential integrity rules preventing it from being deleted                
                ValidateDelete(db, ref validationErrors);
                if (validationErrors.Count == 0)
                {
                    this.DeleteForReal(db);
                    return true;
                }
                else
                {
                    //The record can not be deleted.
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }
        protected void UpdateFailed(ref EntValidationErrors validationErrors)
        {
            validationErrors.Add("This record was updated by someone else while you were editing it.  Your changes were not saved.  Click the Cancel button and enter this screen again to see the changes.");
        }
        protected void DBActionFailed(ref EntValidationErrors validationErrors)
        {
            validationErrors.Add("Data Base Action Failed! Your changes were not saved. ");
        }
        protected void PrimaryKeyUpdateFailed(ref EntValidationErrors validationErrors, string PKeyName)
        {
            validationErrors.Add("The record was updated! But Your " + PKeyName + " change was not saved. ");
        }
        #endregion Protected Methods
        #region Private Methods
        private string GetConnectionString()
        {
            return DBHelper.GetCreditDBConnectionString();
        }
        #endregion Private Methods
    }
}
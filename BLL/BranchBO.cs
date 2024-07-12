using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region CBG_CategoryBO
    [Serializable()]
    public class BranchBO : BaseEO
    {
        public BranchBO()
        {
        }
        #region Properties
        public long ID { get; set; }
        public string Name { get; set; }
        public long DistrictID { get; set; }
        public string T24Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Branch td = new BranchData().Select(id);
            if (td != null)
            {
                MapEntityToProperties(td);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Load(string id)
        {
            throw new NotImplementedException();
        }
        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            Branch td = (Branch)entity;
            ID = td.ID;
            Name = td.Name;
            DistrictID = (long)td.DistrictID;
            T24Code = td.T24Code;
            Address = td.Address;
            Contact = td.Contact;
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
                        ID = new BranchData().Insert(db, Name, DistrictID, T24Code, Address, Contact, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new BranchData().Update(db, ID, Name, DistrictID, T24Code, Address, Contact, InsertUserId, userAccountId, EntryDate, Version))
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
                throw new Exception("DBAction not Save.");
            }
        }
        protected override void Validate(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            if (Name.ToString().Trim().Length <= 0)
            {
                validationErrors.Add("Branch name is required!");
            }
            if (DistrictID.ToString().Trim().Length <= 0)
            {
                validationErrors.Add("District Name is required!");
            }
            if (T24Code.ToString().Trim().Length <= 0)
            {
                validationErrors.Add("T24Code  is required!");
            }
            if (Address.ToString().Trim().Length <= 0)
            {
                validationErrors.Add(" Address is required!");
            }
            if (Contact.ToString().Trim().Length <= 0)
            {
                validationErrors.Add("Contact is required!");
            }
            if (DBAction == DBActionEnum.Insert && new BranchData().IsDuplicateEntry(Name))
            {
                validationErrors.Add("Title name is previously registered. Please use edit window to edit the record.");
            }
            int numericValue;
            if (int.TryParse(Name, out numericValue))
            {
                validationErrors.Add("Title name can not be numeric value.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new BranchData().Delete(db, ID, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return Name;
        }
        #endregion Overrides
    }
    #endregion DistrictBO
    #region DistrictBOList
    [Serializable()]
    public class BranchBOList : BaseEOList<BranchBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new BranchData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Branch> branches)
        {
            foreach (Branch branch in branches)
            {
                BranchBO newbranchBO = new BranchBO();
                newbranchBO.MapEntityToProperties(branch);
                this.Add(newbranchBO);
            }
        }
        #endregion Private Methods
        #region Public Methods
        // This method is used for loading search results
        // It can be extended by adding or changing the parameter lists
        // Its corrosponding DAL class is the Select method in Other methods region
        public void LoadByBranchName(string Name)
        {
            LoadFromList(new BranchData().SelectByBranchName(Name));
        }
        #endregion Public Methods
    }
    #endregion DistrictBOList
}

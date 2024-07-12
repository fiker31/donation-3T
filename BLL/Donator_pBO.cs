using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region Donator_p
    [Serializable()]
    public class Donator_pBO : BaseEO
    {
        public Donator_pBO()
        {
           
        }
        #region Properties
        public long id { get; set; }
        public string FullName { get; set; }
      
        public string CustomerPhone { get; set; }
        
        public string RecursiveDonations { get; set; }
       

        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Donator_p td = new Donator_pData().Select(id);
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
            Donator_pBO td = (Donator_pBO)entity;
            id = td.id;
            FullName = FullName;
            CustomerPhone = CustomerPhone;
            RecursiveDonations = RecursiveDonations;
                
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
                        id = new Donator_pData().Insert(db, FullName, CustomerPhone, RecursiveDonations, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new Donator_pData().Update(db, id, FullName, CustomerPhone, RecursiveDonations, InsertUserId, userAccountId, EntryDate, Version))
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
            if (FullName.Trim().Length <= 0)
            {
                validationErrors.Add(" FullName is required.");
            }
            else if (CustomerPhone.Trim().Length <= 0)
            {
                validationErrors.Add("CustomerPhone is required.");
            }
            else if (RecursiveDonations.Trim().Length <= 0)
            {
                validationErrors.Add("RecursiveDonations is required.");
            }
                      
            else if (DBAction == DBActionEnum.Insert && new Donator_pData().IsDuplicateEntry(FullName))
            {
                validationErrors.Add("Donator profile with this companyname was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new Donator_pData().Delete(db, id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return FullName;
        }
        #endregion Overrides
        
                
    }
    #endregion DistrictBO 
    #region DistrictBOList
    [Serializable()]
    public class Donator_pBOList : BaseEOList<Donator_pBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new Donator_pData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Donator_p> Donator_ps)
        {
            foreach (Donator_p Donator_p in Donator_ps)
            {
                Donator_pBO Donator_pBO = new Donator_pBO();
                Donator_pBO.MapEntityToProperties(Donator_p);
                this.Add(Donator_pBO);
            }
        }
        #endregion Private Methods
        #region Public Methods
        // This method is used for loading search results
        // It can be extended by adding or changing the parameter lists
        // Its corrosponding DAL class is the Select method in Other methods region

        #endregion Public Methods
    }
    #endregion DistrictBOList
}

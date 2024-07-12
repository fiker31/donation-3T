using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region DistrictBO
    [Serializable()]
    public class DistrictBO : BaseEO
    {
        public DistrictBO()
        {
        }
        #region Properties
        public long ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            District td = new DistrictData().Select(id);
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
            District td = (District)entity;
            ID = td.ID;
            Name = td.Name;
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
                        ID = new DistrictData().Insert(db, Name, Address, Contact, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new DistrictData().Update(db, ID, Name, Address, Contact, InsertUserId, userAccountId, EntryDate, Version))
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
                validationErrors.Add("Name  is required!");
            }
            else if (Address.ToString().Trim().Length <= 0)
            {
                validationErrors.Add("Address  is required!");
            }
            else if (Contact.ToString().Trim().Length <= 0)
            {
                validationErrors.Add("Contact  is required!");
            }
            if (DBAction == DBActionEnum.Insert && new DistrictData().IsDuplicateEntry(Name))
            {
                validationErrors.Add("District name is previously registered. Please use edit window to edit the record.");
            }
            int numericValue;
            if (int.TryParse(Name, out numericValue))
            {
                validationErrors.Add("District name can not be numeric value.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new DistrictData().Delete(db, ID, Version);
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
    public class DistrictBOList : BaseEOList<DistrictBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new DistrictData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<District> districts)
        {
            foreach (District district in districts)
            {
                DistrictBO newdistrictBO = new DistrictBO();
                newdistrictBO.MapEntityToProperties(district);
                this.Add(newdistrictBO);
            }
        }
        #endregion Private Methods
        #region Public Methods
        // This method is used for loading search results
        // It can be extended by adding or changing the parameter lists
        // Its corrosponding DAL class is the Select method in Other methods region
        public void LoadByDistrictName(string Name)
        {
            LoadFromList(new DistrictData().SelectByDistrictName(Name));
        }
        #endregion Public Methods
    }
    #endregion DistrictBOList
}

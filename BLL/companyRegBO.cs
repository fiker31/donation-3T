using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region companyReg
    [Serializable()]
    public class companyRegBO : BaseEO
    {
        public companyRegBO()
        {
        }
        #region Properties
        public long id { get; set; }
        public string companyname { get; set; }
        public string companydescription { get; set; }
        public string location { get; set; }
        public string phone { get; set; }
        public string pobox { get; set; }
        public string generalmanager { get; set; }
        public string tillnumber { get; set; }
        public DateTime formationDate { get; set; }

        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            companyReg td = new companyRegData().Select(id);
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
            companyReg td = (companyReg)entity;
            id = td.id;
            companyname = td.companyname;
            companydescription = td.companydescription;
            location = td.location;
            phone = td.phone;
            pobox = td.pobox;
            generalmanager = td.generalmanager;
            tillnumber = td.tillnumber;
            formationDate = (DateTime)td.formationDate;
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
                        id = new companyRegData().Insert(db, companyname, companydescription, location, phone, pobox, generalmanager, tillnumber,formationDate, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new companyRegData().Update(db, id, companyname, companydescription, location, phone, pobox, generalmanager, tillnumber, formationDate, InsertUserId, userAccountId, EntryDate, Version))
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
            if (companyname.Trim().Length <= 0)
            {
                validationErrors.Add(" companyname is required.");
            }
            else if (companydescription.Trim().Length <= 0)
            {
                validationErrors.Add("companydescription is required.");
            }
            else if (location.Trim().Length <= 0)
            {
                validationErrors.Add("location is required.");
            }
           

            else if (DBAction == DBActionEnum.Insert && new companyRegData().IsDuplicateEntry(companyname))
            {
                validationErrors.Add("companyReg with this companyname was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new companyRegData().Delete(db, id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return companyname;
        }
        #endregion Overrides
    }
    #endregion DistrictBO 
    #region DistrictBOList
    [Serializable()]
    public class companyRegBOList : BaseEOList<companyRegBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new companyRegData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<companyReg> Empoloyees)
        {
            foreach (companyReg companyReg in Empoloyees)
            {
                companyRegBO companyRegBO = new companyRegBO();
                companyRegBO.MapEntityToProperties(companyReg);
                this.Add(companyRegBO);
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

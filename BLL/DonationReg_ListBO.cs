using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
using System.Data.Linq;

namespace BLL
{
    #region DonationReg_ListBO
    [Serializable()]
    public class DonationReg_ListBO : BaseEO
    {
        public DonationReg_ListBO()
        {
        }
        #region Properties
        public long id { get; set; }
        public string companyid { get; set; }
        public string donationtitle { get; set; }
        public string donationdescription { get; set; }
        public string donationamountrequired { get; set; }
        public string donationenddate { get; set; }
        public string specialshortcode { get; set; }
        public string donationrelatedlinks { get; set; }
        public string sms { get; set; }
		public Binary poster { get; set; }
        public string email { get; set; }

        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            DonationReg_List td = new DonationReg_ListData().Select(id);
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
            DonationReg_List td = (DonationReg_List)entity;
            id = td.id;
            companyid = companyid;
            donationtitle = donationtitle;
            donationdescription = donationdescription;
            donationamountrequired = donationamountrequired;
            poster = poster;
            donationenddate = donationenddate;
            specialshortcode = specialshortcode;
            donationrelatedlinks = donationrelatedlinks;
            sms = sms;
            email = email;
                
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
                        id = new DonationReg_ListData().Insert(db, Convert.ToInt32(companyid), donationtitle, donationdescription, Convert.ToInt32(donationamountrequired),poster, Convert.ToDateTime(donationenddate), Convert.ToInt32(specialshortcode) , donationrelatedlinks, sms, email, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new DonationReg_ListData().Update(db, id, Convert.ToInt32(companyid), donationtitle, donationdescription, Convert.ToInt32(donationamountrequired),poster, Convert.ToDateTime(donationenddate), Convert.ToInt32(specialshortcode), donationrelatedlinks, sms, email, InsertUserId, userAccountId, EntryDate, Version))
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
          
             if (donationtitle.Trim().Length <= 0)
            {
                validationErrors.Add("DonationTitle is required.");
            }
            else if (donationdescription.Trim().Length <= 0)
            {
                validationErrors.Add("DonationDescription is required.");
            }
            else if (donationamountrequired.Trim().Length <= 0)
            {
                validationErrors.Add("DonationAmountRequired is required.");
            }
           

            
            else if (donationrelatedlinks.Trim().Length <= 0)
            {
                validationErrors.Add("relatedlinks is required.");
            }
           
            else if (sms.Trim().Length <= 0)
            {
                validationErrors.Add("sms is required.");
            }
            else if (email.Trim().Length <= 0)
            {
                validationErrors.Add("email is required.");
            }

            else if (DBAction == DBActionEnum.Insert && new DonationReg_ListData().IsDuplicateEntry(donationtitle))
            {
                validationErrors.Add("DonationReg_List with this DonationTitle was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new DonationReg_ListData().Delete(db, id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return donationtitle;
        }
        #endregion Overrides
        
                
    }
    #endregion DistrictBO 
    #region DistrictBOList
    [Serializable()]
    public class DonationReg_ListBOList : BaseEOList<DonationReg_ListBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new DonationReg_ListData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<DonationReg_List> DonationReg_Lists)
        {
            foreach (DonationReg_List DonationReg_List in DonationReg_Lists)
            {
                DonationReg_ListBO DonationReg_ListBO = new DonationReg_ListBO();
                DonationReg_ListBO.MapEntityToProperties(DonationReg_List);
                this.Add(DonationReg_ListBO);
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

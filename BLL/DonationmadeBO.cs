using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region DonationmadeBO
    [Serializable()]
    public class DonationmadeBO : BaseEO
    {
        public DonationmadeBO()
        {

        }
        #region Properties
        public long id { get; set; }
        public string DonationID { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerName { get; set; }
        public string DonationAmount { get; set; }
        public string DonationDate { get; set; }
      

        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Donationmade td = new DonationmadeData().Select(id);
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
            DonationmadeBO td = (DonationmadeBO)entity;
            id = td.id;
            DonationID = DonationID;
            CustomerPhone = CustomerPhone;
            CustomerName = CustomerName;
            DonationAmount = DonationAmount;
            DonationDate = DonationDate;

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
                        id = new DonationmadeData().Insert(db, Convert.ToInt32(DonationID), CustomerPhone, CustomerName, DonationAmount, Convert.ToDateTime(DonationDate), userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new DonationmadeData().Update(db, id, Convert.ToInt32(DonationID), CustomerPhone, CustomerName, DonationAmount, Convert.ToDateTime(DonationDate),  InsertUserId, userAccountId, EntryDate, Version))
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

            if (CustomerPhone.Trim().Length <= 0)
            {
                validationErrors.Add("CustomerPhone is required.");
            }
            else if (CustomerName.Trim().Length <= 0)
            {
                validationErrors.Add("CustomerName is required.");
            }
            else if (DonationAmount.Trim().Length <= 0)
            {
                validationErrors.Add("DonationAmount is required.");
            }
            else if (DonationDate.Trim().Length <= 0)
            {
                validationErrors.Add("DonationDate is required.");
            }

            else if (DBAction == DBActionEnum.Insert && new DonationmadeData().IsDuplicateEntry(CustomerName))
            {
                validationErrors.Add("Donationmade with this CustomerName was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new DonationmadeData().Delete(db, id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return CustomerName;
        }
        #endregion Overrides


    }
    #endregion DistrictBO 
    #region DistrictBOList
    [Serializable()]
    public class DonationmadeBOList : BaseEOList<DonationmadeBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new DonationmadeData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Donationmade> Donationmades)
        {
            foreach (Donationmade Donationmade in Donationmades)
            {
                DonationmadeBO DonationmadeBO = new DonationmadeBO();
                DonationmadeBO.MapEntityToProperties(Donationmade);
                this.Add(DonationmadeBO);
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

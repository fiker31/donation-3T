using BLL.Framework;
using DAL;
using System;
using System.Collections.Generic;
using DAL.Framework;
namespace BLL
{
    #region HolidayBO
    [Serializable()]
    public class HolidayBO : BaseEO
    {
        public HolidayBO()
        {
        }
        #region Properties
        public long Id { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Holiday td = new HolidayData().Select(id);
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
            Holiday td = (Holiday)entity;
            Id = td.Id;
            HolidayName = td.HolidayName;
            HolidayDate = td.HolidayDate;
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
                        Id = new HolidayData().Insert(db, HolidayName, HolidayDate, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new HolidayData().Update(db, Id, HolidayName, HolidayDate, InsertUserId, userAccountId, EntryDate, Version))
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
            HolidayData hData = new HolidayData();
            if (HolidayName == "")
            {
                validationErrors.Add("Holiday Description is Required.");
            }
            if (Convert.ToString(HolidayDate) == "01/01/1900 00:00:00")
            {
                validationErrors.Add("Holiday Date is required.");
            }
            if (hData.IsDuplicateEntry(db, HolidayDate) && DBAction == DBActionEnum.Insert)
            {
                validationErrors.Add("Holiday for this Date already exists. Duplicate Entry is not Allowed.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new HolidayData().Delete(db, Id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
    }
    #endregion HolidayBO
    #region HolidayBOList
    [Serializable()]
    public class HolidayBOList : BaseEOList<HolidayBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new HolidayData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Holiday> holidays)
        {
            foreach (Holiday holiday in holidays)
            {
                HolidayBO newHolidayBO = new HolidayBO();
                newHolidayBO.MapEntityToProperties(holiday);
                this.Add(newHolidayBO);
            }
        }
        #endregion Private Methods
        #region Public Methods
        // This method is used for loading search results
        // It can be extended by adding or changing the parameter lists
        // Its corrosponding DAL class method is the SelectSearch method in Other methods region
        public void LoadSearch(string holidayname, DateTime holidaydate)
        {
            LoadFromList(new HolidayData().SelectSearch(holidayname, holidaydate));
        }
        #endregion Public Methods
    }
    #endregion HolidayBOList
}

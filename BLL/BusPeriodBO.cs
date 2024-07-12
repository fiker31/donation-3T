using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region BusPeriodBO
    [Serializable()]
    public class BusPeriodBO : BaseEO
    {
        public BusPeriodBO()
        {
        }
        public enum PStatusEnum
        {
            CurrentAndActive = 0,
            Previous = 1
        }
        #region Properties
        public long RowId { get; set; }
        public string BusinessPeriod { get; set; }
        public PStatusEnum PStatus { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            BusPeriod td = new BusPeriodData().Select(id);
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
        public bool LoadCurrentPeriod()
        {
            //Get the entity object from the DAL.
            BusPeriod td = new BusPeriodData().SelectCurrentPeriod();
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
            BusPeriod td = (BusPeriod)entity;
            RowId = td.RowId;
            BusinessPeriod = td.BusinessPeriod;
            PStatus = (PStatusEnum)td.PStatus;
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
                        RowId = new BusPeriodData().Insert(db, BusinessPeriod, 0, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new BusPeriodData().Update(db, RowId, BusinessPeriod, 1, InsertUserId, userAccountId, EntryDate, Version))
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
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new BusPeriodData().Delete(db, RowId, Version);
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
    #endregion BusPeriodBO
    #region BusPeriodBOList
    [Serializable()]
    public class BusPeriodBOList : BaseEOList<BusPeriodBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new BusPeriodData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<BusPeriod> busPeriods)
        {
            foreach (BusPeriod busPeriod in busPeriods)
            {
                BusPeriodBO newBusPeriodBO = new BusPeriodBO();
                newBusPeriodBO.MapEntityToProperties(busPeriod);
                this.Add(newBusPeriodBO);
            }
        }
        #endregion Private Methods
        #region Public Methods
        // This method is used for loading search results
        // It can be extended by adding or changing the parameter lists
        // Its corrosponding DAL class method is the SelectSearch method in Other methods region
        #endregion Public Methods
    }
    #endregion BusPeriodBOList
}

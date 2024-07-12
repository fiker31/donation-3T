using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region Donation_CategoryBO
    [Serializable()]
    public class Donation_CategoryBO : BaseEO
    {
        public Donation_CategoryBO()
        {
        }
        #region Properties
        public long Id { get; set; }
        public string CategoryName { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Donation_Category td = new Donation_CategoryData().Select(id);
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
            Ticket_Category td = (Ticket_Category)entity;
            Id = td.ID;
            CategoryName = td.CategoryName;
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
                        Id = new Donation_CategoryData().Insert(db, CategoryName, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new Donation_CategoryData().Update(db, Id, CategoryName, InsertUserId, userAccountId, EntryDate, Version))
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
            if (CategoryName.Trim().Length <= 0)
            {
                validationErrors.Add("Category Name is required.");
            }
            else if (DBAction == DBActionEnum.Insert && new Donation_CategoryData().IsDuplicateEntry(CategoryName))
            {
                validationErrors.Add("Category with this name was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new Donation_CategoryData().Delete(db, Id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return CategoryName;
        }
        #endregion Overrides
    }
    #endregion DistrictBO 
    #region DistrictBOList
    [Serializable()]
    public class Donation_CategoryBOList : BaseEOList<Donation_CategoryBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new Donation_CategoryData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Donation_Category> proTemplateHeads)
        {
            foreach (Donation_Category templateHead in proTemplateHeads)
            {
                Donation_CategoryBO templateHeadBo = new Donation_CategoryBO();
                templateHeadBo.MapEntityToProperties(templateHead);
                this.Add(templateHeadBo);
            }
        }
        #endregion Private Methods
        #region Public Methods
        // This method is used for loading search results
        // It can be extended by adding or changing the parameter lists
        // Its corrosponding DAL class is the Select method in Other methods region
        public void LoadByProTempletName(string Donation_CategoryName)
        {
            LoadFromList(new Donation_CategoryData().SelectByCategoryName(Donation_CategoryName));
        }
        #endregion Public Methods
    }
    #endregion DistrictBOList
}

using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region Ticket_CategoryBO
    [Serializable()]
    public class StudentBO : BaseEO
    {
        public StudentBO()
        {
        }
        #region Properties
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Student td = new StudentData().Select(id);
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
            Student td = (Student)entity;
            Id = td.Id;
            Firstname = td.Firstname;
            Lastname = td.Lastname;
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
                        Id = new StudentData().Insert(db, Firstname, Lastname, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new StudentData().Update(db, Id, Firstname, Lastname, InsertUserId, userAccountId, EntryDate, Version))
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
            if (Firstname.Trim().Length <= 0)
            {
                validationErrors.Add("First Name is required.");
            }
            else if (Lastname.Trim().Length <= 0)
            {
                validationErrors.Add("Last Name is required.");
            }
            else if (DBAction == DBActionEnum.Insert && new StudentData().IsDuplicateEntry(Firstname))
            {
                validationErrors.Add("Student with this First Name was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new Ticket_CategoryData().Delete(db, Id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
        }
        public override void Init()
        {
        }
        protected override string GetDisplayText()
        {
            return Firstname;
        }
        #endregion Overrides
    }
    #endregion DistrictBO 
    #region DistrictBOList
    [Serializable()]
    public class StudentBOList : BaseEOList<StudentBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new StudentData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Student> students)
        {
            foreach (Student student in students)
            {
                StudentBO studentBO = new StudentBO();
                studentBO.MapEntityToProperties(student);
                this.Add(studentBO);
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

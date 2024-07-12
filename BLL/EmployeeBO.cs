using BLL.Framework;
using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region Ticket_CategoryBO
    [Serializable()]
    public class EmployeeBO : BaseEO
    {
        public EmployeeBO()
        {
        }
        #region Properties
        public long id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get the entity object from the DAL.
            Employee td = new EmployeeData().Select(id);
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
            Employee td = (Employee)entity;
            id = td.id;
            Firstname = td.Firstname;
            Lastname = td.Lastname;
            Department = td.Department;
            Position = td.Position;
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
                        id = new EmployeeData().Insert(db, Firstname, Lastname, Department, Position, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new EmployeeData().Update(db, id, Firstname, Lastname, Department, Position, InsertUserId, userAccountId, EntryDate, Version))
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
            else if (Department.Trim().Length <= 0)
            {
                validationErrors.Add("Department is required.");
            }
            else if (Position.Trim().Length <= 0)
            {
                validationErrors.Add("Position is required.");
            }
            else if (DBAction == DBActionEnum.Insert && new EmployeeData().IsDuplicateEntry(Firstname))
            {
                validationErrors.Add("Employee with this First Name was registered.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new EmployeeData().Delete(db, id, Version);
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
    public class EmployeeBOList : BaseEOList<EmployeeBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new EmployeeData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<Employee> Empoloyees)
        {
            foreach (Employee employee in Empoloyees)
            {
                EmployeeBO employeeBO = new EmployeeBO();
                employeeBO.MapEntityToProperties(employee);
                this.Add(employeeBO);
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

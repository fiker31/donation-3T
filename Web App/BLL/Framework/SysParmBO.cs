using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FCRMSDAL;
using FCRMSDAL.Framework;

namespace FCRMSBLL.Framework
{
    #region System Parameters
    public static class SysParmBO
    {
        static SysParmBO()
        {
            Load();
        }

        #region Properties

        public static byte MinPasswordLen { get; set; }

        public static byte MinLoginIdLen { get; set; }

        public static byte GLAccountLen { get; set; }

        public static byte SLAccountLen { get; set; }

        public static decimal TotalCapital { get; set; }

        #endregion Properties

        #region Public Methods

        public static bool Load()
        {
            SysParm sp = new SysParamData().Select();
            if (sp != null)
            {
                MapEntityToProperties(sp);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Save(ref EntValidationErrors validationErrors, string userAccountId)
        {

            FCRMSDataContext db = new FCRMSDataContext(DBHelper.GetFCRMSDBConnectionString());
            //Validate the object
            Validate(db, ref validationErrors);

            //Check if there were any validation errors

            if (validationErrors.Count == 0)
            {

                //Update
                SysParamData sp = new SysParamData();
                if (!sp.Update(db, GLAccountLen, SLAccountLen, MinPasswordLen, MinLoginIdLen, TotalCapital))
                {
                    validationErrors.Add("Update Failed");
                    return false;
                }
                return true;
            }
            else
            {
                //Didn't pass validation.
                return false;
            }

        #endregion Public Methods
        }

        #region Private Methods

        private static void MapEntityToProperties(SysParm entity)
        {
            MinPasswordLen = entity.MinPwdLen;
            MinLoginIdLen = entity.MinUIdLen;
           
        }
        private static void Validate(FCRMSDataContext db, ref EntValidationErrors validationErrors)
        {


        }

        #endregion private Methods




    }
      
    #endregion System Parameters
}

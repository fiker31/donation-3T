using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCRMSDAL.Framework
{
    #region System Parameters
    public  class SysParamData
    {
        public  SysParm Select()
        {
            using (FCRMSDataContext db = new FCRMSDataContext(DBHelper.GetFCRMSDBConnectionString()))
            {
                SysParm SP = (from sp in db.SysParms
                             select sp).Single();
                
                return SP;
            }
        }

        #region Update


        public bool Update(string connectionString, byte GLCodeLen, byte SLCodeLen,
            byte MinPWDLen, byte MinLoginIdLen, decimal TotalCapital)
        {
            using (FCRMSDataContext db = new FCRMSDataContext(connectionString))
            {
                return Update( db,  GLCodeLen,  SLCodeLen,
             MinPWDLen,  MinLoginIdLen,  TotalCapital);
            }
        }


        public bool Update(FCRMSDataContext db, byte GLCodeLen, byte SLCodeLen,
            byte MinPWDLen, byte MinLoginIdLen, decimal TotalCapital)
        {
            int result = db.ExecuteCommand(" UPDATE SysPar SET GLCodeLen = " + GLCodeLen + ", SLCodeLen = " + SLCodeLen +
                               " ,MinPWDLen = " + MinPWDLen + ", MinLoginIdLen = " + MinLoginIdLen + ", TotalCapital = " + TotalCapital);


            return (result==1);
        }
        #endregion Update
    }
    #endregion System Parameters
}

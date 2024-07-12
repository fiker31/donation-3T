using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FCRMSDAL;
using System.Configuration;
using System.Data.Linq;

namespace FCRMSDAL.Framework
{
    #region User Status
    public class UserStatusData
    {

        public List<UserStatus> Select()
        {
            using (FCRMSDataContext db = new FCRMSDataContext(DBHelper.GetFCRMSDBConnectionString()))
            {
                List<UserStatus> ES = (from es in db.UserStatus
                                       select es).ToList();

                return ES;
            }
        }

    }
    #endregion User Status

    #region Edit Type

    public class EditTypeData
    {

        public List<EditType> Select()
        {
            using (FCRMSDataContext db = new FCRMSDataContext(DBHelper.GetFCRMSDBConnectionString()))
            {
                List<EditType> CC = (from cc in db.EditTypes
                                     select cc).ToList();

                return CC;
            }
        }

    }

    #endregion Edit Type

    #region Request Status

    public class RequestStatusData
    {

        public List<RequestStatus> Select()
        {
            using (FCRMSDataContext db = new FCRMSDataContext(DBHelper.GetFCRMSDBConnectionString()))
            {
                List<RequestStatus> CF = (from cf in db.RequestStatus
                                          select cf).ToList();

                return CF;
            }
        }

    }
    #endregion Request Status
}
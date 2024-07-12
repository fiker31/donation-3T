using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuditLogUtil
{
    
        public struct AuditCriteria
        {
            public long OperationId;
            public long ColumnId;
            public string UserName;
            public string RowKey;
            public DateTime FromDate;
            public DateTime ToDate;
            public bool AuditDelete;
            public bool AuditUpdate;
            public bool AuditInsert;

        }

}

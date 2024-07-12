using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuditLogUtil
{
        public class AuditTable
        {
            public int TableId { get;  set; }
            public string TableName { get; set; }
            public string PrimaryKeyField { get; set; }
            public bool AuditInserts { get; set; }
            public bool AuditUpdates { get; set; }
            public bool AuditDeletes { get; set; }
            public string UserColumn { get; set; }
            public string Description { get; set; }

            public List<AuditColumn> AuditColumns { get; set; }
        }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3615
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace AuditLogUtil {
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("AuditDataset")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class AuditDataset : global::System.Data.DataSet {
        
        private vwAuditLogDataTable tablevwAuditLog;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public AuditDataset() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected AuditDataset(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["vwAuditLog"] != null)) {
                    base.Tables.Add(new vwAuditLogDataTable(ds.Tables["vwAuditLog"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public vwAuditLogDataTable vwAuditLog {
            get {
                return this.tablevwAuditLog;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone() {
            AuditDataset cln = ((AuditDataset)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["vwAuditLog"] != null)) {
                    base.Tables.Add(new vwAuditLogDataTable(ds.Tables["vwAuditLog"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tablevwAuditLog = ((vwAuditLogDataTable)(base.Tables["vwAuditLog"]));
            if ((initTable == true)) {
                if ((this.tablevwAuditLog != null)) {
                    this.tablevwAuditLog.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "AuditDataset";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/Audit.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tablevwAuditLog = new vwAuditLogDataTable();
            base.Tables.Add(this.tablevwAuditLog);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializevwAuditLog() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            AuditDataset ds = new AuditDataset();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        public delegate void vwAuditLogRowChangeEventHandler(object sender, vwAuditLogRowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class vwAuditLogDataTable : global::System.Data.TypedTableBase<vwAuditLogRow> {
            
            private global::System.Data.DataColumn columnAuditLogID;
            
            private global::System.Data.DataColumn columnOperationId;
            
            private global::System.Data.DataColumn columnTableName;
            
            private global::System.Data.DataColumn columnDescription;
            
            private global::System.Data.DataColumn columnColumnID;
            
            private global::System.Data.DataColumn columnColumnName;
            
            private global::System.Data.DataColumn columnRowKey;
            
            private global::System.Data.DataColumn columnOldValue;
            
            private global::System.Data.DataColumn columnNewValue;
            
            private global::System.Data.DataColumn columnEvent;
            
            private global::System.Data.DataColumn columnUserName;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public vwAuditLogDataTable() {
                this.TableName = "vwAuditLog";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal vwAuditLogDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected vwAuditLogDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn AuditLogIDColumn {
                get {
                    return this.columnAuditLogID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn OperationIdColumn {
                get {
                    return this.columnOperationId;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn TableNameColumn {
                get {
                    return this.columnTableName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn DescriptionColumn {
                get {
                    return this.columnDescription;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn ColumnIDColumn {
                get {
                    return this.columnColumnID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn ColumnNameColumn {
                get {
                    return this.columnColumnName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn RowKeyColumn {
                get {
                    return this.columnRowKey;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn OldValueColumn {
                get {
                    return this.columnOldValue;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn NewValueColumn {
                get {
                    return this.columnNewValue;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn EventColumn {
                get {
                    return this.columnEvent;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn UserNameColumn {
                get {
                    return this.columnUserName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public vwAuditLogRow this[int index] {
                get {
                    return ((vwAuditLogRow)(this.Rows[index]));
                }
            }
            
            public event vwAuditLogRowChangeEventHandler vwAuditLogRowChanging;
            
            public event vwAuditLogRowChangeEventHandler vwAuditLogRowChanged;
            
            public event vwAuditLogRowChangeEventHandler vwAuditLogRowDeleting;
            
            public event vwAuditLogRowChangeEventHandler vwAuditLogRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddvwAuditLogRow(vwAuditLogRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public vwAuditLogRow AddvwAuditLogRow(string TableName, string Description, string ColumnName, string RowKey, string OldValue, string NewValue, string Event, string UserName) {
                vwAuditLogRow rowvwAuditLogRow = ((vwAuditLogRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        null,
                        TableName,
                        Description,
                        null,
                        ColumnName,
                        RowKey,
                        OldValue,
                        NewValue,
                        Event,
                        UserName};
                rowvwAuditLogRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowvwAuditLogRow);
                return rowvwAuditLogRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                vwAuditLogDataTable cln = ((vwAuditLogDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new vwAuditLogDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnAuditLogID = base.Columns["AuditLogID"];
                this.columnOperationId = base.Columns["OperationId"];
                this.columnTableName = base.Columns["TableName"];
                this.columnDescription = base.Columns["Description"];
                this.columnColumnID = base.Columns["ColumnID"];
                this.columnColumnName = base.Columns["ColumnName"];
                this.columnRowKey = base.Columns["RowKey"];
                this.columnOldValue = base.Columns["OldValue"];
                this.columnNewValue = base.Columns["NewValue"];
                this.columnEvent = base.Columns["Event"];
                this.columnUserName = base.Columns["UserName"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnAuditLogID = new global::System.Data.DataColumn("AuditLogID", typeof(long), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnAuditLogID);
                this.columnOperationId = new global::System.Data.DataColumn("OperationId", typeof(long), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnOperationId);
                this.columnTableName = new global::System.Data.DataColumn("TableName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTableName);
                this.columnDescription = new global::System.Data.DataColumn("Description", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnColumnID = new global::System.Data.DataColumn("ColumnID", typeof(long), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnColumnID);
                this.columnColumnName = new global::System.Data.DataColumn("ColumnName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnColumnName);
                this.columnRowKey = new global::System.Data.DataColumn("RowKey", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnRowKey);
                this.columnOldValue = new global::System.Data.DataColumn("OldValue", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnOldValue);
                this.columnNewValue = new global::System.Data.DataColumn("NewValue", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnNewValue);
                this.columnEvent = new global::System.Data.DataColumn("Event", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnEvent);
                this.columnUserName = new global::System.Data.DataColumn("UserName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnUserName);
                this.columnAuditLogID.AutoIncrement = true;
                this.columnAuditLogID.AutoIncrementSeed = -1;
                this.columnAuditLogID.AutoIncrementStep = -1;
                this.columnAuditLogID.AllowDBNull = false;
                this.columnAuditLogID.ReadOnly = true;
                this.columnOperationId.AutoIncrement = true;
                this.columnOperationId.AutoIncrementSeed = -1;
                this.columnOperationId.AutoIncrementStep = -1;
                this.columnOperationId.AllowDBNull = false;
                this.columnOperationId.ReadOnly = true;
                this.columnTableName.AllowDBNull = false;
                this.columnTableName.MaxLength = 255;
                this.columnDescription.AllowDBNull = false;
                this.columnDescription.MaxLength = 255;
                this.columnColumnID.AutoIncrement = true;
                this.columnColumnID.AutoIncrementSeed = -1;
                this.columnColumnID.AutoIncrementStep = -1;
                this.columnColumnID.AllowDBNull = false;
                this.columnColumnID.ReadOnly = true;
                this.columnColumnName.AllowDBNull = false;
                this.columnColumnName.MaxLength = 100;
                this.columnRowKey.AllowDBNull = false;
                this.columnRowKey.MaxLength = 512;
                this.columnOldValue.MaxLength = 4000;
                this.columnNewValue.MaxLength = 4000;
                this.columnEvent.AllowDBNull = false;
                this.columnEvent.MaxLength = 1;
                this.columnUserName.AllowDBNull = false;
                this.columnUserName.MaxLength = 10;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public vwAuditLogRow NewvwAuditLogRow() {
                return ((vwAuditLogRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new vwAuditLogRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(vwAuditLogRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.vwAuditLogRowChanged != null)) {
                    this.vwAuditLogRowChanged(this, new vwAuditLogRowChangeEvent(((vwAuditLogRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.vwAuditLogRowChanging != null)) {
                    this.vwAuditLogRowChanging(this, new vwAuditLogRowChangeEvent(((vwAuditLogRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.vwAuditLogRowDeleted != null)) {
                    this.vwAuditLogRowDeleted(this, new vwAuditLogRowChangeEvent(((vwAuditLogRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.vwAuditLogRowDeleting != null)) {
                    this.vwAuditLogRowDeleting(this, new vwAuditLogRowChangeEvent(((vwAuditLogRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemovevwAuditLogRow(vwAuditLogRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                AuditDataset ds = new AuditDataset();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "vwAuditLogDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class vwAuditLogRow : global::System.Data.DataRow {
            
            private vwAuditLogDataTable tablevwAuditLog;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal vwAuditLogRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tablevwAuditLog = ((vwAuditLogDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public long AuditLogID {
                get {
                    return ((long)(this[this.tablevwAuditLog.AuditLogIDColumn]));
                }
                set {
                    this[this.tablevwAuditLog.AuditLogIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public long OperationId {
                get {
                    return ((long)(this[this.tablevwAuditLog.OperationIdColumn]));
                }
                set {
                    this[this.tablevwAuditLog.OperationIdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string TableName {
                get {
                    return ((string)(this[this.tablevwAuditLog.TableNameColumn]));
                }
                set {
                    this[this.tablevwAuditLog.TableNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Description {
                get {
                    return ((string)(this[this.tablevwAuditLog.DescriptionColumn]));
                }
                set {
                    this[this.tablevwAuditLog.DescriptionColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public long ColumnID {
                get {
                    return ((long)(this[this.tablevwAuditLog.ColumnIDColumn]));
                }
                set {
                    this[this.tablevwAuditLog.ColumnIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ColumnName {
                get {
                    return ((string)(this[this.tablevwAuditLog.ColumnNameColumn]));
                }
                set {
                    this[this.tablevwAuditLog.ColumnNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string RowKey {
                get {
                    return ((string)(this[this.tablevwAuditLog.RowKeyColumn]));
                }
                set {
                    this[this.tablevwAuditLog.RowKeyColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string OldValue {
                get {
                    try {
                        return ((string)(this[this.tablevwAuditLog.OldValueColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'OldValue\' in table \'vwAuditLog\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablevwAuditLog.OldValueColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NewValue {
                get {
                    try {
                        return ((string)(this[this.tablevwAuditLog.NewValueColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'NewValue\' in table \'vwAuditLog\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablevwAuditLog.NewValueColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Event {
                get {
                    return ((string)(this[this.tablevwAuditLog.EventColumn]));
                }
                set {
                    this[this.tablevwAuditLog.EventColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string UserName {
                get {
                    return ((string)(this[this.tablevwAuditLog.UserNameColumn]));
                }
                set {
                    this[this.tablevwAuditLog.UserNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsOldValueNull() {
                return this.IsNull(this.tablevwAuditLog.OldValueColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetOldValueNull() {
                this[this.tablevwAuditLog.OldValueColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNewValueNull() {
                return this.IsNull(this.tablevwAuditLog.NewValueColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNewValueNull() {
                this[this.tablevwAuditLog.NewValueColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class vwAuditLogRowChangeEvent : global::System.EventArgs {
            
            private vwAuditLogRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public vwAuditLogRowChangeEvent(vwAuditLogRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public vwAuditLogRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591
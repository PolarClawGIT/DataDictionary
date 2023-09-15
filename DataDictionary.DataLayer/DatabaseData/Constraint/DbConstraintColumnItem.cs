using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    public interface IDbConstraintColumnItem : IDbConstraintKey, IDbCatalogKey, IDbTableColumnKey, IBindingTableRow
    {
        string? ReferenceSchemaName { get; }
        string? ReferenceTableName { get; }
        string? ReferenceColumnName { get; }
        int? OrdinalPosition { get; }
    }

    [Serializable]
    public class DbConstraintColumnItem : BindingTableRow, IDbConstraintColumnItem, INotifyPropertyChanged, ISerializable
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public string? CatalogName { get { return GetValue("CatalogName"); } }
        public string? SchemaName { get { return GetValue("SchemaName"); } }
        public string? ConstraintName { get { return GetValue("ConstraintName"); } }
        public string? TableName { get { return GetValue("TableName"); } }
        public string? ColumnName { get { return GetValue("ColumnName"); } }
        public int? OrdinalPosition { get { return GetValue<int>("OrdinalPosition"); } }
        public string? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }
        public string? ReferenceTableName { get { return GetValue("ReferenceTableName"); } }
        public string? ReferenceColumnName { get { return GetValue("ReferenceColumnName"); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(string)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ColumnName", typeof(string)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(int)){ AllowDBNull = true},
            new DataColumn("ReferenceSchemaName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceTableName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceColumnName", typeof(string)){ AllowDBNull = true},
        };

        public DbConstraintColumnItem() : base() { }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }



        #region ISerializable
        protected DbConstraintColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

    }

}

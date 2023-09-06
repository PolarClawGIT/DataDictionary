using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// This represents a list of Database on the Server.
    /// </summary>
    [Serializable]
    public class DbDatabaseItem : BindingTableRow, IDbCatalogKeyUnique, ISerializable
    {
        public virtual string? CatalogName { get { return GetValue("database_name"); } }
        public static InformationSchema.Collection Schema { get { return InformationSchema.Collection.Databases; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("database_name", typeof(string)){ AllowDBNull = true},
            new DataColumn("dbid", typeof(short)){ AllowDBNull = false},
            new DataColumn("create_date", typeof(DateTime)){ AllowDBNull = false},
        };

        public DbDatabaseItem() : base() { }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        protected DbDatabaseItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

    }
}

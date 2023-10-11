using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// This represents a list of Database on the Server.
    /// This is used when filling a list of Catalogs for the User Interface.
    /// </summary>
    public class DbDatabaseItem : BindingTableRow, IDbCatalogKeyUnique
    {
        /// <inheritdoc/>
        public virtual string? DatabaseName { get { return GetValue("database_name"); } }

        /// <summary>
        /// Schema type to get using SqlClient GetSchema method.
        /// </summary>
        public static InformationSchema.Collection Schema { get { return InformationSchema.Collection.Databases; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("database_name", typeof(string)){ AllowDBNull = true},
            new DataColumn("dbid", typeof(short)){ AllowDBNull = false},
            new DataColumn("create_date", typeof(DateTime)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for a DbDatabaseItem
        /// </summary>
        public DbDatabaseItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}

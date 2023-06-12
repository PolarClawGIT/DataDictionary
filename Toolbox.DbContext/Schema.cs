using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.DbContext
{
    public class Schema
    {
        /// <summary>
        /// List of Collections returned by GetSchema.
        /// https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-schema-collections
        /// </summary>
        public enum Collection
        {
            // Handled by GetSchema
            MetaDataCollections,
            DataSourceInformation,
            DataTypes,
            Restrictions,
            ReservedWords,
            Users,
            Databases,
            Tables,
            Columns,
            AllColumns,
            ColumnSetColumns,
            StructuredTypeMembers,
            Views,
            ViewColumns,
            ProcedureParameters,
            Procedures,
            ForeignKeys,
            IndexColumns,
            Indexes,
            UserDefinedTypes
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{

    /// <summary>
    /// Class used to store fn_listextendedproperty call and results
    /// </summary>
    public class PropertyMetaData : BindingTableRow, IProperty
    {
        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? Level0Type { get { return GetValue(nameof(Level0Type)); } }

        /// <inheritdoc/>
        public String? Level0Name { get { return GetValue(nameof(Level0Name)); } }

        /// <inheritdoc/>
        public String? Level1Type { get { return GetValue(nameof(Level1Type)); } }

        /// <inheritdoc/>
        public String? Level1Name { get { return GetValue(nameof(Level1Name)); } }

        /// <inheritdoc/>
        public String? Level2Type { get { return GetValue(nameof(Level2Type)); } }

        /// <inheritdoc/>
        public String? Level2Name { get { return GetValue(nameof(Level2Name)); } }

        /// <inheritdoc/>
        public String? PropertyName { get { return GetValue(nameof(PropertyName)); } }

        /// <inheritdoc/>
        public String? PropertyValue { get { return GetValue(nameof(PropertyValue)); } }

        /// <summary>
        /// Constructor for Catalog Schema Information Schema
        /// </summary>
        public PropertyMetaData() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            // Parameter Data
            new DataColumn(nameof(Level0Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level0Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Name), typeof(string)){ AllowDBNull = true},
            // Results Data
            new DataColumn(nameof(PropertyName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Extended Properties from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IProperty> GetProperties(IConnection connection)
        {
            BindingTable<PropertyMetaData> schemas = new BindingTable<PropertyMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = Property.TSql_InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}

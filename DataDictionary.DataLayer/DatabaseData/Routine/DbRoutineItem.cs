using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for Database Routine (procedures and functions).
    /// </summary>
    public interface IDbRoutineItem : IDbRoutineKey, IDbCatalogKey, IDbObjectScope, IDbIsSystem, IDataItem
    {
        /// <summary>
        /// Type of Routine (such as procedure or function)
        /// </summary>
        String? RoutineType { get; }
    }

    /// <summary>
    /// Implementation for Database Routine (procedures and functions).
    /// </summary>
    [Serializable]
    public class DbRoutineItem : BindingTableRow, IDbRoutineItem, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public string? CatalogName { get { return GetValue("CatalogName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? RoutineName { get { return GetValue("RoutineName"); } }

        /// <inheritdoc/>
        public string? RoutineType { get { return GetValue("RoutineType"); } }

        /// <inheritdoc/>
        public bool IsSystem
        {
            get
            {
                return SchemaName is "dbo" &&
                    RoutineName is "sp_creatediagram" or
                    "sp_renamediagram" or
                    "sp_alterdiagram" or
                    "sp_dropdiagram" or
                    "fn_diagramobjects" or
                    "sp_helpdiagrams" or
                    "sp_helpdiagramdefinition" or
                    "sp_upgraddiagrams";
            }
        }

        /// <inheritdoc/>
        public DbObjectScope ObjectScope
        {
            get
            {
                if (Enum.TryParse(RoutineType, true, out DbObjectScope value))
                { return value; }
                else { return DbObjectScope.NULL; }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(string)){ AllowDBNull = false},
            new DataColumn("RoutineType", typeof(string)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for Database Routine Item.
        /// </summary>
        public DbRoutineItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            return new DbExtendedPropertyGetCommand(connection)
            {
                Level0Name = SchemaName,
                Level0Type = "SCHEMA",
                Level1Name = RoutineName,
                Level1Type = RoutineType
            }.GetCommand();
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Routine Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbRoutineItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbRoutineKey(this).ToString(); }
    }
}

using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Alias Items
    /// </summary>
    public interface IDomainEntityAliasItem : IDomainEntityAliasKey, IDomainEntityKey, IDbCatalogScope, IDbObjectScope, IDataItem
    { }

    /// <summary>
    /// Implementation for Domain Entity Alias Items
    /// </summary>
    [Serializable]
    public class DomainEntityAliasItem : BindingTableRow, IDomainEntityAliasItem
    {
        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>("EntityId"); } protected set { SetValue("EntityId", value); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("CatalogName"); } set { SetValue("CatalogName", value); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } set { SetValue("SchemaName", value); } }

        /// <inheritdoc/>
        public string? ObjectName { get { return GetValue("ObjectName"); } set { SetValue("ObjectName", value); } }

        /// <inheritdoc/>
        public DbCatalogScope CatalogScope { get; set; } = DbCatalogScope.NULL;

        /// <inheritdoc/>
        public DbObjectScope ObjectScope { get; set; } = DbObjectScope.NULL;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("EntityAliasId", typeof(int)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = true},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ObjectName", typeof(string)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        public DomainEntityAliasItem() : base() { }

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="key"></param>
        public DomainEntityAliasItem(IDomainEntityKey key) : this()
        { EntityId = key.EntityId; }

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        public DomainEntityAliasItem(IDomainEntityKey key, IDbTableItem source) : this()
        {
            EntityId = key.EntityId;
            DatabaseName = source.DatabaseName;
            SchemaName = source.SchemaName;
            ObjectName = source.TableName;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntityAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(DatabaseName)) { result.Append(DatabaseName); }

            if (!string.IsNullOrWhiteSpace(SchemaName)) { result.Append(SchemaName); }
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(string.Format(".{0}", SchemaName)); }
                else { result.Append(SchemaName); }
            }

            if (!string.IsNullOrWhiteSpace(ObjectName))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(string.Format(".{0}", ObjectName)); }
                else { result.Append(ObjectName); }
            }

            return result.ToString();
        }

    }
}


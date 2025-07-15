using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Class that takes a Table and builds an AppModel.Entity
    /// </summary>
    /// <remarks>Helper Class</remarks>
    public class TableEntity
    {
        ITableValue sourceTable; // Source Table

        /// <summary>
        /// The Entity built from the sourceTable
        /// </summary>
        public EntityValue Entity { get; private set; }

        /// <summary>
        /// List of Aliases for the sourceTable
        /// </summary>
        public IEnumerable<IEntityAliasValue> Aliases
        {
            get
            {
                List<EntityAliasValue> result = new List<EntityAliasValue>();
                foreach (ITableValue item in GetAlias(sourceTable))
                {
                    AliasIndex index = new AliasIndex(item);
                    result.Add(new EntityAliasValue(Entity, index));
                }
                return result;
            }
        }

        /// <summary>
        /// List of Properties for the sourceTable
        /// </summary>
        public IEnumerable<IEntityPropertyValue> Properties
        {
            get
            {
                List<EntityPropertyValue> result = new List<EntityPropertyValue>();

                foreach (AppCatalog.IPropertyValue databaseProperty in GetCatalogProperty(sourceTable))
                {
                    if (GetModelProperty(databaseProperty) is AppModel.IPropertyValue modelProperty)
                    {
                        result.Add(new EntityPropertyValue(Entity, modelProperty)
                        { PropertyValue = databaseProperty.PropertyValue });
                    }

                    if (databaseProperty.IsDescription && String.IsNullOrEmpty(Entity.EntityDescription))
                    { Entity.EntityDescription = databaseProperty.PropertyValue; }
                }
                return result;
            }
        }

        /// <summary>
        /// List of Attributes of the Entity for the sourceTable
        /// </summary>
        public IEnumerable<IEntityAttributeValue> Attributes
        {
            get
            {
                List<EntityAttributeValue> result = new List<EntityAttributeValue>();

                foreach (ITableColumnValue item in GetColumns(sourceTable))
                {
                    EntityAttributeValue value = new EntityAttributeValue(Entity);
                    value.AttributeKnownAs = item.ColumnName;
                    
                    value.AttributePath = item.CreatePath();
                    value.OrdinalPosition = item.OrdinalPosition;
                    value.IsNullable = item.IsNullable;

                    result.Add(value);
                }

                return result;
            }
        }

        /// <inheritdoc cref="ITableData.GetAlias(ITableIndexName)"/>
        public required Func<ITableIndexName, IEnumerable<ITableValue>> GetAlias { get; init; }

        /// <inheritdoc cref="AppCatalog.IPropertyData.GetProperty(ITableIndexName)"/>
        public required Func<ITableIndexName, IEnumerable<AppCatalog.IPropertyValue>> GetCatalogProperty { get; init; }

        /// <inheritdoc cref="AppModel.IPropertyGetValue.GetValue(IPropertyIndex)"/>
        public required Func<AppCatalog.IPropertyValue, AppModel.IPropertyValue?> GetModelProperty { get; init; }

        /// <inheritdoc cref="ITableData.GetColumns(ITableIndexName)"/>
        public required Func<ITableIndexName, IEnumerable<ITableColumnValue>> GetColumns { get; init; }

        /// <summary>
        /// Create a new TableEntity
        /// </summary>
        public TableEntity(ITableValue source)
        {
            sourceTable = source;

            Entity = new EntityValue()
            {
                EntityTitle = source.TableName,
                EntityName = new TableIndexName(source).ToString(),
            };
        }
    }
}

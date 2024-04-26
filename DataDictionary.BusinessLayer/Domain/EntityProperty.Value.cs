using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityPropertyValue : IDomainEntityPropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class EntityPropertyValue : DomainEntityPropertyItem, IEntityPropertyValue
    {
        /// <inheritdoc/>
        public EntityPropertyValue() : base() { }

        /// <inheritdoc/>
        public EntityPropertyValue(IEntityIndex EntityKey) : base(EntityKey) { }

        /// <inheritdoc/>
        public EntityPropertyValue(IEntityIndex EntityKey,
                                     IPropertyIndex propertyKey,
                                     IExtendedPropertyValue value)
            : base(EntityKey, propertyKey, value) { }

        internal XElement? GetXElement(IPropertyItem property, IEnumerable<ElementItem>? options = null)
        {
            XElement? result = null;
            IEntityPropertyValue EntityNames;
            IPropertyItem propertyNames;

            if (options is not null)
            {
                foreach (ElementItem option in options)
                {
                    Object? value = null;

                    switch (option.ColumnName)
                    {
                        case nameof(propertyNames.PropertyTitle): value = property.PropertyTitle; break;
                        case nameof(propertyNames.ExtendedProperty): value = property.ExtendedProperty; break;
                        case nameof(EntityNames.PropertyValue): value = PropertyValue; break;
                        case nameof(EntityNames.DefinitionText): value = DefinitionText; break;
                        default:
                            break;
                    }

                    if (value is not null)
                    { result = new XElement(Scope.ToName(), option.GetXElement(value)); }
                }
            }

            return result;
        }

        internal static IReadOnlyList<ColumnValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelEntityProperty;
            IEntityPropertyValue EntityNames;
            IPropertyItem propertyNames;
            List<ColumnValue> result = new List<ColumnValue>()
            {
                new ColumnValue() {ColumnName = nameof(propertyNames.PropertyTitle),    DataType = typeof(String), AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(propertyNames.ExtendedProperty), DataType = typeof(String), AllowDBNull = true,  Scope = scope},
                new ColumnValue() {ColumnName = nameof(EntityNames.PropertyValue),   DataType = typeof(String), AllowDBNull = true,  Scope = scope},
                new ColumnValue() {ColumnName = nameof(EntityNames.DefinitionText),  DataType = typeof(String), AllowDBNull = true,  Scope = scope},
            };

            return result;
        }
    }
}

using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    public class DomainData
    {
        public BindingTable<DomainAttributeItem> DomainAttributes = ModelFactory.Create<DomainAttributeItem>();
        public BindingTable<DomainAttributePropertyItem> DomainAttributeProperties = ModelFactory.Create<DomainAttributePropertyItem>();
        public BindingTable<DomainAttributeAliasItem> DomainAttributeAliases = ModelFactory.Create<DomainAttributeAliasItem>();

        public DomainData() { }

        public void ImportAttributes(DatabaseMetaData data)
        {
            // Looking for: 
            // - Not in System Tables or System Schema's
            // - Not already aliased to another attribute
            IEnumerable<IDbColumnItem> newAttributes = data.DbColumns.Where(
                w => w.GetTable(data.DbTables) is IDbTableItem table && !table.IsSystem &&
                w.GetSchema(data.DbSchemas) is IDbSchemaItem schema && !schema.IsSystem &&
                DomainAttributeAliases.FirstOrDefault(
                    a => w.SchemaName == a.ScopeName &&
                    w.TableName == a.ObjectName &&
                    w.CollationName == a.ElementName)
                is null);

            foreach (IGrouping<String?, IDbColumnItem> columnItem in newAttributes.GroupBy(g => g.ColumnName))
            {
                IDbColumnItem columnSource = columnItem.First();
                DomainAttributeItem newAttribute = new DomainAttributeItem() { AttributeTitle = columnSource.ColumnName };
                List<IDbExtendedPropertyItem> propeties = new List<IDbExtendedPropertyItem>();

                DomainAttributes.Add(newAttribute);

                foreach (IDbColumnItem newAlais in columnItem)
                {
                    DomainAttributeAliases.Add(new DomainAttributeAliasItem()
                    {
                        AttributeId = newAttribute.AttributeId,
                        ScopeName = newAlais.SchemaName,
                        ObjectName = newAlais.TableName,
                        ElementName = newAlais.ColumnName
                    });

                    propeties.AddRange(newAlais.GetProperties(data.DbExtendedProperties));
                }

                foreach (IGrouping<String?, IDbExtendedPropertyItem> propertyItem in propeties.GroupBy(g => g.PropertyName))
                {
                    IDbExtendedPropertyItem propertySource = propertyItem.First();

                    DomainAttributeProperties.Add(new DomainAttributePropertyItem()
                    {
                        AttributeId = newAttribute.AttributeId,
                        PropertyName = propertySource.PropertyName,
                        PropertyValue = propertySource.PropertyValue
                    });
                }
            }
        }
    }
}

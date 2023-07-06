using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    public class DomainData
    {
        public BindingTable<DomainAttributeItem> DomainAttributes = new BindingTable<DomainAttributeItem>();
        // TODO: DomainAlais
        // TODO: DomainProperties

        public DomainData() { }

        public void ImportAttributes(IEnumerable<IDbColumnItem> columns)
        {
            foreach (IGrouping<string?, IDbColumnItem> item in columns.GroupBy(g => g.ColumnName))
            {
                if (item.Key is String key &&
                    DomainAttributes.FirstOrDefault(
                        w => w.AttributeTitle is String &&
                        w.ParentAttributeId is null &&
                        w.AttributeTitle.Equals(key, StringComparison.CurrentCultureIgnoreCase))
                    is DomainAttributeItem parentItem)
                { // TODO: Child Attribute, Need to work out when to add a child
                    IDbColumnItem columnItem = item.First();

                    DomainAttributes.Add(new DomainAttributeItem()
                    {
                        AttributeId = Guid.NewGuid(),
                        ParentAttributeId = parentItem.AttributeId,
                        AttributeTitle = columnItem.ColumnName
                    });
                }
                else
                { // Parent Attribute
                    IDbColumnItem columnItem = item.First();
                    DomainAttributes.Add(new DomainAttributeItem()
                    {
                        AttributeId = Guid.NewGuid(),
                        AttributeTitle = columnItem.ColumnName
                    });
                }
            }
        }
    }
}

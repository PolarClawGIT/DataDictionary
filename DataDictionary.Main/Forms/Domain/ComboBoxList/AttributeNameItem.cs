using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record class AttributeNameItem : IDomainAttributeKey, IDomainAttributeKeyName
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String AttributeTitle { get; private set; } = String.Empty;

        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultAttributeId = null, String? defaultAttributeTitle = null)
            where T : IDomainAttributeKey, IDomainAttributeKeyName
        {

            AttributeNameItem propertyNameDataItem = new AttributeNameItem();
            BindingList<AttributeNameItem> list = new BindingList<AttributeNameItem>();
            list.Add(new AttributeNameItem() { AttributeId = Guid.Empty, AttributeTitle = "(not specified)" });

            if (defaultAttributeId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultAttributeTitle) && source.Count(w => defaultId.Equals(w.AttributeId)) == 0)
            { list.Add(new AttributeNameItem() { AttributeId = defaultId, AttributeTitle = defaultAttributeTitle }); }

            foreach (T item in source.OrderBy(o => o.AttributeTitle))
            {
                if (item.AttributeId is Guid attributeId && attributeId != Guid.Empty && item.AttributeTitle is String attributeTitle)
                { list.Add(new AttributeNameItem() { AttributeId = attributeId, AttributeTitle = attributeTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.AttributeId);
            control.DisplayMember = nameof(propertyNameDataItem.AttributeTitle);
        }
    }
}

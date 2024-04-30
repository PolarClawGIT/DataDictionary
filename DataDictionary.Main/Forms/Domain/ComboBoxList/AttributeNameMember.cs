using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record class AttributeNameMember : IAttributeIndex, IAttributeIndexName
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String AttributeTitle { get; private set; } = String.Empty;

        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultAttributeId = null, String? defaultAttributeTitle = null)
            where T : IAttributeIndex, IAttributeIndexName
        {

            AttributeNameMember propertyNameDataItem = new AttributeNameMember();
            BindingList<AttributeNameMember> list = new BindingList<AttributeNameMember>();
            list.Add(new AttributeNameMember() { AttributeId = Guid.Empty, AttributeTitle = "(not specified)" });

            if (defaultAttributeId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultAttributeTitle) && source.Count(w => defaultId.Equals(w.AttributeId)) == 0)
            { list.Add(new AttributeNameMember() { AttributeId = defaultId, AttributeTitle = defaultAttributeTitle }); }

            foreach (T item in source.OrderBy(o => o.AttributeTitle))
            {
                if (item.AttributeId is Guid attributeId && attributeId != Guid.Empty && item.AttributeTitle is String attributeTitle)
                { list.Add(new AttributeNameMember() { AttributeId = attributeId, AttributeTitle = attributeTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.AttributeId);
            control.DisplayMember = nameof(propertyNameDataItem.AttributeTitle);
        }
    }
}

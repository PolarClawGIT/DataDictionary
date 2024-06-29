using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record AttributeNameList : IAttributeIndex, IAttributeIndexName
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String AttributeTitle { get; private set; } = String.Empty;

        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultAttributeId = null, String? defaultAttributeTitle = null)
            where T : IAttributeIndex, IAttributeIndexName
        {

            AttributeNameList memberItem = new AttributeNameList();
            BindingList<AttributeNameList> list = new BindingList<AttributeNameList>();
            list.Add(new AttributeNameList() { AttributeId = Guid.Empty, AttributeTitle = "(not specified)" });

            if (defaultAttributeId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultAttributeTitle) && source.Count(w => defaultId.Equals(w.AttributeId)) == 0)
            { list.Add(new AttributeNameList() { AttributeId = defaultId, AttributeTitle = defaultAttributeTitle }); }

            foreach (T item in source.OrderBy(o => o.AttributeTitle))
            {
                if (item.AttributeId is Guid attributeId && attributeId != Guid.Empty && item.AttributeTitle is String attributeTitle)
                { list.Add(new AttributeNameList() { AttributeId = attributeId, AttributeTitle = attributeTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(memberItem.AttributeId);
            control.DisplayMember = nameof(memberItem.AttributeTitle);
        }
    }
}

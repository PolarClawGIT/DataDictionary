using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record AttributeNameList : IAttributeIndex, IAttributeIndexName
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String AttributeTitle { get; private set; } = String.Empty;

        public static void Load(DataGridViewComboBoxColumn control)
        {
            BindingList<AttributeNameList> list = new BindingList<AttributeNameList>();

            foreach (AttributeNameList item in BusinessData.Model.Attribute.Attributes.
                Select(s => new AttributeNameList()
                {
                    AttributeId = s.AttributeId,
                    AttributeTitle = s.AttributeTitle ?? String.Empty
                }))
            { list.Add(item); }
            //control.DefaultCellStyle.NullValue = Guid.Empty; // This does not work
            //control.DefaultCellStyle.DataSourceNullValue = Guid.Empty; // This does not work

            control.ValueMember = nameof(AttributeId);
            control.DisplayMember = nameof(AttributeTitle);
            control.DataSource = list;
        }

        public static void Load(ComboBoxData control)
        {
            BindingList<AttributeNameList> list = new BindingList<AttributeNameList>();

            foreach (AttributeNameList item in BusinessData.Model.Attribute.Attributes.
                Select(s => new AttributeNameList()
                {
                    AttributeId = s.AttributeId,
                    AttributeTitle = s.AttributeTitle ?? String.Empty
                }))
            { list.Add(item); }

            control.ValueMember = nameof(AttributeId);
            control.DisplayMember = nameof(AttributeTitle);
            control.DataSource = list;
        }

    }
}

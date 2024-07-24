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

        public static void Load(DataGridViewComboBoxColumn control)
        {
            BindingList<AttributeNameList> list = new BindingList<AttributeNameList>();

            foreach (AttributeNameList item in BusinessData.DomainModel.Attributes.
                Select(s => new AttributeNameList()
                {
                    AttributeId = s.AttributeId,
                    AttributeTitle = s.AttributeTitle ?? String.Empty
                }))
            { list.Add(item); }
            //control.DefaultCellStyle.NullValue = Guid.Empty; // This does not work
            //control.DefaultCellStyle.DataSourceNullValue = Guid.Empty; // This does not work

            control.ValueMember = nameof(AttributeNameList.AttributeId);
            control.DisplayMember = nameof(AttributeNameList.AttributeTitle);
            control.DataSource = list;
        }

        public static void Load(ComboBoxData control)
        {
            BindingList<AttributeNameList> list = new BindingList<AttributeNameList>();

            foreach (AttributeNameList item in BusinessData.DomainModel.Attributes.
                Select(s => new AttributeNameList()
                {
                    AttributeId = s.AttributeId,
                    AttributeTitle = s.AttributeTitle ?? String.Empty
                }))
            { list.Add(item); }

            control.ValueMember = nameof(AttributeNameList.AttributeId);
            control.DisplayMember = nameof(AttributeNameList.AttributeTitle);
            control.DataSource = list;
        }

    }
}

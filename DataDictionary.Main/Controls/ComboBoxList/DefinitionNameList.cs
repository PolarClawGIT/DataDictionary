using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record DefinitionNameList : IDefinitionIndex, IDefinitionIndexName
    {
        /// <inheritdoc/>
        public Guid? DefinitionId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String DefinitionTitle { get; set; } = String.Empty;

        public static void Load(ComboBoxData control)
        { Load(control, BusinessData.Model.Definitions); }

        public static void Load(ComboBoxData control, IEnumerable<IDefinitionValue> values)
        {
            DefinitionNameList DefinitionNameDataItem = new DefinitionNameList();
            BindingList<DefinitionNameList> list = new BindingList<DefinitionNameList>();
            list.Add(new DefinitionNameList() { DefinitionId = Guid.Empty, DefinitionTitle = "(select Definition Type)" });

            foreach (IDefinitionValue item in values)
            {
                if (item.DefinitionId is Guid DefinitionId && item.DefinitionTitle is String DefinitionTitle)
                { list.Add(new DefinitionNameList() { DefinitionId = DefinitionId, DefinitionTitle = DefinitionTitle }); }
            }

            control.ValueMember = nameof(DefinitionNameDataItem.DefinitionId);
            control.DisplayMember = nameof(DefinitionNameDataItem.DefinitionTitle);
            control.DataSource = list;
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {   Load(control, BusinessData.Model.Definitions); }

        public static void Load(DataGridViewComboBoxColumn control, IEnumerable<IDefinitionValue> values)
        {
            DefinitionNameList DefinitionNameDataItem = new DefinitionNameList();
            BindingList<DefinitionNameList> list = new BindingList<DefinitionNameList>();
            list.Add(new DefinitionNameList() { DefinitionId = Guid.Empty, DefinitionTitle = "(select Definition Type)" });

            foreach (DefinitionValue item in values)
            {
                if (item.DefinitionId is Guid DefinitionId && item.DefinitionTitle is String DefinitionTitle)
                { list.Add(new DefinitionNameList() { DefinitionId = DefinitionId, DefinitionTitle = DefinitionTitle }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(DefinitionNameDataItem.DefinitionId);
            control.DisplayMember = nameof(DefinitionNameDataItem.DefinitionTitle);
            
        }
    }
}

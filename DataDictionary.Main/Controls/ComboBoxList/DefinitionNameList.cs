using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record DefinitionNameList : IDefinitionIndex, IDefinitionIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? DefinitionId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String DefinitionTitle { get; set; } = String.Empty;

        DefinitionNameList(IDefinitionValue value)
        {
            DefinitionId = value.DefinitionId;
            DefinitionTitle = value.DefinitionTitle ?? String.Empty;
        }

        DefinitionNameList(String? emptyText = "(n/a)")
        { DefinitionTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<DefinitionNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(DefinitionId), () => nameof(DefinitionTitle));
        }

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<DefinitionNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(DefinitionId), () => nameof(DefinitionTitle));
        }

        static BindingComboList<DefinitionNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<DefinitionNameList> comboList = new BindingComboList<DefinitionNameList>();

            comboList.BuildList(
                source: BusinessData.Model.Definitions,
                constructor: (c) => new DefinitionNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.DefinitionTitle = s.DefinitionTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.DefinitionTitle));
                },
                orderBy: (o) => o.DefinitionTitle,
                areEquel: (a, b) => new DefinitionIndex(a).Equals(b),
                emptyValue: () => new DefinitionNameList(emptyText));

            return comboList;
        }
    }
}

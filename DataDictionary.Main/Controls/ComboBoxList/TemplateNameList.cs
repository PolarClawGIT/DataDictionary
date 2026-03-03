using DataDictionary.BusinessLayer.Obsolete;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record TemplateNameList : ITemplateIndex, ITemplateIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String TemplateTitle { get; private set; } = String.Empty;

        TemplateNameList(ITemplateValue value)
        {
            TemplateId = value.TemplateId;
            TemplateTitle = value.TemplateTitle ?? String.Empty;
        }

        TemplateNameList(String? emptyText = "(n/a)")
        { TemplateTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<TemplateNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(TemplateId), () => nameof(TemplateTitle));
        }

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<TemplateNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(TemplateId), () => nameof(TemplateTitle));
        }

        static BindingComboList<TemplateNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<TemplateNameList> comboList = new BindingComboList<TemplateNameList>();

            comboList.BuildList(
                source: BusinessData.Scripting.Templates,
                constructor: (c) => new TemplateNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.TemplateTitle = s.TemplateTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.TemplateTitle));
                },
                orderBy: (o) => o.TemplateTitle,
                areEquel: (a, b) => new TemplateIndex(a).Equals(b),
                emptyValue: () => new TemplateNameList(emptyText));

            return comboList;
        }
    }
}

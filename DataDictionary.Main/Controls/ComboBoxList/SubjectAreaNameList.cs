using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record class SubjectAreaNameList : ISubjectAreaIndex, ISubjectAreaIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String SubjectAreaTitle { get; private set; } = String.Empty;

        SubjectAreaNameList(ISubjectAreaValue value)
        {
            SubjectAreaId = value.SubjectAreaId;
            SubjectAreaTitle = value.SubjectAreaTitle ?? String.Empty;
        }

        SubjectAreaNameList(String? emptyText = "(n/a)")
        { SubjectAreaTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<SubjectAreaNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(SubjectAreaId), () => nameof(SubjectAreaTitle));
        }

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<SubjectAreaNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(SubjectAreaId), () => nameof(SubjectAreaTitle));
        }

        static BindingComboList<SubjectAreaNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<SubjectAreaNameList> comboList = new BindingComboList<SubjectAreaNameList>();

            comboList.BuildList(
                source: BusinessData.Model.SubjectAreas,
                constructor: (c) => new SubjectAreaNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.SubjectAreaTitle = s.SubjectAreaTitle ?? String.Empty;
                    IBindingPropertyChanged.OnPropertyChanged(comboList, t.PropertyChanged, nameof(t.SubjectAreaTitle));
                },
                orderBy: (o) => o.SubjectAreaTitle,
                areEquel: (a, b) => new SubjectAreaIndex(a).Equals(b),
                emptyValue: () => new SubjectAreaNameList(emptyText));

            return comboList;
        }

    }
}

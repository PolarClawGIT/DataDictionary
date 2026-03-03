using DataDictionary.BusinessLayer.Obsolete;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record DataSourceNameList : IDataSourceIndex, IDataSourceIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? DataSourceId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String DataSourceTitle { get; private set; } = String.Empty;

        DataSourceNameList(IDataSourceValue value)
        {
            DataSourceId = value.DataSourceId;
            DataSourceTitle = value.DataSourceTitle ?? String.Empty;
        }

        DataSourceNameList(String? emptyText = "(n/a)")
        { DataSourceTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<DataSourceNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(DataSourceId), () => nameof(DataSourceTitle));
        }

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<DataSourceNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(DataSourceId), () => nameof(DataSourceTitle));
        }

        static BindingComboList<DataSourceNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<DataSourceNameList> comboList = new BindingComboList<DataSourceNameList>();

            comboList.BuildList(
                source: BusinessData.Scripting.DataSources,
                constructor: (c) => new DataSourceNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.DataSourceTitle = s.DataSourceTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.DataSourceTitle));
                },
                orderBy: (o) => o.DataSourceTitle,
                areEquel: (a, b) => new DataSourceIndex(a).Equals(b),
                emptyValue: () => new DataSourceNameList(emptyText));

            return comboList;
        }
    }
}

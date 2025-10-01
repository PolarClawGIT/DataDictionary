using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record DataSourceNameList : IDataSourceIndex, IDataSourceIndexName
    {
        /// <inheritdoc/>
        public Guid? DataSourceId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String DataSourceTitle { get; private set; } = String.Empty;

        public static void Load(DataGridViewComboBoxColumn control)
        {
            BindingList<DataSourceNameList> list = new BindingList<DataSourceNameList>();

            foreach (DataSourceNameList item in BusinessData.Scripting.DataSources.
                Select(s => new DataSourceNameList()
                {
                    DataSourceId = s.DataSourceId,
                    DataSourceTitle = s.DataSourceTitle ?? String.Empty
                }).OrderBy(o => o.DataSourceTitle))
            { list.Add(item); }

            control.ValueMember = nameof(DataSourceId);
            control.DisplayMember = nameof(DataSourceTitle);
            control.DataSource = list;
        }

        public static void Load(ComboBoxData control)
        {
            BindingList<DataSourceNameList> list = new BindingList<DataSourceNameList>();

            foreach (DataSourceNameList item in BusinessData.Scripting.DataSources.
                Select(s => new DataSourceNameList()
                {
                    DataSourceId = s.DataSourceId,
                    DataSourceTitle = s.DataSourceTitle ?? String.Empty
                }))
            { list.Add(item); }

            control.ValueMember = nameof(DataSourceId);
            control.DisplayMember = nameof(DataSourceTitle);
            control.DataSource = list;
        }

    }
}

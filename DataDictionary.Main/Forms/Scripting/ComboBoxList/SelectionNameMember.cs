using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record SelectionNameMember : ISelectionIndex, ISelectionIndexName
    {
        /// <inheritdoc/>
        public Guid? SelectionId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String SelectionTitle { get; private set; } = String.Empty;

        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultSelectionId = null, String? defaultSelectionTitle = null)
            where T : ISelectionIndex, ISelectionIndexName
        {

            SelectionNameMember memberItem = new SelectionNameMember();
            BindingList<SelectionNameMember> list = new BindingList<SelectionNameMember>();
            list.Add(new SelectionNameMember() { SelectionId = Guid.Empty, SelectionTitle = "(not specified)" });

            if (defaultSelectionId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultSelectionTitle) && source.Count(w => defaultId.Equals(w.SelectionId)) == 0)
            { list.Add(new SelectionNameMember() { SelectionId = defaultId, SelectionTitle = defaultSelectionTitle }); }

            foreach (T item in source.OrderBy(o => o.SelectionTitle))
            {
                if (item.SelectionId is Guid SelectionId && SelectionId != Guid.Empty && item.SelectionTitle is String SelectionTitle)
                { list.Add(new SelectionNameMember() { SelectionId = SelectionId, SelectionTitle = SelectionTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(memberItem.SelectionId);
            control.DisplayMember = nameof(memberItem.SelectionTitle);
        }
    }
}

using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Controls
{
    record ScopeNameMember
    {
        public ScopeType ScopeType { get; set; } = ScopeType.Null;
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameMember Empty { get; } = new ScopeNameMember();

        protected ScopeNameMember() : base() { }

        public static void Load(ComboBoxData control)
        {
            ScopeNameMember scopeNameItem = new ScopeNameMember();
            BindingList<ScopeNameMember> list = new BindingList<ScopeNameMember>();

            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            {
                String name = item.ToName();
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameMember() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {
            ScopeNameMember scopeNameItem = new ScopeNameMember();
            BindingList<ScopeNameMember> list = new BindingList<ScopeNameMember>();

            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            {
                String name = item.ToName();
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameMember() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }
    }
}

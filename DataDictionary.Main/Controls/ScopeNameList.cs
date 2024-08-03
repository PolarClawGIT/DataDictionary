using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Controls
{
    record ScopeNameList
    {
        public ScopeType ScopeType { get; set; } = ScopeType.Null;
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameList Empty { get; } = new ScopeNameList();
        public static ScopeType NullValue { get; } = ScopeType.Null;

        protected ScopeNameList() : base() { }

        public static void Load(ComboBoxData control)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = new BindingList<ScopeNameList>();

            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            {
                String name = ImageEnumeration.Cast(item).DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameList() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = new BindingList<ScopeNameList>();

            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            {
                String name = ImageEnumeration.Cast(item).DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameList() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }
    }
}

using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record ScopeNameList
    {
        public ScopeType ScopeType { get; set; } = ScopeType.Null;
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameList Empty { get; } = new ScopeNameList();
        public static ScopeType NullValue { get; } = ScopeType.Null;

        protected ScopeNameList() : base() { }

        public static void Load(ComboBoxData control, params IEnumerable<ScopeType> scopes)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = new BindingList<ScopeNameList>();

            foreach (ScopeType item in scopes)
            {
                String name = item.GetEnumeration().DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameList() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        public static void Load(ComboBoxData control)
        { Load(control, Enum.GetValues<ScopeType>()); }

        public static void Load(DataGridViewComboBoxColumn control, params IEnumerable<ScopeType> scopes)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = new BindingList<ScopeNameList>();

            foreach (ScopeType item in scopes)
            {
                String name = item.GetEnumeration().DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameList() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {   Load(control, Enum.GetValues<ScopeType>()); }
    }
}

using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record ScopeNameList
    {
        public ScopeType ScopeType { get; init; } = ScopeType.Null;
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameList Empty { get; } = new ScopeNameList();
        public static ScopeType NullValue { get; } = ScopeType.Null;

        protected ScopeNameList() : base() { }

        /// <summary>
        /// Loads the ComboBox with the ScopeType values.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="scopes"></param>
        public static void Load(ComboBox control, params IEnumerable<ScopeType> scopes)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = BuildList(scopes);

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        /// <inheritdoc cref="Load(ComboBox, IEnumerable{ScopeType})"/>
        public static void Load(ToolStripComboBox control, params IEnumerable<ScopeType> scopes)
        { Load(control.ComboBox, scopes); }

        /// <inheritdoc cref="Load(ComboBox, IEnumerable{ScopeType})"/>
        public static void Load(ComboBoxData control, params IEnumerable<ScopeType> scopes)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = BuildList(scopes);

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        /// <inheritdoc cref="Load(ComboBox, IEnumerable{ScopeType})"/>
        public static void Load(DataGridViewComboBoxColumn control, params IEnumerable<ScopeType> scopes)
        {
            ScopeNameList scopeNameItem = new ScopeNameList();
            BindingList<ScopeNameList> list = BuildList(scopes);

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        static BindingList<ScopeNameList> BuildList(params IEnumerable<ScopeType> scopes)
        {
            BindingList<ScopeNameList> list = new BindingList<ScopeNameList>();

            if (scopes.Count() == 0)
            { scopes = Enum.GetValues<ScopeType>(); }

            foreach (ScopeType item in scopes)
            {
                String name = item.GetEnumeration().DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameList() { ScopeType = item, ScopeName = name }); }
            }

            return list;
        }
    }
}

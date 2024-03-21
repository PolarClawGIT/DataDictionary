using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainAlias : UserControl
    {

        Dictionary<ListViewItem, NamedScopeKey> alaisViewItems = new Dictionary<ListViewItem, NamedScopeKey>();
        Stack<NamedScopeKey> navigationStack = new Stack<NamedScopeKey>();

        /// <summary>
        /// The current Alias Item
        /// </summary>
        public NameSpaceItem SelectedAlias { get; private set; } = new NameSpaceItem();

        public DomainAlias() :base()
        {
            InitializeComponent();

            ImageList aliasImages = new ImageList();
            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            { aliasImages.Images.Add(item.ToScopeName(), item.ToImage()); }

            aliasBrowser.SmallImageList = aliasImages;
            aliasBrowser.Columns.Add("Alias");
        }

        public void BindData(BindingSource propertyBinding)
        {
            ScopeNameItem.Load(aliasScopeData);
            INamedScopeItem nameOfValues;
            AliasListLoad();
            
            aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), SelectedAlias, nameof(nameOfValues.MemberFullName), true, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), SelectedAlias, nameof(nameOfValues.Scope), true, DataSourceUpdateMode.OnPropertyChanged, ScopeType.Null));
        }

        private void AliasListLoad()
        {
            aliasBrowser.Items.Clear();
            alaisViewItems.Clear();

            NamedScopeItem parent = BusinessData.NameScope.RootItem;
            NamedScopeKey? parentKey = null;

            if (navigationStack.TryPeek(out parentKey) && parentKey is NamedScopeKey)
            { parent = BusinessData.NameScope[parentKey]; }

            foreach (NamedScopeKey childKey in parent.Children)
            {
                if (BusinessData.NameScope.ContainsKey(childKey))
                {
                    NamedScopeItem child = BusinessData.NameScope[childKey];
                    ListViewItem childItem = new ListViewItem(child.MemberName, child.Scope.ToScopeName());
                    childItem.ToolTipText = child.MemberFullName;

                    alaisViewItems.Add(childItem, childKey);
                    aliasBrowser.Items.Add(childItem);
                }
            }
            aliasBrowser.Sorting = SortOrder.Ascending;
            aliasBrowser.Sort();
            aliasBrowser.Sorting = SortOrder.None;

            if (parentKey is NamedScopeKey)
            { // Doing custom hot Tracks
                ListViewItem parentItem = new ListViewItem(parent.MemberName, parent.Scope.ToScopeName());
                parentItem.Font = new Font(parentItem.Font, FontStyle.Underline);
                parentItem.ForeColor = Color.Blue;

                alaisViewItems.Add(parentItem, parentKey);
                aliasBrowser.Items.Insert(0, parentItem);
            }

            aliasBrowser.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void AliasBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aliasBrowser.SelectedItems.Count > 0
                && alaisViewItems.ContainsKey(aliasBrowser.SelectedItems[0]))
            {
                NamedScopeKey selectedKey = alaisViewItems[aliasBrowser.SelectedItems[0]];
                //SelectedAlias = BusinessData.NameScope[selectedKey];

                var x = BusinessData.NameScope[selectedKey];

                aliasNameData.Text = BusinessData.NameScope[selectedKey].MemberFullName;
                aliasScopeData.SelectedValue = BusinessData.NameScope[selectedKey].Scope;

                if (navigationStack.TryPeek(out NamedScopeKey? parentKey)
                    && selectedKey.Equals(parentKey))
                {
                    navigationStack.Pop();
                    AliasListLoad();
                }

                else if (BusinessData.NameScope[selectedKey].Children.Count > 0)
                {
                    navigationStack.Push(selectedKey);
                    AliasListLoad();
                }
            }
        }

        public event EventHandler? SelectedItemChanged;
        private void AliasSelectedItemChanged(object sender, EventArgs e)
        { if (SelectedItemChanged is EventHandler handler) { handler(sender, e); } }

        public new event EventHandler? Validated;
        private void AliasValidated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void AliasValidating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }

        private void AliasBrowser_DoubleClick(object sender, EventArgs e)
        {
            if (aliasBrowser.SelectedItems.Count > 0
                && alaisViewItems.ContainsKey(aliasBrowser.SelectedItems[0]))
            {
                //NameScopeKey selectedKey = alaisViewItems[aliasBrowser.SelectedItems[0]];
                AliasSelectedItemChanged(this, EventArgs.Empty);
            }
        }
    }
}

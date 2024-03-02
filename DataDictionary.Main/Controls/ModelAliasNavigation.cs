using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System.ComponentModel;

namespace DataDictionary.Main.Controls
{
    partial class ModelAliasNavigation : UserControl
    {
        Dictionary<ListViewItem, NameScopeKey> alaisViewItems = new Dictionary<ListViewItem, NameScopeKey>();
        Stack<NameScopeKey> navigationStack = new Stack<NameScopeKey>();

        /// <summary>
        /// The current Alias Item selected
        /// </summary>
        public NameScopeItem? SelectedAlias { get; private set; }

        public ModelAliasNavigation()
        {
            InitializeComponent();
            aliasList.Columns.Add("Alias");
        }

        private void ModelAliasNavigation_Load(object sender, EventArgs e)
        {
            ImageList aliasImages = new ImageList();
            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            { aliasImages.Images.Add(item.ToScopeName(), item.ToImage()); }

            aliasList.SmallImageList = aliasImages;

            aliasScopeData.DataSource = Enum.GetNames(typeof(ScopeType));

            AliasListLoad();
        }

        private void AliasListLoad()
        {
            aliasList.Items.Clear();
            alaisViewItems.Clear();

            NameScopeItem parent = BusinessData.NameScope.RootItem;
            NameScopeKey? parentKey = null;

            if (navigationStack.TryPeek(out parentKey) && parentKey is NameScopeKey)
            { parent = BusinessData.NameScope[parentKey]; }

            foreach (NameScopeKey childKey in parent.Children)
            {
                NameScopeItem child = BusinessData.NameScope[childKey];
                ListViewItem childItem = new ListViewItem(child.MemberName, child.Scope.ToScopeName());
                childItem.ToolTipText = child.MemberFullName;

                alaisViewItems.Add(childItem, childKey);
                aliasList.Items.Add(childItem);
            }
            aliasList.Sorting = SortOrder.Ascending;
            aliasList.Sort();
            aliasList.Sorting = SortOrder.None;

            if (parentKey is NameScopeKey)
            {
                ListViewItem parentItem = new ListViewItem(parent.MemberName, parent.Scope.ToScopeName());
                parentItem.Font = new Font(parentItem.Font, FontStyle.Underline);
                parentItem.ToolTipText = "navigate to Parent";

                alaisViewItems.Add(parentItem, parentKey);
                aliasList.Items.Insert(0, parentItem);
            }

            aliasList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void AliasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aliasList.SelectedItems.Count > 0
               && alaisViewItems.ContainsKey(aliasList.SelectedItems[0]))
            {
                NameScopeKey selectedKey = alaisViewItems[aliasList.SelectedItems[0]];

                if (navigationStack.TryPeek(out NameScopeKey? parentKey)
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

                aliasNameData.Text = BusinessData.NameScope[selectedKey].MemberFullName;
                aliasScopeData.Text = BusinessData.NameScope[selectedKey].Scope.ToScopeName();
                SelectedAlias = BusinessData.NameScope[selectedKey];

                AliasSelectedItemChanged(this, EventArgs.Empty);
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
    }
}

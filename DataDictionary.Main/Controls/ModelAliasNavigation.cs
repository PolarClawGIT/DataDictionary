using DataDictionary.BusinessLayer.ContextName;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System.ComponentModel;

namespace DataDictionary.Main.Controls
{
    partial class ModelAliasNavigation : UserControl
    {
        Dictionary<ListViewItem, ContextNameKey> alaisViewItems = new Dictionary<ListViewItem, ContextNameKey>();
        Stack<ContextNameKey> navigationStack = new Stack<ContextNameKey>();

        /// <summary>
        /// The current Alias Item selected
        /// </summary>
        public ContextNameItem? SelectedAlias { get; private set; }

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

            ContextNameItem parent = Program.Data.ContextName.RootItem;
            ContextNameKey? parentKey = null;

            if (navigationStack.TryPeek(out parentKey) && parentKey is ContextNameKey)
            { parent = Program.Data.ContextName[parentKey]; }

            foreach (ContextNameKey childKey in parent.Children)
            {
                ContextNameItem child = Program.Data.ContextName[childKey];
                ListViewItem childItem = new ListViewItem(child.MemberName, child.Scope.ToScopeName());
                childItem.ToolTipText = child.MemberFullName;

                alaisViewItems.Add(childItem, childKey);
                aliasList.Items.Add(childItem);
            }
            aliasList.Sorting = SortOrder.Ascending;
            aliasList.Sort();
            aliasList.Sorting = SortOrder.None;

            if (parentKey is ContextNameKey)
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
                ContextNameKey selectedKey = alaisViewItems[aliasList.SelectedItems[0]];

                if (navigationStack.TryPeek(out ContextNameKey? parentKey)
                    && selectedKey.Equals(parentKey))
                {
                    navigationStack.Pop();
                    AliasListLoad();
                }

                else if (Program.Data.ContextName[selectedKey].Children.Count > 0)
                {
                    navigationStack.Push(selectedKey);
                    AliasListLoad();
                }

                aliasNameData.Text = Program.Data.ContextName[selectedKey].MemberFullName;
                aliasScopeData.Text = Program.Data.ContextName[selectedKey].Scope.ToScopeName();
                SelectedAlias = Program.Data.ContextName[selectedKey];

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

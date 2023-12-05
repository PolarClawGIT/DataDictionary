using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Alias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Controls
{
    partial class ModelAliasNavigation : UserControl
    {
        Dictionary<ListViewItem, ModelAliasKey> alaisViewItems = new Dictionary<ListViewItem, ModelAliasKey>();
        Stack<ModelAliasKey> navigationStack = new Stack<ModelAliasKey>();

        /// <summary>
        /// The current Alias Item selected
        /// </summary>
        public ModelAliasItem? SelectedAlias { get; private set; }

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

            ModelAliasItem parent = Program.Data.ModelAlias.RootItem;
            ModelAliasKey? parentKey = null;

            if (navigationStack.TryPeek(out parentKey) && parentKey is ModelAliasKey)
            { parent = Program.Data.ModelAlias[parentKey]; }

            foreach (ModelAliasKey childKey in parent.Children)
            {
                ModelAliasItem child = Program.Data.ModelAlias[childKey];
                ListViewItem childItem = new ListViewItem(child.AliasName, child.ScopeId.ToScopeName());

                alaisViewItems.Add(childItem, childKey);
                aliasList.Items.Add(childItem);
            }
            aliasList.Sorting = SortOrder.Ascending;
            aliasList.Sort();

            if (parentKey is ModelAliasKey)
            {
                ListViewItem parentItem = new ListViewItem(parent.AliasName, parent.ScopeId.ToScopeName());
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
                ModelAliasKey selectedKey = alaisViewItems[aliasList.SelectedItems[0]];

                if (navigationStack.TryPeek(out ModelAliasKey? parentKey)
                    && selectedKey.Equals(parentKey))
                {
                    navigationStack.Pop();
                    AliasListLoad();
                }

                else if (Program.Data.ModelAlias[selectedKey].Children.Count > 0)
                {
                    navigationStack.Push(selectedKey);
                    AliasListLoad();
                }

                aliasNameData.Text = Program.Data.ModelAlias[selectedKey].AliasName;
                aliasScopeData.Text = Program.Data.ModelAlias[selectedKey].ScopeId.ToScopeName();
                SelectedAlias = Program.Data.ModelAlias[selectedKey];

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

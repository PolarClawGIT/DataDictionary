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
        public ModelAliasKey? SelectedItem { get; private set; }

        public ModelAliasNavigation()
        {
            InitializeComponent();
            aliasList.Columns.Add("Alias", aliasList.Width - SystemInformation.VerticalScrollBarWidth);
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

            List<KeyValuePair<ModelAliasKey, ModelAliasItem>> viewItemList = new List<KeyValuePair<ModelAliasKey, ModelAliasItem>>();

            if (navigationStack.TryPeek(out ModelAliasKey? parentKey) && parentKey is ModelAliasKey)
            {
                viewItemList.Add(new KeyValuePair<ModelAliasKey, ModelAliasItem>(parentKey, Program.Data.ModelAlias[parentKey]));
                viewItemList.AddRange(Program.Data.ModelAlias.Where(w => w.Key.SystemParentId == parentKey.SystemId));
            }
            else
            { viewItemList.AddRange(Program.Data.ModelAlias.Where(w => w.Key.SystemParentId is null)); }

            foreach (KeyValuePair<ModelAliasKey, ModelAliasItem> item in viewItemList)
            {
                ListViewItem childItem = new ListViewItem(item.Value.AliasName, item.Value.ScopeId.ToScopeName());
                alaisViewItems.Add(childItem, item.Key);
                aliasList.Items.Add(childItem);
            }
        }

        private void AliasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aliasList.SelectedItems.Count > 0
               && alaisViewItems.ContainsKey(aliasList.SelectedItems[0]))
            {
                SelectedItem = alaisViewItems[aliasList.SelectedItems[0]];

                if (navigationStack.TryPeek(out ModelAliasKey? parentKey)
                    && SelectedItem.Equals(parentKey))
                { navigationStack.Pop(); }

                else if (Program.Data.ModelAlias.Count(w => w.Key.SystemParentId == SelectedItem.SystemId) > 0)
                {
                    navigationStack.Push(SelectedItem);
                    AliasListLoad();
                }

                aliasNameData.Text = Program.Data.ModelAlias[SelectedItem].AliasName;
                aliasScopeData.Text = Program.Data.ModelAlias[SelectedItem].ScopeId.ToScopeName();

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

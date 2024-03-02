using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainAlias : UserControl
    {
        Func<IDomainAlias?> GetCurrent = () => { return null; };

        Dictionary<ListViewItem, NameScopeKey> alaisViewItems = new Dictionary<ListViewItem, NameScopeKey>();
        Stack<NameScopeKey> navigationStack = new Stack<NameScopeKey>();

        /// <summary>
        /// The current Alias Item selected
        /// </summary>
        public NameScopeItem? SelectedAlias { get; private set; }

        public DomainAlias()
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
            IDomainAlias nameOfValues;
            GetCurrent = () => { return propertyBinding.Current as IDomainAlias; };

            aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), propertyBinding, nameof(nameOfValues.AliasName), true, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), propertyBinding, nameof(nameOfValues.Scope), true, DataSourceUpdateMode.OnPropertyChanged, ScopeType.Null));

            AliasListLoad();
            RefreshControls();
        }

        private void AliasListLoad()
        {
            aliasBrowser.Items.Clear();
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
                aliasBrowser.Items.Add(childItem);
            }
            aliasBrowser.Sorting = SortOrder.Ascending;
            aliasBrowser.Sort();
            aliasBrowser.Sorting = SortOrder.None;

            if (parentKey is NameScopeKey)
            { // Doing custom hot Tracks
                ListViewItem parentItem = new ListViewItem(parent.MemberName, parent.Scope.ToScopeName());
                parentItem.Font = new Font(parentItem.Font, FontStyle.Underline);
                parentItem.ForeColor = Color.Blue;

                alaisViewItems.Add(parentItem, parentKey);
                aliasBrowser.Items.Insert(0, parentItem);
            }

            aliasBrowser.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void RefreshControls()
        {
            if (GetCurrent() is IDomainAlias currentRow)
            {
                aliasNameData.Enabled = true;
                aliasScopeData.Enabled = true;
                aliasBrowser.Enabled = true;
            }
            else
            {
                aliasNameData.Enabled = false;
                aliasScopeData.Enabled = false;
                aliasBrowser.Enabled = false;
            }
        }

        private void AliasBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aliasBrowser.SelectedItems.Count > 0
                && alaisViewItems.ContainsKey(aliasBrowser.SelectedItems[0]))
            {
                NameScopeKey selectedKey = alaisViewItems[aliasBrowser.SelectedItems[0]];

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
                NameScopeKey selectedKey = alaisViewItems[aliasBrowser.SelectedItems[0]];
                aliasNameData.Text = BusinessData.NameScope[selectedKey].MemberFullName;
                aliasScopeData.Text = BusinessData.NameScope[selectedKey].Scope.ToScopeName();

                AliasSelectedItemChanged(this, EventArgs.Empty);
            }
        }
    }
}

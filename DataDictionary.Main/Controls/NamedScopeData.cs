using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
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

namespace DataDictionary.Main.Controls
{
    partial class NamedScopeData : UserControl
    {
        INamedScopeData namedScope = BusinessData.NamedScope;
        Dictionary<ListViewItem, NamedScopeKey> crossRefrence = new Dictionary<ListViewItem, NamedScopeKey>();


        public String HeaderText { get { return groupBox.Text; } set { groupBox.Text = value; } }
        public String ApplyText { get { return applyCommand.Text; } set { applyCommand.Text = value; } }
        public Image? ApplyImage { get { return applyCommand.Image; } set { applyCommand.Image = value; } }

        [Browsable(false)]
        public NamedScopeKey? ScopeKey { get; private set; }

        [Browsable(false)]
        public NamedScopePath ScopePath
        {
            get { return scopePath; }
            set { scopePath = value; pathData.Text = value.MemberFullPath; }
        }
        private NamedScopePath scopePath = new NamedScopePath();

        [Browsable(false)]
        public ScopeType Scope
        {
            get { return scope; }
            set { scope = value; scopeData.SelectedValue = value; }
        }
        private ScopeType scope = ScopeType.Null;

        public Boolean ReadOnly
        {
            get { return pathData.ReadOnly; }
            set
            {
                pathData.ReadOnly = value;
                scopeData.ReadOnly = value;
                browser.Enabled = !value;
            }
        }

        /// <summary>
        /// Constructor for NamedScopeData
        /// </summary>
        public NamedScopeData() : base()
        {
            InitializeComponent();

            ImageList aliasImages = new ImageList();
            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            { aliasImages.Images.Add(item.ToName(), item.ToImage()); }

            browser.SmallImageList = aliasImages;
            browser.Columns.Add("Path", browser.Width);

            ScopeNameMember.Load(scopeData);
            SetNamedScope();
        }

        void SetNamedScope()
        {
            browser.Items.Clear();
            crossRefrence.Clear();
            ScopeKey = null;

            foreach (NamedScopeKey item in namedScope.RootKeys())
            {
                INamedScopeValue value = namedScope[item];

                ListViewItem browserItem = new ListViewItem(value.GetTitle(), value.Scope.ToName());
                browserItem.ToolTipText = value.NamedPath.MemberFullPath;

                if (ScopeKey is null) { ScopeKey = value.GetKey(); }
                browser.Items.Add(browserItem);
                crossRefrence.Add(browserItem, item);
            }
        }

        void SetNamedScope(NamedScopeKey key)
        {
            browser.Items.Clear();
            crossRefrence.Clear();

            // Parent Nodes
            foreach (INamedScopeValue value in namedScope.ParentKeys(key)
                .Select(s => namedScope[new NamedScopeKey(s)])
                .OrderBy(o => o.GetPosition())
                .ThenBy(o => o.GetTitle()))
            {
                ListViewItem browserItem = new ListViewItem(value.GetTitle(), value.Scope.ToName());
                browserItem.ToolTipText = value.NamedPath.MemberFullPath;

                browser.Items.Add(browserItem);
                crossRefrence.Add(browserItem, value.GetKey());
            }

            // Root nodes if no parents
            if (namedScope.ParentKeys(key).Count == 0)
            {
                foreach (INamedScopeValue value in namedScope.RootKeys()
                    .Where(w => !key.Equals(w))
                    .Select(s => namedScope[new NamedScopeKey(s)])
                    .OrderBy(o => o.GetPosition())
                    .ThenBy(o => o.GetTitle()))
                {
                    ListViewItem browserItem = new ListViewItem(value.GetTitle(), value.Scope.ToName());
                    browserItem.ToolTipText = value.NamedPath.MemberFullPath;

                    if (ScopeKey is null) { ScopeKey = value.GetKey(); }
                    browser.Items.Add(browserItem);
                    crossRefrence.Add(browserItem, value.GetKey());
                }
            }

            // Current Node
            INamedScopeValue currentValue = namedScope[key];
            ListViewItem currentItem = new ListViewItem(currentValue.GetTitle(), currentValue.Scope.ToName());
            currentItem.ToolTipText = currentValue.NamedPath.MemberFullPath;
            currentItem.Font = new Font(currentItem.Font, FontStyle.Underline);
            currentItem.ForeColor = Color.Blue;

            browser.Items.Add(currentItem);
            crossRefrence.Add(currentItem, key);
            ScopeKey = currentValue.GetKey();

            // Child Nodes
            foreach (INamedScopeValue value in namedScope.ChildrenKeys(key)
                .Select(s => namedScope[new NamedScopeKey(s)]).
                OrderBy(o => o.Scope).
                ThenBy(o => o.GetPosition()).
                ThenBy(o => o.GetTitle()))
            {
                ListViewItem browserItem = new ListViewItem(value.GetTitle(), value.Scope.ToName());
                browserItem.ToolTipText = value.NamedPath.MemberFullPath;
                browserItem.IndentCount = 1;

                browser.Items.Add(browserItem);
                crossRefrence.Add(browserItem, value.GetKey());
            }

            // Update form to match selected item
            if (ScopeKey is not null && namedScope.ContainsKey(ScopeKey) && namedScope[ScopeKey] is INamedScopeValue setValue)
            {
                ScopePath = setValue.NamedPath;
                Scope = setValue.Scope;
            }
        }

        void SetNamedScope(NamedScopePath path)
        {
            if (namedScope.PathKeys(path).FirstOrDefault() is NamedScopeKey key)
            { SetNamedScope(key); }
            else { ScopeKey = null; ScopePath = path; }
        }

        private void Browser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (browser.SelectedItems.Count > 0
                && crossRefrence.ContainsKey(browser.SelectedItems[0]))
            {
                NamedScopeKey key = crossRefrence[browser.SelectedItems[0]];
                SetNamedScope(key);
            }
        }

        public event EventHandler? OnApply;
        private void applyCommand_Click(object sender, EventArgs e)
        {
            if (OnApply is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }

        private void PathData_Validating(object sender, CancelEventArgs e)
        {
            NamedScopePath value = new NamedScopePath(NamedScopePath.Parse(pathData.Text).ToArray());
            SetNamedScope(value);
            pathData.Text = value.MemberFullPath;
        }

        private void PathData_Validated(object sender, EventArgs e)
        {

        }

    }
}

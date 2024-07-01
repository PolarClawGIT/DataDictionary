﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;

namespace DataDictionary.Main.Controls
{
    partial class NamedScopeData : UserControl
    {
        INamedScopeData namedScope = BusinessData.NamedScope;
        Dictionary<ListViewItem, NamedScopeIndex> crossRefrence = new Dictionary<ListViewItem, NamedScopeIndex>();

        public String HeaderText { get { return groupBox.Text; } set { groupBox.Text = value; } }
        public String ApplyText { get { return applyCommand.Text; } set { applyCommand.Text = value; } }
        public Image? ApplyImage { get { return applyCommand.Image; } set { applyCommand.Image = value; } }

        [Browsable(false)]
        public NamedScopeIndex? ScopeKey { get; private set; }

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
            get { return !groupBox.Enabled; }
            set { groupBox.Enabled = !value; }
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

            ScopeNameList.Load(scopeData);
            SetNamedScope();
        }

        void SetNamedScope()
        {
            browser.Items.Clear();
            crossRefrence.Clear();
            ScopeKey = null;

            foreach (NamedScopeIndex item in namedScope.RootKeys())
            {
                INamedScopeValue value = namedScope.GetValue(item);

                ListViewItem browserItem = new ListViewItem(value.Title, value.Scope.ToName());
                browserItem.ToolTipText = value.NamedPath.MemberFullPath;

                if (ScopeKey is null) { ScopeKey = value.Index; }
                browser.Items.Add(browserItem);
                crossRefrence.Add(browserItem, item);
            }
        }

        void SetNamedScope(NamedScopeIndex key)
        {
            browser.Items.Clear();
            crossRefrence.Clear();

            // Parent Nodes
            foreach (INamedScopeValue value in namedScope.ParentKeys(key).
                Select(s => namedScope.GetValue(s)).
                OrderBy(o => o.OrdinalPosition).
                ThenBy(o => o.Title))
            {
                ListViewItem browserItem = new ListViewItem(value.Title, value.Scope.ToName());
                browserItem.ToolTipText = value.NamedPath.MemberFullPath;

                browser.Items.Add(browserItem);
                crossRefrence.Add(browserItem, value.Index);
            }

            // Root nodes if no parents
            if (namedScope.ParentKeys(key).Count == 0)
            {
                foreach (INamedScopeValue value in namedScope.RootKeys().
                    Where(w => !key.Equals(w)).
                    Select(s => namedScope.GetValue(s)).
                    OrderBy(o => o.OrdinalPosition).
                    ThenBy(o => o.Title))
                {
                    ListViewItem browserItem = new ListViewItem(value.Title, value.Scope.ToName());
                    browserItem.ToolTipText = value.NamedPath.MemberFullPath;

                    if (ScopeKey is null) { ScopeKey = value.Index; }
                    browser.Items.Add(browserItem);
                    crossRefrence.Add(browserItem, value.Index);
                }
            }

            // Current Node
            INamedScopeValue currentValue = namedScope.GetValue(key);
            ListViewItem currentItem = new ListViewItem(currentValue.Title, currentValue.Scope.ToName());
            currentItem.ToolTipText = currentValue.NamedPath.MemberFullPath;
            currentItem.Font = new Font(currentItem.Font, FontStyle.Underline);
            currentItem.ForeColor = Color.Blue;

            browser.Items.Add(currentItem);
            crossRefrence.Add(currentItem, key);
            ScopeKey = currentValue.Index;

            // Child Nodes
            foreach (var value in namedScope.ChildrenKeys(key).
                Select(s => namedScope.GetValue(s)).
                OrderBy(o => o.Scope).
                ThenBy(o => o.OrdinalPosition).
                ThenBy(o => o.Title))
            {
                ListViewItem browserItem = new ListViewItem(value.Title, value.Scope.ToName());
                browserItem.ToolTipText = value.NamedPath.MemberFullPath;
                browserItem.IndentCount = 1;

                browser.Items.Add(browserItem);
                crossRefrence.Add(browserItem, value.Index);
            }

            // Update form to match selected item
            if (ScopeKey is not null && namedScope.ContainsKey(ScopeKey) && namedScope.GetValue(ScopeKey) is INamedScopeValue setValue)
            {
                ScopePath = setValue.NamedPath;
                Scope = setValue.Scope;
            }
        }

        void SetNamedScope(NamedScopePath path)
        {
            if (namedScope.PathKeys(path).FirstOrDefault() is NamedScopeIndex key)
            { SetNamedScope(key); }
            else { ScopeKey = null; ScopePath = path; }
        }

        private void Browser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (browser.SelectedItems.Count > 0
                && crossRefrence.ContainsKey(browser.SelectedItems[0]))
            {
                NamedScopeIndex key = crossRefrence[browser.SelectedItems[0]];
                SetNamedScope(key);
            }
        }

        public event EventHandler? OnApply;
        private void ApplyCommand_Click(object sender, EventArgs e)
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

        private void scopeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scopeData.SelectedValue is ScopeType value)
            { Scope = value; }
        }
    }
}

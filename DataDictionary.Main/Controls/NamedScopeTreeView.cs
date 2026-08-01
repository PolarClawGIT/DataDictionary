using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// TreeView Control wired up to the NamedScope.
    /// </summary>
    partial class NamedScopeTreeView : UserControl
    {
        NamedScopeTreeViewData data;
        INamedScopeData sourceData = BusinessData.NamedScope;

        /// <summary>
        /// The Worker Method of ApplicationData.DoWork
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; set; }

        /// <summary>
        /// Text that appears at the top of the control
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String HeaderText
        {
            get { return headerTitle.Text ?? String.Empty; }
            set { headerTitle.Text = value; }
        }

        public NamedScopeTreeView()
        {
            InitializeComponent();

            refreshCommand.Image = Resources.Icon_TreeView.MergeImage(Resources.ItemRefresh);
            reloadCommand.Image = Resources.Icon_TreeView.MergeImage(Resources.ItemSync);

            data = new NamedScopeTreeViewData(treeViewData);
            treeViewData.ImageList = new ImageList();
            treeViewData.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());

            DoWork = (work, complete) =>
            {   // No worker assigned, do the work in the foreground.
                foreach (WorkItem item in work)
                { item.DoWork(); }

                if (complete is not null)
                { complete(new RunWorkerCompletedEventArgs(this, null, false)); }
            };
        }

        /// <summary>
        /// Causes the Tree to re-read the Data and rebuild.
        /// </summary>
        public void RefreshCommand()
        {
            DoWork(RefreshWork(), onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        /// <summary>
        /// Create Work Items version of the RefreshCommand
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkItem> RefreshWork()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(data.BeginUpdate());
            work.AddRange(data.BuildNodes(sourceData));
            work.AddRange(data.EndUpdate());
            return work;
        }

        private void RefreshCommand_Click(object sender, EventArgs e)
        { RefreshCommand(); }

        /// <summary>
        /// Causes the Tree to re-load the Data and rebuild.
        /// </summary>
        public void ReloadCommand()
        {
            DoWork(ReloadWork(), onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        /// <summary>
        /// Create Work Items version of the ReloadCommand
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkItem> ReloadWork()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(data.BeginUpdate());
            work.AddRange(sourceData.Load());
            work.AddRange(data.BuildNodes(sourceData));
            work.AddRange(data.EndUpdate());
            return work;
        }

        private void ReloadCommand_Click(object sender, EventArgs e)
        { ReloadCommand(); }

        /// <summary>
        /// A NamedScope item was selected.
        /// </summary>
        public event EventHandler<NamedScopeValueEventArgs>? OnNamedScopeSelected;

        /// <summary>
        /// Used in determine if the node should expand the node or not.
        /// </summary>
        /// <remarks>
        /// Normally, a double click anywhere on the node will expand/collapse the node.
        /// 
        /// The code Event NodeMouseClick (happens first), captures what was clicked (to be passed to Expand/Collapse).
        /// The code Events Before Expand/Collapse, depending on if +/- clicked cancel the action.
        /// The code Events After Expand/Collapse, reset the flag back to null (the event hand been handled).
        /// The code Event NodeMouseDoubleClick (happens last), determine if +/- was clicked an ignore the event if so.
        ///   Null = Click was not fired. Node.Expanded() or Node.Collapse() is used. Expand/Collapse reset to Null.
        ///   True = +/- of the node was clicked (possibly double clicked).
        ///   False = Something other then the +/- of the node was clicked (possibly double clicked).
        /// </remarks>
        Boolean? isTreeNodePlusMinus = null;

        private void TreeViewData_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is not null && e.Node.TreeView is not null)
            { isTreeNodePlusMinus = e.Node.TreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.PlusMinus; }

            if (e.Clicks > 1) { throw new NotImplementedException(); } // This never occurs even on a double click.
        }

        private void TreeViewData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (isTreeNodePlusMinus == false) { e.Cancel = true; } // AfterCollapse does not fire
            else if (isTreeNodePlusMinus == true) { e.Cancel = false; }
            else { } // Was not triggered by Click event

            isTreeNodePlusMinus = null; // Reset to undetermined avoid calling above logic
        }

        private void TreeViewData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (isTreeNodePlusMinus == false) { e.Cancel = true; } // AfterExpanded does not fire
            else if (isTreeNodePlusMinus == true) { e.Cancel = false; }
            else { } // Was not triggered by Click event

            isTreeNodePlusMinus = null; // Reset to undetermined avoid calling above logic
        }

        private void TreeViewData_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Need to get the Hit Location itself because the flag may have been reset.
            if (e.Node is not null
                && e.Node.TreeView is not null
                && e.Node.TreeView.HitTest(e.Location).Location != TreeViewHitTestLocations.PlusMinus
                && OnNamedScopeSelected is EventHandler<NamedScopeValueEventArgs> hander
                && data.GetValue(e.Node) is INamedScopeSourceValue value)
            { hander(this, new NamedScopeValueEventArgs(value)); }
        }
    }

    class NamedScopeValueEventArgs : EventArgs
    {
        public INamedScopeSourceValue Value { get; }
        public NamedScopeValueEventArgs(INamedScopeSourceValue value) : base()
        { Value = value; }
    }

}

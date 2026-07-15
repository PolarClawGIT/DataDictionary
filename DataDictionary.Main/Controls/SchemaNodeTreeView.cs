using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// TreeView Control wired up to the SchemaNode.
    /// </summary>
    partial class SchemaNodeTreeView : UserControl
    {
        /// <summary>
        /// The Worker Method of ApplicationData.DoWork
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; set; }

        SchemaNodeTreeViewData data;

        /// <summary>
        /// Text that appears at the top of the control
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String HeaderText
        {
            get { return headerTitle.Text ?? String.Empty; }
            set { headerTitle.Text = value; }
        }

        public SchemaNodeTreeView()
        {
            InitializeComponent();

            treeViewData.ImageList = new ImageList();
            treeViewData.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());
            treeViewData.ImageList.AddImages(Enum.GetValues<ObjectValueType>().ToList());
            refreshCommand.Image = Resources.Icon_XMLSchema.MergeImage(Resources.ItemRefresh);
            reloadCommand.Image = Resources.Icon_XMLSchema.MergeImage(Resources.ItemSync);
            
            data = new SchemaNodeTreeViewData(treeViewData);

            DoWork = (work, complete) =>
            {   // No worker assigned, do the work in the foreground.
                foreach (WorkItem item in work)
                { item.DoWork(); }

                if (complete is not null)
                { complete(new RunWorkerCompletedEventArgs(this, null, false)); }
            };
        }

        /// <summary>
        /// A NamedScope item was selected.
        /// </summary>
        public event EventHandler<XmlBuilderIndex>? OnNodeSelected;

        public void LoadTree(IEnumerable<XmlBuilder> builders)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(data.BeginUpdate());
            work.AddRange(data.BuildNodes(builders));
            work.AddRange(data.EndUpdate());

            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

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
                && OnNodeSelected is EventHandler<XmlBuilderIndex> hander
                && data.GetValue(e.Node) is XmlBuilderIndex value)
            { hander(this, new XmlBuilderIndex(value)); }
        }
    }
}

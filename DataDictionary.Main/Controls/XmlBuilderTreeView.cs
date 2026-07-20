using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.Scripting;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// TreeView Control wired up to the XmlBuilder.
    /// </summary>
    partial class XmlBuilderTreeView : UserControl
    {
        /// <summary>
        /// The Worker Method of ApplicationData.DoWork
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; set; }

        XmlBuilderTreeViewData data;

        /// <summary>
        /// Text that appears at the top of the control
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String HeaderText
        {
            get { return headerTitle.Text ?? String.Empty; }
            set { headerTitle.Text = value; }
        }

        /// <summary>
        /// Sets the Enabled of the useDefaultCommand
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Boolean IsUseDefault
        {
            get { return useDefaultCommand.Enabled; }
            set { useDefaultCommand.Enabled = value; }
        }

        /// <summary>
        /// Sets the Enabled of the overrideCommand
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Boolean IsOverride
        {
            get { return overrideCommand.Enabled; }
            set { overrideCommand.Enabled = value; }
        }

        public XmlBuilderTreeView()
        {
            InitializeComponent();

            treeViewData.ImageList = new ImageList();
            treeViewData.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());
            treeViewData.ImageList.AddImages(Enum.GetValues<ObjectValueType>().ToList());
            viewDetailsCommand.Image = Resources.Icon_XMLSchema.MergeImage(Resources.ItemBrowse);
            useDefaultCommand.Image = Resources.Icon_XMLSchema.MergeImage(Resources.ItemDetached);
            overrideCommand.Image = Resources.Icon_XMLSchema.MergeImage(Resources.ItemImport);

            IsUseDefault = false;

            data = new XmlBuilderTreeViewData(treeViewData);

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

            if (e.Node is not null
                && e.Node.TreeView is not null
                && e.Node.TreeView.HitTest(e.Location).Location != TreeViewHitTestLocations.PlusMinus
                && data.GetValue(e.Node) is XmlBuilderIndex value)
            { headerTitle.Text = value.Member; }

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

        // TODO: Remodel using the "CommandButton" from the ApplicationData form.
        // Can the CommandButton be moved out of the ApplicationData? Its own control?

        /// <summary>
        /// Event raised when a Button is pressed. The type of button is returned.
        /// </summary>
        public event EventHandler<ButtonType>? OnButtonClick;

        private void ViewDetailsCommand_Click(object sender, EventArgs e)
        {
            if (OnButtonClick is EventHandler<ButtonType> handler)
            { handler(this, ButtonType.Browse); }
        }

        private void OverrideCommand_Click(object sender, EventArgs e)
        {
            if (OnButtonClick is EventHandler<ButtonType> handler)
            { handler(this, ButtonType.Add); }
        }

        private void UseDefaultCommand_Click(object sender, EventArgs e)
        {
            if (OnButtonClick is EventHandler<ButtonType> handler)
            { handler(this, ButtonType.Delete); }
        }
    }
}

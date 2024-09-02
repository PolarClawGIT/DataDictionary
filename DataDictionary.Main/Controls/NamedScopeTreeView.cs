using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// TreeView Control wired up to the NamedScope.
    /// </summary>
    partial class NamedScopeTreeView : UserControl
    {
        NamedScopeTreeViewData data;

        /// <summary>
        /// The Worker Method of ApplicationData.DoWork
        /// </summary>
        public Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?>? DoWork { get; set; } = null;

        public NamedScopeTreeView()
        {
            InitializeComponent();

            data = new NamedScopeTreeViewData(treeViewData);
            treeViewData.ImageList = ImageEnumeration.AsImageList();
        }

        private void RefreshCommand_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(data.BeginUpdate());
            work.AddRange(data.BuildNodes(BusinessData.NamedScope));
            work.AddRange(data.EndUpdate());

            if (DoWork is not null)
            { DoWork(work, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {

            }
        }

        /// <summary>
        /// Causes the Reload event to occur.
        /// </summary>
        public void Reload()
        { ReloadCommand_Click(this, EventArgs.Empty); }

        private void ReloadCommand_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(data.BeginUpdate());
            work.AddRange(BusinessData.LoadNamedScope());
            work.AddRange(data.BuildNodes(BusinessData.NamedScope));
            work.AddRange(data.EndUpdate());

            if (DoWork is not null)
            { DoWork(work, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {

            }
        }

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
            isTreeNodePlusMinus = e.Node.TreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.PlusMinus;
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
            Boolean isPlusMinus = e.Node.TreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.PlusMinus;

            if (!isPlusMinus
                && OnNamedScopeSelected is EventHandler<NamedScopeValueEventArgs> hander
                && data.GetValue(e.Node) is INamedScopeValue value)
            { hander(this, new NamedScopeValueEventArgs(value)); }
        }
    }

    class NamedScopeValueEventArgs : EventArgs
    {
        public INamedScopeValue Value { get; }
        public NamedScopeValueEventArgs(INamedScopeValue value) : base()
        { Value = value; }
    }

}

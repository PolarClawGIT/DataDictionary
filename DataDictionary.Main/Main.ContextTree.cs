using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main
    {
        protected override void HandleMessage(RefreshNavigation message)
        {
            base.HandleMessage(message);
            List<WorkItem> work = new List<WorkItem>();
            //work.AddRange(BusinessData.LoadNamedScope());
            work.AddRange(contextNameNavigation.Load(BusinessData.NamedScope));
            this.DoWork(work);
        }

        private void RefreshCommand_Click(object? sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            //work.AddRange(BusinessData.LoadNamedScope());
            work.AddRange(contextNameNavigation.Load(BusinessData.NamedScope));
            this.DoWork(work);
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

        private void contextNameNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            isTreeNodePlusMinus = e.Node.TreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.PlusMinus;
            if (e.Clicks > 1) { throw new NotImplementedException(); } // This never occurs even on a double click.
        }

        private void contextNameNavigation_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (isTreeNodePlusMinus == false) { e.Cancel = true; } // AfterExpanded does not fire
            else if (isTreeNodePlusMinus == true) { e.Cancel = false; }
            else { } // Was not triggered by Click event

            isTreeNodePlusMinus = null; // Reset to undetermined avoid calling above logic
        }

        private void contextNameNavigation_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (isTreeNodePlusMinus == false) { e.Cancel = true; } // AfterCollapse does not fire
            else if (isTreeNodePlusMinus == true) { e.Cancel = false; }
            else { } // Was not triggered by Click event

            isTreeNodePlusMinus = null; // Reset to undetermined avoid calling above logic
        }

        private void DataSourceNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Need to get the Hit Location itself because the flag may have been reset.
            Boolean isPlusMinus = e.Node.TreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.PlusMinus;

            if (!isPlusMinus && contextNameNavigation.SelectedNode is TreeNode node)
            {
                if (node.GetNamedScope() is INamedScopeSourceValue target)
                {
                    dynamic dataNode = target;
                    Activate(dataNode);
                }
            }
        }

        void Activate(ICatalogValue catalogItem)
        { Activate((data) => new Forms.Database.DbCatalog(catalogItem), catalogItem); }

        void Activate(BusinessLayer.Database.ISchemaValue schemaItem)
        { Activate((data) => new Forms.Database.DbSchema(schemaItem), schemaItem); }

        void Activate(ITableValue tableItem)
        { Activate((data) => new Forms.Database.DbTable(tableItem), tableItem); }

        void Activate(ITableColumnValue columnItem)
        { Activate((data) => new Forms.Database.DbTableColumn(columnItem), columnItem); }

        void Activate(IConstraintValue constraintItem)
        { Activate((data) => new Forms.Database.DbConstraint(constraintItem), constraintItem); }

        void Activate(IRoutineValue routineItem)
        { Activate((data) => new Forms.Database.DbRoutine(routineItem), routineItem); }

        void Activate(IRoutineParameterValue routineParameterItem)
        { Activate((data) => new Forms.Database.DbRoutineParameter(routineParameterItem), routineParameterItem); }

        void Activate(IDomainValue domainItem)
        { Activate((data) => new Forms.Database.DbDomain(domainItem), domainItem); }

        void Activate(ILibrarySourceValue sourceItem)
        { Activate((data) => new Forms.Library.LibrarySource(sourceItem), sourceItem); }

        void Activate(ILibraryMemberValue memberItem)
        { Activate((data) => new Forms.Library.LibraryMember(memberItem), memberItem); }

        void Activate(AttributeValue attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        void Activate(EntityValue entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity(entityItem), entityItem); }

        void Activate(SubjectAreaValue subjectItem)
        { Activate((data) => new Forms.Model.ModelSubjectArea(subjectItem), subjectItem); }

        void Activate(ModelValue modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

        void Activate(BusinessLayer.Scripting.DefinitionValue schemaItem)
        { Activate((data) => new Forms.Scripting.SchemaManager(schemaItem), schemaItem); }

        void Activate(BusinessLayer.Scripting.SelectionValue selectionItem)
        { Activate((data) => new Forms.Scripting.SelectionManager(selectionItem), selectionItem); }

        void Activate(BusinessLayer.Scripting.TransformValue transformItem)
        { Activate((data) => new Forms.Scripting.TransformManager(transformItem), transformItem); }
    }
}

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
            this.DoWork(contextNameNavigation.Load(BusinessData.NamedScope));
        }

        private void RefreshCommand_Click(object? sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(BusinessData.LoadNamedScope());
            work.AddRange(contextNameNavigation.Load(BusinessData.NamedScope));
            this.DoWork(work);
        }

        /// <summary>
        /// Used in determine if the code should expand the node or not.
        /// </summary>
        /// <remarks>
        /// The code in NodeMouseClick (happens first) captures where the mouse was clicked
        /// The code in Before Expand/Collapse ignores any click not on the +/-
        /// The code in the NodeMouseDoubleClick (happens last) ignores the double click on the +/-
        /// Normally, a double click anywhere on the node will expand/collapse the node.
        /// This limits expand/collapse to the use of the +/- only.
        /// </remarks>
        TreeViewHitTestLocations treeViewHitTest = TreeViewHitTestLocations.None;

        private void contextNameNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        { treeViewHitTest = e.Node.TreeView.HitTest(e.Location).Location; }

        private void contextNameNavigation_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        { if (treeViewHitTest != TreeViewHitTestLocations.PlusMinus) { e.Cancel = true; } }

        private void contextNameNavigation_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        { if (treeViewHitTest != TreeViewHitTestLocations.PlusMinus) { e.Cancel = true; } }

        private void DataSourceNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeViewHitTest != TreeViewHitTestLocations.PlusMinus // If the +/- was double clicked, ignore that.
                && contextNameNavigation.SelectedNode is TreeNode node)
            {
                INamedScopeValue? item = node.GetNamedScope();
                if (item is INamedScopeValue taget)
                {
                    dynamic dataNode = taget;
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
        { Activate((data) => new Forms.Domain.ModelSubjectArea(subjectItem), subjectItem); }

        void Activate(ModelValue modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

        void Activate(BusinessLayer.Scripting.SchemaValue schemaItem)
        { Activate((data) => new Forms.Scripting.SchemaManager(schemaItem), schemaItem); }
    }
}

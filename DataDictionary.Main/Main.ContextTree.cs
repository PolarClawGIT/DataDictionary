using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main
    {
        Dictionary<TreeNode, NamedScopeItem> contextNodes = new Dictionary<TreeNode, NamedScopeItem>();

        List<NamedScopeItem> expandedContextNodes = new List<NamedScopeItem>();
        void ClearTree()
        {
            contextNameNavigation.Invoke(() =>
            {
                expandedContextNodes.Clear();
                expandedContextNodes.AddRange(contextNodes
                    .Where(w => w.Key.IsExpanded
                        || (w.Key.Nodes.Count == 0
                            && w.Key.Parent is not null
                            && w.Key.Parent.IsExpanded))
                    .Select(s => s.Value));
                contextNameNavigation.Nodes.Clear();
            });
        }

        void BuildTree(Action<Int32, Int32>? progress = null)
        {
            contextNodes.Clear();
            IDictionary<TreeNode, NamedScopeItem> nodes = contextNameNavigation.Load(BusinessData.NameScope, progress);

            foreach (KeyValuePair<TreeNode, NamedScopeItem> item in nodes)
            { contextNodes.Add(item.Key, item.Value); }
        }


        private void NameScope_ListChanged(object? sender, NamedScopeChangedEventArgs e)
        { // TODO: Not working as expected
            TreeNodeCollection taget = contextNameNavigation.Nodes;

            if (e.ChangedType == NameScopedChangedType.EndBatch)
            { RefreshTree(); } // TODO: Causes a circler loop
            else if (e.ChangedType == NameScopedChangedType.ItemAdded && e.Item is NamedScopeItem addedItem)
            {// Handle Add
                if (contextNodes.FirstOrDefault(w => addedItem.SystemParentKey is not null && addedItem.SystemParentKey.Equals(w.Value.SystemKey)).Key is TreeNode parentNode)
                { taget = parentNode.Nodes; }

                TreeNode node = contextNameNavigation.Invoke<TreeNode>(() =>
                {
                    TreeNode newNode = taget.Add(addedItem.MemberName);
                    newNode.ImageKey = addedItem.Scope.ToScopeName();
                    newNode.SelectedImageKey = addedItem.Scope.ToScopeName();
                    contextNodes.Add(newNode, addedItem);
                    newNode.ToolTipText = addedItem.MemberFullName;
                    addedItem.PropertyChanged += Item_PropertyChanged;

                    return newNode;

                    void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
                    { newNode.Text = addedItem.MemberName; }
                });
            }

            else if (e.ChangedType == NameScopedChangedType.ItemDeleted && e.Item is NamedScopeItem deletedItem)
            { // Handle Remove
                NamedScopeKey deleteKey = deletedItem.SystemKey;
                if (contextNodes.FirstOrDefault(w => deletedItem.SystemKey.Equals(w.Value.SystemKey)).Key is TreeNode node)
                {
                    contextNameNavigation.Invoke(() =>
                    {
                        node.Remove();
                        contextNodes.Remove(node);
                    });
                }
            }
        }

        protected override void HandleMessage(RefreshNavigation message)
        {
            base.HandleMessage(message);
            RefreshTree();
        }

        private void RefreshCommand_Click(object? sender, EventArgs e)
        { RefreshTree(); }

        private void RefreshTree()
        {
            BusinessData.NameScope.Clear();
            ClearTree();

            List<WorkItem> work = new List<WorkItem>();
            List<NamedScopeItem> names = new List<NamedScopeItem>();
            Action<Int32, Int32> progress = (x, y) => { };
            work.AddRange(BusinessData.Export(names));
            work.AddRange(BusinessData.NameScope.Import(names));

            WorkItem treeWork = new WorkItem() { DoWork = () => { BuildTree(progress); } };
            progress = treeWork.OnProgressChanged;
            work.Add(treeWork);

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { ExpandedTree(); }
        }

        private void ExpandedTree()
        {
            foreach (NamedScopeItem item in expandedContextNodes)
            {
                NamedScopeKey key = new NamedScopeKey(item);

                var target = contextNodes.FirstOrDefault(w => key.Equals(w.Value));
                if (target.Key is not null && !target.Key.IsExpanded)
                { target.Key.ExpandParent(); }
            }
        }

        private void DataSourceNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (contextNameNavigation.SelectedNode is TreeNode node
                && contextNodes.ContainsKey(node)
                && contextNodes[node].Source is Object taget)
            {
                dynamic dataNode = taget;
                Activate(dataNode);
            }
        }


        void Activate(DbCatalogItem catalogItem)
        { Activate((data) => new Forms.Database.DbCatalog(catalogItem), catalogItem); }

        void Activate(DbSchemaItem schemaItem)
        { Activate((data) => new Forms.Database.DbSchema(schemaItem), schemaItem); }

        void Activate(DbTableItem tableItem)
        { Activate((data) => new Forms.Database.DbTable(tableItem), tableItem); }

        void Activate(DbTableColumnItem columnItem)
        { Activate((data) => new Forms.Database.DbTableColumn(columnItem), columnItem); }

        void Activate(DbConstraintItem constraintItem)
        { Activate((data) => new Forms.Database.DbConstraint(constraintItem), constraintItem); }

        void Activate(DbRoutineItem routineItem)
        { Activate((data) => new Forms.Database.DbRoutine(routineItem), routineItem); }

        void Activate(DbRoutineParameterItem routineParameterItem)
        { Activate((data) => new Forms.Database.DbRoutineParameter(routineParameterItem), routineParameterItem); }

        void Activate(DbDomainItem domainItem)
        { Activate((data) => new Forms.Database.DbDomain(domainItem), domainItem); }

        void Activate(LibrarySourceItem sourceItem)
        { Activate((data) => new Forms.Library.LibrarySource(sourceItem), sourceItem); }

        void Activate(LibraryMemberItem memberItem)
        { Activate((data) => new Forms.Library.LibraryMember(memberItem), memberItem); }

        void Activate(AttributeItem attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity(entityItem), entityItem); }

        void Activate(ModelSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.ModelSubjectArea(subjectItem), subjectItem); }

        void Activate(ModelItem modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

    }
}

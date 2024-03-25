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
            BusinessData.NameScope.ListChanged -= NameScope_ListChanged;

            contextNameNavigation.Invoke(() =>
            {
                expandedContextNodes.Clear();
                expandedContextNodes.AddRange(contextNodes.Where(w => w.Key.IsExpanded).Select(s => s.Value));
                contextNameNavigation.Nodes.Clear();
                contextNodes.Clear();
            });
        }

        void BuildTree()
        {
            contextNameNavigation.BeginUpdate();
            contextNameNavigation.UseWaitCursor = true;
            contextNameNavigation.Enabled = false;

            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem treeWork = new WorkItem()
            { WorkName = "Load Context Name Tree", DoWork = LoadTree };
            progress = treeWork.OnProgressChanged;

            work.Add(treeWork);
            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                foreach (NamedScopeItem item in expandedContextNodes)
                {
                    NamedScopeKey key = new NamedScopeKey(item);

                    KeyValuePair<TreeNode, NamedScopeItem> value = contextNodes.FirstOrDefault(w => key.Equals(w.Value));

                    if (value.Key is TreeNode node)
                    { node.ExpandParent(); }
                }

                foreach (TreeNode item in contextNodes.Where(w => expandedContextNodes.Contains(w.Value)).Select(s => s.Key).ToList())
                { item.ExpandParent(); }

                contextNameNavigation.UseWaitCursor = false;
                contextNameNavigation.Enabled = true;
                contextNameNavigation.EndUpdate();
                BusinessData.NameScope.ListChanged += NameScope_ListChanged;
            }

            void LoadTree()
            {
                Int32 totalWork = BusinessData.NameScope.Count;
                Int32 completeWork = 0;
                progress(completeWork, totalWork);

                CreateNodes(
                    contextNameNavigation.Nodes,
                    BusinessData.NameScope.RootItem.Children.Select(s => BusinessData.NameScope[s]).ToList());

                void CreateNodes(TreeNodeCollection target, IEnumerable<NamedScopeItem> items)
                {
                    foreach (IGrouping<ScopeType, NamedScopeItem>? scopeGroup in items.GroupBy(g => g.Scope).OrderBy(o => o.Key))
                    {
                        TreeNodeCollection nodes = target;

                        if (scopeGroup.Count() > 1)
                        {
                            TreeNode scopeNode = contextNameNavigation.Invoke<TreeNode>(() =>
                            {
                                TreeNode newNode = target.Add(scopeGroup.Key.ToScopeName().Split(".").Last());
                                newNode.ImageKey = scopeGroup.Key.ToScopeName();
                                newNode.SelectedImageKey = scopeGroup.Key.ToScopeName();
                                newNode.NodeFont = new Font(newNode.TreeView.Font, FontStyle.Italic);
                                newNode.ToolTipText = String.Format("set of {0}", newNode.Text);

                                nodes = newNode.Nodes;
                                return newNode;
                            });
                        }

                        foreach (NamedScopeItem item in scopeGroup.OrderBy(o => o.OrdinalPosition).ThenBy(o => o.MemberName))
                        {
                            TreeNode node = contextNameNavigation.Invoke<TreeNode>(() =>
                            {
                                TreeNode newNode = nodes.Add(item.MemberTitle);
                                newNode.ImageKey = item.Scope.ToScopeName();
                                newNode.SelectedImageKey = item.Scope.ToScopeName();
                                contextNodes.Add(newNode, item);
                                newNode.ToolTipText = item.MemberFullName;
                                item.PropertyChanged += Item_PropertyChanged;

                                return newNode;

                                void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
                                { newNode.Text = item.MemberName; }
                            });

                            if (item.Children.Count > 0)
                            {
                                CreateNodes(
                                    node.Nodes,
                                    item.Children.Select(s => BusinessData.NameScope[s]));
                            }

                            progress(completeWork++, totalWork);
                        }
                    }
                }
            }
        }

        private void NameScope_ListChanged(object? sender, NamedScopeChangedEventArgs e)
        {
            TreeNodeCollection taget = contextNameNavigation.Nodes;

            if (e.ChangedType == NameScopedChangedType.BeginBatch)
            { RefreshTree(); }

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

        private void RefreshCommand_Click(object? sender, EventArgs e)
        { RefreshTree(); }

        private void RefreshTree()
        {
            ClearTree();
            BusinessData.NameScope.Clear();

            List<WorkItem> work = new List<WorkItem>();
            List<NamedScopeItem> names = new List<NamedScopeItem>();
            work.AddRange(BusinessData.Export(names));
            work.AddRange(BusinessData.NameScope.Import(names));

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { BuildTree(); }
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

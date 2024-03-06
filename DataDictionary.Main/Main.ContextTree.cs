using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NameScope;
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
        Dictionary<TreeNode, NameScopeItem> contextNodes = new Dictionary<TreeNode, NameScopeItem>();

        List<NameScopeItem> expandedContextNodes = new List<NameScopeItem>();
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
            contextNameLayout.UseWaitCursor = true;
            contextNameLayout.Enabled = false;

            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem treeWork = new WorkItem()
            { WorkName = "Load Context Name Tree", DoWork = LoadTree };
            progress = treeWork.OnProgressChanged;

            work.Add(treeWork);
            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                foreach (NameScopeItem item in expandedContextNodes)
                {
                    NameScopeKey key = new NameScopeKey(item);

                    KeyValuePair<TreeNode, NameScopeItem> value = contextNodes.FirstOrDefault(w => key.Equals(w));

                    if (value.Key is TreeNode node)
                    { node.Expand(); }
                }

                foreach (TreeNode item in contextNodes.Where(w => expandedContextNodes.Contains(w.Value)).Select(s => s.Key).ToList())
                { item.ExpandParent(); }

                contextNameLayout.UseWaitCursor = false;
                contextNameLayout.Enabled = true;
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

                void CreateNodes(TreeNodeCollection target, IEnumerable<NameScopeItem> items)
                {
                    foreach (IGrouping<ScopeType, NameScopeItem>? scopeGroup in items.GroupBy(g => g.Scope).OrderBy(o => o.Key))
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

                        foreach (NameScopeItem item in scopeGroup.OrderBy(o => o.OrdinalPosition).ThenBy(o => o.MemberName))
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

        private void NameScope_ListChanged(object? sender, NameScopeChangedEventArgs e)
        {
            TreeNodeCollection taget = contextNameNavigation.Nodes;

            if (e.ChangedType == NameScopeChangedType.BeginBatch)
            { RefreshTree(); }

            else if (e.ChangedType == NameScopeChangedType.ItemAdded && e.Item is NameScopeItem addedItem)
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

            else if (e.ChangedType == NameScopeChangedType.ItemDeleted && e.Item is NameScopeItem deletedItem)
            { // Handle Remove
                NameScopeKey deleteKey = deletedItem.SystemKey;
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

        private void RefreshCommand_Click(object sender, EventArgs e)
        { RefreshTree(); }

        private void RefreshTree()
        {
            ClearTree();
            BusinessData.NameScope.Clear();

            List<WorkItem> work = new List<WorkItem>();
            List<NameScopeItem> names = new List<NameScopeItem>();
            work.AddRange(BusinessData.Export(names));
            work.AddRange(BusinessData.NameScope.Import(names));

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { BuildTree(); }
        }

        private void NewAttributeCommand_ButtonClick(object sender, EventArgs e)
        {
            DomainAttributeItem item = new DomainAttributeItem();

            BusinessData.DomainModel.Attributes.Add(item);
            BusinessData.NameScope.Add(new NameScopeItem(BusinessData.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void NewEntityCommand_ButtonClick(object sender, EventArgs e)
        {
            DomainEntityItem item = new DomainEntityItem();

            BusinessData.DomainModel.Entities.Add(item);
            BusinessData.NameScope.Add(new NameScopeItem(BusinessData.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void NewSubjectAreaCommand_ButtonClick(object sender, EventArgs e)
        {
            ModelSubjectAreaItem item = new ModelSubjectAreaItem();

            BusinessData.ModelSubjectAreas.Add(item);
            BusinessData.NameScope.Add(new NameScopeItem(BusinessData.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void ManageModelCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelManager()); }

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


        void Activate(IDbCatalogItem catalogItem)
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

        void Activate(DomainAttributeItem attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity(entityItem), entityItem); }

        void Activate(ModelSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.ModelSubjectArea(subjectItem), subjectItem); }

        void Activate(ModelItem modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

        #region ToolStrip Items
        private void manageLibrariesCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Database.CatalogManager()); }

        private void menuCatalogItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Database), BusinessData.DatabaseModel.DbCatalogs); }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Attribute), BusinessData.DomainModel.Attributes); }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Diagram), BusinessData.ModelSubjectAreas); }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_HelpTableOfContent), BusinessData.ApplicationData.HelpSubjects); }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Library), BusinessData.LibraryModel.LibrarySources); }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Class), BusinessData.LibraryModel.LibraryMembers); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Key), BusinessData.DatabaseModel.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_KeyColumn), BusinessData.DatabaseModel.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_DomainType), BusinessData.DatabaseModel.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Procedure), BusinessData.DatabaseModel.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Parameter), BusinessData.DatabaseModel.DbRoutineParameters); }

        private void menuRoutineDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Dependancy), BusinessData.DatabaseModel.DbRoutineDependencies); }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Schema), BusinessData.DatabaseModel.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Table), BusinessData.DatabaseModel.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Column), BusinessData.DatabaseModel.DbTableColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_ExtendedProperty), BusinessData.DatabaseModel.DbExtendedProperties); }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), BusinessData.DomainModel.Attributes.Properties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), BusinessData.DomainModel.Attributes.Aliases); }

        private void entitiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Entities), BusinessData.DomainModel.Entities); }

        private void entityPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), BusinessData.DomainModel.Entities.Properties); }

        private void entityAliasToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), BusinessData.DomainModel.Entities.Aliases); }

        #endregion

    }
}

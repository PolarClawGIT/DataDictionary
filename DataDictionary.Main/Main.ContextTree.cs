using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.ContextName;
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
        Dictionary<TreeNode, ContextNameItem> contextNodes = new Dictionary<TreeNode, ContextNameItem>();

        List<ContextNameItem> expandedContextNodes = new List<ContextNameItem>();
        void ClearTree()
        {
            expandedContextNodes.Clear();
            expandedContextNodes.AddRange(contextNodes.Where(w => w.Key.IsExpanded).Select(s => s.Value));
            Program.Data.ContextName.ListChanged -= ModelContextName_ListChanged;

            contextNameNavigation.Nodes.Clear();
            contextNodes.Clear();
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
                foreach (TreeNode item in contextNodes.Where(w => expandedContextNodes.Contains(w.Value)).Select(s => s.Key).ToList())
                { item.ExpandParent(); }

                contextNameLayout.UseWaitCursor = false;
                contextNameLayout.Enabled = true;
                contextNameNavigation.EndUpdate();
                Program.Data.ContextName.ListChanged += ModelContextName_ListChanged;
            }

            void LoadTree()
            {
                Int32 totalWork = Program.Data.ContextName.Count;
                Int32 completeWork = 0;
                progress(completeWork, totalWork);

                CreateNodes(
                    contextNameNavigation.Nodes,
                    Program.Data.ContextName.RootItem.Children.Select(s => Program.Data.ContextName[s]).ToList());

                void CreateNodes(TreeNodeCollection target, IEnumerable<ContextNameItem> items)
                {
                    foreach (IGrouping<ScopeType, ContextNameItem>? scopeGroup in items.GroupBy(g => g.Scope).OrderBy(o => o.Key))
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

                        foreach (ContextNameItem item in scopeGroup.OrderBy(o => o.OrdinalPosition).ThenBy(o => o.MemberName))
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
                                    item.Children.Select(s => Program.Data.ContextName[s]));
                            }

                            progress(completeWork++, totalWork);
                        }
                    }

                }
            }
        }

        private void ModelContextName_ListChanged(object? sender, ContextNameChangedEventArgs e)
        {
            TreeNodeCollection taget = contextNameNavigation.Nodes;

            if (sender is ContextNameDictionary source)
            {
                if (e.ChangedType == ContextNameChangedType.ItemAdded && e.Item is ContextNameItem addedItem)
                {
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

                if (e.ChangedType == ContextNameChangedType.ItemDeleted && e.Item is ContextNameItem deletedItem)
                {
                    ContextNameKey deleteKey = deletedItem.SystemKey;
                    if (contextNodes.FirstOrDefault(w => deletedItem.SystemKey.Equals(w.Value.SystemKey)).Key is TreeNode node)
                    {
                        node.Remove();
                        contextNodes.Remove(node);
                    }
                }
            }
        }

        private void RefreshCommand_Click(object sender, EventArgs e)
        {
            ClearTree();

            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Program.Data.RemoveContextName());
            work.AddRange(Program.Data.LoadContextName());

            this.DoWork(work, OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { BuildTree(); }
        }

        private void NewAttributeCommand_ButtonClick(object sender, EventArgs e)
        {
            DomainAttributeItem item = new DomainAttributeItem();

            Program.Data.DomainAttributes.Add(item);
            Program.Data.ContextName.Add(new ContextNameItem(Program.Data.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void NewEntityCommand_ButtonClick(object sender, EventArgs e)
        {
            DomainEntityItem item = new DomainEntityItem();

            Program.Data.DomainEntities.Add(item);
            Program.Data.ContextName.Add(new ContextNameItem(Program.Data.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void NewSubjectAreaCommand_ButtonClick(object sender, EventArgs e)
        {
            ModelSubjectAreaItem item = new ModelSubjectAreaItem();
            Program.Data.ModelSubjectAreas.Add(item);
            Program.Data.ContextName.Add(new ContextNameItem(Program.Data.Model, item));

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


        void Activate(DbCatalogItem catalogItem)
        { Activate((data) => new Forms.Database.DbCatalog() { DataKey = new DbCatalogKey(catalogItem) }, catalogItem); }

        void Activate(DbSchemaItem schemaItem)
        { Activate((data) => new Forms.Database.DbSchema() { DataKey = new DbSchemaKeyName(schemaItem) }, schemaItem); }

        void Activate(DbTableItem tableItem)
        { Activate((data) => new Forms.Database.DbTable() { DataKey = new DbTableKeyName(tableItem) }, tableItem); }

        void Activate(DbTableColumnItem columnItem)
        { Activate((data) => new Forms.Database.DbTableColumn() { DataKey = new DbTableColumnKeyName(columnItem) }, columnItem); }

        void Activate(DbConstraintItem constraintItem)
        { Activate((data) => new Forms.Database.DbConstraint() { DataKey = new DbConstraintKeyName(constraintItem) }, constraintItem); }

        void Activate(DbRoutineItem routineItem)
        { Activate((data) => new Forms.Database.DbRoutine() { DataKey = new DbRoutineKeyName(routineItem) }, routineItem); }

        void Activate(DbRoutineParameterItem routineParameterItem)
        { Activate((data) => new Forms.Database.DbRoutineParameter() { DataKey = new DbRoutineParameterKeyName(routineParameterItem) }, routineParameterItem); }

        void Activate(DbDomainItem domainItem)
        { Activate((data) => new Forms.Database.DbDomain() { DataKey = new DbDomainKeyName(domainItem) }, domainItem); }

        void Activate(LibrarySourceItem sourceItem)
        { Activate((data) => new Forms.Library.LibrarySource() { DataKey = new LibrarySourceKey(sourceItem) }, sourceItem); }

        void Activate(LibraryMemberItem memberItem)
        { Activate((data) => new Forms.Library.LibraryMember() { DataKey = new LibraryMemberKey(memberItem) }, memberItem); }

        void Activate(DomainAttributeItem attributeItem)
        //{ Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }
        { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity() { DataKey = new DomainEntityKey(entityItem) }, entityItem); }

        void Activate(ModelSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.ModelSubjectArea() { DataKey = new ModelSubjectAreaKey(subjectItem) }, subjectItem); }

        void Activate(ModelItem modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

        #region ToolStrip Items
        private void manageLibrariesCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Database.CatalogManager()); }

        private void menuCatalogItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Database), Program.Data.DbCatalogs); }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Attribute), Program.Data.DomainAttributes); }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Diagram), Program.Data.ModelSubjectAreas); }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_HelpTableOfContent), Program.Data.HelpSubjects); }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Library), Program.Data.LibrarySources); }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Class), Program.Data.LibraryMembers); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Key), Program.Data.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_KeyColumn), Program.Data.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_DomainType), Program.Data.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Procedure), Program.Data.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Parameter), Program.Data.DbRoutineParameters); }

        private void menuRoutineDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Dependancy), Program.Data.DbRoutineDependencies); }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Schema), Program.Data.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Table), Program.Data.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Column), Program.Data.DbTableColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_ExtendedProperty), Program.Data.DbExtendedProperties); }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), Program.Data.DomainAttributeProperties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), Program.Data.DomainAttributeAliases); }

        private void entitiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Entities), Program.Data.DomainEntities); }

        private void entityPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), Program.Data.DomainEntityProperties); }

        private void entityAliasToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), Program.Data.DomainEntityAliases); }

        #endregion

    }
}

using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main
    {
        Dictionary<TreeNode, Object> dbDataNodes = new Dictionary<TreeNode, Object>();

        List<Object> expandedDbNode = new List<object>();
        void ClearDataSourcesTree()
        {
            expandedDbNode.AddRange(dbDataNodes.Where(w => w.Key.IsExpanded).Select(s => s.Value));

            dataSourceNavigation.Nodes.Clear();
            dbDataNodes.Clear();
        }

        void BuildDataSourcesTree()
        {
            dataSourceNavigation.BeginUpdate();
            dataSourceNavigation.UseWaitCursor = true;
            dataSourceNavigation.Enabled = false;

            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem treeWork = new WorkItem()
            { WorkName = "Load Data Source Tree", DoWork = LoadTree };
            progress = treeWork.OnProgressChanged;

            work.Add(treeWork);
            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                foreach (TreeNode item in dbDataNodes.Where(w => expandedDbNode.Contains(w.Value)).Select(s => s.Key).ToList())
                { item.ExpandParent(); }
                expandedDbNode.Clear();

                dataSourceNavigation.UseWaitCursor = false;
                dataSourceNavigation.Enabled = true;
                dataSourceNavigation.EndUpdate();
            }

            void LoadTree()
            {
                Int32 totalWork = Program.Data.ModelAlias.Count;
                Int32 completeWork = 0;
                progress(completeWork, totalWork);

                CreateNodes(
                    dataSourceNavigation.Nodes,
                    Program.Data.ModelAlias.RootItem.Children.Select(s => Program.Data.ModelAlias[s]));

                void CreateNodes(TreeNodeCollection target, IEnumerable<ModelAliasItem> items)
                {
                    foreach (ModelAliasItem item in items.OrderBy(o => o.ItemName))
                    {
                        TreeNode node = dataSourceNavigation.Invoke<TreeNode>(() =>
                        {
                            TreeNode newNode = target.Add(item.ItemName);
                            newNode.ImageKey = item.ScopeId.ToScopeName();
                            newNode.SelectedImageKey = item.ScopeId.ToScopeName();
                            if (item.Source is object sourceItem) { dbDataNodes.Add(newNode, sourceItem); }

                            return newNode;
                        });

                        if (item.Children.Count > 0)
                        { 
                            CreateNodes(
                                node.Nodes,
                                item.Children.Select(s => Program.Data.ModelAlias[s])); 
                        }

                        progress(completeWork++, totalWork);
                    }
                }
            }
        }


        private void dataSourceNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (dataSourceNavigation.SelectedNode is TreeNode node && dbDataNodes.ContainsKey(node))
            {
                dynamic dataNode = dbDataNodes[node];
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
    }
}

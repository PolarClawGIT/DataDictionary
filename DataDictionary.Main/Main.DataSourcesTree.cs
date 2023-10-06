using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Main.Properties;

namespace DataDictionary.Main
{
    partial class Main
    {
        #region dataSourceNavigation
        Dictionary<TreeNode, Object> dbDataNodes = new Dictionary<TreeNode, Object>();
        enum dbDataImageIndex
        {
            Unknown,
            // Database
            Database,
            Schema,
            Tables,
            Table,
            TableKey,
            View,
            Domains,
            Domain,
            Columns,
            Column,
            ComputedColumn,
            Constraint,
            ConstraintColumn,
            Routines,
            Routine,
            StoredProcedure,
            ScalarFunction,
            TableFunction,
            Parameter,
            Dependencies,
            Dependency,
            // Class
            Library,
            NameSpace,
            Class,
            Method,
            Field
        }

        static Dictionary<dbDataImageIndex, (String imageKey, Image image)> dbDataImageItems = new Dictionary<dbDataImageIndex, (String imageKey, Image image)>()
        {
            {dbDataImageIndex.Unknown,          ("Unknown",          Resources.QuestionMark) },
            {dbDataImageIndex.Database,         ("Database",         Resources.Database) },
            {dbDataImageIndex.Schema,           ("Schema",           Resources.Schema) },
            {dbDataImageIndex.Tables,           ("Tables",           Resources.TableGroup) },
            {dbDataImageIndex.Table,            ("Table",            Resources.Table) },
            {dbDataImageIndex.TableKey,         ("TableKey",         Resources.TableKey) },
            {dbDataImageIndex.Columns,          ("Columns",          Resources.ColumnGroup) },
            {dbDataImageIndex.Column,           ("Column",           Resources.Column) },
            {dbDataImageIndex.ComputedColumn,   ("ComputedColumn",   Resources.ComputedColumn) },
            {dbDataImageIndex.Constraint,       ("Constraint",       Resources.Key) },
            {dbDataImageIndex.ConstraintColumn, ("ConstraintColumn", Resources.KeyColumn) },
            {dbDataImageIndex.View,             ("View",             Resources.View) },
            {dbDataImageIndex.Domains,          ("Domains",          Resources.Type) },
            {dbDataImageIndex.Domain,           ("Domain",           Resources.DomainType) },
            {dbDataImageIndex.Routines,         ("Routines",         Resources.MethodSet) },
            {dbDataImageIndex.Routine,          ("Routine",          Resources.Method) },
            {dbDataImageIndex.StoredProcedure,  ("StoredProcedure",  Resources.Procedure) },
            {dbDataImageIndex.ScalarFunction,   ("ScalarFunction",   Resources.ScalarFunction) },
            {dbDataImageIndex.TableFunction,    ("TableFunction",    Resources.TableFunction) },
            {dbDataImageIndex.Parameter,        ("Parameter",        Resources.Parameter) },
            {dbDataImageIndex.Dependencies,     ("Dependencies",       Resources.Dependancy) },
            {dbDataImageIndex.Dependency,       ("Dependency",       Resources.Dependancy) },

            {dbDataImageIndex.Library,          ("Library",          Resources.Library) },
            {dbDataImageIndex.NameSpace,        ("NameSpace",        Resources.Namespace) },
            {dbDataImageIndex.Class,            ("Class",            Resources.Class) },
            {dbDataImageIndex.Method,           ("Method",           Resources.Method) },
            {dbDataImageIndex.Field,            ("Field",            Resources.Field) },
        };

        void SetImages(TreeView tree, IEnumerable<(String imageKey, Image image)> images)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach ((string imageKey, Image image) image in images.Where(w => !tree.ImageList.Images.ContainsKey(w.imageKey)))
            { tree.ImageList.Images.Add(image.imageKey, image.image); }
        }

        void BuildDataSourcesTree()
        {
            Object? selected = null;
            if (dataSourceNavigation.SelectedNode is not null && dbDataNodes.ContainsKey(dataSourceNavigation.SelectedNode))
            { selected = dbDataNodes[dataSourceNavigation.SelectedNode]; }

            dataSourceNavigation.Nodes.Clear();
            dbDataNodes.Clear();

            foreach (IDbCatalogItem catalogItem in Program.Data.DbCatalogs.OrderBy(o => o.CatalogName))
            {
                if (String.IsNullOrWhiteSpace(catalogItem.CatalogName))
                { } //TODO: This event may fire when there is no data or the data is being changed. Caused by the deleted row not being handled correctly.

                TreeNode catalogNode = CreateNode(catalogItem.CatalogName, dbDataImageIndex.Database, catalogItem);
                dataSourceNavigation.Nodes.Add(catalogNode);

                foreach (IDbSchemaItem schemaItem in Program.Data.DbSchemta.OrderBy(o => o.SchemaName).Where(
                    w => w.IsSystem == false &&
                    w.CatalogName == catalogItem.CatalogName))
                {
                    TreeNode schemaNode = CreateNode(schemaItem.SchemaName, dbDataImageIndex.Schema, schemaItem, catalogNode);
                    TreeNode tablesNode = CreateNode("Tables & Views", dbDataImageIndex.Tables, null, schemaNode);

                    foreach (IDbTableItem tableItem in Program.Data.DbTables.OrderBy(o => o.TableName).Where(
                        w => w.IsSystem == false && new DbSchemaKey(w).Equals(schemaItem)))
                    {
                        DbTableKey tableKey = new DbTableKey(tableItem);
                        TreeNode tableNode;
                        TreeNode? tableConstraintNode = null;
                        if (tableItem.ObjectScope == DbObjectScope.View)
                        { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.View, tableItem, tablesNode); }
                        else if (tableItem.ObjectScope == DbObjectScope.Table)
                        { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.Table, tableItem, tablesNode); }
                        else { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.Unknown, tableItem, tablesNode); }

                        TreeNode columnsNode = CreateNode("Columns", dbDataImageIndex.Columns, null, tableNode);


                        foreach (IDbTableColumnItem columnItem in Program.Data.DbTableColumns.Where(
                            w => tableKey.Equals(w)).OrderBy(o => o.OrdinalPosition))
                        { CreateNode(columnItem.ColumnName, dbDataImageIndex.Column, columnItem, columnsNode); }

                        foreach (DbConstraintItem contraintItem in Program.Data.DbConstraints.Where(
                            w => tableKey.Equals(w)))
                        {
                            if (tableConstraintNode is null)
                            { tableConstraintNode = CreateNode("Constraints", dbDataImageIndex.TableKey, null, tableNode); }

                            TreeNode constraintNode = CreateNode(contraintItem.ConstraintName, dbDataImageIndex.Constraint, contraintItem, tableConstraintNode);

                            foreach (DbConstraintColumnItem contraintColumnItem in Program.Data.DbConstraintColumns.Where(
                                w => new DbConstraintKey(w).Equals(new DbConstraintKey(contraintItem))))
                            { CreateNode(contraintColumnItem.ColumnName, dbDataImageIndex.ConstraintColumn, contraintColumnItem, constraintNode); }
                        }
                    }

                    TreeNode? routinesNode = null;

                    foreach (IDbRoutineItem routineItem in Program.Data.DbRoutines.OrderBy(o => o.RoutineName).Where(
                        w => w.IsSystem == false && new DbSchemaKey(w).Equals(schemaItem)))
                    {
                        DbRoutineKey routineKey = new DbRoutineKey(routineItem);
                        TreeNode? routineNode;

                        if (routinesNode is null)
                        { routinesNode = CreateNode("Routines", dbDataImageIndex.Routines, null, schemaNode); }

                        DbRoutineParameterItem? firstParameter = Program.Data.DbRoutineParameters.OrderBy(o => o.OrdinalPosition).FirstOrDefault(w => routineKey.Equals(w));


                        if (routineItem.ObjectScope == DbObjectScope.Procedure)
                        { routineNode = CreateNode(routineItem.RoutineName, dbDataImageIndex.StoredProcedure, routineItem, routinesNode); }

                        else if (routineItem.ObjectScope == DbObjectScope.Function && firstParameter is DbRoutineParameterItem isScalar && isScalar.OrdinalPosition == 0)
                        { routineNode = CreateNode(routineItem.RoutineName, dbDataImageIndex.ScalarFunction, routineItem, routinesNode); }

                        else if (routineItem.ObjectScope == DbObjectScope.Function && firstParameter is DbRoutineParameterItem isTable && isTable.OrdinalPosition != 0)
                        { routineNode = CreateNode(routineItem.RoutineName, dbDataImageIndex.TableFunction, routineItem, routinesNode); }

                        else
                        { routineNode = CreateNode(routineItem.RoutineName, dbDataImageIndex.Unknown, routineItem, routinesNode); }

                        foreach (DbRoutineParameterItem routineParameter in Program.Data.DbRoutineParameters.Where(
                            w => routineKey.Equals(w)).OrderBy(o => o.OrdinalPosition))
                        { CreateNode(routineParameter.ParameterName, dbDataImageIndex.Parameter, routineParameter, routineNode); }

                        TreeNode? routineDependenciesNode = null;

                        foreach (DbRoutineDependencyItem routineDependency in Program.Data.DbRoutineDependencies.Where(
                            w => routineKey.Equals(w)))
                        {
                            if (routineDependenciesNode is null)
                            { routineDependenciesNode = CreateNode("Dependencies", dbDataImageIndex.Dependencies, null, routinesNode); }

                            String nameValue = String.Format("{0}", routineDependency.ReferenceSchemaName);

                            if (!String.IsNullOrWhiteSpace(routineDependency.ReferenceObjectName))
                            { nameValue = String.Format("{0}.{1}", nameValue, routineDependency.ReferenceObjectName); }

                            if (!String.IsNullOrWhiteSpace(routineDependency.ReferenceColumnName))
                            { nameValue = String.Format("{0}.{1}", nameValue, routineDependency.ReferenceColumnName); }

                            CreateNode(nameValue, dbDataImageIndex.Dependency, routineDependency, routineDependenciesNode);
                        }
                    }

                    TreeNode? domainsNode = null;

                    foreach (IDbDomainItem domainItem in Program.Data.DbDomains.OrderBy(o => o.DomainName).Where(
                        w => new DbSchemaKey(w).Equals(schemaItem)))
                    {
                        DbDomainKey domainKey = new DbDomainKey(domainItem);

                        if (domainsNode is null)
                        { domainsNode = CreateNode("Domains", dbDataImageIndex.Domains, null, schemaNode); }

                        CreateNode(domainItem.DomainName, dbDataImageIndex.Domain, domainItem, domainsNode);
                    }
                }
            }

            foreach (ILibrarySourceItem librarySourceItem in Program.Data.LibrarySources.OrderBy(o => o.LibraryTitle))
            {
                LibrarySourceKeyUnique sourceKey = new LibrarySourceKeyUnique(librarySourceItem);
                TreeNode sourceNode = CreateNode(librarySourceItem.LibraryTitle, dbDataImageIndex.Library, librarySourceItem);
                dataSourceNavigation.Nodes.Add(sourceNode);

                foreach (LibraryMemberItem memberItem in Program.Data.LibraryMembers
                    .Where(w => sourceKey.Equals(w))
                    .OrderBy(o => o.MemberNameSpace)
                    .ThenBy(o => o.MemberName))
                {
                    String nodeKey = String.Empty;
                    TreeNode parentNode = sourceNode;

                    // Create Namespace Nodes
                    if (memberItem.MemberNameSpace is String nameSpace)
                    {
                        foreach (String nameSpaceElement in nameSpace.Split("."))
                        {
                            if (String.IsNullOrWhiteSpace(nodeKey))
                            { nodeKey = nameSpaceElement; }
                            else if (sourceNode.Nodes.Find(nodeKey, true).FirstOrDefault() is TreeNode foundNode)
                            {
                                parentNode = foundNode;
                                nodeKey = String.Format("{0}.{1}", nodeKey, nameSpaceElement);
                            }

                            if (sourceNode.Nodes.Find(nodeKey, true).FirstOrDefault() is TreeNode exsitngNode)
                            { } // Node Already exists by this key. Nothing to do.
                            else
                            { CreateNode(nameSpaceElement, dbDataImageIndex.NameSpace, null, parentNode, nodeKey); }
                        }
                    }

                    // Create the Node Member (NameSpace node already created)
                    if (sourceNode.Nodes.Find(memberItem.MemberNameSpace, true).FirstOrDefault() is TreeNode nameSpaceNode)
                    {
                        switch (memberItem.MemberItemType().type)
                        {
                            case LibraryMemberType.Type:
                                CreateNode(memberItem.MemberName,
                                    dbDataImageIndex.Class,
                                    memberItem,
                                    nameSpaceNode,
                                    String.Format("{0}.{1}", nodeKey, memberItem.MemberName));
                                break;
                            case LibraryMemberType.Field or LibraryMemberType.Property:
                                CreateNode(memberItem.MemberName,
                                    dbDataImageIndex.Field,
                                    memberItem,
                                    nameSpaceNode);
                                break;
                            case LibraryMemberType.Method or LibraryMemberType.Event:
                                CreateNode(memberItem.MemberName,
                                    dbDataImageIndex.Method,
                                    memberItem,
                                    nameSpaceNode);
                                break;
                            default:
                                CreateNode(memberItem.MemberName,
                                    dbDataImageIndex.Unknown,
                                    memberItem,
                                    nameSpaceNode);
                                break;
                        }
                    }
                    else
                    {// Should never happen
                        InvalidOperationException ex = new InvalidOperationException("Parent Node could not be found");
                        ex.Data.Add(nameof(memberItem.MemberNameSpace), memberItem.MemberNameSpace);
                        ex.Data.Add(nameof(memberItem.MemberName), memberItem.MemberName);
                        throw ex;
                    }
                }

            }

            if (dbDataNodes.FirstOrDefault(w => ReferenceEquals(w.Value, selected)) is KeyValuePair<TreeNode, object> selectedNode)
            { dataSourceNavigation.SelectedNode = selectedNode.Key; }

            TreeNode CreateNode(String? nodeText, dbDataImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null, String? key = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result;
                if (parentNode is not null)
                {
                    if (String.IsNullOrWhiteSpace(key))
                    { result = parentNode.Nodes.Add(nodeText); }
                    else { result = parentNode.Nodes.Add(key, nodeText); }
                }
                else { result = new TreeNode(nodeText); }

                result.ImageKey = dbDataImageItems[imageIndex].imageKey;
                result.SelectedImageKey = dbDataImageItems[imageIndex].imageKey;

                if (source is not null) { dbDataNodes.Add(result, source); }

                return result;
            }
        }

        private void dataSourceNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (dbDataNodes.ContainsKey(dataSourceNavigation.SelectedNode))
            {
                Object dataNode = dbDataNodes[dataSourceNavigation.SelectedNode];

                if (dataNode is IDbSchemaItem schemaItem)
                { Activate((data) => new Forms.Database.DbSchema() { DataKey = new DbSchemaKey(schemaItem) }, schemaItem); }

                if (dataNode is IDbTableItem tableItem)
                { Activate((data) => new Forms.Database.DbTable() { DataKey = new DbTableKey(tableItem) }, tableItem); }

                if (dataNode is IDbTableColumnItem columnItem)
                { Activate((data) => new Forms.Database.DbTableColumn() { DataKey = new DbTableColumnKey(columnItem) }, columnItem); }

                if (dataNode is IDbConstraintItem constraintItem)
                { Activate((data) => new Forms.Database.DbConstraint() { DataKey = new DbConstraintKey(constraintItem) }, constraintItem); }

                if (dataNode is IDbRoutineItem routineItem)
                { Activate((data) => new Forms.Database.DbRoutine() { DataKey = new DbRoutineKey(routineItem) }, routineItem); }

                if (dataNode is IDbRoutineParameterItem routineParameterItem)
                { Activate((data) => new Forms.Database.DbRoutineParameter() { DataKey = new DbRoutineParameterKey(routineParameterItem) }, routineParameterItem); }

                if (dataNode is IDbDomainItem domainItem)
                { Activate((data) => new Forms.Database.DbDomain() { DataKey = new DbDomainKey(domainItem) }, domainItem); }

                if (dataNode is ILibrarySourceItem sourceItem)
                { Activate((data) => new Forms.Library.LibrarySource() { DataKey = new LibrarySourceKey(sourceItem) }, sourceItem); }

                if (dataNode is ILibraryMemberItem memberItem)
                { Activate((data) => new Forms.Library.LibraryMember() { DataKey = new LibraryMemberKey(memberItem) }, memberItem); }
            }
        }
        #endregion
    }
}

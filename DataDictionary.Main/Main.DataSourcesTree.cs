﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
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

        void ClearDataSourcesTree()
        {
            dataSourceNavigation.Nodes.Clear();
            dbDataNodes.Clear();
        }

        void BuildDataSourcesTree()
        {
            foreach (IDbCatalogItem catalogItem in Program.Data.DbCatalogs.OrderBy(o => o.DatabaseName))
            {
                if (String.IsNullOrWhiteSpace(catalogItem.DatabaseName))
                { } //TODO: This event may fire when there is no data or the data is being changed. Caused by the deleted row not being handled correctly.

                TreeNode catalogNode = CreateNode(dataSourceNavigation.Nodes, catalogItem.DatabaseName, dbDataImageIndex.Database, catalogItem);

                foreach (IDbSchemaItem schemaItem in Program.Data.DbSchemta.OrderBy(o => o.SchemaName).Where(
                    w => w.IsSystem == false &&
                    w.DatabaseName == catalogItem.DatabaseName))
                {
                    TreeNode schemaNode = CreateNode(catalogNode.Nodes, schemaItem.SchemaName, dbDataImageIndex.Schema, schemaItem);
                    TreeNode tablesNode = CreateNode(schemaNode.Nodes, "Tables & Views", dbDataImageIndex.Tables);

                    foreach (IDbTableItem tableItem in Program.Data.DbTables.OrderBy(o => o.TableName).Where(
                        w => w.IsSystem == false && new DbSchemaKey(w).Equals(schemaItem)))
                    {
                        DbTableKey tableKey = new DbTableKey(tableItem);
                        TreeNode tableNode;
                        TreeNode? tableConstraintNode = null;
                        if (tableItem.ObjectScope == DbObjectScope.View)
                        { tableNode = CreateNode(tablesNode.Nodes, tableItem.TableName, dbDataImageIndex.View, tableItem); }
                        else if (tableItem.ObjectScope == DbObjectScope.Table)
                        { tableNode = CreateNode(tablesNode.Nodes, tableItem.TableName, dbDataImageIndex.Table, tableItem); }
                        else { tableNode = CreateNode(tablesNode.Nodes, tableItem.TableName, dbDataImageIndex.Unknown, tableItem); }

                        TreeNode columnsNode = CreateNode(tableNode.Nodes, "Columns", dbDataImageIndex.Columns);


                        foreach (IDbTableColumnItem columnItem in Program.Data.DbTableColumns.Where(
                            w => tableKey.Equals(w)).OrderBy(o => o.OrdinalPosition))
                        { CreateNode(columnsNode.Nodes, columnItem.ColumnName, dbDataImageIndex.Column, columnItem); }

                        foreach (DbConstraintItem contraintItem in Program.Data.DbConstraints.Where(
                            w => tableKey.Equals(w)))
                        {
                            if (tableConstraintNode is null)
                            { tableConstraintNode = CreateNode(tableNode.Nodes, "Constraints", dbDataImageIndex.TableKey); }

                            TreeNode constraintNode = CreateNode(tableConstraintNode.Nodes, contraintItem.ConstraintName, dbDataImageIndex.Constraint, contraintItem);

                            foreach (DbConstraintColumnItem contraintColumnItem in Program.Data.DbConstraintColumns.Where(
                                w => new DbConstraintKey(w).Equals(new DbConstraintKey(contraintItem))))
                            { CreateNode(constraintNode.Nodes, contraintColumnItem.ColumnName, dbDataImageIndex.ConstraintColumn, contraintColumnItem); }
                        }
                    }

                    TreeNode? routinesNode = null;

                    foreach (IDbRoutineItem routineItem in Program.Data.DbRoutines.OrderBy(o => o.RoutineName).Where(
                        w => w.IsSystem == false && new DbSchemaKey(w).Equals(schemaItem)))
                    {
                        DbRoutineKey routineKey = new DbRoutineKey(routineItem);
                        TreeNode? routineNode;

                        if (routinesNode is null)
                        { routinesNode = CreateNode(schemaNode.Nodes, "Routines", dbDataImageIndex.Routines); }

                        DbRoutineParameterItem? firstParameter = Program.Data.DbRoutineParameters.OrderBy(o => o.OrdinalPosition).FirstOrDefault(w => routineKey.Equals(w));


                        if (routineItem.ObjectScope == DbObjectScope.Procedure)
                        { routineNode = CreateNode(routinesNode.Nodes, routineItem.RoutineName, dbDataImageIndex.StoredProcedure, routineItem); }

                        else if (routineItem.ObjectScope == DbObjectScope.Function && firstParameter is DbRoutineParameterItem isScalar && isScalar.OrdinalPosition == 0)
                        { routineNode = CreateNode(routinesNode.Nodes, routineItem.RoutineName, dbDataImageIndex.ScalarFunction, routineItem); }

                        else if (routineItem.ObjectScope == DbObjectScope.Function && firstParameter is DbRoutineParameterItem isTable && isTable.OrdinalPosition != 0)
                        { routineNode = CreateNode(routinesNode.Nodes, routineItem.RoutineName, dbDataImageIndex.TableFunction, routineItem); }

                        else
                        { routineNode = CreateNode(routinesNode.Nodes, routineItem.RoutineName, dbDataImageIndex.Unknown, routineItem); }

                        foreach (DbRoutineParameterItem routineParameter in Program.Data.DbRoutineParameters.Where(
                            w => routineKey.Equals(w)).OrderBy(o => o.OrdinalPosition))
                        { CreateNode(routineNode.Nodes, routineParameter.ParameterName, dbDataImageIndex.Parameter, routineParameter); }

                    }

                    TreeNode? domainsNode = null;

                    foreach (IDbDomainItem domainItem in Program.Data.DbDomains.OrderBy(o => o.DomainName).Where(
                        w => new DbSchemaKey(w).Equals(schemaItem)))
                    {
                        DbDomainKey domainKey = new DbDomainKey(domainItem);

                        if (domainsNode is null)
                        { domainsNode = CreateNode(schemaNode.Nodes, "Domains", dbDataImageIndex.Domains, null); }

                        CreateNode(domainsNode.Nodes, domainItem.DomainName, dbDataImageIndex.Domain, domainItem);
                    }
                }
            }

            foreach (ILibrarySourceItem librarySourceItem in Program.Data.LibrarySources.OrderBy(o => o.LibraryTitle))
            {
                LibrarySourceKeyUnique sourceKey = new LibrarySourceKeyUnique(librarySourceItem);
                TreeNode sourceNode = CreateNode(dataSourceNavigation.Nodes, librarySourceItem.LibraryTitle, dbDataImageIndex.Library, librarySourceItem);

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
                            { CreateNode(parentNode.Nodes, nameSpaceElement, dbDataImageIndex.NameSpace, null, nodeKey); }
                        }
                    }

                    // Create the Node Member (NameSpace node already created)
                    if (sourceNode.Nodes.Find(memberItem.MemberNameSpace, true).FirstOrDefault() is TreeNode nameSpaceNode)
                    {
                        switch (memberItem.ObjectType)
                        {
                            case LibraryMemberType.Type:
                                CreateNode(
                                    nameSpaceNode.Nodes,
                                    memberItem.MemberName,
                                    dbDataImageIndex.Class,
                                    memberItem,
                                    String.Format("{0}.{1}", nodeKey, memberItem.MemberName));
                                break;
                            case LibraryMemberType.Field or LibraryMemberType.Property:
                                CreateNode(
                                    nameSpaceNode.Nodes,
                                    memberItem.MemberName,
                                    dbDataImageIndex.Field,
                                    memberItem);
                                break;
                            case LibraryMemberType.Method or LibraryMemberType.Event:
                                CreateNode(
                                    nameSpaceNode.Nodes,
                                    memberItem.MemberName,
                                    dbDataImageIndex.Method,
                                    memberItem);
                                break;
                            default:
                                CreateNode(
                                    nameSpaceNode.Nodes,
                                    memberItem.MemberName,
                                    dbDataImageIndex.Unknown,
                                    memberItem);
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

            TreeNode CreateNode(TreeNodeCollection target, String? nodeText, dbDataImageIndex imageIndex, Object? source = null, String? key = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result;

                if (String.IsNullOrWhiteSpace(key))
                { result = target.Add(nodeText); }
                else { result = target.Add(key, nodeText); }

                result.ImageKey = dbDataImageItems[imageIndex].imageKey;
                result.SelectedImageKey = dbDataImageItems[imageIndex].imageKey;

                if (source is not null) { dbDataNodes.Add(result, source); }

                return result;
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
        { Activate((data) => new Forms.Database.DbSchema() { DataKey = new DbSchemaKey(schemaItem) }, schemaItem); }

        void Activate(DbTableItem tableItem)
        { Activate((data) => new Forms.Database.DbTable() { DataKey = new DbTableKey(tableItem) }, tableItem); }

        void Activate(DbTableColumnItem columnItem)
        { Activate((data) => new Forms.Database.DbTableColumn() { DataKey = new DbTableColumnKey(columnItem) }, columnItem); }

        void Activate(DbConstraintItem constraintItem)
        { Activate((data) => new Forms.Database.DbConstraint() { DataKey = new DbConstraintKey(constraintItem) }, constraintItem); }

        void Activate(DbRoutineItem routineItem)
        { Activate((data) => new Forms.Database.DbRoutine() { DataKey = new DbRoutineKey(routineItem) }, routineItem); }

        void Activate(DbRoutineParameterItem routineParameterItem)
        { Activate((data) => new Forms.Database.DbRoutineParameter() { DataKey = new DbRoutineParameterKey(routineParameterItem) }, routineParameterItem); }

        void Activate(DbDomainItem domainItem)
        { Activate((data) => new Forms.Database.DbDomain() { DataKey = new DbDomainKey(domainItem) }, domainItem); }

        void Activate(LibrarySourceItem sourceItem)
        { Activate((data) => new Forms.Library.LibrarySource() { DataKey = new LibrarySourceKey(sourceItem) }, sourceItem); }

        void Activate(LibraryMemberItem memberItem)
        { Activate((data) => new Forms.Library.LibraryMember() { DataKey = new LibraryMemberKey(memberItem) }, memberItem); }
    }
}

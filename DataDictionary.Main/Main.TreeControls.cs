using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main
{
    partial class Main
    {
        #region dbMetaDataNavigation
        Dictionary<TreeNode, Object> dbDataNodes = new Dictionary<TreeNode, Object>();
        enum dbDataImageIndex
        {
            Unknown,
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
            Dependency
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
            {dbDataImageIndex.Dependency,       ("Dependency",       Resources.Dependancy) },
        };

        void SetImages(TreeView tree, IEnumerable<(String imageKey, Image image)> images)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach ((string imageKey, Image image) image in images.Where(w => !tree.ImageList.Images.ContainsKey(w.imageKey)))
            { tree.ImageList.Images.Add(image.imageKey, image.image); }
        }

        void BuildDbDataTree()
        {
            dbMetaDataNavigation.Nodes.Clear();
            dbDataNodes.Clear();

            foreach (IDbCatalogItem catalogItem in Program.Data.DbCatalogs.OrderBy(o => o.CatalogName))
            {
                if (String.IsNullOrWhiteSpace(catalogItem.CatalogName))
                {
                    //TODO: This event may fire when there is no data or the data is being changed. Caused by the deleted row not being handled correctly.
                }

                TreeNode catalogNode = CreateNode(catalogItem.CatalogName, dbDataImageIndex.Database, catalogItem);
                dbMetaDataNavigation.Nodes.Add(catalogNode);

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

                        foreach (DbRoutineDependencyItem routineDependency in Program.Data.DbRoutineDependencies.Where(
                            w => routineKey.Equals(w)))
                        {
                            String nameValue = String.Format("{0}", routineDependency.ReferenceSchemaName);

                            if (!String.IsNullOrWhiteSpace(routineDependency.ReferenceObjectName))
                            { nameValue = String.Format("{0}.{1}", nameValue, routineDependency.ReferenceObjectName); }

                            if (!String.IsNullOrWhiteSpace(routineDependency.ReferenceColumnName))
                            { nameValue = String.Format("{0}.{1}", nameValue, routineDependency.ReferenceColumnName); }

                            CreateNode(nameValue, dbDataImageIndex.Dependency, routineDependency, routineNode);
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

            TreeNode CreateNode(String? nodeText, dbDataImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = dbDataImageItems[imageIndex].imageKey;
                result.SelectedImageKey = dbDataImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { dbDataNodes.Add(result, source); }

                return result;
            }
        }

        private void dbMetaDataNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (dbDataNodes.ContainsKey(e.Node))
            {
                Object dataNode = dbDataNodes[e.Node];

                if (dataNode is IDbSchemaItem schemaItem)
                { Activate((data) => new Forms.Database.DbSchema(schemaItem), schemaItem); }

                if (dataNode is IDbTableItem tableItem)
                { Activate((data) => new Forms.Database.DbTable(tableItem), tableItem); }

                if (dataNode is IDbTableColumnItem columnItem)
                { Activate((data) => new Forms.Database.DbTableColumn(columnItem), columnItem); }

                if (dataNode is IDbConstraintItem constraintItem)
                { Activate((data) => new Forms.Database.DbConstraint(constraintItem), constraintItem); }

                if(dataNode is IDbRoutineItem routineItem)
                { Activate((data) => new Forms.Database.DbRoutine(routineItem), routineItem); }

                if (dataNode is IDbDomainItem domainItem)
                { Activate((data) => new Forms.Database.DbDomain(domainItem), domainItem); }

            }
        }
        #endregion

        #region domainModelNavigation
        Dictionary<TreeNode, Object> domainModelNodes = new Dictionary<TreeNode, Object>();
        enum domainModelImageIndex
        {
            Attribute,
            Attributes,
            Property,
            Alias,
            Entity
        }

        static Dictionary<domainModelImageIndex, (String imageKey, Image image)> domainModelImageItems = new Dictionary<domainModelImageIndex, (String imageKey, Image image)>()
        {
            {domainModelImageIndex.Attribute,    ("Attribute",   Resources.Attribute) },
            {domainModelImageIndex.Attributes,   ("Attributes",  Resources.Parameter) },
            {domainModelImageIndex.Property,     ("Property",    Resources.Property) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Synonym) },
            {domainModelImageIndex.Entity,       ("Entity",      Resources.ClassPublic) },
        };


        void BuildDomainModelTreeByAttribute()
        {
            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();

            foreach (IDomainAttributeItem attributeItem in
                Program.Data.DomainAttributes.
                OrderBy(o => o.AttributeTitle))
            {
                TreeNode attributeNode = CreateAttribute(attributeItem, null);
                domainModelNavigation.Nodes.Add(attributeNode);
            }

            TreeNode CreateAttribute(IDomainAttributeItem attributeItem, TreeNode? parent)
            {
                TreeNode attributeNode = CreateNode(attributeItem.AttributeTitle, domainModelImageIndex.Attribute, attributeItem);
                DomainAttributeKey key = new DomainAttributeKey(attributeItem);

                List<DomainAttributePropertyItem> properties = Program.Data.DomainAttributeProperties.Where(w => key.Equals(w)).ToList();
                foreach (DomainAttributePropertyItem propertyItem in properties)
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(propertyTitle, domainModelImageIndex.Property, propertyItem, attributeNode);
                }

                List<DomainAttributeAliasItem> alias = Program.Data.DomainAttributeAliases.Where(w => key.Equals(w)).ToList();
                foreach (DomainAttributeAliasItem aliasItem in alias)
                { CreateNode(aliasItem.ToString(), domainModelImageIndex.Alias, aliasItem, attributeNode); }

                // TODO: Children not yet supported
                //var children = Program.Data.DomainAttributes.Where(w => w.AttributeId == attributeItem.ParentAttributeId).ToList();
                //foreach (DomainAttributeItem childAttributeItem in Program.Data.DomainAttributes.Where(w => w.AttributeId == attributeItem.ParentAttributeId))
                //{ attributeNode.Nodes.Add(CreateAttribute(childAttributeItem, attributeNode)); }

                List<DomainEntityItem> entities = Program.Data.GetEntities(key).OrderBy(o => o.EntityTitle).ToList();
                foreach (DomainEntityItem item in entities)
                { CreateNode(item.EntityTitle, domainModelImageIndex.Entity, item, attributeNode); }

                if (parent is not null) { parent.Nodes.Add(attributeNode); }
                return attributeNode;
            }

            TreeNode CreateNode(String? nodeText, domainModelImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = domainModelImageItems[imageIndex].imageKey;
                result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { domainModelNodes.Add(result, source); }

                return result;
            }
        }

        void BuildDomainModelTreeByEntity()
        {
            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();


            foreach (IDomainEntityItem entityItem in
                Program.Data.DomainEntities.
                OrderBy(o => o.EntityTitle))
            {
                TreeNode entityNode = CreateEntity(entityItem, null);
                domainModelNavigation.Nodes.Add(entityNode);
            }

            TreeNode CreateEntity(IDomainEntityItem entityItem, TreeNode? parent)
            {
                TreeNode entityNode = CreateNode(entityItem.EntityTitle, domainModelImageIndex.Entity, entityItem);
                DomainEntityKey key = new DomainEntityKey(entityItem);

                List<DomainEntityPropertyItem> properties = Program.Data.DomainEntityProperties.Where(w => key.Equals(w)).ToList();
                foreach (DomainEntityPropertyItem propertyItem in properties)
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(propertyTitle, domainModelImageIndex.Property, propertyItem, entityNode);
                }

                List<DomainEntityAliasItem> alias = Program.Data.DomainEntityAliases.Where(w => key.Equals(w)).ToList();
                foreach (DomainEntityAliasItem aliasItem in alias)
                { CreateNode(aliasItem.ToString(), domainModelImageIndex.Alias, aliasItem, entityNode); }

                List<DomainAttributeItem> attributes = Program.Data.GetAttributes(key).OrderBy(o => o.AttributeTitle).ToList();
                foreach (DomainAttributeItem item in attributes)
                { CreateNode(item.AttributeTitle, domainModelImageIndex.Attribute, item, entityNode); }

                if (parent is not null) { parent.Nodes.Add(entityNode); }
                return entityNode;
            }

            TreeNode CreateNode(String? nodeText, domainModelImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = domainModelImageItems[imageIndex].imageKey;
                result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { domainModelNodes.Add(result, source); }

                return result;
            }
        }

        private void domainModelNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (domainModelNodes.ContainsKey(e.Node))
            {
                Object dataNode = domainModelNodes[e.Node];

                if (dataNode is IDomainAttributeItem attributeItem)
                { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

                if (dataNode is IDomainEntityItem entityItem)
                { Activate((data) => new Forms.Domain.DomainEntity(entityItem), entityItem); }
            }
        }

        private void choiceDisplayOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (choiceAttribute.Checked)
            { BuildDomainModelTreeByAttribute(); }
            else if (choiceEntity.Checked)
            { BuildDomainModelTreeByEntity(); }
        }
        #endregion
    }
}

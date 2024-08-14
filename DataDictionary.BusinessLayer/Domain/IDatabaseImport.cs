using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    public class DatabaseImport
    {
        IDatabaseModel database;
        IDomainModel model;

        public DatabaseImport(IDatabaseModel source, IDomainModel target)
        {
            database = source;
            model = target;
        }

        public void Import(ICatalogIndex catalog)
        {
            CatalogIndex key = new CatalogIndex(catalog);

            foreach (TableValue item in database.DbTables.Where(w => key.Equals(w)))
            { Import(catalog, item); }
        }

        public void Import(ICatalogIndex catalog, ITableIndexName table)
        {
            CatalogIndex key = new CatalogIndex(catalog);
            TableIndexName tableName = new TableIndexName(table);

            foreach (TableColumnValue item in database.DbTableColumns.Where(w => key.Equals(w) && tableName.Equals(w)))
            {
                // TODO: Build Entities

                // Build Attributes
                Import(catalog, item);
            }
        }

        public void Import(ICatalogIndex catalog, ITableColumnIndexName column)
        {
            CatalogIndex key = new CatalogIndex(catalog);
            TableColumnIndexName columnIndex = new TableColumnIndexName(column);
            List<AliasIndex> alias = new List<AliasIndex>();

            var currentColumn = database.DbTableColumns.FirstOrDefault(w => columnIndex.Equals(w));

            var aliasColumns = database.DbTableColumns.
                Where(w => key.Equals(w) && String.Equals(w.ColumnName, columnIndex.ColumnName, KeyExtension.CompareString)).
                ToList();

            // Discover all Alias
            foreach (TableColumnValue item in aliasColumns)
            { GetAlias(item); }

            // Experiment to find Table and Constraint for each alias
            //var x = alias.Join(
            //        database.DbTables,
            //        alias => new TableIndexName(alias),
            //        table => new TableIndexName(table),
            //        (alias, table) => new { alias , table }).
            //    Join (
            //        database.DbConstraints,
            //        alias => new TableIndexName(alias.alias),
            //        constraint => new TableIndexName(constraint),
            //        (alias, constraint) => new {alias.alias, alias.table, constraint }).
            //    ToList();

            if (currentColumn is not null)
            {
                AttributeValue newAttribute = new AttributeValue()
                {
                    AttributeTitle = currentColumn.ColumnName,
                    MemberName = currentColumn.ColumnName,
                    IsDerived = currentColumn.IsComputed ?? false,
                    IsIntegral = !currentColumn.IsComputed ?? false,
                    IsNullable = currentColumn.IsNullable ?? false,
                    IsValued = !currentColumn.IsNullable ?? false,
                };

                List<AttributeAliasValue> attributeAliases = alias.
                    Select(s => new AttributeAliasValue(newAttribute, s)).
                    ToList();

                List<AttributePropertyValue> attributeProperties = model.Properties.
                    Where(w => !String.IsNullOrWhiteSpace(w.ExtendedPropertyName)).
                    Join(database.DbExtendedProperties.
                        Where(w => new ExtendedPropertyIndexName(currentColumn).Equals(w)),
                        model => model.ExtendedPropertyName,
                        data => data.PropertyName,
                        (model, data) => new AttributePropertyValue(newAttribute, model, data)).
                    ToList();

                //TODO: Handle items that already exists.
                model.Attributes.Add(newAttribute);
                attributeAliases.ForEach(f => model.Attributes.Aliases.Add(f));
                attributeProperties.ForEach(f => model.Attributes.Properties.Add(f));

                //TODO: Hook up to the Entities
            }

            void GetAlias(TableColumnValue value)
            {
                TableColumnIndexName columnKey = new TableColumnIndexName(value);
                alias.Add(new AliasIndex(value));

                var constraints = database.DbConstraintColumns.
                    Where(w => columnKey.Equals(w)).
                    Join(database.DbTableColumns,
                        constraint => new ConstraintColumnIndexReferenced(constraint).AsColumnName(),
                        column => new TableColumnIndexName(column),
                        (constraint, column) => new { constraint, column }).
                    Where(w => !alias.Contains(new AliasIndex(w.column))).
                    ToList();

                foreach (var item in constraints)
                { GetAlias(item.column); }

            }

        }
    }
}

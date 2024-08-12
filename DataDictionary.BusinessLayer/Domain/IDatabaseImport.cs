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
    /// <summary>
    /// Interface for Importing a Catalog into a Domain
    /// </summary>
    public interface ICatalogImport
    {
        /// <summary>
        /// Imports the Catalog into the Domain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDefinition"></param>
        /// <param name="key"></param>
        void Import(IDatabaseModel source, IPropertyData propertyDefinition, ICatalogIndex key);
    }

    /// <summary>
    /// Interface for Importing a Catalog Table into a Domain
    /// </summary>
    public interface ITableImport : ICatalogImport
    {
        /// <summary>
        /// Imports the Catalog Table into the Domain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDefinition"></param>
        /// <param name="key"></param>
        void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableIndex key);
    }

    /// <summary>
    /// Interface for Importing a Catalog Table Column into a Domain
    /// </summary>
    public interface ITableColumnImport : ITableImport
    {
        /// <summary>
        /// Interface for Importing a Catalog Table Column into a Domain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDefinition"></param>
        /// <param name="key"></param>
        void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableColumnIndex key);
    }

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
            {
                Import(catalog, item);
            }

        }

        public void Import(ICatalogIndex catalog, ITableIndexName table)
        {
            CatalogIndex key = new CatalogIndex(catalog);
            TableIndexName tableName = new TableIndexName(table);

            foreach (TableColumnValue item in database.DbTableColumns.Where(w => key.Equals(w) && tableName.Equals(w)))
            {
                // Build Entities

                // Build Attributes
                Import(catalog, item);
            }
        }

        public void Import(ICatalogIndex catalog, ITableColumnIndexName column)
        {
            CatalogIndex key = new CatalogIndex(catalog);
            TableColumnIndexName columnIndex = new TableColumnIndexName(column);
            List<TableColumnIndexName> alias = new List<TableColumnIndexName>();

            var columns = database.DbTableColumns.
                Where(w => key.Equals(w) && String.Equals(w.ColumnName, columnIndex.ColumnName, KeyExtension.CompareString)).
                ToList();

            foreach (TableColumnValue item in columns)
            { GetAlias(item); }

            //var constraints = columns.GroupJoin(database.DbConstraintColumns,
            //    column => new ConstraintColumnIndexName(column),
            //    constraint => new ConstraintColumnIndexName((IConstraintColumnIndexName)constraint),
            //    (column, constraints) => new { column, constraints = constraints.ToList() }).
            //    ToList();

            var x = alias.Join(
                database.DbTableColumns,
                alias => alias,
                column => new TableColumnIndexName(column),
                (alias, column) => new
                {
                    alias,
                    column,
                    a = database.DbConstraintColumns.Where(w => alias.Equals(w)).ToList(),
                    isBaseTable = column.TableType is DbTableType.Table or DbTableType.TemporalTable
                        && database.DbConstraintColumns.Count(w => alias.Equals(w) && String.IsNullOrWhiteSpace(w.ReferenceColumnName)) == 0
                }).ToList();

            var breakPoint = 1;

            void GetAlias(ITableColumnIndexName value)
            {
                TableColumnIndexName column = new TableColumnIndexName(value);
                alias.Add(column);

                var constraints = database.DbConstraintColumns.
                    Where(w => column.Equals(w)).
                    Join(database.DbTableColumns,
                    constraint => new TableColumnIndexName(new ConstraintColumnIndexName(constraint)),
                    column => new TableColumnIndexName(column),
                    (constraint, column) => new TableColumnIndexName(constraint)).
                    Except(alias).
                    ToList();

                foreach (var item in constraints)
                { GetAlias(item); }

            }

        }
    }
}

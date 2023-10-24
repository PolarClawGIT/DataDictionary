using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData : IModelCatalog
    {
        /// <inheritdoc/>
        public DbCatalogCollection DbCatalogs { get; } = new DbCatalogCollection();

        /// <inheritdoc/>
        public DbSchemaCollection DbSchemta { get; } = new DbSchemaCollection();

        /// <inheritdoc/>
        public DbDomainCollection DbDomains { get; } = new DbDomainCollection();

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        public DbTableCollection DbTables { get; } = new DbTableCollection();

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        public DbTableColumnCollection DbTableColumns { get; } = new DbTableColumnCollection();

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        public DbExtendedPropertyCollection DbExtendedProperties { get; } = new DbExtendedPropertyCollection();

        /// <summary>
        /// List of Database Constraints (keys...) within the Model.
        /// </summary>
        public DbConstraintCollection DbConstraints { get; } = new DbConstraintCollection();

        /// <summary>
        /// List of Database Constraint Columns within the Model.
        /// </summary>
        public DbConstraintColumnCollection DbConstraintColumns { get; } = new DbConstraintColumnCollection();

        /// <summary>
        /// List of Database Routines (procedures, functions, ...) within the Model.
        /// </summary>
        public DbRoutineCollection DbRoutines { get; } = new DbRoutineCollection();

        /// <summary>
        /// List of Database Parameters for the Routines within the Model.
        /// </summary>
        public DbRoutineParameterCollection DbRoutineParameters { get; } = new DbRoutineParameterCollection();

        /// <summary>
        /// List of Database Dependencies for the Routines within the Model.
        /// </summary>
        public DbRoutineDependencyCollection DbRoutineDependencies { get; } = new DbRoutineDependencyCollection();
    }
}

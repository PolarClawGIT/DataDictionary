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

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Catalog
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public interface IModelCatalog
    {
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        DbCatalogCollection DbCatalogs { get; }

        /// <summary>
        /// List of Database Schemta within the Model.
        /// </summary>
        DbSchemaCollection DbSchemta { get; }

        /// <summary>
        /// List of Database Domains (types) within the Model.
        /// </summary>
        DbDomainCollection DbDomains { get; }

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        DbTableCollection DbTables { get; }

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        DbTableColumnCollection DbTableColumns { get; }

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        DbExtendedPropertyCollection DbExtendedProperties { get; }

        /// <summary>
        /// List of Database Constraints (keys...) within the Model.
        /// </summary>
        DbConstraintCollection DbConstraints { get; }

        /// <summary>
        /// List of Database Constraint Columns within the Model.
        /// </summary>
        DbConstraintColumnCollection DbConstraintColumns { get; }

        /// <summary>
        /// List of Database Routines (procedures, functions, ...) within the Model.
        /// </summary>
        DbRoutineCollection DbRoutines { get; }

        /// <summary>
        /// List of Database Parameters for the Routines within the Model.
        /// </summary>
        DbRoutineParameterCollection DbRoutineParameters { get; }

        /// <summary>
        /// List of Database Dependencies for the Routines within the Model.
        /// </summary>
        DbRoutineDependencyCollection DbRoutineDependencies { get; }
    }

    partial class ModelData: IModelCatalog
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

using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Catalog
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
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
}

// Ignore Spelling: Nullable
namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog RoutineColumn interface (data elements only)
    /// </summary>
    public interface IRoutineColumn : IRoutineColumnKeyName, IDataType, IOrdinalPosition, IRoutineType, IDomainKeyReference
    {
        /// <summary>
        /// Is the Column Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// Column Default value
        /// </summary>
        String? ColumnDefault { get; }

        /// <summary>
        /// Is the Column an Identity
        /// </summary>
        Boolean? IsIdentity { get; }

        /// <summary>
        /// Is the Column Computed
        /// </summary>
        Boolean? IsComputed { get; }

        /// <summary>
        /// If the Column is Computed, the Computed Definition
        /// </summary>
        String? ComputedDefinition { get; }
    }

    static class RoutineColumn
    {
        public const String GetProcedure = "[AppCatalog].[procGetRoutineColumn]";
        public const String SetProcedure = "[AppCatalog].[procSetRoutineColumn]";
        public const String TableType = "[AppCatalog].[udttRoutineColumn]";
    }
}
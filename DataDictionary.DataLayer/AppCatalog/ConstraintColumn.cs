namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog ConstraintColumn interface (data elements only)
    /// </summary>
    public interface IConstraintColumn : IConstraintColumnKeyName, IOrdinalPosition,
        ITableColumnKeyName, IConstraintColumnKeyReferenced
    {
        /// <inheritdoc cref="IConstraintColumnKeyName.ColumnName"/>
        new String? ColumnName { get; }
    }
    
    static class ConstraintColumn
    {
        public const String GetProcedure = "[AppCatalog].[procGetConstraintColumn]";
        public const String SetProcedure = "[AppCatalog].[procSetConstraintColumn]";
        public const String TableType = "[AppCatalog].[udttConstraintColumn]";
    }
}
namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Routine interface (data elements only)
    /// </summary>
    public interface IRoutine : IRoutineKeyName, IRoutineType
    {

    }

    /// <summary>
    /// Common Catalog Routine Type
    /// </summary>
    public interface IRoutineType
    {
        /// <summary>
        /// Type of Routine (Procedure, Function, ...)
        /// </summary>
        String? RoutineType { get; }
    }

    static class Routine
    {
        public const String GetProcedure = "[AppCatalog].[procGetRoutine]";
        public const String IsSystem = "dbo.sp_creatediagram, dbo.sp_renamediagram, dbo.sp_alterdiagram, dbo.sp_dropdiagram, dbo.fn_diagramobjects, dbo.sp_helpdiagrams, dbo.sp_helpdiagramdefinition, dbo.sp_upgraddiagrams, INFORMATION_SCHEMA.*, sys.*";
        public const String RoutineId = "@RoutineId";
        public const String SetProcedure = "[AppCatalog].[procSetRoutine]";
        public const String TableType = "[AppCatalog].[udttRoutine]";
    }
}
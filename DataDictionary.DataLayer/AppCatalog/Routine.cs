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

    /// <summary>
    /// Static class containing constants related to the Routines database operations.
    /// </summary>
    static class Routine
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Routine));

        /// <summary>
        /// The stored procedure to retrieve routine information.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// A list of system routines that are predefined in the database.
        /// </summary>
        public readonly static String IsSystem = "dbo.sp_creatediagram, dbo.sp_renamediagram, dbo.sp_alterdiagram, dbo.sp_dropdiagram, dbo.fn_diagramobjects, dbo.sp_helpdiagrams, dbo.sp_helpdiagramdefinition, dbo.sp_upgraddiagrams, INFORMATION_SCHEMA.*, sys.*";

        /// <summary>
        /// The parameter name for the routine identifier.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// The stored procedure to set routine information.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// The table type used for routines in the database.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
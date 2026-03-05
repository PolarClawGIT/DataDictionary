namespace DataDictionary.DataLayer.AppGeneral;

/// <summary>
/// Static class containing constants related to the HelpSubject database operations.
/// </summary>
public static class HelpSubject
{
    /// <inheritdoc cref="Object.GetType"/>
    static readonly SchemaName schema = new SchemaName(typeof(HelpSubject));

    /// <summary>
    /// The stored procedure for retrieving help subjects.
    /// </summary>
    public readonly static String GetProcedure = schema.FullName("[procGetHelpSubject]");

    /// <summary>
    /// The parameter name for HelpId in database operations.
    /// </summary>
    public readonly static String HelpId = "@HelpId";

    /// <summary>
    /// The stored procedure for setting help subjects.
    /// </summary>
    public readonly static String SetProcedure = schema.FullName("[procSetHelpSubject]");

    /// <summary>
    /// The user-defined table type for help subjects.
    /// </summary>
    public readonly static String TableType = schema.FullName("[udttHelpSubject]");
}
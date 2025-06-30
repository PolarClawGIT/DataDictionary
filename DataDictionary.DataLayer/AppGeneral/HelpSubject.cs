namespace DataDictionary.DataLayer.AppGeneral;

/// <summary>
/// Static class containing constants related to the HelpSubject database operations.
/// </summary>
public static class HelpSubject
{
    /// <summary>
    /// The stored procedure for retrieving help subjects.
    /// </summary>
    public const string GetProcedure = "[AppGeneral].[procGetHelpSubject]";

    /// <summary>
    /// The parameter name for HelpId in database operations.
    /// </summary>
    public const string HelpId = "@HelpId";

    /// <summary>
    /// The stored procedure for setting help subjects.
    /// </summary>
    public const string SetProcedure = "[AppGeneral].[procSetHelpSubject]";

    /// <summary>
    /// The user-defined table type for help subjects.
    /// </summary>
    public const string TableType = "[AppGeneral].[udttHelpSubject]";
}
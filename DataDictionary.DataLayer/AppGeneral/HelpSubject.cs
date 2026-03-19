namespace DataDictionary.DataLayer.AppGeneral;

/// <summary>
/// Static class containing constants related to the HelpSubject database operations.
/// </summary>
public static class HelpSubject
{
    static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(HelpSubject)) { Identifier = "@HelpId" };

    /// <summary>
    /// The stored procedure for retrieving help subjects.
    /// </summary>
    public readonly static String GetProcedure = dataObject.GetProcedure;

    /// <summary>
    /// The parameter name for HelpId in database operations.
    /// </summary>
    public readonly static String Identifier = dataObject.Identifier;

    /// <summary>
    /// The stored procedure for setting help subjects.
    /// </summary>
    public readonly static String SetProcedure = dataObject.SetProcedure;

    /// <summary>
    /// The user-defined table type for help subjects.
    /// </summary>
    public readonly static String TableType = dataObject.TableType;
}
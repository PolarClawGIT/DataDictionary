namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Base Model SubjectArea Interface (data elements only)
    /// </summary>
    public interface ISubjectArea
    {
        /// <summary>
        /// Description of the Subject Area
        /// </summary>
        String? SubjectAreaDescription { get; }

        /// <summary>
        /// NameSpace used for the Subject Area
        /// </summary>
        String? SubjectName { get; }
    }

    /// <summary>
    /// Static class containing constants related to the Subject Area database operations.
    /// </summary>
    static class SubjectArea
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(SubjectArea));

        /// <summary>
        /// Stored procedure for retrieving Subject Area data.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure for setting Subject Area data.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// Parameter name for Subject Area ID.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// Table type for Subject Area data.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
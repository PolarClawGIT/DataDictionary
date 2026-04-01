namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Attribute Name within the scope of a Subject Area
    /// </summary>
    public interface IAttributeSubjectAreaName
    {
        /// <summary>
        /// Attribute Name within the Subject Area.
        /// </summary>
        String? AttributeName { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Attribute Subject Area database operations.  
    /// </summary>  
    static class AttributeSubjectArea
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(AttributeSubjectArea));

        /// <summary>  
        /// The stored procedure used to retrieve Attribute Subject Area data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set Attribute Subject Area data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for Attribute Subject Area data operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
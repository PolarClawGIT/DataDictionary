namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Domain interface (data elements only)
    /// </summary>
    public interface IDomain : IDomainKeyName, IDataType
    {
        /// <summary>
        /// The Default value for the Domain
        /// </summary>
        String? DomainDefault { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Domain database operations.
    /// </summary>  
    static class Domain
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Domain));

        /// <summary>  
        /// Identifier for the Domain  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// Stored procedure to retrieve Domain information  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// Stored procedure to set Domain information  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// Table type for Domain  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
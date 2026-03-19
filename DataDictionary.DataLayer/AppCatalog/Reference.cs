namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Reference Interface (data elements only)
    /// </summary>
    public interface IReference : IReferenceKeyName, IReferencedKeyColumn
    {
        /// <summary>
        /// The type of the Referencing Object
        /// </summary>
        String? ObjectType { get; }

        /// <summary>
        /// The type of the Referenced Object
        /// </summary>
        String? ReferencedType { get; }

        /// <summary>
        /// Is the Reference Item Dependent on the Caller
        /// </summary>
        Boolean? IsCallerDependent { get; }

        /// <summary>
        /// Is the Reference Item Ambiguous
        /// </summary>
        Boolean? IsAmbiguous { get; }

        /// <summary>
        /// Is the Reference Item Selected
        /// </summary>
        Boolean? IsSelected { get; }

        /// <summary>
        /// Is the Reference Item Modified (usually Updated but can be Deleted)
        /// </summary>
        Boolean? IsModified { get; }

        /// <summary>
        /// Is the Reference Item all columns Selected
        /// </summary>
        Boolean? IsSelectAll { get; }

        /// <summary>
        /// Is the Reference Item Found All Columns
        /// </summary>
        Boolean? IsAllColumnsFound { get; }

        /// <summary>
        /// Is the Reference Item is Insert All
        /// </summary>
        Boolean? IsInsertAll { get; }

        /// <summary>
        /// Is the Reference Item is Complete
        /// </summary>
        Boolean? IsIncomplete { get; }
    }

    /// <summary>
    /// Static class containing constants related to the References database operations..
    /// </summary>
    static class Reference
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Reference));

        /// <summary>
        /// The stored procedure for retrieving references.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// The parameter name for the reference ID.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// The stored procedure for setting references.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// The user-defined table type for references.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
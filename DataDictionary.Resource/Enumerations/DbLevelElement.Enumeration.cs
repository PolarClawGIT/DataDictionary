namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for Database Extended Procedure Element Level type.
    /// </summary>
    public class DbLevelElementEnumeration : Enumeration<DbLevelElementType, DbLevelElementEnumeration>
    {
        /// <summary>
        /// Internal Constructor for Database Extended Procedure Element Level Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        DbLevelElementEnumeration(DbLevelElementType value, String name) : base(value, name) { }

        /// <summary>
        /// Static constructor, loads data.
        /// </summary>
        static DbLevelElementEnumeration()
        {
            List<DbLevelElementEnumeration> data = new List<DbLevelElementEnumeration>()
            {
                new DbLevelElementEnumeration(DbLevelElementType.Null, String.Empty) { DisplayName = "not defined" },
                new DbLevelElementEnumeration(DbLevelElementType.Default,"DEFAULT"),
                new DbLevelElementEnumeration(DbLevelElementType.Column,"COLUMN"),
                new DbLevelElementEnumeration(DbLevelElementType.Constraint,"CONSTRAINT"),
                new DbLevelElementEnumeration(DbLevelElementType.EventNotification,"EVENT NOTIFICATION"),
                new DbLevelElementEnumeration(DbLevelElementType.Index,"INDEX"),
                new DbLevelElementEnumeration(DbLevelElementType.Parameter,"PARAMETER"),
                new DbLevelElementEnumeration(DbLevelElementType.Trigger,"TRIGGER"),
            };



            BuildDictionary(data);
        }

    }
}
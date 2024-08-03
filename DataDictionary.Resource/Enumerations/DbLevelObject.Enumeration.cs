namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for Database Extended Procedure Object Level type.
    /// </summary>
    public class DbLevelObjectEnumeration : Enumeration<DbLevelObjectType, DbLevelObjectEnumeration>
    {
        /// <summary>
        /// Internal Constructor for Database Extended Procedure Object Level Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        DbLevelObjectEnumeration(DbLevelObjectType value, String name) : base(value, name) { }

        /// <summary>
        /// Static constructor, loads data.
        /// </summary>
        static DbLevelObjectEnumeration()
        {
            List<DbLevelObjectEnumeration> data = new List<DbLevelObjectEnumeration>()
            {
                new DbLevelObjectEnumeration(DbLevelObjectType.Null, String.Empty) { DisplayName = "not defined" },
                new DbLevelObjectEnumeration(DbLevelObjectType.Aggregate,"AGGREGATE"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Default,"DEFAULT"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Function,"FUNCTION"),
                new DbLevelObjectEnumeration(DbLevelObjectType.LogicalFileName,"LOGICAL FILE NAME"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Procedure,"PROCEDURE"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Queue,"QUEUE"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Rule,"RULE"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Synonym,"SYNONYM"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Table,"TABLE"),
                new DbLevelObjectEnumeration(DbLevelObjectType.Type,"TYPE"),
                new DbLevelObjectEnumeration(DbLevelObjectType.View,"VIEW"),
                new DbLevelObjectEnumeration(DbLevelObjectType.XmlSchemaCollection,"XML SCHEMA COLLECTION"),
            };

            BuildDictionary(data);
        }

    }
}
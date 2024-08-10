namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for DbObjectType
/// </summary>
/// <see cref="https://learn.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-objects-transact-sql?view=sql-server-ver16"/>
public enum DbObjectType
{
    Null,
    AggregateFunction,
    CheckConstraint,
    ClrScalarFunction,
    ClrStoredProcedure,
    ClrTableValuedFunction,
    ClrTrigger,
    DefaultConstraint,
    EdgeConstraint,
    ExtendedStoredProcedure,
    ForeignKeyConstraint,
    InternalTable,
    PlanGuide,
    PrimaryKeyConstraint,
    ReplicationFilterProcedure,
    Rule,
    SequenceObject,
    ServiceQueue,
    InlineTableValuedFunction,
    ScalarFunction,
    StoredProcedure,
    TableValuedFunction,
    Trigger,
    Synonym,
    SystemTable,
    TypeTable,
    UniqueConstraint,
    UserTable,
    View,
    // Added for the Application
    Column 

}

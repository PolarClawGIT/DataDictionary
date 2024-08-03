namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Level0 MS Extended Property Types. These are Database Level.
    /// Not all types are supported by the Application.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
    public enum DbLevelCatalogType
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        Null,

        /// <summary>
        /// MS SQL Assembly.
        /// </summary>
        Assembly,

        /// <summary>
        /// MS SQL Contract.
        /// </summary>
        Contract,

        /// <summary>
        /// MS SQL EventNotification.
        /// </summary>
        EventNotification,

        /// <summary>
        /// MS SQL FileGroup.
        /// </summary>
        Filegroup,

        /// <summary>
        /// MS SQL MessageType.
        /// </summary>
        MessageType,

        /// <summary>
        /// MS SQL PartitionFunction.
        /// </summary>
        PartitionFunction,

        /// <summary>
        /// MS SQL PartitionScheme
        /// </summary>
        PartitionScheme,

        /// <summary>
        /// MS SQL RemoteServiceBinding.
        /// </summary>
        RemoteServiceBinding,

        /// <summary>
        /// MS SQL Route.
        /// </summary>
        Route,

        /// <summary>
        /// MS SQL Schema. Application Supported.
        /// </summary>
        Schema,

        /// <summary>
        /// MS SQL Service.
        /// </summary>
        Service,

        /// <summary>
        /// MS SQL Trigger.
        /// </summary>
        Trigger,

        /// <summary>
        /// MS SQL Type.
        /// </summary>
        Type,

        /// <summary>
        /// MS SQL User.
        /// </summary>
        User,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Level1 MS Extended Property Types. These are Object Level.
    /// Not all types are supported by the Application.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
    public enum DbLevelObjectType
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        Null,

        /// <summary>
        /// MS SQL Aggregate.
        /// </summary>
        Aggregate,

        /// <summary>
        /// MS SQL Default.
        /// </summary>
        Default,

        /// <summary>
        /// MS SQL Function. Application Supported.
        /// </summary>
        Function,

        /// <summary>
        /// MS SQL LogicalFileName.
        /// </summary>
        LogicalFileName,

        /// <summary>
        /// MS SQL Procedure. Application Supported.
        /// </summary>
        Procedure,

        /// <summary>
        /// MS SQL Queue.
        /// </summary>
        Queue,

        /// <summary>
        /// MS SQL Rule.
        /// </summary>
        Rule,

        /// <summary>
        /// MS SQL Synonym.
        /// </summary>
        Synonym,

        /// <summary>
        /// MS SQL Table. Application Supported.
        /// </summary>
        Table,

        /// <summary>
        /// MS SQL Type. Application Supported.
        /// </summary>
        Type,

        /// <summary>
        /// MS SQL View. Application Supported.
        /// </summary>
        View,

        /// <summary>
        /// MS SQL XmlSchemaCollection.
        /// </summary>
        XmlSchemaCollection,
    }

}

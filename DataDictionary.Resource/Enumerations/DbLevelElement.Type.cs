using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Level2 MS Extended Property Types. These are Element Level.
    /// Not all types are supported by the Application.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
    public enum DbLevelElementType
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        Null,

        /// <summary>
        /// MS SQL Default.
        /// </summary>
        Default,

        /// <summary>
        /// MS SQL Column. Application Supported.
        /// </summary>
        Column,

        /// <summary>
        /// MS SQL Constraint.
        /// </summary>
        Constraint,

        /// <summary>
        /// MS SQL EventNotification.
        /// </summary>
        EventNotification,

        /// <summary>
        /// MS SQL Index.
        /// </summary>
        Index,

        /// <summary>
        /// MS SQL Parameter. Application Supported.
        /// </summary>
        Parameter,

        /// <summary>
        /// MS SQL Trigger.
        /// </summary>
        Trigger,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// List of supported Constraint Types.
    /// </summary>
    public enum DbConstraintType
    {
        /// <summary>
        /// Unknown Constraint Type
        /// </summary>
        Null,

        /// <summary>
        /// Check Constraint
        /// </summary>
        Check,

        /// <summary>
        /// Unique Key Constraint
        /// </summary>
        Unique,

        /// <summary>
        /// Primary Key Constraint
        /// </summary>
        PrimaryKey,

        /// <summary>
        /// Foreign Key Constraint
        /// </summary>
        ForeignKey,
    }
}

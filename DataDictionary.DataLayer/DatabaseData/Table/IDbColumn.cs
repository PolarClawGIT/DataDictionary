using DataDictionary.DataLayer.DatabaseData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Commonly used Column Position
    /// </summary>
    public interface IDbColumnPosition
    {
        /// <summary>
        /// The Position/Order of the Column
        /// </summary>
        Nullable<Int32> OrdinalPosition { get; }
    }

    /// <summary>
    /// Common Properties of a Database Column and Parameters.
    /// Used by Table Column and Routine Parameter.
    /// </summary>
    public interface IDbColumn : IDbDomain, IDbColumnPosition
    {

    }
}

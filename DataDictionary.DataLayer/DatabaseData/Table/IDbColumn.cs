using DataDictionary.DataLayer.AppCatalog;
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
    [Obsolete()]
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
    [Obsolete]
    public interface IDbColumn : IDataType, IDbColumnPosition
    {

    }
}

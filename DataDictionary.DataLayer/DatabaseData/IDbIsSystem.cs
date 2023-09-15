using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// Interface for Database Items are a system object.
    /// </summary>
    public interface IDbIsSystem
    {
        /// <summary>
        /// The object is a database system object.
        /// </summary>
        Boolean IsSystem { get; }
    }
}

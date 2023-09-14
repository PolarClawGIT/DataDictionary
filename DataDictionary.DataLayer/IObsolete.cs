using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Interface for the Obsolete property
    /// </summary>
    public interface IObsolete
    {
        /// <summary>
        /// An item is marked as Obsolete and its use should be phased out.
        /// The item is a candidate to be deleted when all references are removed.
        /// </summary>
        bool? Obsolete { get; }
    }
}

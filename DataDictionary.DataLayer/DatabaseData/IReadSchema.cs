using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// A data object capable of Reading Database Schema data
    /// </summary>
    public interface IReadSchema
    {
        /// <summary>
        /// Gets the Database Command that returns schema data.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        Command SchemaCommand(IConnection connection);
    }
}

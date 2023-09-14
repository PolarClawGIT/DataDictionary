using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// A data object capable of Reading Data
    /// </summary>
    public interface IReadData
    {
        /// <summary>
        /// Gets the Database Command that returns data.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        Command LoadCommand(IConnection connection);
    }

    /// <summary>
    /// A data object capable of Reading Data by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IReadData<TKey>
        where TKey: IKey
    {
        /// <summary>
        /// Gets the Database Command for a specific key that returns data
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Command LoadCommand(IConnection connection, TKey key);
    }
}

using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// A data object capable of Deleting Data by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDeleteData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Gets the Database Command for a specific key that deletes data
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Command DeleteCommand(IConnection connection, TKey key);
    }
}

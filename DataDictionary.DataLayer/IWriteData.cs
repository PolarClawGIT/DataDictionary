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
    /// A data object capable of writing data.
    /// </summary>
    public interface IWriteData
    {
        /// <summary>
        /// Returns the Command to Save the Data.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// <example>
        /// public Command SaveCommand(IConnection connection)
        /// { return SaveCommand(connection); }
        ///   Incorrect implementation.
        ///   This will cause unintended recursion and a Stack Overflow and close the application without an exception or notification.
        /// </example>
        Command SaveCommand(IConnection connection);
    }

    /// <summary>
    /// A data object capable of writing data by Key.
    /// </summary>
    public interface IWriteData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Returns the Command to Save the Data with a specific key.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Command SaveCommand(IConnection connection, TKey key);
    }
}

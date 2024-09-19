using DataDictionary.Resource;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer;

/// <summary>
/// A data object capable of Reading History (Modification) Data
/// </summary>
public interface ITemporalData
{
    /// <summary>
    /// Gets the Database Command that returns History data
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    Command HistoryCommand(IConnection connection);
}

/// <summary>
/// A data object capable of Reading History (Modification) Data by Key
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface ITemporalData<TKey>
    where TKey : IKey
{
    /// <summary>
    /// Gets the Database Command that returns History data by Key
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    Command HistoryCommand(IConnection connection, TKey key);
}

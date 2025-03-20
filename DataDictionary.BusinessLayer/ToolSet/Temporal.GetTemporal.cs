using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface that supports returning the Temporal data.
    /// </summary>
    public interface IGetTemporal
    {
        /// <summary>
        /// Returns the Temporal Data
        /// </summary>
        /// <returns></returns>
        ITemporalData GetTemporal();
    }

    /// <summary>
    /// Interface that supports returning the Temporal data.
    /// </summary>
    public interface IGetTemporal<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Returns the Temporal Data
        /// </summary>
        /// <returns></returns>
        ITemporalData GetTemporal(TKey key);
    }
}

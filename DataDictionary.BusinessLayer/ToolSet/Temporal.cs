using DataDictionary.DataLayer;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Item can be cast as a Temporal Value
    /// </summary>
    public interface ITemporal : ITemporalItem, IDataValue
    { }
}

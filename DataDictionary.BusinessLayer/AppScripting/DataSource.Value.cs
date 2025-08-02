using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDataSourceValue : IDataSourceItem, IDataSourceIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class DataSourceValue : DataSourceItem, IDataSourceValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public DataSourceValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new DataSourceIndex(this),
                GetScope = () => Scope,
                GetTitle = () => DataSourceTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsTitleChanged = (e) => e.PropertyName is nameof(DataSourceTitle)
            };
        }
    }
}

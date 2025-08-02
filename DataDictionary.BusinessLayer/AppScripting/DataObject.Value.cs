using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDataObjectValue : IDataObjectItem, IDataSourceIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class DataObjectValue : DataObjectItem, IDataObjectValue, IPathValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttribute; } }

        /// <summary>
        /// Path Index of the DataPath
        /// </summary>
        public PathIndex DataObjectPath
        {
            get
            { return new PathIndex(new PathIndex(PathIndex.Parse(DataPath).ToArray())); }
            set
            {
                DataPath = value.MemberFullPath;
                OnPropertyChanged(nameof(DataObjectPath));
            }
        }

        /// <summary>
        /// Member name of DataPath
        /// </summary>
        public String DataObjectMember
        { get { return DataObjectPath.Member; } }

        /// <inheritdoc/>
        public DataObjectValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DataSourceIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(DataPath))
                    { return new PathIndex(DataPath); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(DataPath).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => DataObjectMember ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DataPath),
                IsTitleChanged = (e) => e.PropertyName is nameof(DataPath)
            };
        }
    }
}

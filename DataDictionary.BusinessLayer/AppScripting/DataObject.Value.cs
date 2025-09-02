using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDataObjectValue : IDataObjectItem, IDataSourceIndex,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Path Index of the DataPath
        /// </summary>
        new PathIndex ObjectPath { get; set; }
    }

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

        /// <inheritdoc/>
        public new PathIndex ObjectPath
        {   // Changing the propoerty in the base class is not always caught by the OnPropertyChanged.
            // Extra code is needed to check if the data has changed and update the backing field.  
            get
            {
                if (!objectPathValue.MemberFullPath.Equals(base.ObjectPath))
                { objectPathValue = new PathIndex(PathIndex.Parse(base.ObjectPath).ToArray()); }

                return objectPathValue;
            }
            set
            { 
                base.ObjectPath = value.MemberFullPath;
                objectPathValue.Set(value);
                OnPropertyChanged(nameof(base.ObjectPath));
            }
        }
        PathIndex objectPathValue = new PathIndex();

        /// <inheritdoc/>
        public DataObjectValue() : base()
        { pathValue = InitPath(); }

        /// <inheritdoc/>
        public DataObjectValue(IDataSourceIndex dataSource) : base(dataSource)
        { pathValue = InitPath(); }

        PathValue InitPath()
        {
            return new PathValue(this)
            {
                GetIndex = () => new DataSourceIndex(this),
                GetPath = () => ObjectPath,
                GetScope = () => Scope,
                GetTitle = () => ObjectPath.Member ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ObjectPath),
                IsTitleChanged = (e) => e.PropertyName is nameof(ObjectPath)
            };
        }
    }
}

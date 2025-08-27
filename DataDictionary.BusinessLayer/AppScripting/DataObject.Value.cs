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
        PathIndex ObjectPath { get; set; }
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
        public PathIndex ObjectPath
        {   // Changing the propoerty in the base class is not always caught by the OnPropertyChanged.
            // Extra code is needed to check if the data has changed and update the backing field.  
            get
            {   
                if (!objectPathValue.MemberFullPath.Equals(DataPath))
                { objectPathValue = new PathIndex(PathIndex.Parse(DataPath).ToArray()); }

                return objectPathValue;
            }
            set
            {
                DataPath = value.MemberFullPath;
                objectPathValue = value;
                OnPropertyChanged(nameof(ObjectPath));
            }
        }
        PathIndex objectPathValue = new PathIndex();

        /// <inheritdoc/>
        public override String? DataPath
        {   
            get { return base.DataPath; }
            set { base.DataPath = value; OnPropertyChanged(nameof(ObjectPath)); }
        }

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
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(DataPath))
                    { return new PathIndex(DataPath); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(DataPath).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => ObjectPath.Member ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DataPath),
                IsTitleChanged = (e) => e.PropertyName is nameof(DataPath)
            };
        }
    }
}

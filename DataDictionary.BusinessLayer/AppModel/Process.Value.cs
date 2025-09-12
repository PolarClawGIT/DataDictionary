using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessValue : IProcessItem, IProcessIndex, IProcessIndexName,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public partial class ProcessValue : ProcessItem, IProcessValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProcess; } }

        /// <summary>
        /// Path Index version of the ProcessName
        /// </summary>
        public PathIndex ProcessPath
        {
            get
            { return new PathIndex(new PathIndex(PathIndex.Parse(ProcessName).ToArray())); }
            set
            {
                ProcessName = value.MemberFullPath;
                OnPropertyChanged(nameof(ProcessPath));
            }
        }

        /// <inheritdoc/>
        public ProcessValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new ProcessIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(ProcessName))
                    { return new PathIndex(ProcessTitle); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(ProcessName).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => ProcessTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ProcessTitle) or nameof(ProcessName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ProcessTitle)
            };
        }
    }
}

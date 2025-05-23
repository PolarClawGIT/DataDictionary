using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessArgumentValue : IProcessArgumentItem,
        IProcessIndex, IScopeType, ITemporal, IBindingRowState
    {
        /// <summary>
        /// Returns the Argument Name converted to a Path.
        /// </summary>
        PathIndex ArgumentPath { get; }
    }

    /// <inheritdoc/>
    public class ProcessArgumentValue : ProcessArgumentItem, IProcessArgumentValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        public DataIndex Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        public String Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProcessArgument; } }

        /// <inheritdoc/>
        public ProcessArgumentValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new ProcessIndex(this),
                GetTitle = () => ArgumentTitle ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(ArgumentTitle)
            };
        }

        /// <inheritdoc cref="ProcessArgumentItem(IProcessKey)"/>
        public ProcessArgumentValue(IProcessIndex Process) : base(Process)
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new ProcessIndex(this),
                GetTitle = () => ArgumentTitle ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(ArgumentTitle)
            };
        }

        /// <inheritdoc/>
        public PathIndex ArgumentPath
        {
            get
            {
                return new PathIndex(
                    new PathIndex(PathIndex.Parse(base.ArgumentName).ToArray()));
            }
            set { base.ArgumentName = value.MemberFullPath; }
        }
    }
}

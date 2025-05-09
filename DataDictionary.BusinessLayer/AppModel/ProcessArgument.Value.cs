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
    { }

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

        /// <inheritdoc cref="ProcessArgumentItem.ArgumentName"/>
        public PathIndex ArgumentPath
        {
            get
            {
                return new PathIndex(
                    new PathIndex(PathIndex.Parse(base.ArgumentName).ToArray()));
            }
            set { base.ArgumentName = value.MemberFullPath; }
        }

        /// <inheritdoc cref="ProcessArgumentItem.ArgumentType"/>
        public new PathIndex ArgumentType
        {
            get
            {
                return new PathIndex(
                    new PathIndex(PathIndex.Parse(base.ArgumentType).ToArray()));
            }
            set { base.ArgumentType = value.MemberFullPath; }
        }


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

        // <summary>
        // Constructor for ProcessArgumentValue
        // </summary>
        // <param name="Process"></param>
        // <param name="Argument"></param>
        //public ProcessArgumentValue(IProcessIndex Process, ArgumentValue Argument) : this(Process)
        //{
        //    ArgumentKnownAs = Argument.ArgumentTitle;
        //    ArgumentPath = Argument.ArgumentPath;
        //    FindArgument = (path) => Argument;
        //}
    }
}

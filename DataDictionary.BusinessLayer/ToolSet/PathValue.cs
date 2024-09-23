using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Item can be cast as a general Path Value
    /// </summary>
    public interface IPathValue : IPathIndex, IDataValue
    {
        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return AsPathValue().Path; } }

        /// <inheritdoc/>
        IDataValue IDataValue.AsDataValue() { return AsPathValue(); }

        /// <summary>
        /// Returns the value as the Path Value.
        /// </summary>
        /// <returns></returns>
        IPathValue AsPathValue();
    }

    /// <summary>
    /// Implementation for an Item can be cast as a Path Value
    /// </summary>
    class PathValue : DataValue, IPathValue
    {
        /// <inheritdoc/>
        public virtual PathIndex Path { get { return GetPath(); } }

        /// <summary>
        /// Function that returns the Path of the source.
        /// </summary>
        public required Func<PathIndex> GetPath { get; init; }

        /// <summary>
        /// Function to indicate that the Path has changed.
        /// </summary>
        public required Func<PropertyChangedEventArgs, Boolean> IsPathChanged { get; init; }

        /// <inheritdoc/>
        public PathValue(IDataValue source) : base(source)
        { }

        /// <inheritdoc/>
        public override event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        protected override void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (IsPathChanged(e))
            { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(Path)); }
        }


        /// <inheritdoc/>
        public IPathValue AsPathValue()
        { return this; }
    }
}

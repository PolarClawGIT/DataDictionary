using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Item can be cast as a general Path Value
    /// </summary>
    public interface IPathValue : IPathIndex, IDataValue
    { }

    /// <summary>
    /// Implementation for an Item can be cast as a Path Value.
    /// A path value contains both a PathIndex and a reference to the base object that generated the path.
    /// </summary>
    class PathValue : DataValue, IPathValue
    {
        /// <inheritdoc/>
        public virtual PathItem Path { get { return GetPath(); } }

        /// <summary>
        /// Function that returns the Path of the source.
        /// </summary>
        public required Func<PathItem> GetPath { get; init; }

        /// <summary>
        /// Function to indicate that the Path has changed.
        /// </summary>
        public required Func<PropertyChangedEventArgs, Boolean> IsPathChanged { get; init; }

        /// <inheritdoc/>
        public PathValue(IBindingPropertyChanged source) : base(source)
        { }

        /// <inheritdoc/>
        public override event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        protected override void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (IsPathChanged(e))
            { this.OnPropertyChanged(PropertyChanged, nameof(Path)); }
        }


        /// <inheritdoc/>
        public IPathValue AsPathValue()
        { return this; }
    }
}

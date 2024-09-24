using DataDictionary.Resource;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Interface for a generic DataLayer Value Source
    /// </summary>
    [Obsolete]
    public interface IDataLayerSource : IBindingPropertyChanged
    {
        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        DataLayerIndex Index { get { return GetIndex(); } }

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        String Title { get { return GetTitle(); } }

        /// <summary>
        /// Obsolete: Return the DataLayer ID.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        DataLayerIndex GetIndex() { throw new NotSupportedException(); }

        /// <summary>
        /// Obsolete: Generic Title/Name for the value
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        String GetTitle() { throw new NotSupportedException(); }

        /// <summary>
        /// Obsolete: Condition to test if the Title was the property that changed.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        [Obsolete]
        Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs) { throw new NotSupportedException(); }
    }

    /// <summary>
    /// 
    /// </summary>
    [Obsolete]
    public interface IDataLayerValue: 
        IBindingPropertyChanged
    {
        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        DataLayerIndex Index { get; }

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        String Title { get; }
    }

    /// <summary>
    /// Generic Data Layer Value
    /// </summary>
    [Obsolete]
    class DataLayerValue : IDataLayerValue
    {
        IDataLayerSource source;

        /// <inheritdoc/>
        public DataLayerIndex Index { get; }

        /// <inheritdoc/>
        public String Title { get { return titleValue; } }
        String titleValue;

        /// <summary>
        /// Creates a DataLayerValue from a source value
        /// </summary>
        /// <param name="source"></param>
        public DataLayerValue(IDataLayerSource source)
        {
            this.source = source;
            Index = source.GetIndex();
            titleValue = source.GetTitle();
            source.PropertyChanged += Source_PropertyChanged;
        }

        private void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (source.IsTitleChanged(e))
            {
                titleValue = source.GetTitle();
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, e.PropertyName);
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        { return titleValue; }

    }
}

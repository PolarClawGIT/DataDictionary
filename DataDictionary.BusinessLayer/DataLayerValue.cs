using DataDictionary.Resource;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Interface for a generic DataLayer Value Source
    /// </summary>
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
        /// Return the DataLayer ID.
        /// </summary>
        /// <returns></returns>
        DataLayerIndex GetIndex();

        /// <summary>
        /// Generic Title/Name for the value
        /// </summary>
        /// <returns></returns>
        String GetTitle();

        /// <summary>
        /// Condition to test if the Title was the property that changed.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs);
    }

    /// <summary>
    /// 
    /// </summary>
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

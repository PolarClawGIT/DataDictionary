using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer;
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
    /// Interface Item/Value for Synchronize Value
    /// </summary>
    public interface ISynchronizeValue<TItem>
        where TItem : class, IBindingRowState, IBindingPropertyChanged
    {
        /// <summary>
        /// Is the value in the Model
        /// </summary>
        Boolean InModel { get; }

        /// <summary>
        /// Is the value in the Database
        /// </summary>
        Boolean InDatabase { get; }

        /// <summary>
        /// Get the Source Item (Model or Database)
        /// </summary>
        TItem Source { get; }
    }

    /// <summary>
    /// Item/Value for Synchronize Value
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class SynchronizeValue<TItem> : ISynchronizeValue<TItem>, INotifyPropertyChanged
        where TItem : class, IBindingRowState, IBindingPropertyChanged
    {

        /// <inheritdoc/>
        public Boolean InModel
        {
            get { return inModel; }
            internal set { inModel = value; OnPropertyChanged(nameof(InModel)); }
        }
        private Boolean inModel = false;

        /// <inheritdoc/>
        public Boolean InDatabase
        {
            get { return inDatabase; }
            internal set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); }
        }
        private Boolean inDatabase = false;

        /// <inheritdoc/>
        public TItem Source { get; }

        /// <summary>
        /// Constructor for SynchronizeValue
        /// </summary>
        /// <param name="data"></param>
        protected SynchronizeValue(TItem data) : base()
        {
            Source = data;
            data.PropertyChanged += Source_OnPropertyChanged;
        }

        /// <summary>
        /// Event raised when the Source OnPropertyChanged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Only call OnPropertyChanged for exposed properties of Source</remarks>
        protected abstract void Source_OnPropertyChanged(Object? sender, PropertyChangedEventArgs e);

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(String? propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

    }


}

using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Subset of BindingList for Data Objects
    /// </summary>
    public interface IBindingData
    {
        /// <inheritdoc cref="IBindingList{T}.RaiseListChangedEvents"/>
        Boolean RaiseListChangedEvents { get; set; }

        /// <inheritdoc cref="IBindingList{T}.ResetBindings"/>
        void ResetBindings();
    }
}

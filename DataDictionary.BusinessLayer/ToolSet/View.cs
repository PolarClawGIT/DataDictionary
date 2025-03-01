// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.Resource;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Wrapper class that returns the BindingView(s) to support TValue
    /// Represents a top level list of values.
    /// </summary>
    public interface IView<TValue>
        where TValue : class, IBindingPropertyChanged, new()
    {
        /// <summary>
        /// The current value setup for DataBinding. Zero to one value expected.
        /// </summary>
        BindingView<TValue> Values { get; }

        /// <summary>
        /// Loads the current value from the Database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory);

        /// <summary>
        /// Saves the current value to the database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(IDatabaseWork factory);

        /// <summary>
        /// Removes the current value (does not delete it from the Database directly).
        /// </summary>
        void Remove();
    }

    /// <summary>
    /// Wrapper class that returns the BindingView(s) to support TValue.
    /// Represent a single value and any supporting values.
    /// </summary>
    public interface IView<TValue, TKey> : IView<TValue>
        where TValue : class, IBindingPropertyChanged, new()
        where TKey : IKey, new()
    {
        /// <summary>
        /// Index of the current Value
        /// </summary>
        TKey Index { get; }

        /// <inheritdoc cref="ITemporal.CreatedOn"/>
        TemporalIndex AsOfUtcDate { get; }

        /// <summary>
        /// The current value (not for Binding).
        /// </summary>
        TValue Value { get; }

        /// <summary>
        /// Loads the current value from the Database as of a specific date.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="asOfUtcDate"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate);
    }


}

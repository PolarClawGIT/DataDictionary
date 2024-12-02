using DataDictionary.DataLayer;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Item can be cast as a Modification Value
    /// </summary>
    public interface ITemporalValue : IDataValue, ITemporalItem, ITemporalIndex
    { }

    /// <summary>
    /// Implementation for an Item can be cast as a Modification Value
    /// </summary>
    class TemporalValue : DataValue, ITemporalValue
    {
        /// <inheritdoc/>
        public virtual String CreatedBy { get { return GetCreatedBy(); } }

        /// <inheritdoc/>
        public virtual DateTime? CreatedOn { get { return GetCreatedOn(); } }

        /// <inheritdoc/>
        public virtual String RemovedBy { get { return GetRemovedBy(); } }

        /// <inheritdoc/>
        public virtual DateTime? RemovedOn { get { return GetRemovedOn(); } }

        /// <inheritdoc/>
        public Boolean? IsInserted { get { return GetIsInserted(); } }

        /// <inheritdoc/>
        public Boolean? IsUpdated { get { return GetIsUpdated(); } }

        /// <inheritdoc/>
        public Boolean? IsDeleted { get { return GetIsDeleted(); } }

        /// <inheritdoc/>
        public Boolean? IsCurrent { get { return GetIsCurrent(); } }

        /// <inheritdoc/>
        public virtual DbModificationType Modification { get { return GetModification(); } }

        /// <summary>
        /// Function that returns the CreatedBy of the source.
        /// </summary>
        public required Func<String> GetCreatedBy { get; init; }

        /// <summary>
        /// Function that returns the CreatedOn of the source.
        /// </summary>
        public required Func<DateTime> GetCreatedOn { get; init; }

        /// <summary>
        /// Function that returns the RemovedBy of the source.
        /// </summary>
        public required Func<String> GetRemovedBy { get; init; }

        /// <summary>
        /// Function that returns the RemovedOn of the source.
        /// </summary>
        public required Func<DateTime> GetRemovedOn { get; init; }

        /// <summary>
        /// Function that returns the IsInserted of the source.
        /// </summary>
        public required Func<Boolean> GetIsInserted { get; init; }

        /// <summary>
        /// Function that returns the IsUpdated of the source.
        /// </summary>
        public required Func<Boolean> GetIsUpdated { get; init; }

        /// <summary>
        /// Function that returns the IsDeleted of the source.
        /// </summary>
        public required Func<Boolean> GetIsDeleted { get; init; }

        /// <summary>
        /// Function that returns the IsCurrent of the source.
        /// </summary>
        public required Func<Boolean> GetIsCurrent { get; init; }

        /// <summary>
        /// Function that returns the Modification of the source.
        /// </summary>
        public required Func<DbModificationType> GetModification { get; init; }

        /// <summary>
        /// Constructor for TemporalValue
        /// </summary>
        /// <param name="source"></param>
        public TemporalValue(IBindingPropertyChanged source) : base(source)
        { }

        /// <summary>
        /// Create Method that executes the constructor for TemporalValue with default behaviors for ITemporalValue
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="baseValue"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TemporalValue Create<TSource>(DataValue baseValue, TSource source)
            where TSource : IBindingPropertyChanged, ITemporalValue
        {
            return new TemporalValue(source)
            {
                GetIndex = baseValue.GetIndex,
                GetScope = baseValue.GetScope,
                GetTitle = baseValue.GetTitle,
                IsTitleChanged = baseValue.IsTitleChanged,
                GetIsCurrent = () => source.IsCurrent ?? false,
                GetIsDeleted = () => source.IsDeleted ?? false,
                GetIsInserted = () => source.IsInserted ?? false,
                GetIsUpdated = () => source.IsUpdated ?? false,
                GetModification = () => source.Modification,
                GetCreatedBy = () => source.CreatedBy ?? String.Empty,
                GetCreatedOn = () => source.CreatedOn ?? DateTime.MaxValue,
                GetRemovedBy = () => source.RemovedBy ?? String.Empty,
                GetRemovedOn = () => source.RemovedOn ?? DateTime.MaxValue,
            };
        }

        /// <inheritdoc/>
        public ITemporalValue AsModificationValue()
        { return this; }
    }
}

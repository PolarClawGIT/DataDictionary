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
        public virtual String ModifiedBy { get { return GetModifiedBy(); } }

        /// <inheritdoc/>
        public virtual DateTime? ModifiedOn { get { return GetModifiedOn(); } }

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
        /// Function that returns the ModifiedBy of the source.
        /// </summary>
        public required Func<String> GetModifiedBy { get; init; }

        /// <summary>
        /// Function that returns the ModifiedOn of the source.
        /// </summary>
        public required Func<DateTime> GetModifiedOn { get; init; }

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
                GetModifiedBy = () => source.ModifiedBy ?? String.Empty,
                GetModifiedOn = () => source.ModifiedOn ?? DateTime.MaxValue,
            };
        }

        /// <inheritdoc/>
        public ITemporalValue AsModificationValue()
        { return this; }
    }
}

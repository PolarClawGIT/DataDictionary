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
        /// Function that returns the Modification of the source.
        /// </summary>
        public required Func<DbModificationType> GetModification { get; init; }

        public TemporalValue(IBindingPropertyChanged source) : base(source)
        { }

        /// <inheritdoc/>
        public ITemporalValue AsModificationValue()
        { return this; }
    }
}

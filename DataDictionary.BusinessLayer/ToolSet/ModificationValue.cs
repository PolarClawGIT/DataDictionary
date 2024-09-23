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
    public interface IModificationValue : IDataValue, ITemporalItem
    {
        /// <inheritdoc/>
        String? ITemporalItem.ModifiedBy { get { return AsModificationValue().ModifiedBy; } }

        /// <inheritdoc/>
        DateTime? ITemporalItem.ModifiedOn { get { return AsModificationValue().ModifiedOn; } }

        /// <inheritdoc/>
        DbModificationType ITemporalItem.Modification { get { return AsModificationValue().Modification; } }

        /// <summary>
        /// Returns the value as the general Modification Value.
        /// </summary>
        /// <returns></returns>
        IModificationValue AsModificationValue();
    }

    /// <summary>
    /// Implementation for an Item can be cast as a Modification Value
    /// </summary>
    class ModificationValue : DataValue, IModificationValue
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
        public Func<String> GetModifiedBy { get; init; }

        /// <summary>
        /// Function that returns the ModifiedOn of the source.
        /// </summary>
        public Func<DateTime> GetModifiedOn { get; init; }

        /// <summary>
        /// Function that returns the Modification of the source.
        /// </summary>
        public Func<DbModificationType> GetModification { get; init; }

        public ModificationValue(IModificationValue source) : base(source)
        {
            GetModifiedBy = () => source.ModifiedBy ?? String.Empty;
            GetModifiedOn = () => source.ModifiedOn ?? DateTime.MaxValue;
            GetModification = () => source.Modification;
        }

        /// <inheritdoc/>
        public IModificationValue AsModificationValue()
        { return this; }
    }
}

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
    /// Item can be cast as a Generic Modification Value
    /// </summary>
    public interface IModificationValue : ITemporalItem, IDataValue, IBindingRowState
    {
        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return AsModificationValue().Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return AsModificationValue().Title; } }

        /// <summary>
        /// Returns the value as the generic Modification Value.
        /// </summary>
        /// <returns></returns>
        IModificationValue AsModificationValue();
    }

    /// <summary>
    /// Implementation for an Item can be cast as a Generic Modification Value
    /// </summary>
    class ModificationValue : DataValue, IModificationValue
    {
        /// <inheritdoc/>
        public String ModifiedBy { get { return GetModifiedBy(); } }

        /// <inheritdoc/>
        public DateTime? ModifiedOn { get { return GetModifiedOn(); } }

        /// <inheritdoc/>
        public DbModificationType Modification { get { return GetModification(); } }

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

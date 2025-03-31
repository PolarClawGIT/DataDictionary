// Ignore Spelling: Utc

using DataDictionary.DataLayer;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for Temporal Value
    /// </summary>
    public interface ITemporalValue : DataLayer.ITemporal, ITemporalKey, IDataValue
    { }

    /// <summary>
    /// The Temporal Value of a base ITemporal.
    /// </summary>
    public class TemporalValue : ITemporalValue
    {
        ITemporal sourceValue;

        /// <summary>
        /// Constructor for TemporalValue
        /// </summary>
        /// <param name="temporal"></param>
        internal TemporalValue(ITemporal temporal)
        {
            sourceValue = temporal;
            sourceValue.PropertyChanged += Value_PropertyChanged;

            void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, e); }
            }
        }

        /// <summary>
        /// Returns the source value as TValue, if possible.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public Boolean TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
            where TValue: class, IDataValue, ITemporal
        {
            if(sourceValue is TValue item)
            {   value= item; return true; }
            else { value = null; return false; }
        }

        /// <inheritdoc cref="IDataValue.Index"/>
        public DataIndex Index { get { return sourceValue.Index; } }

        /// <inheritdoc cref="IDataValue.Title"/>
        public String Title { get { return sourceValue.Title; } }

        /// <inheritdoc cref="ITemporalKey.AsOfUtcDate"/>
        public DateTime AsOfUtcDate { get { return sourceValue.Temporal.AsOfUtcDate; } }

        /// <inheritdoc cref="IScopeType.Scope"/>
        public ScopeType Scope { get { return sourceValue.Scope; } }

        /// <inheritdoc cref="DataLayer.ITemporal.CreatedOn"/>
        public DateTime? CreatedOn { get { return sourceValue.Temporal.CreatedOn; } }

        /// <inheritdoc cref="DataLayer.ITemporal.CreatedBy"/>
        public String? CreatedBy { get { return sourceValue.Temporal.CreatedBy; } }

        /// <inheritdoc cref="DataLayer.ITemporal.RemovedOn"/>
        public DateTime? RemovedOn { get { return sourceValue.Temporal.RemovedOn; } }

        /// <inheritdoc cref="DataLayer.ITemporal.RemovedBy"/>
        public String? RemovedBy { get { return sourceValue.Temporal.RemovedBy; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsInserted"/>
        public Boolean? IsInserted { get { return sourceValue.Temporal.IsInserted; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsUpdated"/>
        public Boolean? IsUpdated { get { return sourceValue.Temporal.IsUpdated; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsDeleted"/>
        public Boolean? IsDeleted { get { return sourceValue.Temporal.IsDeleted; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsCurrent"/>
        public Boolean? IsCurrent { get { return sourceValue.Temporal.IsCurrent; } }

        /// <inheritdoc cref="DataLayer.ITemporal.Modification"/>
        public DbModificationType Modification { get { return sourceValue.Temporal.Modification; } }

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

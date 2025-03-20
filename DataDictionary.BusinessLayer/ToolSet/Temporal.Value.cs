// Ignore Spelling: Utc

using DataDictionary.DataLayer;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
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
    /// Interface for Temporal Value
    /// </summary>
    public interface ITemporalValue : DataLayer.ITemporal, ITemporalKey, IDataValue
    { }

    /// <summary>
    /// The Temporal Value of a base ITemporal.
    /// </summary>
    public class TemporalValue : ITemporalValue
    {
        ITemporal value;

        /// <summary>
        /// Constructor for TemporalValue
        /// </summary>
        /// <param name="temporal"></param>
        internal TemporalValue(ITemporal temporal)
        {
            value = temporal;
            value.PropertyChanged += Value_PropertyChanged;

            void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, e); }
            }
        }


        /// <inheritdoc cref="IDataValue.Index"/>
        public DataIndex Index { get { return value.Index; } }

        /// <inheritdoc cref="IDataValue.Title"/>
        public String Title { get { return value.Title; } }

        /// <inheritdoc cref="ITemporalKey.AsOfUtcDate"/>
        public DateTime AsOfUtcDate { get { return value.Temporal.AsOfUtcDate; } }

        /// <inheritdoc cref="IScopeType.Scope"/>
        public ScopeType Scope { get { return value.Scope; } }

        /// <inheritdoc cref="DataLayer.ITemporal.CreatedOn"/>
        public DateTime? CreatedOn { get { return value.Temporal.CreatedOn; } }

        /// <inheritdoc cref="DataLayer.ITemporal.CreatedBy"/>
        public String? CreatedBy { get { return value.Temporal.CreatedBy; } }

        /// <inheritdoc cref="DataLayer.ITemporal.RemovedOn"/>
        public DateTime? RemovedOn { get { return value.Temporal.RemovedOn; } }

        /// <inheritdoc cref="DataLayer.ITemporal.RemovedBy"/>
        public String? RemovedBy { get { return value.Temporal.RemovedBy; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsInserted"/>
        public Boolean? IsInserted { get { return value.Temporal.IsInserted; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsUpdated"/>
        public Boolean? IsUpdated { get { return value.Temporal.IsUpdated; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsDeleted"/>
        public Boolean? IsDeleted { get { return value.Temporal.IsDeleted; } }

        /// <inheritdoc cref="DataLayer.ITemporal.IsCurrent"/>
        public Boolean? IsCurrent { get { return value.Temporal.IsCurrent; } }

        /// <inheritdoc cref="DataLayer.ITemporal.Modification"/>
        public DbModificationType Modification { get { return value.Temporal.Modification; } }

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

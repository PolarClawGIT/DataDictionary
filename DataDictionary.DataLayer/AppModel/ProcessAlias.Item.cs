using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Process Alias Items
    /// </summary>
    public interface IProcessAliasItem : IProcessKey, IAliasKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Process Alias Items
    /// </summary>
    [Serializable]
    public class ProcessAliasItem : BindingTableRow, IProcessAliasItem
    {
        /// <inheritdoc/>
        public Guid? ProcessId
        { get { return GetValue<Guid>(nameof(ProcessId)); } protected set { SetValue(nameof(ProcessId), value); } }

        /// <inheritdoc/>
        public String? AliasPath { get { return GetValue(nameof(AliasPath)); } set { SetValue(nameof(AliasPath), value); } }

        /// <inheritdoc/>
        public ScopeType AliasScope
        {
            get
            {
                String value = GetValue(nameof(AliasScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(AliasScope), null); }
                else { SetValue(nameof(AliasScope), ScopeEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ProcessId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AliasScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AliasPath), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Process Alias Items
        /// </summary>
        public ProcessAliasItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Process Alias Items
        /// </summary>
        /// <param name="key"></param>
        public ProcessAliasItem(IProcessKey key) : this()
        { ProcessId = key.ProcessId; }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Process Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ProcessAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AliasPath is String) { return AliasPath; }
            else { return String.Empty; }
        }

    }
}

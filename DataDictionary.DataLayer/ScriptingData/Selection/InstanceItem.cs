using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Scripting Selection Instance data.
    /// </summary>
    public interface IInstanceItem :ISelectionKey, IInstanceKey, IInstanceKeyName, IScopeKey
    {

    }

    /// <summary>
    /// Implementation for the Scripting Selection Instance data.
    /// </summary>
    [Serializable]
    public class InstanceItem : BindingTableRow, IInstanceItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? InstanceId
        {
            get { return GetValue<Guid>("InstanceId"); }
            protected set { SetValue("InstanceId", value); }
        }

        /// <inheritdoc/>
        public Guid? SelectionId
        {
            get { return GetValue<Guid>("SelectionId"); }
            protected set { SetValue("SelectionId", value); }
        }

        /// <inheritdoc/>
        public String? InstanceName => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(GetValue("ScopeName") ?? String.Empty); }
            set { SetValue("ScopeName", value.ToName()); OnPropertyChanged(nameof(Scope)); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("InstanceId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SelectionId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(String)){ AllowDBNull = false},
            new DataColumn("InstanceName", typeof(String)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        /// <inheritdoc/>
        public override string ToString()
        { return InstanceName??String.Empty; }
    }
}

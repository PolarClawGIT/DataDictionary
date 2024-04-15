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
    /// Interface for the Scripting Selection Path data.
    /// </summary>
    public interface ISelectionPathItem :ISelectionKey, ISelectionPathKey, ISelectionPathKeyName, IScopeKey
    {

    }

    /// <summary>
    /// Implementation for the Scripting Selection Path data.
    /// </summary>
    [Serializable]
    public class SelectionPathItem : BindingTableRow, ISelectionPathItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SelectionPathId
        {
            get { return GetValue<Guid>("SelectionPathId"); }
            protected set { SetValue("SelectionPathId", value); }
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
            new DataColumn("SelectionPathId", typeof(Guid)){ AllowDBNull = false},
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

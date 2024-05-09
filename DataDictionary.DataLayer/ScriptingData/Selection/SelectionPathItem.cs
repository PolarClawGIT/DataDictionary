using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
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
            get { return GetValue<Guid>(nameof(SelectionPathId)); }
            protected set { SetValue(nameof(SelectionPathId), value); }
        }

        /// <inheritdoc/>
        public Guid? SelectionId
        {
            get { return GetValue<Guid>(nameof(SelectionId)); }
            protected set { SetValue(nameof(SelectionId), value); }
        }

        /// <inheritdoc/>
        public String? InstanceName => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(ScopeName ?? String.Empty).Scope; }
            set { ScopeName = value.ToName(); OnPropertyChanged(nameof(Scope)); }
        }

        /// <inheritdoc cref="IScopeKey.Scope"/>
        protected String? ScopeName { get { return GetValue(nameof(ScopeName)); } set { SetValue(nameof(ScopeName), value); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(SelectionPathId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SelectionId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ScopeName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(InstanceName), typeof(String)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        /// <inheritdoc/>
        public override string ToString()
        { return InstanceName??String.Empty; }
    }
}

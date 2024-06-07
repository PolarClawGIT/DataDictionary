using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Scripting Selection Path data.
    /// </summary>
    [Obsolete("To be removed", true)]
    public interface ISelectionPathItem : ISelectionKey, IScopeKey
    {
        /// <summary>
        /// The NameSpace Path for the Selected item.
        /// </summary>
        String? SelectionPath { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Selection Path data.
    /// </summary>
    [Serializable]
    [Obsolete("To be removed", true)]
    public class SelectionPathItem : BindingTableRow, ISelectionPathItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SelectionId
        {
            get { return GetValue<Guid>(nameof(SelectionId)); }
            protected set { SetValue(nameof(SelectionId), value); }
        }

        /// <inheritdoc/>
        public String? SelectionPath
        {
            get { return GetValue(nameof(SelectionPath)); }
            set { SetValue(nameof(SelectionPath), value); }
        }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(ScopeName ?? String.Empty).Scope; }
            set { ScopeName = value.ToName(); OnPropertyChanged(nameof(Scope)); }
        }

        /// <inheritdoc cref="IScopeKey.Scope"/>
        protected String? ScopeName { get { return GetValue(nameof(ScopeName)); } set { SetValue(nameof(ScopeName), value); } }

        /// <summary>
        /// Constructor for Scripting Selection Path
        /// </summary>
        public SelectionPathItem() : base() { }

        /// <summary>
        /// Constructor for Scripting Selection Path
        /// </summary>
        /// <param name="key"></param>
        public SelectionPathItem(ISelectionKey key) : this()
        { SelectionId = key.SelectionId; }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(SelectionId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ScopeName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SelectionPath), typeof(String)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        /// <inheritdoc/>
        public override string ToString()
        { return SelectionPath ?? String.Empty; }
    }
}

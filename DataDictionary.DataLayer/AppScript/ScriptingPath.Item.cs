using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template Path data.
    /// </summary>
    [Obsolete("replace", true)]
    public interface IScriptingPathItem : 
        IScriptingTemplateKey, IScriptingPathKeyName,
        ITemporalItem
    {
        /// <summary>
        /// Application Scope of the item to Script.
        /// </summary>
        ScopeType NameSpaceScope { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Path data.
    /// </summary>
    [Serializable]
    [Obsolete("replace", true)]
    public class ScriptingPathItem : BindingTableRow, IScriptingPathItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public String? NameSpace
        {
            get { return GetValue(nameof(NameSpace)); }
            set { SetValue(nameof(NameSpace), value); }
        }

        /// <inheritdoc/>
        public ScopeType NameSpaceScope
        {
            get
            {
                String value = GetValue(nameof(NameSpaceScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set { SetValue(nameof(NameSpaceScope), ScopeEnumeration.Cast(value).Name); }
        }

        /// <inheritdoc/>
        //public ScopeType Scope { get; } = ScopeType.ScriptingTemplatePath;

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Template Path
        /// </summary>
        protected ScriptingPathItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Template Path
        /// </summary>
        public ScriptingPathItem(IScriptingTemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = 
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NameSpace), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NameSpaceScope), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ScriptingPathItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return NameSpace ?? String.Empty; }
    }
}

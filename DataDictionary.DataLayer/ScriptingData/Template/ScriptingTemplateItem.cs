using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Scripting Template data.
    /// </summary>
    public interface IScriptingTemplateItem : IScriptingTemplateKey, IScriptingTemplateKeyName, IScriptAsType, IScopeKey
    {
        /// <summary>
        /// Description for the Template
        /// </summary>
        String? TemplateDescription { get; }

        /// <summary>
        /// The XSL Script used to perform the Transform
        /// </summary>
        String? TransformScript { get; }

        /// <summary>
        /// The Scope to do a Document Break on.
        /// </summary>
        ScopeType BreakOnScope { get; }

        /// <summary>
        /// Root Directory to place documents in (must be a Special Folder).
        /// </summary>
        Environment.SpecialFolder? RootDirectory { get; }

        /// <summary>
        /// From the RootDirectory, the location where the XML Document(s) are to be placed.  
        /// </summary>
        String? DocumentDirectory { get; }

        /// <summary>
        /// Prefix to the XML Document. Used with the ElementName within BreakOnScope.
        /// </summary>
        String? DocumentPrefix { get; }

        /// <summary>
        /// Suffix to the XML Document. Used with the ElementName within BreakOnScope.
        /// </summary>
        String? DocumentSuffix { get; }

        /// <summary>
        /// Extension to the XML Document. Used with the ElementName within BreakOnScope.
        /// </summary>
        String? DocumentExtension { get; }

        /// <summary>
        /// From the RootDirectory, the location where the Script Document(s) are to be placed
        /// </summary>
        String? ScriptDirectory { get; }

        /// <summary>
        /// Prefix to the Script Document. Used with the ElementName within BreakOnScope.
        /// </summary>
        String? ScriptPrefix { get; }

        /// <summary>
        /// Suffix to the Script Document. Used with the ElementName within BreakOnScope.
        /// </summary>
        String? ScriptSuffix { get; }

        /// <summary>
        /// Extension to the Script Document. Used with the ElementName within BreakOnScope.
        /// </summary>
        String? ScriptExtension { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template data.
    /// </summary>
    [Serializable]
    public class ScriptingTemplateItem : BindingTableRow, IScriptingTemplateItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public String? TemplateTitle
        {
            get { return GetValue(nameof(TemplateTitle)); }
            set { SetValue(nameof(TemplateTitle), value); }
        }

        /// <inheritdoc/>
        public String? TemplateDescription
        {
            get { return GetValue(nameof(TemplateDescription)); }
            set { SetValue(nameof(TemplateDescription), value); }
        }

        /// <inheritdoc/>
        public ScopeType BreakOnScope
        {
            get { return ScopeKey.Parse(GetValue(nameof(BreakOnScope)) ?? String.Empty).Scope; }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(BreakOnScope), null); }
                else { SetValue(nameof(BreakOnScope), value.ToName()); }
            }
        }

        /// <inheritdoc/>
        public String? TransformScript
        {
            get { return GetValue(nameof(TransformScript)); }
            set { SetValue(nameof(TransformScript), value); }
        }

        /// <inheritdoc/>
        public Environment.SpecialFolder? RootDirectory
        {
            get
            {
                String? value = GetValue(nameof(RootDirectory));
                if (Enum.TryParse(value, true, out Environment.SpecialFolder folder))
                { return folder; }
                else { return null; }
            }
            set
            {
                if (value is null) { SetValue(nameof(RootDirectory), null); }
                else { SetValue(nameof(RootDirectory), value.ToString()); }
            }
        }

        /// <inheritdoc/>
        public String? DocumentDirectory
        {
            get { return GetValue(nameof(DocumentDirectory)); }
            set { SetValue(nameof(DocumentDirectory), value); }
        }

        /// <inheritdoc/>
        public String? DocumentPrefix
        {
            get { return GetValue(nameof(DocumentPrefix)); }
            set { SetValue(nameof(DocumentPrefix), value); }
        }

        /// <inheritdoc/>
        public String? DocumentSuffix
        {
            get { return GetValue(nameof(DocumentSuffix)); }
            set { SetValue(nameof(DocumentSuffix), value); }
        }

        /// <inheritdoc/>
        public String? DocumentExtension
        {
            get { return GetValue(nameof(DocumentExtension)); }
            set { SetValue(nameof(DocumentExtension), value); }
        }

        /// <inheritdoc/>
        public ScriptAsType ScriptAs
        {
            get { return ScriptAsTypeKey.Parse(GetValue(nameof(ScriptAs)) ?? String.Empty).ScriptAs; }
            set { SetValue(nameof(ScriptAs), new ScriptAsTypeKey(value).ToString()); }
        }

        /// <inheritdoc/>
        public String? ScriptDirectory
        {
            get { return GetValue(nameof(ScriptDirectory)); }
            set { SetValue(nameof(ScriptDirectory), value); }
        }

        /// <inheritdoc/>
        public String? ScriptPrefix
        {
            get { return GetValue(nameof(ScriptPrefix)); }
            set { SetValue(nameof(ScriptPrefix), value); }
        }

        /// <inheritdoc/>
        public String? ScriptSuffix
        {
            get { return GetValue(nameof(ScriptSuffix)); }
            set { SetValue(nameof(ScriptSuffix), value); }
        }

        /// <inheritdoc/>
        public String? ScriptExtension
        {
            get { return GetValue(nameof(ScriptExtension)); }
            set { SetValue(nameof(ScriptExtension), value); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ScriptingTemplate;

        /// <summary>
        /// Constructor for Scripting Transform
        /// </summary>
        public ScriptingTemplateItem() : base()
        {
            if (TemplateId is null) { TemplateId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(TemplateTitle)) { TemplateTitle = "(new Template)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(BreakOnScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(TransformScript), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RootDirectory), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DocumentDirectory), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DocumentPrefix), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DocumentSuffix), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DocumentExtension), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ScriptAs), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ScriptDirectory), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ScriptPrefix), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ScriptSuffix), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ScriptExtension), typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ScriptingTemplateItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return TemplateTitle ?? String.Empty; }
    }
}

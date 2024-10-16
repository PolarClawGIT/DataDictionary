﻿using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData
{
    /// <summary>
    /// Interface for the Scripting Template data.
    /// </summary>
    public interface IScriptingTemplateItem : IScriptingTemplateKey, IScriptingTemplateKeyName, IScriptAsType, ITemplateDirectory, IScopeType
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
            get
            {
                String value = GetValue(nameof(BreakOnScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(BreakOnScope), null); }
                else { SetValue(nameof(BreakOnScope), ScopeEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public String? TransformScript
        {
            get { return GetValue(nameof(TransformScript)); }
            set { SetValue(nameof(TransformScript), value); }
        }

        /// <inheritdoc/>
        public TemplateDirectoryType RootDirectory
        {
            get
            {
                String? value = GetValue(nameof(RootDirectory));
                if (TemplateDirectoryEnumeration.TryParse(value, null, out TemplateDirectoryEnumeration? result))
                { return result.Value; }
                else { return TemplateDirectoryType.Null; }
            }
            set
            {
                if (value is TemplateDirectoryType.Null)
                { SetValue(nameof(RootDirectory), null); }
                else { SetValue(nameof(RootDirectory), TemplateDirectoryEnumeration.Cast(value).Name); }
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
        public TemplateScriptAsType ScriptAs
        {
            get
            {
                String? value = GetValue(nameof(ScriptAs));
                if (TemplateScriptAsEnumeration.TryParse(value, null, out TemplateScriptAsEnumeration? result))
                { return result.Value; }
                else { return TemplateScriptAsType.none; }
            }
            set
            {
                if (value is TemplateScriptAsType.none)
                { SetValue(nameof(ScriptAs), null); }
                else { SetValue(nameof(ScriptAs), TemplateScriptAsEnumeration.Cast(value).Name); }
            }
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
            if (String.IsNullOrWhiteSpace(DocumentExtension)) { DocumentExtension = "xml"; }
            if (RootDirectory is TemplateDirectoryType.Null) { RootDirectory = TemplateDirectoryType.MySources; }
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

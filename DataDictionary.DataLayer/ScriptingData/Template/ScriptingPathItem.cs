﻿using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Scripting Template Path data.
    /// </summary>
    public interface IScriptingPathItem : IScriptingTemplateKey, IScriptingPathKeyName, IScopeKey
    {
        /// <summary>
        /// Application Scope of the item to Script.
        /// </summary>
        ScopeType PathScope { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Path data.
    /// </summary>
    [Serializable]
    public class ScriptingPathItem : BindingTableRow, IScriptingPathItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public String? PathName
        {
            get { return GetValue(nameof(PathName)); }
            set { SetValue(nameof(PathName), value); }
        }

        /// <inheritdoc/>
        public ScopeType PathScope
        {
            get { return ScopeKey.Parse(GetValue(nameof(PathScope)) ?? String.Empty).Scope; }
            set { SetValue(nameof(PathScope), new ScopeKey(value).ToString()); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ScriptingTemplatePath;

        /// <summary>
        /// Constructor for Scripting Template Path
        /// </summary>
        protected ScriptingPathItem() : base()
        { }

        /// <summary>
        /// Constructor for Scripting Template Path
        /// </summary>
        public ScriptingPathItem(IScriptingTemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(PathName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(PathScope), typeof(String)){ AllowDBNull = true},
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
        protected ScriptingPathItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return PathName ?? String.Empty; }
    }
}

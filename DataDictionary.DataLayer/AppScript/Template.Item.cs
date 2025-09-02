using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template data.
    /// </summary>
    public interface ITemplateItem : ITemplateKey, ITemplate,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Scripting Template data.
    /// </summary>
    [Serializable]
    public class TemplateItem : BindingTableRow, ITemplateItem, ISerializable
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
        public String? BreakOnScope
        {
            get { return GetValue(nameof(BreakOnScope)); }
            set { SetValue(nameof(BreakOnScope), value); }
        }

        /// <inheritdoc/>
        public String? TransformScript
        {
            get { return GetValue(nameof(TransformScript)); }
            set { SetValue(nameof(TransformScript), value); }
        }

        /// <inheritdoc/>
        public String? RootDirectory
        {
            get { return GetValue(nameof(RootDirectory)); }
            set { SetValue(nameof(RootDirectory), value); }
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
        public String? ScriptAs
        {
            get { return GetValue(nameof(ScriptAs)); }
            set { SetValue(nameof(ScriptAs), value); }
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
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Data Source
        /// </summary>
        public TemplateItem() : base()
        {
            if (TemplateId is null) { TemplateId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(TemplateTitle)) { TemplateTitle = "(new Template)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
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
        protected TemplateItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return TemplateTitle ?? String.Empty; }
    }
}

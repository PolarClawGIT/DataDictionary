﻿using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Transform
{
    /// <summary>
    /// Interface for the Scripting Transform data.
    /// </summary>
    public interface ITransformItem : ITransformKey, ITransformKeyName, IScopeKey
    {
        /// <summary>
        /// Description of the Transform. How the Transform is used.
        /// </summary>
        String? TransformDescription { get; }

        /// <summary>
        /// Is the result to be returned as Text (not XML)
        /// </summary>
        Boolean AsText { get; }

        /// <summary>
        /// Is the result to be returned as XML (not Text)
        /// </summary>
        Boolean AsXml { get; }

        /// <summary>
        /// Raw XSLT Transform Script (linked to TransformDocument)
        /// </summary>
        String? TransformScript { get; }

        /// <summary>
        /// The XSLT Transform Document (linked to TransformScript)
        /// </summary>
        XDocument? TransformDocument { get; }

        /// <summary>
        /// Exception was generated when parsing the TransformScript.
        /// </summary>
        Exception? TransformException { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Transform data.
    /// </summary>
    public class TransformItem : BindingTableRow, ITransformItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)); }
            protected set { SetValue(nameof(TransformId), value); }
        }

        /// <inheritdoc/>
        public String? TransformTitle { get { return GetValue(nameof(TransformTitle)); } set { SetValue(nameof(TransformTitle), value); } }

        /// <inheritdoc/>
        public String? TransformDescription { get { return GetValue(nameof(TransformDescription)); } set { SetValue(nameof(TransformDescription), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTransform; } }

        /// <inheritdoc/>
        public bool AsText
        {
            get
            {
                if (GetValue<bool>(nameof(AsText), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(AsText), value);
                if (value == true) { SetValue<Boolean>(nameof(AsXml), !value); }
            }
        }

        /// <inheritdoc/>
        public bool AsXml
        {
            get
            {
                if (GetValue<bool>(nameof(AsXml), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(AsXml), value);
                if (value == true) { SetValue<Boolean>(nameof(AsText), !value); }
            }
        }

        /// <inheritdoc/>
        public String? TransformScript
        {
            get
            {
                String? value = GetValue(nameof(TransformScript));

                if (transformValue is null && !String.IsNullOrWhiteSpace(value))
                {
                    TransformDocument = XDocument.Parse(value);
                    if (TransformException is null)
                    { value = TransformDocument.ToString(); }
                }

                return value;
            }
            set
            {
                SetValue(nameof(TransformScript), value);
                TransformException = null;

                try
                {
                    if (!String.IsNullOrWhiteSpace(value))
                    { TransformDocument = XDocument.Parse(value, LoadOptions.PreserveWhitespace); }
                }
                catch (Exception ex)
                {
                    ex.Data.Add(nameof(TransformScript), value);
                    TransformException = ex;
                    base.OnPropertyChanged(nameof(TransformException));
                }
            }
        }

        /// <inheritdoc/>
        public XDocument? TransformDocument
        {
            get { return transformValue; }
            set
            {
                transformValue = value;
                TransformException = null;
                String? data;
                if (value is null) { data = null; }
                else
                {
                    if (value.Declaration is XDeclaration)
                    { // Remove the encoding.
                        value.Declaration = null;
                        TransformException = new ArgumentException("Encoding Removed (using UTF-16)");
                    }
                    data = value.ToString();
                }

                SetValue(nameof(TransformScript), data);
                base.OnPropertyChanged(nameof(TransformDocument));
            }
        }
        private XDocument? transformValue = null;


        /// <inheritdoc/>
        public Exception? TransformException { get; protected set; }

        /// <summary>
        /// Constructor for Scripting Transform
        /// </summary>
        public TransformItem() : base()
        {
            if (TransformId is null) { TransformId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(TransformTitle)) { TransformTitle = "(new Transform)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(TransformId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TransformTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TransformDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AsText), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(AsXml), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(TransformScript), typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Table Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TransformItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return TransformTitle ?? String.Empty; }
    }
}

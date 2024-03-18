using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Transform
{
    /// <summary>
    /// Interface for the Scripting Transform data.
    /// </summary>
    public interface ITransformItem : ITransformKey, IDataItem, IScopeKey
    {
        /// <summary>
        /// Title of the Transform.
        /// </summary>
        String? TransformTitle { get; }

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
            get { return GetValue<Guid>("TransformId"); }
            protected set { SetValue("TransformId", value); }
        }

        /// <inheritdoc/>
        public String? TransformTitle { get { return GetValue("TransformTitle"); } set { SetValue("TransformTitle", value); } }

        /// <inheritdoc/>
        public String? TransformDescription { get { return GetValue("TransformDescription"); } set { SetValue("TransformDescription", value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTransform; } }

        /// <inheritdoc/>
        public bool AsText
        {
            get
            {
                if (GetValue<bool>("AsText", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("AsText", value);
                if (value == true) { SetValue<Boolean>("AsXml", !value); }
            }
        }

        /// <inheritdoc/>
        public bool AsXml
        {
            get
            {
                if (GetValue<bool>("AsXml", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("AsXml", value);
                if (value == true) { SetValue<Boolean>("AsText", !value); }
            }
        }

        /// <inheritdoc/>
        public String? TransformScript
        {
            get { return GetValue("TransformScript"); }
            set
            {
                SetValue("TransformScript", value);
                TransformException = null;
                transformValue = null;

                try
                {
                    if (!String.IsNullOrWhiteSpace(value))
                    { transformValue = XDocument.Parse(value); }

                }
                catch (Exception ex)
                {
                    ex.Data.Add(nameof(TransformScript), value);
                    TransformException = ex;
                    base.OnPropertyChanged(nameof(TransformException));
                }

                base.OnPropertyChanged(nameof(TransformDocument));
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
                else { data = value.ToString(); }

                SetValue("TransformScript", data);
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
            new DataColumn("TransformId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("TransformTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("TransformDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("AsText", typeof(bool)){ AllowDBNull = true},
            new DataColumn("AsXml", typeof(bool)){ AllowDBNull = true},
            new DataColumn("TransformScript", typeof(string)){ AllowDBNull = true},
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
        { return TransformTitle??String.Empty; }
    }
}

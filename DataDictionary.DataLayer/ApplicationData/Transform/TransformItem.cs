using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ApplicationData.Transform
{
    /// <summary>
    /// Interface for the Transform data.
    /// </summary>
    [Obsolete("To be replaced by Scripting Objects")]
    public interface ITransformItem : ITransformKey, IScopeKeyName, IDataItem
    {
        /// <summary>
        /// Title of the Transform.
        /// </summary>
        String? TransformTitle { get; set; }

        /// <summary>
        /// Description of the Transform. How the Transform is used.
        /// </summary>
        String? TransformDescription { get; set; }

        /// <summary>
        /// Is the result to be returned as Text (not XML)
        /// </summary>
        Boolean AsText { get; set; }

        /// <summary>
        /// Is the result to be returned as XML (not Text)
        /// </summary>
        Boolean AsXml { get; set; }

        /// <summary>
        /// Raw Transform Script (linked to TransformScript)
        /// </summary>
        String? TransformSource { get; set; }

        /// <summary>
        /// The XSLT Transform Script (linked to TransformSource)
        /// </summary>
        XDocument? TransformScript { get; set; }

        /// <summary>
        /// Exception was generated when parsing the TransformScript.
        /// </summary>
        Exception? TransformException { get; }
    }

    /// <summary>
    /// Implementation of the Transform data.
    /// </summary>
    [Serializable]
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
        public String? ScopeName { get { return GetValue("ScopeName"); } set { SetValue("ScopeName", value); } }

        /// <inheritdoc/>
        public Boolean AsText
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
        public Boolean AsXml
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
        public String? TransformSource
        {
            get { return GetValue("TransformScript"); }
            set
            {
                GetValue("TransformScript");
                base.OnPropertyChanged(nameof(TransformScript));
            }
        }

        /// <inheritdoc/>
        public XDocument? TransformScript
        {
            get
            {
                TransformException = null;
                String? value = GetValue("TransformScript");

                try
                {
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    else { return XDocument.Parse(value); }
                }
                catch (Exception ex)
                {
                    ex.Data.Add(nameof(TransformScript), value);
                    TransformException = ex;
                    base.OnPropertyChanged(nameof(TransformException));
                    return null;
                }

            }
            set
            {
                String? data;
                if (value is null) { data = null; }
                else { data = value.ToString(); }

                SetValue("TransformScript", data);
                base.OnPropertyChanged(nameof(TransformSource));
            }
        }

        /// <inheritdoc/>
        public Exception? TransformException { get; protected set; }

        /// <summary>
        /// Constructor for Domain Attribute Item
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
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = true},
            new DataColumn("AsText", typeof(bool)){ AllowDBNull = true},
            new DataColumn("AsXml", typeof(bool)){ AllowDBNull = true},
            new DataColumn("TransformScript", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}

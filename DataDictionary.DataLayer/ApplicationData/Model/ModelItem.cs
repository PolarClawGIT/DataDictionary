using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Model
{
    /// <summary>
    /// Interface for the Model.
    /// </summary>
    public interface IModelItem : IModelKey, IObsolete, IDataItem
    {
        /// <summary>
        /// Title for the Model.
        /// </summary>
        string? ModelTitle { get; set; }

        /// <summary>
        /// Description for the Model.
        /// </summary>
        string? ModelDescription { get; set; }
    }

    /// <summary>
    /// Implementation of the Model data.
    /// </summary>
    [Serializable]
    public class ModelItem : BindingTableRow, IModelItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? ModelId { get { return GetValue<Guid>("ModelId"); } protected set { SetValue("ModelId", value); } }

        /// <inheritdoc/>
        public string? ModelTitle { get { return GetValue("ModelTitle"); } set { SetValue("ModelTitle", value); } }

        /// <inheritdoc/>
        public string? ModelDescription { get { return GetValue("ModelDescription"); } set { SetValue("ModelDescription", value); } }

        /// <inheritdoc/>
        public bool? Obsolete { get { return GetValue<bool>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue("Obsolete", value); } }

        /// <summary>
        /// Constructor for the Model data
        /// </summary>
        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
            Obsolete = false;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("ModelId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ModelTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("ModelDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(bool)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public Boolean Validate()
        {
            Boolean result = false;

            if (String.IsNullOrWhiteSpace(this.ModelTitle))
            { SetRowError("[ModelTitle] cannot be empty"); }
            else if (this.ModelId == Guid.Empty)
            { SetRowError("[ModelId] cannot be empty"); }
            else { result = true; }

            return result;
        }

        #region ISerializable
        /// <summary>
        /// Serialization constructor for Model.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ModelItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ApplicationData.Model.SubjectArea
{
    /// <summary>
    /// Interface for Model Subject Area Item
    /// </summary>
    public interface IModelSubjectAreaItem : IModelSubjectAreaKey, IDataItem
    {

        /// <summary>
        /// Parent of the Subject Area
        /// </summary>
        Guid? SubjectAreaParentId { get; }

        /// <summary>
        /// Title of the Subject Area
        /// </summary>
        String? SubjectAreaTitle { get; }

        /// <summary>
        /// Description of the Subject Area
        /// </summary>
        String? SubjectAreaDescription { get; }
    }

    /// <summary>
    /// Implementation for Model Subject Area Item
    /// </summary>
    [Serializable]
    public class ModelSubjectAreaItem : BindingTableRow, IModelSubjectAreaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get { return GetValue<Guid>("SubjectAreaId"); } protected set { SetValue("SubjectAreaId", value); } }

        /// <inheritdoc/>
        public Guid? SubjectAreaParentId { get { return GetValue<Guid>("SubjectAreaParentId"); } set { SetValue("SubjectAreaParentId", value); } }

        /// <inheritdoc/>
        public String? SubjectAreaTitle { get { return GetValue("SubjectAreaTitle"); } set { SetValue("SubjectAreaTitle", value); } }

        /// <inheritdoc/>
        public String? SubjectAreaDescription { get { return GetValue("SubjectAreaDescription"); } set { SetValue("SubjectAreaDescription", value); } }

        /// <summary>
        /// Constructor for Domain Attribute Item
        /// </summary>
        public ModelSubjectAreaItem() : base()
        {
            if (SubjectAreaId is null) { SubjectAreaId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(SubjectAreaTitle)) { SubjectAreaTitle = "(new Subject Area)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("SubjectAreaId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SubjectAreaParentId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("SubjectAreaTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("SubjectAreaDescription", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ModelSubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (SubjectAreaTitle is not null) { return SubjectAreaTitle; } else { return string.Empty; } }
    }
}

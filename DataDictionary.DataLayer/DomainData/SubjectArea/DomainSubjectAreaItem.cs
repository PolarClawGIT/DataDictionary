using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.SubjectArea
{
    /// <summary>
    /// Interface for Domain Subject Are Item
    /// </summary>
    public interface IDomainSubjectAreaItem: IDomainSubjectAreaKey, IDataItem
    {
        /// <summary>
        /// Title of the Domain Subject Area (aka Name of the Subject Area)
        /// </summary>
        String? SubjectAreaTitle { get; }

        /// <summary>
        /// Description of the Domain Subject Area
        /// </summary>
        String? SubjectAreaDescription { get; set; }
    }

    /// <summary>
    /// Implementation for Domain Subject Area Item
    /// </summary>
    [Serializable]
    public class DomainSubjectAreaItem : BindingTableRow, IDomainSubjectAreaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId
        { get { return GetValue<Guid>("SubjectAreaId"); } protected set { SetValue("SubjectAreaId", value); } }

        /// <inheritdoc/>
        public string? SubjectAreaTitle { get { return GetValue("SubjectAreaTitle"); } set { SetValue("SubjectAreaTitle", value); } }

        /// <inheritdoc/>
        public string? SubjectAreaDescription { get { return GetValue("SubjectAreaDescription"); } set { SetValue("SubjectAreaDescription", value); } }

        /// <summary>
        /// Constructor for Domain SubjectArea Item
        /// </summary>
        public DomainSubjectAreaItem() : base()
        {
            if (SubjectAreaId is null) { SubjectAreaId = Guid.NewGuid(); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("SubjectAreaId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("SubjectAreaTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("SubjectAreaDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain SubjectArea Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainSubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (SubjectAreaTitle is not null) { return SubjectAreaTitle; } else { return string.Empty; } }
    }
}

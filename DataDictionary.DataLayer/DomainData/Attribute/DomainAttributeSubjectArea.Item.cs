using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Subject Area Item
    /// </summary>
    public interface IDomainAttributeSubjectAreaItem : IDomainAttributeKey, IModelSubjectAreaKey
    { }

    /// <summary>
    /// Implementation for Domain Attribute Subject Area Item
    /// </summary>
    public class DomainAttributeSubjectAreaItem : BindingTableRow, IDomainAttributeSubjectAreaItem, ISerializable
    {

        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get { return GetValue<Guid>(nameof(AttributeId)); }
            protected set { SetValue(nameof(AttributeId), value); }
        }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        {
            get { return GetValue<Guid>(nameof(SubjectAreaId)); }
            protected set { SetValue(nameof(SubjectAreaId), value); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for Domain Attribute Subject Area Items
        /// </summary>
        public DomainAttributeSubjectAreaItem() : base() { }

        /// <summary>
        /// Constructor for Domain Attribute Subject Area Items
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="subject"></param>
        public DomainAttributeSubjectAreaItem(IDomainAttributeKey attribute, IModelSubjectAreaKey subject) : this()
        {
            AttributeId = attribute.AttributeId;
            SubjectAreaId = subject.SubjectAreaId;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAttributeSubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}

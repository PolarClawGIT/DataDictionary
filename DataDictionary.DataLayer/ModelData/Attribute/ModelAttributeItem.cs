using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ModelData.Attribute
{
    /// <summary>
    /// Interface for the Model Attribute
    /// </summary>
    public interface IModelAttributeItem : IModelAttributeKey,
        IDomainAttributeKeyName, IDomainAttributeKey,
        IModelSubjectAreaUniqueKey
    { }

    /// <summary>
    /// Implementation for the Model Attribute
    /// </summary>
    public class ModelAttributeItem : BindingTableRow, IModelAttributeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public string? AttributeTitle { get { return GetValue(nameof(AttributeTitle)); } }

        /// <inheritdoc/>
        public Guid? SubjectAreaId { get { return GetValue<Guid>(nameof(SubjectAreaId)); } protected set { SetValue(nameof(SubjectAreaId), value); } }

        /// <inheritdoc/>
        public string? SubjectAreaTitle { get { return GetValue(nameof(SubjectAreaTitle)); } }

        /// <summary>
        /// Constructor for Model Attribute Item
        /// </summary>
        public ModelAttributeItem() : base()
        { }

        /// <summary>
        /// Constructor for Model Attribute Item
        /// </summary>
        public ModelAttributeItem(IDomainAttributeKey source) : this()
        { AttributeId = source.AttributeId; }

        /// <summary>
        /// Constructor for Model Attribute Item
        /// </summary>
        public ModelAttributeItem(IDomainAttributeKey attribute, IModelSubjectAreaKey subject) : this(attribute)
        { SubjectAreaId = subject.SubjectAreaId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeTitle), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SubjectAreaTitle), typeof(string)){ AllowDBNull = true},
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
        protected ModelAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}

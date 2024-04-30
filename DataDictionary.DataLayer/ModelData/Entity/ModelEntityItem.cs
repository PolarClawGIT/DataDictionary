using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ModelData.Entity
{
    /// <summary>
    /// Interface for the Model Entity
    /// </summary>
    public interface IModelEntityItem : IModelEntityKey,
        IDomainEntityKeyName, IDomainEntityKey,
        IModelSubjectAreaUniqueKey
    { }

    /// <summary>
    /// Implementation for the Model Entity
    /// </summary>
    public class ModelEntityItem : BindingTableRow, IModelEntityItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId { get { return GetValue<Guid>("EntityId"); } protected set { SetValue("EntityId", value); } }

        /// <inheritdoc/>
        public string? EntityTitle { get { return GetValue("EntityTitle"); } }

        /// <inheritdoc/>
        public Guid? SubjectAreaId { get { return GetValue<Guid>("SubjectAreaId"); } protected set { SetValue("SubjectAreaId", value); } }

        /// <inheritdoc/>
        public string? SubjectAreaTitle { get { return GetValue("SubjectAreaTitle"); } }

        /// <summary>
        /// Constructor for Model Entity Item
        /// </summary>
        public ModelEntityItem() : base()
        { }

        /// <summary>
        /// Constructor for Model Entity Item
        /// </summary>
        public ModelEntityItem(IDomainEntityKey source) : this()
        { EntityId = source.EntityId; }

        /// <summary>
        /// Constructor for Model Entity Item
        /// </summary>
        public ModelEntityItem(IDomainEntityKey Entity, IModelSubjectAreaKey subject) : this(Entity)
        { SubjectAreaId = subject.SubjectAreaId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("EntityTitle", typeof(string)){ AllowDBNull = true},
            new DataColumn("SubjectAreaId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("SubjectAreaTitle", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ModelEntityItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}

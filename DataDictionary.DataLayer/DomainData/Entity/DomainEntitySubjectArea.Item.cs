using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Subject Area Item
    /// </summary>
    public interface IDomainEntitySubjectAreaItem : IDomainEntityKey, IModelSubjectAreaKey
    { }

    /// <summary>
    /// Implementation for Domain Entity Subject Area Item
    /// </summary>
    public class DomainEntitySubjectAreaItem : BindingTableRow, IDomainEntitySubjectAreaItem, ISerializable
    {

        /// <inheritdoc/>
        public Guid? EntityId
        {
            get { return GetValue<Guid>(nameof(EntityId)); }
            protected set { SetValue(nameof(EntityId), value); }
        }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        {
            get { return GetValue<Guid>(nameof(SubjectAreaId)); }
            protected set { SetValue(nameof(SubjectAreaId), value); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for Domain Entity Subject Area Items
        /// </summary>
        public DomainEntitySubjectAreaItem() : base() { }

        /// <summary>
        /// Constructor for Domain Entity Subject Area Items
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="subject"></param>
        public DomainEntitySubjectAreaItem(IDomainEntityKey Entity, IModelSubjectAreaKey subject) : this()
        {
            EntityId = Entity.EntityId;
            SubjectAreaId = subject.SubjectAreaId;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntitySubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}

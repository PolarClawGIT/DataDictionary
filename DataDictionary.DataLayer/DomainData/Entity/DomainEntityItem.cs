using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DomainData.SubjectArea;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Item
    /// </summary>
    public interface IDomainEntityItem : IDomainEntityKey, IDomainSubjectAreaKey, IDataItem
    {
        /// <summary>
        /// Title of the Domain Entity (aka Name of the Entity)
        /// </summary>
        String? EntityTitle { get; }

        /// <summary>
        /// Description of the Domain Entity
        /// </summary>
        String? EntityDescription { get; set; }
    }

    /// <summary>
    /// Implementation for Domain Entity Item
    /// </summary>
    [Serializable]
    public class DomainEntityItem : BindingTableRow, IDomainEntityItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>("EntityId"); } protected set { SetValue("EntityId", value); } }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        { get { return GetValue<Guid>("SubjectAreaId"); } set { SetValue("SubjectAreaId", value); } }

        /// <inheritdoc/>
        public string? EntityTitle { get { return GetValue("EntityTitle"); } set { SetValue("EntityTitle", value); } }

        /// <inheritdoc/>
        public string? EntityDescription { get { return GetValue("EntityDescription"); } set { SetValue("EntityDescription", value); } }

        /// <inheritdoc/>
        public bool? Obsolete { get { return GetValue<bool>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue("Obsolete", value); } }

        /// <summary>
        /// Constructor for Domain Entity Item
        /// </summary>
        public DomainEntityItem() : base()
        {
            if (EntityId is null) { EntityId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(EntityTitle)){ EntityTitle = "(new Entity)"; }
            if (Obsolete is null) { Obsolete = false; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SubjectAreaId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("EntityTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("EntityDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(bool)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
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
        protected DomainEntityItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (EntityTitle is not null) { return EntityTitle; } else { return string.Empty; } }
    }
}

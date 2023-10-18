﻿using DataDictionary.DataLayer.ApplicationData.Model;
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

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Item
    /// </summary>
    public interface IDomainAttributeItem : IDomainAttributeKey, IDomainSubjectAreaKey, IDataItem
    {
        /// <summary>
        /// Title of the Domain Attribute (aka Name of the Attribute)
        /// </summary>
        String? AttributeTitle { get; }

        /// <summary>
        /// Description of the Domain Attribute
        /// </summary>
        String? AttributeDescription { get; set; }
    }

    /// <summary>
    /// Implementation for Domain Attribute Item
    /// </summary>
    [Serializable]
    public class DomainAttributeItem : BindingTableRow, IDomainAttributeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue("AttributeId", value); } }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        { get { return GetValue<Guid>("SubjectAreaId"); } set { SetValue("SubjectAreaId", value); } }

        /// <inheritdoc/>
        public string? AttributeTitle { get { return GetValue("AttributeTitle"); } set { SetValue("AttributeTitle", value); } }

        /// <inheritdoc/>
        public string? AttributeDescription { get { return GetValue("AttributeDescription"); } set { SetValue("AttributeDescription", value); } }

        /// <inheritdoc/>
        public bool? Obsolete { get { return GetValue<bool>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue("Obsolete", value); } }

        /// <summary>
        /// Constructor for Domain Attribute Item
        /// </summary>
        public DomainAttributeItem() : base()
        {
            if (AttributeId is null) { AttributeId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(AttributeTitle)){  AttributeTitle = "(new Attribute)"; }
            if (Obsolete is null) { Obsolete = false; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SubjectAreaId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AttributeTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("AttributeDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(bool)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
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
        protected DomainAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (AttributeTitle is not null) { return AttributeTitle; } else { return string.Empty; } }
    }

    public static class DomainAttributeItemExtension
    {
        public static IDomainAttributeItem? GetAttribute(this IEnumerable<IDomainAttributeItem> source, IDomainAttributeKey item)
        { return source.FirstOrDefault(w => w.AttributeId == item.AttributeId); }

        public static IDomainAttributeItem? GetAttribute(this IDomainAttributeKey item, IEnumerable<IDomainAttributeItem> source)
        { return source.FirstOrDefault(w => w.AttributeId == item.AttributeId); }

        [Obsolete("Not used", true)]
        public static IDomainAttributeItem? GetParentAttribute(this IEnumerable<IDomainAttributeItem> source, IDomainAttributeParentKey item)
        { return source.FirstOrDefault(w => w.AttributeId == item.ParentAttributeId); }

        [Obsolete("Not used", true)]
        public static IDomainAttributeItem? GetParentAttribute(this IDomainAttributeParentKey item, IEnumerable<IDomainAttributeItem> source)
        { return source.FirstOrDefault(w => w.AttributeId == item.ParentAttributeId); }
    }
}
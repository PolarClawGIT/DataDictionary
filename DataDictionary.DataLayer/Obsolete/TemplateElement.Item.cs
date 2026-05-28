using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Scripting Template Element item.
    /// </summary>
    [Obsolete]
    public interface ITemplateElementItem :
        ITemplateKey, ITemplateElementKey, ITemplateElementKeyParent, ITemplateNodeItem,
        ITemporalItem
    {
        /// <summary>
        /// Name of the XML Element.
        /// </summary>
        String? ElementName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Element item.
    /// </summary>
    [Serializable, Obsolete]
    public class TemplateElementItem : BindingTableRow, ITemplateElementItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? ElementId
        {
            get { return GetValue<Guid>(nameof(ElementId)); }
            protected set { SetValue(nameof(ElementId), value); }
        }

        /// <inheritdoc/>
        public String? ElementName
        {
            get { return GetValue(nameof(ElementName)); }
            set { SetValue(nameof(ElementName), value); }
        }

        /// <inheritdoc/>
        public Guid? ParentElementId
        {
            get { return GetValue<Guid>(nameof(ParentElementId)); }
            set { SetValue(nameof(ParentElementId), value); }
        }

        /// <inheritdoc/>
        Guid? ITemplateNodeKey.NodeId { get { return ElementId; } }

        /// <inheritdoc/>
        String? ITemplateNodeItem.NodeName { get { return ElementName; } }

        /// <inheritdoc/>
        public Int32? NodeOrder
        {
            get { return GetValue<Int32>(nameof(NodeOrder)); }
            set { SetValue(nameof(NodeOrder), value); }
        }

        /// <inheritdoc/>
        public NodeRenderAsType RenderValueAs
        {
            get
            {
                String? value = GetValue(nameof(RenderValueAs));
                if (value.TryParse( out NodeRenderAsType result))
                { return result; }
                else { return NodeRenderAsType.none; }
            }
            set
            {
                if (value is NodeRenderAsType.none)
                { SetValue(nameof(RenderValueAs), null); }
                else { SetValue(nameof(RenderValueAs), value.GetEnumeration().Name); }
            }
        }

        /// <inheritdoc/>
        public String? FixedValue
        {
            get { return GetValue(nameof(FixedValue)); }
            set { SetValue(nameof(FixedValue), value); }
        }

        /// <inheritdoc/>
        public ScopeType ObjectScope
        {
            get
            {
                String? value = GetValue(nameof(ObjectScope));
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set { SetValue(nameof(ObjectScope), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? ObjectProperty
        {
            get { return GetValue(nameof(ObjectProperty)); }
            set { SetValue(nameof(ObjectProperty), value); }
        }

        /// <inheritdoc/>
        public Guid? ModelPropertyId
        {
            get { return GetValue<Guid>(nameof(ModelPropertyId)); }
            set { SetValue(nameof(ModelPropertyId), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Template Element Item
        /// </summary>
        protected TemplateElementItem() : base()
        {
            if (ElementId is null) { ElementId = Guid.NewGuid(); }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Template Element Item
        /// </summary>
        /// <param name="template"></param>
        public TemplateElementItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }


        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ElementId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ElementName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ParentElementId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(NodeOrder), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderValueAs), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderValueAs), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(FixedValue), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectProperty), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ModelPropertyId), typeof(Guid)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TemplateElementItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return ElementName ?? String.Empty; }
    }
}

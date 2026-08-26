using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// A XmlBuilder linked to a SchemaNodeValue.
    /// </summary>
    public class XmlBuilderNode : XmlBuilder, IBindingRowState
    {
        /// <summary>
        /// The SchemaNode that override default values.
        /// </summary>
        public virtual SchemaNodeValue? SchemaNode
        {
            get { return field; }
            set
            {
                if (value is not null
                    && value.ObjectScope is ScopeType.Null)
                {   // The incoming SchemaNode is "blank", fill it with defaults.
                    value.ObjectScope = base.ObjectScope;
                    value.ObjectProperty = base.ObjectProperty;
                    value.NodeName = base.NodeName;
                    value.RenderNodeType = base.RenderNodeType;
                    value.RenderTypeCode = base.RenderTypeCode;
                    value.RenderOrder = base.RenderOrder;
                }

                field = value;

                OnPropertyChanged(nameof(ObjectScope));
                OnPropertyChanged(nameof(ObjectProperty));
                OnPropertyChanged(nameof(NodeName));
                OnPropertyChanged(nameof(RenderNodeType));
                OnPropertyChanged(nameof(RenderTypeCode));
                OnPropertyChanged(nameof(RenderOrder));

                OnPropertyChanged(nameof(SchemaNode));
                OnPropertyChanged(nameof(IsOverride));
            }
        }

        /// <summary>
        /// Is the Default value being Overridden by SchemaNodeValue.
        /// </summary>
        public virtual Boolean IsOverride // Needed for DataBinding.
        { get { return SchemaNode is not null; } }

        /// <inheritdoc/>
        public override ScopeType ObjectScope
        {
            get { return SchemaNode?.ObjectScope ?? base.ObjectScope; }
            protected set { SchemaNode?.ObjectScope = value; OnPropertyChanged(nameof(ObjectScope)); }
        }

        /// <inheritdoc/>
        public override String? ObjectProperty
        {
            get { return SchemaNode?.ObjectProperty ?? base.ObjectProperty; }
            protected set { SchemaNode?.ObjectProperty = value; OnPropertyChanged(nameof(ObjectProperty)); }
        }

        /// <inheritdoc/>
        public override String NodeName
        {
            get { return SchemaNode?.NodeName ?? base.NodeName; }
            set { SchemaNode?.NodeName = value; OnPropertyChanged(nameof(NodeName)); }
        }

        /// <inheritdoc/>
        public override Int32? RenderOrder
        {
            get { return SchemaNode?.RenderOrder ?? base.RenderOrder; }
            set { SchemaNode?.RenderOrder = value; OnPropertyChanged(nameof(RenderOrder)); }
        }

        /// <inheritdoc/>
        public override XmlNodeType RenderNodeType
        {
            get { return SchemaNode?.RenderNodeType ?? base.RenderNodeType; }
            set { SchemaNode?.RenderNodeType = value; OnPropertyChanged(nameof(RenderNodeType)); }
        }

        /// <inheritdoc/>
        public override XmlTypeCode RenderTypeCode
        {
            get { return SchemaNode?.RenderTypeCode ?? base.RenderTypeCode; }
            set { SchemaNode?.RenderTypeCode = value; OnPropertyChanged(nameof(RenderTypeCode)); }
        }

        /// <summary>
        /// Builds a XmlBuilder out of a SchemaNodeValue
        /// </summary>
        /// <param name="source"></param>
        /// <param name="schemaNode"></param>
        public XmlBuilderNode(XmlBuilder source, SchemaNodeValue? schemaNode = null) : base(source)
        {
            // Store the default values
            base.ObjectScope = source.ObjectScope;
            base.ObjectProperty = source.ObjectProperty;
            base.NodeName = source.NodeName;
            base.RenderOrder = source.RenderOrder;
            base.RenderNodeType = source.RenderNodeType;
            base.RenderTypeCode = source.RenderTypeCode;

            SchemaNode = schemaNode;
        }

        /// <inheritdoc/>
        public event EventHandler<RowStateEventArgs>? RowStateChanged
        {
            add { SchemaNode?.RowStateChanged += value; }
            remove { SchemaNode?.RowStateChanged -= value; }
        }

        /// <inheritdoc/>
        public DataRowState RowState()
        { return SchemaNode?.RowState() ?? DataRowState.Detached; }
    }
}

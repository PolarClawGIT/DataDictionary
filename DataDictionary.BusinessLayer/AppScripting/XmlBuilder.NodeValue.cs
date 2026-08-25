using DataDictionary.Resource.Enumerations;
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
        protected virtual SchemaNodeValue SchemaNode
        {
            get { return field; }
            set
            {
                if (value.ObjectScope is ScopeType.Null)
                {   // The incoming SchemaNode is "blank", fill it with defaults.
                    value.ObjectScope = base.ObjectScope;
                    value.ObjectProperty = base.ObjectProperty;
                    value.NodeName = base.NodeName;
                    value.RenderNodeType = base.RenderNodeType;
                    value.RenderTypeCode = base.RenderTypeCode;
                    value.RenderOrder = base.RenderOrder;
                }

                field = value;

                OnPropertyChanged(nameof(SchemaNode));
            }
        }

        /// <inheritdoc/>
        public override ScopeType ObjectScope
        {
            get { return SchemaNode.ObjectScope; }
            protected set
            {
                SchemaNode.ObjectScope = value;
                base.ObjectScope = value;
            }
        }

        /// <inheritdoc/>
        public override String? ObjectProperty
        {
            get { return SchemaNode.ObjectProperty; }
            protected set
            {
                SchemaNode.ObjectProperty = value;
                base.ObjectProperty = value;
            }
        }

        /// <inheritdoc/>
        public override String NodeName
        {
            get { return SchemaNode.NodeName ?? String.Empty; }
            set
            {
                SchemaNode.NodeName = value;
                base.NodeName = value;
            }
        }

        /// <inheritdoc/>
        public override Int32? RenderOrder
        {
            get { return SchemaNode.RenderOrder; }
            set
            {
                SchemaNode.RenderOrder = value;
                base.RenderOrder = value;
            }
        }

        /// <inheritdoc/>
        public override XmlNodeType RenderNodeType
        {
            get { return SchemaNode.RenderNodeType; }
            set
            {
                SchemaNode.RenderNodeType = value;
                base.RenderNodeType = value;
            }
        }

        /// <inheritdoc/>
        public override XmlTypeCode RenderTypeCode
        {
            get { return SchemaNode.RenderTypeCode; }
            set
            {
                SchemaNode.RenderTypeCode = value;
                base.RenderTypeCode = value;
            }
        }

        /// <summary>
        /// Builds a XmlBuilder out of a SchemaNodeValue
        /// </summary>
        /// <param name="source"></param>
        /// <param name="schemaNode"></param>
        public XmlBuilderNode(XmlBuilder source, SchemaNodeValue schemaNode) : base(source)
        { SchemaNode = schemaNode; }

        /// <inheritdoc/>
        public event EventHandler<RowStateEventArgs>? RowStateChanged
        {
            add { ((IBindingRowState)SchemaNode).RowStateChanged += value; }
            remove { ((IBindingRowState)SchemaNode).RowStateChanged -= value; }
        }

        /// <inheritdoc/>
        public DataRowState RowState()
        { return ((IBindingRowState)SchemaNode).RowState(); }
    }
}

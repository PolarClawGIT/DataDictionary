using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Specialized Xml Builder that is bound to a SchemaNodeValue.
    /// </summary>
    class SchemaXmlBuilder : XmlBuilder
    {
        SchemaNodeValue baseValue;

        /// <inheritdoc/>
        public override String NodeName
        {
            get { return baseValue.NodeName??String.Empty; }
            set
            {
                base.NodeName = value;

                if (baseValue is not null)
                {   baseValue.NodeName = base.NodeName; }
            }
        }

        /// <inheritdoc/>
        public override Int32? NodeOrder
        {
            get { return baseValue.NodeOrder; }
            set
            {
                base.NodeOrder = value;

                if (baseValue is not null)
                { baseValue.NodeOrder = base.NodeOrder; }
            }
        }

        /// <inheritdoc/>
        public override NodeRenderAsType RenderValueAs
        {
            get { return baseValue.RenderValueAs; }
            set
            {
                base.RenderValueAs = value;

                if (baseValue is not null)
                { baseValue.RenderValueAs = base.RenderValueAs; }
            }
        }

        /// <summary>
        /// XmlBuilder constructor for SchemaNodeValue.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        public SchemaXmlBuilder(XmlBuilder source, SchemaNodeValue value) : base(source)
        {
            baseValue = value;
            //base.ObjectScope = source.ObjectScope;
            //base.ObjectProperty = source.ObjectProperty;

            //base.NodeName = value.NodeName ?? source.NodeName;
            //base.NodeOrder = value.NodeOrder ?? source.NodeOrder;
            //base.RenderValueAs = value.RenderValueAs;

        }
    }
}

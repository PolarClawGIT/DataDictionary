using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    //public delegate Boolean TryGetProperty(IPropertyIndex key, [NotNullWhen(true)] out IPropertyValue? value);
    //public delegate Boolean TryGetDefinition(IDefinitionIndex key, [NotNullWhen(true)] out IDefinitionValue? value);

    /// <summary>
    /// Definition to Build an Xml Element
    /// </summary>
    public partial class XmlBuilder : ISchemaNodeObjectValue
    {
        /// <summary>
        /// Path to the Object to be Rendered. This is normally a Property of the Object.
        /// </summary>
        public virtual PathIndex BuilderPath { get; set; }

        /// <inheritdoc/>
        public virtual ScopeType ObjectScope
        {
            get;
            protected set
            {
                field = value;
                List<String> path = PathIndex.Parse(field.GetName());
                if (!String.IsNullOrWhiteSpace(ObjectProperty))
                { path.Add(ObjectProperty); }

                BuilderPath = new PathIndex(path);
            }
        }

        /// <inheritdoc/>
        public virtual String? ObjectProperty
        {
            get;
            protected set
            {
                field = value;
                List<String> path = PathIndex.Parse(ObjectScope.GetName());
                if (!String.IsNullOrWhiteSpace(value))
                { path.Add(value); }

                BuilderPath = new PathIndex(path);
            }
        }

        /// <inheritdoc/>
        public virtual String NodeName
        {
            get
            {
                if(String.IsNullOrWhiteSpace(field))
                {
                    String value = String.Concat(BuilderPath.Member.Where(c => !Char.IsWhiteSpace(c)));
                    value = XmlConvert.EncodeName(value);
                    return value;
                }
                else { return field; }
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                { field = String.Empty; }
                else
                {   
                    // Clean up the value before storing.
                    value = String.Concat(value.Where(c => !Char.IsWhiteSpace(c)));
                    value = XmlConvert.EncodeName(value);

                    // compare this to the ObjectPath
                    String path = String.Concat(BuilderPath.Member.Where(c => !Char.IsWhiteSpace(c)));
                    path = XmlConvert.EncodeName(value);

                    if (value == path) // Flag get to use the Member name.
                    { field = String.Empty; }
                    else { field = value; }
                }

                //this.OnPropertyChanged(PropertyChanged, nameof(NodeName));
            }
        }

        /// <inheritdoc/>
        public virtual Int32? NodeOrder { get; set; }

        /// <inheritdoc/>
        public virtual NodeRenderAsType RenderValueAs { get; set; }

        /// <summary>
        /// Function that returns the NodeValue.
        /// </summary>
        protected Func<Object, String> GetValue { get; set; }

        /// <summary>
        /// Basic XmlBuilder constructor. The value is set to the ToString of the Build object.
        /// </summary>
        /// <param name="scope"></param>
        public XmlBuilder(ScopeType scope) : base()
        {
            ObjectScope = scope;
            BuilderPath = new PathIndex(PathIndex.Parse(scope.GetName()));
            RenderValueAs = NodeRenderAsType.Element;
            NodeName = String.Empty;

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        /// <summary>
        /// Creates a Clone of the XML Builder
        /// </summary>
        /// <param name="source"></param>
        public XmlBuilder(XmlBuilder source) : this(source.ObjectScope)
        {
            BuilderPath = new PathIndex(source.BuilderPath);
            RenderValueAs = source.RenderValueAs;
            NodeName = source.NodeName;
            GetValue = source.GetValue;
        }

        /// <summary>
        /// GetValue function that returns the ToString of the object passed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Override this method to return something other then ToString.
        /// Overloads handle specific data types.
        /// When calling use: GetValueDelegate((dynamic)objectValue);
        /// </remarks>
        protected virtual String? GetValueDelegate(Object value)
        {
            if (value is null) { return null; }
            else { return value.ToString(); }
        }

        /// <summary>
        /// Build an XML Object using the builder for the Object passed.
        /// </summary>
        /// <typeparam name="TMethod"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual XObject? Build<TMethod>(TMethod value)
            where TMethod : class
        {
            if (value is not IScopeType scope || scope.Scope != ObjectScope)
            {
                Exception ex = new ArgumentException("Incorrect Scope");
                ex.Data.Add(nameof(ObjectScope), ObjectScope.GetName());
                ex.Data.Add(nameof(Type.FullName), value.GetType().FullName);
                throw ex;
            }

            String? nodeValue = GetValue(value);

            if (String.IsNullOrEmpty(NodeName) || RenderValueAs is NodeRenderAsType.none)
            { return null; }

            switch (RenderValueAs)
            {
                case NodeRenderAsType.none:
                    return null;
                case NodeRenderAsType.Element:
                    return new XElement(NodeName);
                case NodeRenderAsType.ElementText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, nodeValue);
                case NodeRenderAsType.ElementCData:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, new XCData(nodeValue));
                case NodeRenderAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                        return new XElement(NodeName, XElement.Parse(nodeValue));
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(NodeName), NodeName);
                        fragementEx.Data.Add(nameof(nodeValue), nodeValue);
                        fragementEx.Data.Add(nameof(RenderValueAs), RenderValueAs.ToString());
                        throw;
                    }
                case NodeRenderAsType.AttributeText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(NodeRenderAsType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(RenderValueAs), RenderValueAs.ToString());
                    throw ex;
            }
        }

        /// <inheritdoc/>
        public override String ToString()
        { return BuilderPath.MemberFullPath; }

        /// <inheritdoc cref="ICloneable.Clone"/>
        public XmlBuilder Clone()
        {
            if (this is ValueType valueType) { return new ValueType(valueType); }
            else if (this is PropertyType propertyType) { return new PropertyType(propertyType); }
            else { return new XmlBuilder(this); }
        }
    }
}

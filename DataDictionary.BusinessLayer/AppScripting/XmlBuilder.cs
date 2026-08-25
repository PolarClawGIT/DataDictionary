using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    //public delegate Boolean TryGetProperty(IPropertyIndex key, [NotNullWhen(true)] out IPropertyValue? value);
    //public delegate Boolean TryGetDefinition(IDefinitionIndex key, [NotNullWhen(true)] out IDefinitionValue? value);

    /// <summary>
    /// Interface for the XmlBuilder
    /// </summary>
    public interface IXmlBuilder : IBindingPropertyChanged, IXmlBuilderIndex, ISchemaNodeObject
    { }

    /// <summary>
    /// Definition to Build an Xml Element
    /// </summary>
    /// <remarks>Does not support Binding</remarks>
    public partial class XmlBuilder : IXmlBuilder
    {
        /// <summary>
        /// Path to the Object to be Rendered. This is normally a Property of the Object.
        /// </summary>
        public virtual XmlBuilderIndex BuilderPath
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(BuilderPath));
            }
        }

        /// <inheritdoc/>
        public virtual ScopeType ObjectScope
        {
            get;
            protected set
            {
                field = value;
                BuilderPath = new XmlBuilderIndex(field, ObjectProperty);
                OnPropertyChanged(nameof(ObjectScope));
            }
        }

        /// <inheritdoc/>
        public virtual String? ObjectProperty
        {
            get;
            protected set
            {
                field = value;
                BuilderPath = new XmlBuilderIndex(ObjectScope, field);
                OnPropertyChanged(nameof(ObjectScope));
            }
        }

        /// <summary>
        /// Type of Value the Object Property represents.
        /// </summary>
        public virtual ObjectValueType ObjectType { get; init; } = ObjectValueType.Null;

        /// <inheritdoc/>
        public virtual String NodeName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(field))
                {
                    String value = String.Concat(((PathIndex)BuilderPath).Member.Where(c => !Char.IsWhiteSpace(c)));
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
                    String path = String.Concat(((PathIndex)BuilderPath).Member.Where(c => !Char.IsWhiteSpace(c)));
                    path = XmlConvert.EncodeName(value);

                    if (value == path) // Flag get to use the Member name.
                    { field = String.Empty; }
                    else { field = value; }
                }

                OnPropertyChanged(nameof(NodeName));
            }
        }

        /// <inheritdoc/>
        public virtual Int32? RenderOrder
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(RenderOrder));
            }
        } = 0;

        /// <summary>
        /// Function that returns the NodeValue.
        /// </summary>
        protected Func<Object, String> GetValue { get; set; }

        /// <inheritdoc/>
        public virtual XmlNodeType RenderNodeType
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(RenderNodeType));
            }
        }

        /// <inheritdoc/>
        public virtual XmlTypeCode RenderTypeCode
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(RenderTypeCode));
            }
        }

        /// <summary>
        /// Basic XmlBuilder constructor. The value is set to the ToString of the Build object.
        /// </summary>
        /// <param name="scope"></param>
        public XmlBuilder(ScopeType scope) : base()
        {
            ObjectScope = scope;
            BuilderPath = new XmlBuilderIndex(scope);
            RenderNodeType = XmlNodeType.Element;
            RenderTypeCode = XmlTypeCode.Node;
            NodeName = String.Empty;

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        /// <summary>
        /// Creates a Clone of the XML Builder
        /// </summary>
        /// <param name="source"></param>
        protected XmlBuilder(XmlBuilder source) : this(source.ObjectScope)
        {
            BuilderPath = new XmlBuilderIndex(source);
            NodeName = source.NodeName;

            ObjectProperty = source.ObjectProperty;
            ObjectType = source.ObjectType;
            RenderTypeCode = source.RenderTypeCode;
            RenderNodeType = source.RenderNodeType;
            RenderOrder = source.RenderOrder;

            GetValue = source.GetValue;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="BindingPropertyChanged.OnPropertyChanged(IBindingPropertyChanged, PropertyChangedEventHandler?, String?)"/>
        protected virtual void OnPropertyChanged(String propertyName)
        { this.OnPropertyChanged(PropertyChanged, nameof(propertyName)); }

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

            if (String.IsNullOrEmpty(NodeName) || RenderNodeType is XmlNodeType.None)
            { return null; }

            switch (RenderNodeType)
            {
                case XmlNodeType.None:
                    return null;
                case XmlNodeType.Element:
                    return new XElement(NodeName);
                case XmlNodeType.Text:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, nodeValue);
                case XmlNodeType.CDATA:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, new XCData(nodeValue));
                case XmlNodeType.Attribute:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(RenderNodeType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(RenderNodeType), RenderNodeType.ToString());
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

        /// <summary>
        /// Returns a list of Supported ScopeTypes
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ScopeType> SupportedScopes()
        {   // TODO: Need to add more types.
            return new List<ScopeType>() { ScopeType.Null, ScopeType.ModelAttribute };
        }
    }
}

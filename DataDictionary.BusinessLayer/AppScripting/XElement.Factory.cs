using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;
using Attribute = DataDictionary.BusinessLayer.AppModel.Attribute;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Base compoents for a XElement Builder Factory.
    /// </summary>
    public interface IXElementFactory
    {
        /// <summary>
        /// Builds a list of XElement Builders used to build XElements.
        /// </summary>
        /// <returns></returns>
        static abstract IEnumerable<XElementBuilder> CreateXElements();
    }

    /// <summary>
    /// Base compoents for a XElement Builder Factory that has a generic parameter.
    /// </summary>
    /// <typeparam name="T">generic parameter</typeparam>
    public interface IXElementFactory<T>
    {
        /// <summary>
        /// Builds a list of XElement Builders used to build XElements.
        /// </summary>
        /// <returns></returns>
        static abstract IEnumerable<XElementBuilder> CreateXElements(T paramter);
    }

    /// <summary>
    /// Base compoents for a XElement Builder Factory that uses the TryGetValue function.
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IXElementFactory<TIndex, TValue>
        where TValue : TIndex
    {
        /// <summary>
        /// Builds a list of XElement Builders used to build XElements.
        /// </summary>
        /// <returns></returns>
        static abstract IEnumerable<XElementBuilder> CreateXElements(TryGetValue<TIndex, TValue> getValue);
    }

    /// <summary>
    /// Implemenation of the XElementFactory.
    /// This takes XElementBuilders and builds out the XElements.
    /// </summary>
    [Obsolete("POC code, not in use", true)]
    public class XElementFactory
    {
        // TODO: This replaces Scripting Engine. POC code.

        XElementBuilderList builders = new XElementBuilderList();

        public required TryGetValue<IDefinitionIndex,IDefinitionValue> DefinitionGet { private get; init; }
        public required TryGetValue<IPropertyIndex, IPropertyValue> PropertyGet { private get; init; }
        public required TryGetValue<IAttributeIndex, IAttributeValue> AttributeGet { private get; init; }
        public required TryGetValue<IEntityIndex, IEntityValue> EntityGet { private get; init; }
        public required TryGetValue<IProcessIndex, IProcessValue> ProcessGet { private get; init; }

        public IReadOnlyDictionary<ScopeType, IEnumerable<String>> Properties
        {
            get
            {
                return builders.
                    Select(s => new { Scope = s.Key, Properties = s.Value.Select(m => m.PropertyName) }).
                    ToDictionary(k => k.Scope, v => v.Properties).
                    AsReadOnly();
            }
        }

        public void Load()
        {
            builders.Clear();
            builders.AddRange(Attribute.CreateXElements(PropertyGet, DefinitionGet));
            builders.AddRange(Entity.CreateXElements(PropertyGet, DefinitionGet));
            builders.AddRange(Process.CreateXElements(PropertyGet, DefinitionGet));
        }

        public XElement Build(IScopeType value)
        {
            if (builders.TryGetValue(value.Scope, out IEnumerable<XElementBuilder>? build))
            { return build.Build(value); }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(value.Scope), value.Scope.GetEnumeration().Name);
                throw ex;
            }
        }

        public IEnumerable<XElement> Build(IEnumerable<IScopeType> values)
        {
            List<XElement> result = new List<XElement>();

            foreach (IScopeType item in values)
            { result.Add(Build(item)); }

            return result;
        }

        public XElement Build(IAttribute attribute, IAttributeIndex index) 
        {
            AttributeIndex key = new AttributeIndex(index);

            if(attribute.Attributes.FirstOrDefault(w => key.Equals(w)) is IAttributeValue value)
            {
                XElement result = Build(value);
                result.Add(attribute.Properties.Where(w => key.Equals(w)));
                result.Add(attribute.Definitions.Where(w => key.Equals(w)));

                return result;
            }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(index), index);
                throw ex;
            }
        }


    }
}

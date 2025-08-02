using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        static abstract IEnumerable<XElementBuilder> CreateXElementBuilders();
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
        static abstract IEnumerable<XElementBuilder> CreateXElementBuilders(T paramter);
    }

    public class XElementFactory
    {
        // TODO:POC code. Move to Scripting Engine.

        Dictionary<ScopeType, IEnumerable<XElementBuilder>> builders = new Dictionary<ScopeType, IEnumerable<XElementBuilder>>();

        public required AppModel.IDefinitionGetValue DefinitionGet { private get; init; }
        public required AppModel.IPropertyGetValue PropertyGet { private get; init; }

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
            builders.Add(ScopeType.ModelAttribute, AttributeValue.CreateXElementBuilders());
            builders.Add(ScopeType.ModelAttributeProperty, AttributePropertyValue.CreateXElementBuilders(PropertyGet));
            builders.Add(ScopeType.ModelAttributeDefinition, AttributeDefinitionValue.CreateXElementBuilders(DefinitionGet));
        }

        public XElement Build(IScopeType value)
        {
            if (builders.TryGetValue(value.Scope, out IEnumerable<XElementBuilder>? build))
            { return build.Build(value); }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(value.Scope), ScopeEnumeration.Cast(value.Scope).Name);
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

            if(attribute.Values.FirstOrDefault(w => key.Equals(w)) is IAttributeValue value)
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

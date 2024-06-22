using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeAliasValue : IDomainAttributeAliasItem, IAttributeIndex, IAliasValue
    {
        /// <summary>
        /// Attribute Alias Name returned as parts.
        /// </summary>
        public List<String> AliasParts { get; }
    }

    /// <inheritdoc/>
    public class AttributeAliasValue : DomainAttributeAliasItem, IAttributeAliasValue, IScripting<IAttributeValue>
    {
        /// <inheritdoc/>
        public List<String> AliasParts { get { return NamedScopePath.Parse(AliasName); } }

        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

        /// <inheritdoc cref="DomainAttributeAliasItem(IDomainAttributeKey)"/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }

        /// <inheritdoc/>
        internal AttributeAliasValue(IDomainAttributeKey key) : base(key) { }

        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeAlias;
            IAttributeAliasValue alaisNames;

            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(alaisNames.Scope),      DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasName),  DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasParts), DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
            };
            return result;
        }

        internal XElement? GetXElement()
        {
            Func<ScopeType, IEnumerable<TemplateNodeValue>> getNodes = (s) => { throw new NotImplementedException(); };

            XElement? result = null;

            foreach (TemplateNodeValue node in getNodes(Scope))
            {
                XObject? value = null;

                switch (node.PropertyName)
                {
                    case nameof(this.AliasName): value = node.BuildXObject(AliasName); break;
                    case nameof(this.AliasScope): value = node.BuildXObject(AliasScope); break;
                    default:
                        break;
                }

                if (value is XObject)
                {
                    if (result is null) { result = new XElement(Scope.ToName()); }
                    result.Add(value);
                }
            }

            return null;
        }


        /// <inheritdoc/>
/*        public XElement? GetXElement(IAttributeValue data, IEnumerable<TemplateElementValue>? options)
        {
            XElement? result = null;

            if (options is not null && options.Count() > 0)
            {

                foreach (TemplateElementValue option in options)
                {
                    Object? value = null;

                    switch (option.PropertyName)
                    {
                        case nameof(Scope): value = Scope.ToName(); break;
                        case nameof(AliasName): value = AliasName; break;
                        default:
                            break;
                    }

                    if (value is not null)
                    {
                        if (result is null) { result = new XElement(ScopeType.ModelAttributeAlias.ToName()); }
                        result.Add(option.GetXElement(value));
                    }

                    // Special Handling needed for AliasParts
                    if (option.PropertyName is nameof(AliasParts))
                    {
                        XElement? aliasElement = null;
                        List<String> scopeParts = NamedScopePath.Parse(Scope.ToName());

                        for (Int32 i = 0; i < AliasParts.Count; i++)
                        {
                            String item = AliasParts[i];
                            Int32 ScopeLevel = scopeParts.Count - AliasParts.Count + i;

                            if (aliasElement is null)
                            { aliasElement = new XElement(String.Format("{0}.{1}", ScopeType.ModelAttributeAlias.ToName(), nameof(AliasParts))); }

                            XElement newItem = option.GetXElement(item);

                            // Added a Scope Level. Repeat the Second Scope level, if needed.
                            if (AliasParts.Count == scopeParts.Count) // Scope matches Alias count
                            { newItem.Add(new XAttribute("Level", scopeParts[i])); }
                            else if(i == 0 && scopeParts.Count > 0) // First Level
                            { newItem.Add(new XAttribute("Level", scopeParts[i])); }
                            else if(ScopeLevel > 0 && scopeParts.Count > ScopeLevel) // Remaining level match
                            { newItem.Add(new XAttribute("Level", scopeParts[ScopeLevel])); }
                            // Do not know what the other levels might be, so don't assign a level.

                            aliasElement.Add(newItem);
                        }

                        if (result is null) { result = new XElement(ScopeType.ModelAttributeAlias.ToName()); }
                        result.Add(aliasElement);
                    }
                }
            }

            return result;
        }*/
    }
}

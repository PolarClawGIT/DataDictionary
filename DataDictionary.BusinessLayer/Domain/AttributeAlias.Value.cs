using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeAliasValue : IDomainAttributeAliasItem,
        IAttributeIndex, IAliasIndex
    {
        /// <summary>
        /// Attribute Alias Name returned as parts.
        /// </summary>
        public List<String> AliasParts { get; }
    }

    /// <inheritdoc/>
    public class AttributeAliasValue : DomainAttributeAliasItem, IAttributeAliasValue
    {
        /// <inheritdoc/>
        public List<String> AliasParts { get { return PathIndex.Parse(AliasName); } }

        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

        /// <inheritdoc cref="DomainAttributeAliasItem(IDomainAttributeKey)"/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }

        /// <summary>
        /// Create Attribute Alias from Attribute and Alias.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alias"></param>
        public AttributeAliasValue(IAttributeIndex key, IAliasIndex alias) : base(key)
        {
            AliasName = alias.AliasName;
            AliasScope = alias.AliasScope;
        }

        /// <inheritdoc/>
        internal AttributeAliasValue(IDomainAttributeKey key) : base(key) { }

        /// <summary>
        /// The Alias Path derived from AliasName
        /// </summary>
        public PathIndex AliasPath
        {
            get { return new PathIndex(PathIndex.Parse(AliasName).ToArray()); }
            set { AliasName = value.MemberFullPath; }
        }

        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeAlias;
            IAttributeAliasValue alaisNames;

            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasScope), DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasName),  DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasParts), DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
            };
            return result;
        }

        internal XElement? GetXElement(ScriptingWork scripting, Func<TemplateNodeValue,IReadOnlyList<XAttribute>> getAttributes)
        { 
            XElement? result = null;

            foreach (TemplateNodeValue node in scripting.Nodes.Where(w => w.PropertyScope == Scope))
            {
                List<XObject> values = new List<XObject>();

                switch (node.PropertyName)
                {
                    case nameof(AliasScope): AddValue(node.BuildXObject(ScopeEnumeration.Cast(AliasScope).Name)); break;
                    case nameof(AliasName): AddValue(node.BuildXObject(AliasName)); break;
                    case nameof(AliasParts):
                        List<String> scopeParts = PathIndex.Parse(ScopeEnumeration.Cast(AliasScope).Name);
                        String levelValue = String.Empty;

                        for (Int32 i = 0; i < AliasParts.Count; i++)
                        {
                            String item = AliasParts[i];
                            XObject? aliasObject = null;
                            Int32 ScopeLevel = scopeParts.Count - AliasParts.Count + i;

                            // Added a Scope Level. Repeat the Second Scope level, if needed.
                            if (AliasParts.Count == scopeParts.Count) // Scope matches Alias count
                            { levelValue = scopeParts[i]; }
                            else if (i <= 1 && scopeParts.Count > 0) // First Level
                            { levelValue = scopeParts[i]; }
                            else if (ScopeLevel > 0 && scopeParts.Count > ScopeLevel) // Remaining level match
                            { levelValue = scopeParts[ScopeLevel]; }

                            XElement newElement;
                            XAttribute newAttribute;

                            switch (node.NodeValueAs)
                            {
                                case TemplateNodeValueAsType.none: break;
                                case TemplateNodeValueAsType.ElementText or TemplateNodeValueAsType.ElementXML:
                                    newElement = new XElement(nameof(AliasParts), item);
                                    newElement.Add(new XAttribute("Level", i));
                                    newElement.Add(new XAttribute("Name", levelValue));
                                    aliasObject = newElement;
                                    break;
                                case TemplateNodeValueAsType.ElementCData:
                                    newElement = new XElement(nameof(AliasParts), new XCData(item));
                                    newElement.Add(new XAttribute("Level", i));
                                    newElement.Add(new XAttribute("Name", levelValue));
                                    aliasObject = newElement;
                                    break;
                                case TemplateNodeValueAsType.Attribute:
                                    newAttribute = new XAttribute(String.Format("Level.{0}.{1}", i, levelValue), item);
                                    aliasObject = newAttribute;
                                    break;
                                default:
                                    break;
                            }


                            AddValue(aliasObject);
                        }
                        break;
                    default:
                        break;
                }

                if (values.Count > 0)
                {
                    if (result is null) { result = new XElement(ScopeEnumeration.Cast(Scope).Name); }
                    result.Add(values.ToArray());
                    result.Add(getAttributes(node).ToArray());
                }

                void AddValue(XObject? value)
                { if (value is XObject) { values.Add(value); } }

            }

            return result;
        }


    }
}

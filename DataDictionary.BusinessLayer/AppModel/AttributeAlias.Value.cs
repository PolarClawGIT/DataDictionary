using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeAliasValue : IAttributeAliasItem,
        IAttributeIndex, IAliasIndex, IAliasSubType,
        IScopeType
    {
        /// <summary>
        /// Attribute Alias Name returned as parts.
        /// </summary>
        public List<String> AliasParts { get; }
    }

    /// <inheritdoc/>
    public partial class AttributeAliasValue : AttributeAliasItem, IAttributeAliasValue
    {
        /// <inheritdoc/>
        public List<String> AliasParts { get { return PathIndex.Parse(base.AliasPath); } }

        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

        /// <inheritdoc cref="AttributeAliasItem(IAttributeKey)"/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeAlias; } }

        /// <summary>
        /// Create Attribute Alias from Attribute and Alias.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alias"></param>
        public AttributeAliasValue(IAttributeIndex key, IAliasIndex alias) : base(key)
        {
            base.AliasPath = alias.AliasPath;
            AliasScope = alias.AliasScope;
        }

        /// <inheritdoc/>
        internal AttributeAliasValue(IAttributeKey key) : base(key) { }

        /// <inheritdoc/>
        public new PathIndex AliasPath
        {
            get
            {
                // Changing the property in the base class is not always caught by the OnPropertyChanged.
                // Extra code is needed to check if the data has changed and update the backing field.  
                if (!aliasPathValue.MemberFullPath.Equals(base.AliasPath))
                { aliasPathValue = new PathIndex(PathIndex.Parse(base.AliasPath).ToArray()); }

                return aliasPathValue;
            }
            set
            {
                base.AliasPath = value.MemberFullPath;
                aliasPathValue.Set(value);
                OnPropertyChanged(nameof(base.AliasPath));
            }
        }
        PathIndex aliasPathValue = new PathIndex();

        [Obsolete("replace", true)]
        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeAlias;
            AttributeAliasValue alaisNames;

            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasScope), DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasPath),  DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(alaisNames.AliasParts), DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
            };
            return result;
        }

        [Obsolete("replace", true)]
        internal XElement? GetXElement(ScriptingWork scripting, Func<ScriptingNodeValue, IReadOnlyList<XAttribute>> getAttributes)
        {
            XElement? result = null;

            foreach (ScriptingNodeValue node in scripting.Nodes.Where(w => w.PropertyScope == Scope))
            {
                List<XObject> values = new List<XObject>();

                switch (node.PropertyName)
                {
                    case nameof(AliasScope): AddValue(node.BuildXObject(ScopeEnumeration.Cast(AliasScope).Name)); break;
                    case nameof(AttributeAliasItem.AliasPath): AddValue(node.BuildXObject(AliasPath)); break;
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

                            switch (node.NodeRenderAs)
                            {
                                case NodeRenderAsType.none: break;
                                case NodeRenderAsType.ElementText or NodeRenderAsType.ElementXML:
                                    newElement = new XElement(nameof(AliasParts), item);
                                    newElement.Add(new XAttribute("Level", i));
                                    newElement.Add(new XAttribute("Name", levelValue));
                                    aliasObject = newElement;
                                    break;
                                case NodeRenderAsType.ElementCData:
                                    newElement = new XElement(nameof(AliasParts), new XCData(item));
                                    newElement.Add(new XAttribute("Level", i));
                                    newElement.Add(new XAttribute("Name", levelValue));
                                    aliasObject = newElement;
                                    break;
                                case NodeRenderAsType.AttributeText:
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

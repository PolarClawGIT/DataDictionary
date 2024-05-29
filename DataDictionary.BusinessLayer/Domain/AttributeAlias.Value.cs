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

        internal static IReadOnlyList<ColumnValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeAlias;
            IAttributeAliasValue alaisNames;

            List<ColumnValue> result = new List<ColumnValue>()
            {
                new ColumnValue() {ColumnName = nameof(alaisNames.Scope),      DataType = typeof(String), AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(alaisNames.AliasName),  DataType = typeof(String), AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(alaisNames.AliasParts), DataType = typeof(String), AllowDBNull = false, Scope = scope},
            };
            return result;
        }

        /// <inheritdoc/>
        public XElement? GetXElement(IAttributeValue data, IEnumerable<DefinitionElementValue>? options)
        {
            XElement? result = null;

            if (options is not null && options.Count() > 0)
            {

                foreach (DefinitionElementValue option in options)
                {
                    Object? value = null;

                    switch (option.ColumnName)
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
                    if (option.ColumnName is nameof(AliasParts))
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
        }
    }
}

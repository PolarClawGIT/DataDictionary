using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Default Builders for the different supported objects.
    /// </summary>
    public static class XmlBuilderExtension
    {
        /// <summary>
        /// Generic XElement that the specific builder are based on.
        /// </summary>
        /// <typeparam name="TRoot"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="builders"></param>
        /// <param name="scope"></param>
        /// <param name="roots"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static XElement? Build<TRoot, TChild>(this IEnumerable<XmlBuilder> builders, ScopeType scope, IEnumerable<TRoot> roots,
                params IEnumerable<(ScopeType scope, IEnumerable<TChild> values, Func<TRoot, TChild, Boolean> filter)> children)
                where TRoot : class, IScopeType
                where TChild : class
        {
            XmlBuilderIndex rootKey = new XmlBuilderIndex(scope);
            Int32 rootCount = roots.Count(w => w.Scope == scope);
            XElement root = new XElement(scope.GetName());

            if (builders.TryGetXmlBuilder(rootKey, out XmlBuilder? builder))
            {
                if (rootCount == 0)
                { root = new XElement(builder.NodeName); }
                else
                {
                    if (rootCount == 1)
                    {
                        TRoot rootItem = roots.First();
                        XObject? firstNode = builder.Build(rootItem);

                        if (firstNode is XElement firstRoot)
                        { root = firstRoot; }
                        else
                        {
                            root = new XElement(builder.NodeName);
                            root.Add(firstNode);
                        }

                        foreach (var child in children)
                        {
                            XmlBuilderIndex childKey = new XmlBuilderIndex(child.scope);

                            if (builders.TryGetXmlBuilder(childKey, out XmlBuilder? childBuilder))
                            {
                                XElement childRoot = new XElement(childBuilder.NodeName);

                                foreach (TChild item in child.values.Where(w => w is IScopeType s && child.scope == s.Scope && child.filter(rootItem, w)))
                                { childRoot.Add(childBuilder.Build(item)); }

                                root.Add(childRoot);
                            }
                        }
                    }
                    else
                    {
                        root = new XElement("Root");

                        foreach (TRoot currentItem in roots.Where(w => scope == w.Scope))
                        {
                            XObject? currentNode = builder.Build(currentItem);
                            root.Add(currentNode);

                            if (currentNode is XElement currentRoot)
                            {
                                foreach (var child in children)
                                {
                                    XmlBuilderIndex childKey = new XmlBuilderIndex(child.scope);
                                    if (builders.TryGetXmlBuilder(childKey, out XmlBuilder? childBuilder))
                                    {
                                        XElement childRoot = new XElement(childBuilder.NodeName);

                                        foreach (TChild item in child.values.Where(w => w is IScopeType s && child.scope == s.Scope && child.filter(currentItem, w)))
                                        { childRoot.Add(childBuilder.Build(item)); }

                                        currentRoot.Add(childRoot);
                                    }

                                }
                            }

                        }

                    }
                }

            }

            return root;
        }

        /// <summary>
        /// Try/Get a specific XmlBuilder from the list.
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="rootKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetXmlBuilder(this IEnumerable<XmlBuilder> builders, XmlBuilderIndex rootKey, [NotNullWhen(true)] out XmlBuilder? value)
        {
            value = null;

            if (builders.Count(w => rootKey.Equals(w)) == 1)
            { value = builders.Single(w => rootKey.Equals(w)); return true; }
            else { return false; }
        }

        /// <summary>
        /// XElement build for AttributeValue.
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="attributes"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static XElement Build(this IEnumerable<XmlBuilder> builders,
                IEnumerable<AttributeValue> attributes,
                IEnumerable<AttributePropertyValue> properties)
        {   // TODO: Probably move to the specific data classes
            XElement? result = builders.Build<AttributeValue, IAttributeIndex>(ScopeType.ModelAttribute, attributes,
                        (ScopeType.ModelAttributeProperty, properties, (r, c) => new AttributeIndex(r).Equals(new AttributeIndex(c)))
                        );

            if (result is XElement) { return result; }
            else { return new XElement(ScopeType.ModelAttribute.GetName()); }
        }

        /// <summary>
        /// Gets the NamedScopeSource for the xmlBuilder
        /// </summary>
        /// <param name="namedScope"></param>
        /// <param name="templateObject"></param>
        /// <returns></returns>
        public static IEnumerable<INamedScopeSourceValue> GetData(this INamedScopeData namedScope, ITemplateObjectNameIndex templateObject)
        {
            TemplateObjectNameIndex key = new TemplateObjectNameIndex(templateObject);
            PathIndex path = new PathIndex(key.ObjectPath);

            return namedScope.PathKeys(path).Select(s => namedScope.GetData(s));
        }


        /// <summary>
        /// Try to Parse a String into an XDocument.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="document"></param>
        /// <param name="exception"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String source, [NotNullWhen(true)] out XDocument? document, [NotNullWhen(false)] out Exception? exception, LoadOptions option = LoadOptions.PreserveWhitespace)
        {
            document = null;
            exception = null;

            try
            {
                document = XDocument.Parse(source, option);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
        }


        /// <summary>
        /// Try to Parse an XDocument into a String.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Boolean TryParse(this XDocument source, [NotNullWhen(true)] out String? document)
        {
            //Note: Online Sources use StringWriter to convert an XDocument to String.
            //This alters the Declaration of the XDocument and forces it to UTF-16, which is the format of Windows Strings.
            //Other solutions run the XDocument thru several more steps that also alter the Declaration or require
            //that the correct Declaration to be known and that is be compatible with a String Encoding.
            //This approach is to add the Declaration using the StringBuilder as a simple string.

            document = null;
            StringBuilder result = new StringBuilder();

            // XDocument.ToString() does not contain the Header, put that back in.
            if (source.Declaration is XDeclaration declaration)
            { result.Append(declaration.ToString()); }
            //else { result.AppendLine(new XDeclaration(null, null, null).ToString()); }

            result.AppendLine(source.ToString());

            document = result.ToString();
            return true;
        }
    }
}


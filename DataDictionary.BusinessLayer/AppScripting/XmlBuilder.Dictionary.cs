using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{


    /// <summary>
    /// Provides a list of XML Builders.
    /// </summary>
    public class XmlBuilderDictionary : Dictionary<PathIndex, XmlBuilder>
    {
        // TODO: Need to return a list including children so a tree structure can be built.
        // TODO: Need a way to load and save to the database. Rebuild into SchemaNode?
        // TODO: This seems to work but it could use some refinement.

        //public required TryGetProperty GetProperty { get; init; } 
        //public required TryGetDefinition GetDefinition { get; init; }

        /// <summary>
        /// Constructor used for initialization only.
        /// </summary>
        internal XmlBuilderDictionary() : base() { }

        /// <summary>
        /// Constructor that build a list of XML Builders.
        /// </summary>
        /// <param name="builders"></param>
        public XmlBuilderDictionary(IEnumerable<XmlBuilder> builders) : this()
        {
            foreach (XmlBuilder item in builders)
            {
                Add(item.ObjectPath, item);

                if (item is XmlBuilder.PropertyType propType)
                {
                    foreach (var child in propType.Children.Values)
                    { Add(child.ObjectPath, child); }
                }
                else if (item is XmlBuilder.ValueType valType)
                {
                    foreach (var child in valType.Properties.Values)
                    { Add(child.ObjectPath, child); }
                }


            }
        }

        /// <summary>
        /// Does a deep-copy/clone of the XmlBuilderDictionary.
        /// </summary>
        /// <param name="source"></param>
        public XmlBuilderDictionary(XmlBuilderDictionary source) : base()
        {
            foreach (var item in source)
            { Add(item.Key, item.Value.Clone()); }
        }

        /// <summary>
        /// Uses the XMLBuilder to create an XML Element.
        /// </summary>
        /// <typeparam name="TRoot"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="scope"></param>
        /// <param name="roots"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public XElement? Build<TRoot, TChild>(ScopeType scope, IEnumerable<TRoot> roots,
            params IEnumerable<(ScopeType scope, IEnumerable<TChild> values, Func<TRoot, TChild, Boolean> filter)> children)
            where TRoot : class, IScopeType
            where TChild : class
        {
            PathIndex rootKey = new PathIndex(scope);
            Int32 rootCount = roots.Count(w => w.Scope == scope);
            XElement root = new XElement(scope.GetName());

            if (TryGetValue(rootKey, out XmlBuilder? builder))
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
                            PathIndex childKey = new PathIndex(child.scope);

                            if (TryGetValue(childKey, out XmlBuilder? childBuilder))
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
                                    PathIndex childKey = new PathIndex(child.scope);
                                    if (TryGetValue(childKey, out XmlBuilder? childBuilder))
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

    }

    /// <summary>
    /// Default Builders for the different supported objects.
    /// </summary>
    public static class XmlBuilderExtension
    {   // TODO: These can be moved to the various classes, once things are working.

        /// <summary>
        /// Execute the XML Builders for AttributeValue.
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="attributes"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static XElement Build(this XmlBuilderDictionary builders,
            IEnumerable<AttributeValue> attributes,
            IEnumerable<AttributePropertyValue> properties)
        {
            XElement? result = builders.Build<AttributeValue, IAttributeIndex>(ScopeType.ModelAttribute, attributes,
                        (ScopeType.ModelAttributeProperty, properties, (r, c) => new AttributeIndex(r).Equals(new AttributeIndex(c)))
                        );

            if (result is XElement) { return result; }
            else { return new XElement(ScopeType.ModelAttribute.GetName()); }
        }

        /// <summary>
        /// Execute the XML Builders for EntityValue.
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="entities"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static XElement Build(this XmlBuilderDictionary builders,
            IEnumerable<EntityValue> entities,
            IEnumerable<EntityPropertyValue> properties)
        {
            XElement? result = builders.Build<EntityValue, IEntityIndex>(ScopeType.ModelEntity, entities,
                        (ScopeType.ModelEntityProperty, properties, (r, c) => new EntityIndex(r).Equals(new EntityIndex(c)))
                        );

            if (result is XElement) { return result; }
            else { return new XElement(ScopeType.ModelAttribute.GetName()); }
        }
    }
}

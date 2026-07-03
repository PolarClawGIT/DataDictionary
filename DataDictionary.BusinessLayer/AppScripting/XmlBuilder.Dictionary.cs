using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Delegate definition to get a XML Builder from the Key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <seealso cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
    public delegate Boolean TryGetXmlBuilder(PathIndex key, [NotNullWhen(true)] out XmlBuilder? value);

    /// <summary>
    /// Provides a list of XML Builders.
    /// </summary>
    public class XmlBuilderDictionary : IReadOnlyDictionary<PathIndex, XmlBuilder>
    {
        // TODO: Need to return a list including children so a tree structure can be built.
        // TODO: Need a way to load and save to the database. Rebuild into SchemaNode?
        // TODO: This seems to work but it could use some refinement.

        //public required TryGetProperty GetProperty { get; init; } 
        //public required TryGetDefinition GetDefinition { get; init; }

        Dictionary<PathIndex, XmlBuilder> data = new Dictionary<PathIndex, XmlBuilder>();

        /// <summary>
        /// Constructor used for initialization only.
        /// </summary>
        public XmlBuilderDictionary() : base() { }

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

        /// <summary>
        /// Creates WorkItems to load the XML Builder list.
        /// </summary>
        /// <param name="getBuilders"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(Func<IEnumerable<XmlBuilder>> getBuilders)
        {   // Should be called after Model Properties and Model Definitions are loaded.

            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Load XmlBuilders", DoWork = () => DataLoad(getBuilders) });
            return work;

            void DataLoad(Func<IEnumerable<XmlBuilder>> getBuilders)
            {
                IEnumerable<XmlBuilder> builders = getBuilders();
                data.Clear();

                foreach (XmlBuilder item in builders)
                {
                    data.Add(item.BuilderPath, item);

                    if (item is XmlBuilder.PropertyType propType)
                    {
                        foreach (var child in propType.Children.Values)
                        { data.Add(child.BuilderPath, child); }
                    }
                    else if (item is XmlBuilder.ValueType valType)
                    {
                        foreach (var child in valType.Properties.Values)
                        { data.Add(child.BuilderPath, child); }
                    }
                }
            }
        }

        #region IReadOnlyDictionary
        /// <inheritdoc/>
        public XmlBuilder this[PathIndex key] => ((IReadOnlyDictionary<PathIndex, XmlBuilder>)data)[key];

        /// <inheritdoc/>
        public IEnumerable<PathIndex> Keys => ((IReadOnlyDictionary<PathIndex, XmlBuilder>)data).Keys;

        /// <inheritdoc/>
        public IEnumerable<XmlBuilder> Values => ((IReadOnlyDictionary<PathIndex, XmlBuilder>)data).Values;

        /// <inheritdoc/>
        public Int32 Count => ((IReadOnlyCollection<KeyValuePair<PathIndex, XmlBuilder>>)data).Count;

        /// <inheritdoc/>
        public Boolean ContainsKey(PathIndex key)
        { return ((IReadOnlyDictionary<PathIndex, XmlBuilder>)data).ContainsKey(key); }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<PathIndex, XmlBuilder>> GetEnumerator()
        { return ((IEnumerable<KeyValuePair<PathIndex, XmlBuilder>>)data).GetEnumerator(); }

        /// <inheritdoc/>
        public Boolean TryGetValue(PathIndex key, [MaybeNullWhen(false)] out XmlBuilder value)
        { return ((IReadOnlyDictionary<PathIndex, XmlBuilder>)data).TryGetValue(key, out value); }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        { return ((IEnumerable)data).GetEnumerator(); }
        #endregion
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

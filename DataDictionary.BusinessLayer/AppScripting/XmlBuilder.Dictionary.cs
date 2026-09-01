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
    public delegate Boolean TryGetXmlBuilder(XmlBuilderIndex key, [NotNullWhen(true)] out XmlBuilder? value);

    /// <summary>
    /// Provides a list of XML Builders.
    /// </summary>
    [Obsolete("Use generic instead", true)]
    public class XmlBuilderDictionary : IReadOnlyDictionary<XmlBuilderIndex, XmlBuilder>
    {
        // TODO: Need to return a list including children so a tree structure can be built.
        // TODO: Need a way to load and save to the database. Rebuild into SchemaNode?
        // TODO: This seems to work but it could use some refinement.

        //public required TryGetProperty GetProperty { get; init; } 
        //public required TryGetDefinition GetDefinition { get; init; }

        Dictionary<XmlBuilderIndex, XmlBuilder> data = new Dictionary<XmlBuilderIndex, XmlBuilder>();

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
            XmlBuilderIndex rootKey = new XmlBuilderIndex(scope);
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
                            XmlBuilderIndex childKey = new XmlBuilderIndex(child.scope);

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
                                    XmlBuilderIndex childKey = new XmlBuilderIndex(child.scope);
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
                        foreach (var child in valType.Children.Values)
                        { data.Add(child.BuilderPath, child); }
                    }
                }
            }
        }

        #region IReadOnlyDictionary
        /// <inheritdoc/>
        public XmlBuilder this[XmlBuilderIndex key] => ((IReadOnlyDictionary<XmlBuilderIndex, XmlBuilder>)data)[key];

        /// <inheritdoc/>
        public IEnumerable<XmlBuilderIndex> Keys => ((IReadOnlyDictionary<XmlBuilderIndex, XmlBuilder>)data).Keys;

        /// <inheritdoc/>
        public IEnumerable<XmlBuilder> Values => ((IReadOnlyDictionary<XmlBuilderIndex, XmlBuilder>)data).Values;

        /// <inheritdoc/>
        public Int32 Count => ((IReadOnlyCollection<KeyValuePair<XmlBuilderIndex, XmlBuilder>>)data).Count;

        /// <inheritdoc/>
        public Boolean ContainsKey(XmlBuilderIndex key)
        { return ((IReadOnlyDictionary<XmlBuilderIndex, XmlBuilder>)data).ContainsKey(key); }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<XmlBuilderIndex, XmlBuilder>> GetEnumerator()
        { return ((IEnumerable<KeyValuePair<XmlBuilderIndex, XmlBuilder>>)data).GetEnumerator(); }

        /// <inheritdoc/>
        public Boolean TryGetValue(XmlBuilderIndex key, [MaybeNullWhen(false)] out XmlBuilder value)
        { return ((IReadOnlyDictionary<XmlBuilderIndex, XmlBuilder>)data).TryGetValue(key, out value); }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        { return ((IEnumerable)data).GetEnumerator(); }
        #endregion
    }


}

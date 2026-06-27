using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{

    // TODO: Need to return a list including children so a tree structure can be built.
    // TODO: Need a way to load and save to the database. Rebuild into SchemaNode?

    public class XmlBuilderDictionary : Dictionary<PathIndex, XmlBuilder>
    {
        //public required TryGetProperty GetProperty { get; init; } 

        //public required TryGetDefinition GetDefinition { get; init; }



        public XmlBuilderDictionary(IEnumerable<XmlBuilder> builders) : base()
        {
            foreach (XmlBuilder item in builders)
            { Add(item.ObjectPath, item); }
        }

        // TODO: This seems to work but it could use some refinement.


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




        public XElement Build(ScopeType scope, IEnumerable<IScopeType> values)
        {
            PathIndex key = new PathIndex(scope);

            if (TryGetValue(key, out XmlBuilder? builder))
            {
                var valueCount = values.Count();

                if (valueCount == 0)
                {
                    return new XElement(builder.NodeName);
                }
                else if (valueCount == 1)
                {
                    XObject? first = builder.Build(values.First());

                    if (first is XElement element)
                    { return element; }
                    else
                    {
                        XElement root = new XElement(builder.NodeName);
                        root.Add(first);
                        return root;
                    }
                }
                else
                {
                    XElement root = new XElement(builder.NodeName);
                    foreach (var item in values.Where(w => w.Scope == scope))
                    { root.Add(builder.Build(item)); }

                    return root;
                }
            }
            else { return new XElement(new XmlBuilder(scope).NodeName); }

        }

    }
}

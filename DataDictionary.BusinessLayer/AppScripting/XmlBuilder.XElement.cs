using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Builder Extension class containing factory method for executing the XML Builders to produce XElement.
    /// </summary>
    /// <remarks>
    /// These could me moved into several different classes.
    /// </remarks>
    public static class XmlBuilderXElement
    {   // TODO: Probably move to the specific data classes


        /// <summary>
        /// Used to find the correct XElement Build method.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="targetObject"></param>
        /// <param name="build"></param>
        /// <returns></returns>
        public static Boolean TryGetBuilder(this IModel model, ITemplateObjectNameIndex targetObject, [NotNullWhen(true)] out Func<IEnumerable<XmlBuilder>, XElement>? build)
        {   // Think this is the factory pattern.

            build = null;
            PathIndex key = new PathIndex(PathIndex.Parse(targetObject.ObjectPath));

            // Search the Attributes and get the Paths associated with them
            var attributes = model.Attribute.Attributes.
                    Join(model.Attribute.SubjectArea,
                        attribute => new AttributeIndex(attribute),
                        subject => new AttributeIndex(subject),
                        (Attribute, Subject) => new { Attribute, Subject }).
                    Join(model.SubjectAreas,
                        subjectKey => new SubjectAreaIndex(subjectKey.Subject),
                        subject => new SubjectAreaIndex(subject),
                        (attributeSubject, subject) => new { Path = new PathIndex(subject.SubjectAreaPath, attributeSubject.Attribute.AttributePath), attributeSubject.Attribute }).
                    Union(model.Attribute.Attributes.
                        Select(s => new { Path = s.AttributePath, Attribute = s })).
                    Where(w => key.Equals(w.Path) && w.Attribute.Scope == targetObject.ObjectScope).
                    ToList();

            // Search the Entities and get the Paths associated with them
            var entities = model.Entity.Entities.
                    Join(model.Entity.SubjectArea,
                        entity => new EntityIndex(entity),
                        subject => new EntityIndex(subject),
                        (Entity, Subject) => new { Entity, Subject }).
                    Join(model.SubjectAreas,
                        subjectKey => new SubjectAreaIndex(subjectKey.Subject),
                        subject => new SubjectAreaIndex(subject),
                        (entitySubject, subject) => new { Path = new PathIndex(subject.SubjectAreaPath, entitySubject.Entity.EntityPath), entitySubject.Entity }).
                    Union(model.Entity.Entities.
                        Select(s => new { Path = s.EntityPath, Entity = s })).
                    Where(w => key.Equals(w.Path) && w.Entity.Scope == targetObject.ObjectScope).
                    ToList();

            // Search the Processes and get the Paths associated with them
            var processes = model.Process.Processes.
                    Join(model.Process.SubjectArea,
                        process => new ProcessIndex(process),
                        subject => new ProcessIndex(subject),
                        (Process, Subject) => new { Process, Subject }).
                    Join(model.SubjectAreas,
                        subjectKey => new SubjectAreaIndex(subjectKey.Subject),
                        subject => new SubjectAreaIndex(subject),
                        (processSubject, subject) => new { Path = new PathIndex(subject.SubjectAreaPath, processSubject.Process.ProcessPath), processSubject.Process }).
                    Union(model.Process.Processes.
                        Select(s => new { Path = s.ProcessPath, Process = s })).
                    Where(w => key.Equals(w.Path) && w.Process.Scope == targetObject.ObjectScope).
                    ToList();

            // Wow, this Linq logic actually worked. Not certain how stable or east to debug it is. Consider breaking it up?

            if (attributes.Count > 0)
            {
                // TODO: Does not give results as expected when the function is actually called.
                build = (builders) => Build(builders, attributes.Select(s => s.Attribute), model.Attribute.Properties);
                return true;
            }
            // TODO: Add results for Entity and Process
            else { return false; }
        }

        [Obsolete("POC, Does not work", true)]
        public static XElement? Build<TItem>(
                this IEnumerable<XmlBuilder> builders,
                IEnumerable<TItem> items,
                params IEnumerable<Func<TItem, XElement?>> forEachItem)
        where TItem : class, IScopeType
        {
            // TODO: Working on revised logic of Build. Not dependent on Scope of the root.
            // Pass to the child node function the parent?

            XElement? root = null;
            List<TItem> itemList = items.ToList(); // To Protect against add/delete in the middle of the process.

            foreach (TItem item in items)
            {
                XmlBuilderIndex key = new XmlBuilderIndex(item.Scope);
                if (builders.TryGetXmlBuilder(key, out XmlBuilder? builder))
                {
                    XObject? node = builder.Build(item);

                    if (root is null)
                    {
                        root = new XElement(item.Scope.GetName());
                        root.Add(node);
                    }

                    foreach (XmlBuilder child in builders.
                        Where(w => key.ObjectScope.Equals(w.ObjectScope) && !String.IsNullOrWhiteSpace(w.ObjectProperty)))
                    {
                        if (node is XElement parentNode)
                        { parentNode.Add(child.Build(item)); }
                    }

                    foreach (var childNode in forEachItem)
                    {
                        XElement? childValue = childNode(item);
                        if (childValue is not null && node is XElement parentNode)
                        { parentNode.Add(childValue); }
                    }
                }
            }

            return root;
        }


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
        [Obsolete("The structure of the builder list has changed. Does not work.",true)]
        public static XElement? Build<TRoot, TChild>(this IEnumerable<XmlBuilder> builders, ScopeType scope, IEnumerable<TRoot> roots,
                params IEnumerable<(ScopeType scope, IEnumerable<TChild> values, Func<TRoot, TChild, Boolean> filter)> children)
                where TRoot : class, IScopeType
                where TChild : class
        {
            XmlBuilderIndex rootKey = new XmlBuilderIndex(scope);
            Int32 rootCount = roots.Count(w => w.Scope == scope);
            XElement root = new XElement(scope.GetName());

            // TODO: it calls the root but none of the child builders. Need to switch to path based?

            if (builders.TryGetXmlBuilder(rootKey, out XmlBuilder? builder))
            {
                if (rootCount == 0)
                { root = new XElement(builder.NodeName); }
                else
                {
                    if (rootCount == 1)
                    {
                        TRoot rootItem = roots.First();
                        XObject? node = builder.Build(rootItem);

                        if (node is XElement rootNode)
                        {
                            foreach (var child in builders.Where(w => w.ObjectScope.Equals(scope) && !rootKey.Equals(w)))
                            { rootNode.Add(child.Build(rootItem)); }

                            root = rootNode;
                        }
                        else
                        {
                            root = new XElement(builder.NodeName);
                            root.Add(node);
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

        static XElement Build<TRoot, TChild>(
                IEnumerable<XmlBuilder> builders,
                IEnumerable<TRoot> rootMembers,
                params IEnumerable<(IEnumerable<TChild> values, Func<TRoot, TChild, Boolean> filter)> childMembers)
                where TRoot : class, IScopeType
                where TChild : class, IScopeType
        {
            XElement? rootNode = null;

            var elementKeys = rootMembers.
                Select(s => new XmlBuilderIndex(s.Scope)).
                Distinct().
                OrderBy(o => o).
                ToList();

            foreach (var elementKey in elementKeys)
            {
                var rootItems = rootMembers.
                    Where(w => elementKey.Equals(new XmlBuilderIndex(w.Scope))).
                    ToList();

                foreach (var rootItem in rootItems)
                {
                    XElement? elementRoot = null;

                    var elementBuilders = builders.
                        Where(w => rootItem.Scope == w.ObjectScope).
                        OrderBy(o => o.BuilderPath.ParentPath).
                        ThenBy(o => o.RenderOrder).
                        ThenBy(o => o.NodeName).
                        ToList();

                    foreach (var builder in elementBuilders)
                    {
                        XObject? elementNode = builder.Build(rootItem);

                        if (elementRoot is null && elementNode is XElement rootElement)
                        { elementRoot = new XElement(rootElement); }
                        else if (elementRoot is null)
                        {
                            elementRoot = new XElement(builder.BuilderPath.Member);
                            elementRoot.Add(elementNode);
                        }
                        else
                        { elementRoot.Add(elementNode); }
                    }

                    foreach (var childMember in childMembers)
                    {
                        XElement? elementChild = null;

                        var childValues = childMember.values.
                            Where(w => childMember.filter(rootItem, w)).
                            ToList();

                        var childKeys = childValues.
                            Select(s => new XmlBuilderIndex(s.Scope)).
                            Distinct().
                            OrderBy(o => o).
                            ToList();

                        foreach (var childKey in childKeys)
                        {
                            var childItems = childValues.Where(w => childKey.Equals(new XmlBuilderIndex(w.Scope)));

                            foreach (var childItem in childItems)
                            {
                                var childBuilders = builders.
                                     Where(w => childItem.Scope == w.ObjectScope).
                                     OrderBy(o => o.BuilderPath.ParentPath).
                                     ThenBy(o => o.RenderOrder).
                                     ThenBy(o => o.NodeName).
                                     ToList();

                                foreach (var childBuilder in childBuilders)
                                {
                                    XObject? childNode = childBuilder.Build(childItem);

                                    if (elementChild is null && childNode is XElement rootElement)
                                    { elementChild = new XElement(rootElement); }
                                    else if (elementChild is null && childNode is not null)
                                    {
                                        elementChild = new XElement(childBuilder.BuilderPath.Member);
                                        elementChild.Add(childNode);
                                    }
                                    else if (elementChild is not null)
                                    { elementChild.Add(childNode); }
                                }
                            }
                        }

                        if (elementRoot is not null)
                        { elementRoot.Add(elementChild); }
                        else { throw new InvalidOperationException(); } // Should never occur.
                    }

                    if (rootNode is null && rootItems.Count == 1)
                    { rootNode = elementRoot; }
                    else if (rootNode is null)
                    {
                        rootNode = new XElement("root");
                        rootNode.Add(elementRoot);
                    }

                }
            }

            if (rootNode is null)
            { return new XElement("root"); }
            else { return rootNode; }
        }

        /// <summary>
        /// XElement builder for AttributeValue
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="attributes"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static XElement Build(this IEnumerable<XmlBuilder> builders,
                IEnumerable<AttributeValue> attributes,
                IEnumerable<AttributePropertyValue> properties)
        {
            XElement result = Build<AttributeValue, AttributePropertyValue>(
                builders,
                attributes,
                (properties, (r, c) => new AttributeIndex(r).Equals(new AttributeIndex(c))
                ));

            return result;
        }
    }
}
